using AutoMapper;
using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
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

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class CommercialPropertyService : ICommercialPropertyService
    {
        private readonly IMapper _mapper;
        private readonly ICommercialPropertyRepo _repo;
        private readonly ILikePropertyRepo _likePropertyRepo;

        public CommercialPropertyService(IMapper mapper,ICommercialPropertyRepo repo , ILikePropertyRepo likePropertyRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _likePropertyRepo = likePropertyRepo;
        }

        public async Task<ResponseDto<PagedResultDto<CommercialPropertyReadDto>>> GetAllPropertiesAsync(Guid UserId, CommercialPropertyQueryDto queryDto)
        {
            IQueryable<CommercialProperty> query = _repo.GetAllProperties();
            
            var result = await query.IF(queryDto.BusinessType != null, a => a.BusinessType.Contains(queryDto.BusinessType))
                                    .IF(queryDto.FloorNumber != null, a => a.FloorNumber == queryDto.FloorNumber)
                                    .IF(queryDto.HasStorage != null, a => a.HasStorage == queryDto.HasStorage)
                                    .IF(queryDto.UserId != null, a => a.Agent.UserId == queryDto.UserId)
                                    .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize })
                                        .ToListAsync();

            var mappedData = _mapper.Map<List<CommercialPropertyReadDto>>(result);


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

            var pagedResult = new PagedResultDto<CommercialPropertyReadDto>(mappedData, queryDto.PageNumber, query.Count(), queryDto.PageSize);

            return new ResponseDto<PagedResultDto<CommercialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<CommercialPropertyReadDto>> GetPropertyByIdAsync(Guid UserId, Guid id)
        {
            var property = await _repo.GetPropertyByIdAsync(id);
            if (property == null)
            {
                throw new NotFoundException($"No property found with ID {id}");
            }
            var mapped = _mapper.Map<CommercialPropertyReadDto>(property);

            #region Add Islike , count for each comment
            //count likes --> call likeCommentRepo
            mapped.LikesCount = await _likePropertyRepo.CountLikesByPropertyId(mapped.PropertyId);
            //check is liked by Current user
            mapped.IsLiked = await _likePropertyRepo.GetLikePropertyByUserAndPropertyId(UserId, mapped.PropertyId) != null; 
            #endregion

            return new ResponseDto<CommercialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Property retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<bool>> UpdateCommercialPropertyAsync(Guid UserId, Guid id, CommercialPropertyUpdateDto propertyDto)
        {
            var existing = await _repo.GetPropertyByIdAsync(id);
            if (existing == null)
            {
                throw new NotFoundException($"No property found with ID {id} for UserId {UserId}");
            }

            CommonFunctions.EnsureAuthorized(existing.Agent.UserId);

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
            await _repo.UpdateCommercialPropertyAsync(id, existing);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Commercial property updated successfully.",
                Data = true
            };
        }
        public async Task<ResponseDto<CommercialPropertyReadDto>> AddPropertyAsync(Guid UserId, Guid AgentId, CommercialPropertyAddDto propertyDto)
        {
            if(propertyDto.UserId != UserId)
            {
                throw new UnauthorizedAccessException($"Current user unauthorized to do such action, mismatch Ids: Current ID {UserId}, givenId {propertyDto.UserId}");
            }
            var property = _mapper.Map<CommercialProperty>(propertyDto);
            property.AgentId = AgentId;

            await _repo.AddCommercialPropertyAsync(property);
            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                await _repo.AddAmenityAsync(amenity);
            }

            var PropertyResponseDto = _mapper.Map<CommercialPropertyReadDto>(property);
            PropertyResponseDto.UserId = propertyDto.UserId;

            return new ResponseDto<CommercialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Commercial property added successfully.",
                Data = PropertyResponseDto
            };
        }

        public async Task<ResponseDto<bool>> DeleteCommercialPropertyAsync(Guid UserId, Guid id)
        {
            var existing = await _repo.GetPropertyByIdAsync(id);
            if (existing == null)
            {
                throw new NotFoundException($"No property found with ID {id} for userId {UserId}");
            }

            CommonFunctions.EnsureAuthorized(existing.Agent.UserId);

            await _repo.DeleteCommercialPropertyAsync(id);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Property deleted successfully.",
                Data = true
            };

        }
    }
}
