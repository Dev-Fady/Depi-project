using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Implements;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityResidentialProperty = DEPI_PROJECT.DAL.Models.ResidentialProperty;


namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class ResidentialPropertyService : IResidentialPropertyService
    {
        private readonly IResidentialPropertyRepo _repo;
        private readonly IMapper _mapper;
        private readonly ILikePropertyRepo _likePropertyRepo;
        public ResidentialPropertyService(IMapper mapper, IResidentialPropertyRepo repo , ILikePropertyRepo likePropertyRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _likePropertyRepo = likePropertyRepo;
        }
        public async Task<ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>> GetAllResidentialPropertyAsync(Guid UserId, ResidentialPropertyQueryDto queryDto)
        {
            var query = _repo.GetAllResidentialProperty();

            var result = await query.IF(queryDto.Bedrooms != null, a => a.Bedrooms == queryDto.Bedrooms)
                                    .IF(queryDto.Bathrooms != null, a => a.Bathrooms == queryDto.Bathrooms)
                                    .IF(queryDto.Floors != null, a => a.Floors == queryDto.Floors)
                                    .IF(queryDto.UserId != null, a => a.Agent.UserId == queryDto.UserId)
                                    .IF(queryDto.KitchenType != null, a => a.KitchenType == queryDto.KitchenType)
                                    .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize })
                                    .ToListAsync();
            
            var mappedData = _mapper.Map<List<ResidentialPropertyReadDto>>(result);

            #region Add Islike , count for each comment

            //Add Islike , count for each comment
            var PropertiesIds = mappedData.Select(p => p.PropertyId).ToList();
            var CountPropertyDic = await _likePropertyRepo.GetAllLikesByPropertyIds(PropertiesIds)
                                    .GroupBy(lc => lc.PropertyId)
                                    .Select(n => new
                                    {
                                        PropertyId = n.Key,
                                        Count = n.Count()
                                    })
                                    .ToDictionaryAsync(n => n.PropertyId, n => n.Count);

            var IsLikedHash = await _likePropertyRepo.GetAllLikesByPropertyIds(PropertiesIds)
                                    .Where(lc => lc.UserID == UserId)
                                    .Select(n => n.PropertyId)
                                    .ToHashSetAsync();

            foreach (var property in mappedData)
            {
                if (CountPropertyDic.TryGetValue(property.PropertyId, out var count))
                {
                    property.LikesCount = count;
                }
                else
                {
                    property.LikesCount = 0;
                }
                if (IsLikedHash.Contains(property.PropertyId))
                {
                    property.IsLiked = true;
                }
                else
                {
                    property.IsLiked = false;
                }
            } 
            #endregion

            var pagedResult = new PagedResultDto<ResidentialPropertyReadDto>(mappedData, queryDto.PageNumber, query.Count(), queryDto.PageSize);
            

            return new ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Residential properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<ResidentialPropertyReadDto>> GetResidentialPropertyByIdAsync(Guid UserId, Guid id)
        {
            var property = await _repo.GetResidentialPropertyByIdAsync(id);
            if (property == null)
            {
                throw new NotFoundException($"No property found with ID {id}");
            }

            var mapped = _mapper.Map<ResidentialPropertyReadDto>(property);

            #region Add Islike , count for each comment
            //count likes --> call likeCommentRepo
            mapped.LikesCount = await _likePropertyRepo.CountLikesByPropertyId(mapped.PropertyId);
            //check is liked by Current user
            mapped.IsLiked = await _likePropertyRepo.GetLikePropertyByUserAndPropertyId(UserId, mapped.PropertyId) != null; 
            #endregion

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<ResidentialPropertyReadDto>> AddResidentialPropertyAsync(Guid UserId, Guid AgentId, ResidentialPropertyAddDto propertyDto)
        {
            if(propertyDto.UserId != UserId)
            {
                throw new UnauthorizedAccessException($"Current user unauthorized to do such action, mismatch Ids: Current ID {UserId}, givenId {propertyDto.UserId}");
            }
            var property = _mapper.Map<EntityResidentialProperty>(propertyDto);
            property.AgentId = AgentId;
            await _repo.AddResidentialPropertyAsync(property);

            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                await _repo.AddAmenityAsync(amenity);
            }

            var PropertyResponseDto = _mapper.Map<ResidentialPropertyReadDto>(property);

            PropertyResponseDto.UserId = propertyDto.UserId;

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property added successfully.",
                Data = PropertyResponseDto
            };
        }

        public async Task<ResponseDto<bool>> UpdateResidentialPropertyAsync(Guid UserId, Guid id, ResidentialPropertyUpdateDto propertyDto)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(id);
            if (existing == null || existing.Agent.UserId != UserId)
            {
                throw new NotFoundException($"No property found with ID {id} for UserId {UserId}");
            }

            _mapper.Map(propertyDto, existing);
            if (propertyDto.Amenity != null)
            {
                if (existing.Amenity == null)
                {
                    existing.Amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                    existing.Amenity.PropertyId = existing.PropertyId;
                    await _repo.AddAmenityAsync(existing.Amenity);
                }
                else
                {
                    _mapper.Map(propertyDto.Amenity, existing.Amenity);
                    await _repo.UpdateAmenityAsync(existing.Amenity);
                }
            }

            await _repo.UpdateResidentialPropertyAsync(id, existing);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property updated successfully.",
                Data = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteResidentialPropertyAsync(Guid UserId, Guid id)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(id);
            if (existing == null)
            {
                throw new NotFoundException($"No property found with ID {id}");
            }

            if(existing.Agent.UserId != UserId){
                throw new UnauthorizedAccessException($"Mismatch user Ids: Current {UserId}, given {existing.Agent.UserId}");
            }

            await _repo.DeleteResidentialPropertyAsync(id);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property deleted successfully.",
                Data = true
            };
        }
    }

}
