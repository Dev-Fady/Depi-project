using AutoMapper;
using DEPI_PROJECT.BLL.Common;
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
        private readonly IAgentService _agentService;
        private readonly ICompoundService _compoundService;
        private readonly ICacheService _cacheService;

        public ResidentialPropertyService(IMapper mapper, 
                                          IResidentialPropertyRepo repo, 
                                          ILikePropertyRepo likePropertyRepo,
                                          IAgentService agentService,
                                          ICompoundService compoundService,
                                          ICacheService cacheService)
        {
            _mapper = mapper;
            _repo = repo;
            _likePropertyRepo = likePropertyRepo;
            _agentService = agentService;
            _compoundService = compoundService;
            _cacheService = cacheService;
        }
        public async Task<ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>> GetAllResidentialPropertyAsync(Guid UserId, ResidentialPropertyQueryDto queryDto)
        {
            var result = _cacheService.GetCached<List<ResidentialProperty>>(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);
            if(result == null)
            {
                result = await _repo.GetAllResidentialProperty(UserId)
                                    .ToListAsync();
                _cacheService.CreateCached<List<ResidentialProperty>>(CacheConstants.RESIDENTIAL_PROPERTY_CACHE, result);
            }

            var filterResult = result
                                .IF(queryDto.Bedrooms != null, a => a.Bedrooms == queryDto.Bedrooms)
                                .IF(queryDto.Bathrooms != null, a => a.Bathrooms == queryDto.Bathrooms)
                                .IF(queryDto.Floors != null, a => a.Floors == queryDto.Floors)
                                .IF(queryDto.UserId != null, a => a.Agent.UserId == queryDto.UserId)
                                .IF(queryDto.KitchenType != null, a => a.KitchenType == queryDto.KitchenType)
                                .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize });
                                
            var mappedData = _mapper.Map<List<ResidentialPropertyReadDto>>(filterResult);

            // await AddIsLikeAndCountOfLikes(UserId, mappedData);


            var pagedResult = new PagedResultDto<ResidentialPropertyReadDto>(mappedData, queryDto.PageNumber, mappedData.Count, queryDto.PageSize);


            return new ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Residential properties retrieved successfully.",
                Data = pagedResult
            };
        }


        public async Task<ResponseDto<ResidentialPropertyReadDto>> GetResidentialPropertyByIdAsync(Guid UserId, Guid id)
        {
            var property = await _repo.GetResidentialPropertyByIdAsync(UserId , id);
            if (property == null)
            {
                throw new NotFoundException($"No property found with ID {id}");
            }

            var mapped = _mapper.Map<ResidentialPropertyReadDto>(property);

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<ResidentialPropertyReadDto>> AddResidentialPropertyAsync(Guid UserId, ResidentialPropertyAddDto propertyDto)
        {
            if(propertyDto.UserId != UserId)
            {
                throw new UnauthorizedAccessException($"Current user unauthorized to do such action, mismatch Ids: Current ID {UserId}, givenId {propertyDto.UserId}");
            }

            // check if compound exists in request body and database
            if(propertyDto.CompoundId.HasValue){
                await _compoundService.GetCompoundIfExistsAsync(propertyDto.CompoundId.Value);
            }

            var property = _mapper.Map<ResidentialProperty>(propertyDto);
            
            // find if agent exist
            var agent = await _agentService.GetByIdAsync(UserId);
            property.AgentId = agent.Data!.Id;
            await _repo.AddResidentialPropertyAsync(property);


            var PropertyResponseDto = _mapper.Map<ResidentialPropertyReadDto>(property);

            PropertyResponseDto.UserId = propertyDto.UserId;
            
            _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property added successfully.",
                Data = PropertyResponseDto
            };
        }

        public async Task<ResponseDto<bool>> UpdateResidentialPropertyAsync(Guid UserId, Guid id, ResidentialPropertyUpdateDto propertyDto)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(UserId, id);
            if (existing == null)
            {
                throw new NotFoundException($"No property found with ID {id} for UserId {UserId}");
            }

            CommonFunctions.EnsureAuthorized(existing.Agent.UserId);

            _mapper.Map(propertyDto, existing);

            await _repo.UpdateResidentialPropertyAsync(id, existing);

            _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property updated successfully.",
                Data = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteResidentialPropertyAsync(Guid UserId, Guid id)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(UserId, id);
            if (existing == null)
            {
                throw new NotFoundException($"No property found with ID {id}");
            }

            CommonFunctions.EnsureAuthorized(existing.Agent.UserId);

            await _repo.DeleteResidentialPropertyAsync(id);

            _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property deleted successfully.",
                Data = true
            };
        }

        private async Task AddIsLikeAndCountOfLikes(Guid UserId, List<ResidentialPropertyReadDto> mappedData)
        {
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
        }

    }

}
