using AutoMapper;
using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Property;
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
        private readonly IAgentService _agentService;
        private readonly ICompoundService _compoundService;
        private readonly ICacheService _cacheService;
        private readonly ILikePropertyRepo _likePropertyRepo;

        public CommercialPropertyService(IMapper mapper,
                                        ICommercialPropertyRepo repo, 
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

        public async Task<ResponseDto<PagedResultDto<CommercialPropertyReadDto>>> GetAllPropertiesAsync(Guid UserId, CommercialPropertyQueryDto queryDto)
        {
            var result = _cacheService.GetCached<List<CommercialProperty>>(CacheConstants.COMMERCIAL_PROPERTY_CACHE);
            if(result == null)
            {
                result = await _repo.GetAllProperties()
                                    .ToListAsync();
                _cacheService.CreateCached<List<CommercialProperty>>(CacheConstants.COMMERCIAL_PROPERTY_CACHE, result);
            }

            var filteredResult = result
                                    .IF(queryDto.BusinessType != null, a => a.BusinessType.Contains(queryDto.BusinessType ?? ""))
                                    .IF(queryDto.FloorNumber != null, a => a.FloorNumber == queryDto.FloorNumber)
                                    .IF(queryDto.HasStorage != null, a => a.HasStorage == queryDto.HasStorage)
                                    .IF(queryDto.UserId != null, a => a.Agent.UserId == queryDto.UserId)
                                    .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize });

            var mappedData = _mapper.Map<List<CommercialPropertyReadDto>>(result);


            //Add Islike , count for each comment
            // await AddIsLikedAndCountOfLikes(UserId, mappedData);

            var pagedResult = new PagedResultDto<CommercialPropertyReadDto>(mappedData, queryDto.PageNumber, mappedData.Count, queryDto.PageSize);

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
            await _repo.UpdateCommercialPropertyAsync(id, existing);

            _cacheService.InvalidateCache(CacheConstants.COMMERCIAL_PROPERTY_CACHE);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Commercial property updated successfully.",
                Data = true
            };
        }
        public async Task<ResponseDto<CommercialPropertyReadDto>> AddPropertyAsync(Guid UserId, CommercialPropertyAddDto propertyDto)
        {
            if(propertyDto.UserId != UserId)
            {
                throw new UnauthorizedAccessException($"Current user unauthorized to do such action, mismatch Ids: Current ID {UserId}, givenId {propertyDto.UserId}");
            }

            // check if compound exists in request body and database
            if(propertyDto.CompoundId.HasValue){
                await _compoundService.GetCompoundIfExistsAsync(propertyDto.CompoundId.Value);
            }

            var property = _mapper.Map<CommercialProperty>(propertyDto);
            
            // find if agent exist
            var agent = await _agentService.GetByIdAsync(UserId);
            property.AgentId = agent.Data!.Id;

            await _repo.AddCommercialPropertyAsync(property);

            var PropertyResponseDto = _mapper.Map<CommercialPropertyReadDto>(property);
            PropertyResponseDto.UserId = propertyDto.UserId;

            _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);

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

            _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Property deleted successfully.",
                Data = true
            };

        }

        private async Task AddIsLikedAndCountOfLikes(Guid UserId, List<CommercialPropertyReadDto> mappedData)
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
