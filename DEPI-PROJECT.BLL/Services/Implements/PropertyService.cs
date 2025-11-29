using AutoMapper;
using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class PropertyService : IPropertyService
    {
        private readonly ICommercialPropertyRepo _commercialPropertyRepo;
        private readonly IResidentialPropertyRepo _residentialPropertyRepo;
        private readonly ICacheService _cacheService;
        
        private readonly IMapper _mapper;

        public PropertyService( ICommercialPropertyRepo commercialPropertyRepo,
                                IResidentialPropertyRepo residentialPropertyRepo,
                                IMapper mapper,
                                ICacheService cacheService)
        {
            _commercialPropertyRepo = commercialPropertyRepo;
            _residentialPropertyRepo = residentialPropertyRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<ResponseDto<PagedResultDto<AllPropertyReadDto>>> GetAll(PropertyQueryDto propertyQueryDto)
        {
            var result = _cacheService.GetCached<AllPropertyReadDto>(CacheConstants.PROPERTY_CACHE);
            if (result == null)
            {
                var query1 = await _commercialPropertyRepo.GetAllProperties().ToListAsync();
                var query2 = await _residentialPropertyRepo.GetAllResidentialProperty().ToListAsync();
                result = new AllPropertyReadDto
                {
                    CommercialProperties = _mapper.Map<List<CommercialPropertyReadDto>>(query1),
                    ResidentialProperties = _mapper.Map<List<ResidentialPropertyReadDto>>(query2)
                };

                _cacheService.CreateCached(CacheConstants.PROPERTY_CACHE, result);
            }

            var filteredCommercialProperties = result.CommercialProperties 
                    .IF(propertyQueryDto.City != null, a => a.City == propertyQueryDto.City)
                    .IF(propertyQueryDto.PropertyType != null, a => a.PropertyType == propertyQueryDto.PropertyType)
                    .IF(propertyQueryDto.PropertyStatus != null, a => a.PropertyStatus == propertyQueryDto.PropertyStatus)
                    .IF(propertyQueryDto.PropertyPurpose != null, a => a.PropertyPurpose == propertyQueryDto.PropertyPurpose)
                    .IF(propertyQueryDto.Address != null, a => a.Address.Contains(propertyQueryDto.Address ?? ""))
                    .IF(propertyQueryDto.Title != null, a => a.Title.Contains(propertyQueryDto.Title ?? ""))
                    .IF(propertyQueryDto.Description != null, a => a.Description.Contains(propertyQueryDto.Description ?? ""))
                    .IF(propertyQueryDto.UpToPrice != null, a => a.Price <= propertyQueryDto.UpToPrice)
                    .IF(propertyQueryDto.UpToSquare != null, a => a.Square <= propertyQueryDto.UpToSquare)
                    .Paginate(new PagedQueryDto { PageNumber = propertyQueryDto.PageNumber, PageSize = propertyQueryDto.PageSize })
                    .OrderByExtended(
                                [
                                    new (propertyQueryDto.OrderBy == OrderByOptions.Price, a => a.Price),
                                    new (propertyQueryDto.OrderBy == OrderByOptions.Sqaure, a => a.Square),
                                    new (propertyQueryDto.OrderBy == OrderByOptions.DateListed, a => a.DateListed),
                                ]
                            ,
                            propertyQueryDto.IsDesc
                    );  

            var filteredResidentialProperties = result.ResidentialProperties
                    .IF(propertyQueryDto.City != null, a => a.City == propertyQueryDto.City)
                    .IF(propertyQueryDto.PropertyType != null, a => a.PropertyType == propertyQueryDto.PropertyType)
                    .IF(propertyQueryDto.PropertyStatus != null, a => a.PropertyStatus == propertyQueryDto.PropertyStatus)
                    .IF(propertyQueryDto.PropertyPurpose != null, a => a.PropertyPurpose == propertyQueryDto.PropertyPurpose)
                    .IF(propertyQueryDto.Address != null, a => a.Address.Contains(propertyQueryDto.Address ?? ""))
                    .IF(propertyQueryDto.Description != null, a => a.Description.Contains(propertyQueryDto.Description ?? ""))
                    .IF(propertyQueryDto.UpToPrice != null, a => a.Price <= propertyQueryDto.UpToPrice)
                    .IF(propertyQueryDto.UpToSquare != null, a => a.Square <= propertyQueryDto.UpToSquare)
                    .Paginate(new PagedQueryDto { PageNumber = propertyQueryDto.PageNumber, PageSize = propertyQueryDto.PageSize })
                    .OrderByExtended(
                                [
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Price, a => a.Price),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Sqaure, a => a.Square),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.DateListed, a => a.DateListed),
                                ]
                            ,
                            propertyQueryDto.IsDesc
                    );


            int totalCount = filteredCommercialProperties.Count() + filteredCommercialProperties.Count();
            
            var resultAsList = new List<AllPropertyReadDto>{new() {
                    ResidentialProperties = filteredResidentialProperties,
                    CommercialProperties = filteredCommercialProperties
                }
            };
            
            var pagedResult = new PagedResultDto<AllPropertyReadDto>(resultAsList, propertyQueryDto.PageNumber, totalCount, propertyQueryDto.PageSize);

            return new ResponseDto<PagedResultDto<AllPropertyReadDto>>
            {
                Data = pagedResult,
                Message = "Properties returned successfully",
                IsSuccess = true
            };
        }

        public async Task<bool> CheckPropertyExist(Guid PropertyId)
        {
            var ResidentialProperty = await _residentialPropertyRepo.GetResidentialPropertyByIdAsync(PropertyId);
            var commercialProperty = await _commercialPropertyRepo.GetPropertyByIdAsync(PropertyId);
            if (ResidentialProperty == null && commercialProperty == null)
            {
                return false;
            }
            return true;
        }
        public async Task<PropertyResponseDto?> GetPropertyById(Guid PropertyId)
        {
            var ResidentialProperty = await _residentialPropertyRepo.GetResidentialPropertyByIdAsync(PropertyId);
            if (ResidentialProperty != null)
            {
                return _mapper.Map<PropertyResponseDto>(ResidentialProperty);
            }
            var CommercialProperty = await _commercialPropertyRepo.GetPropertyByIdAsync(PropertyId);
            if (CommercialProperty != null)
            {
                return _mapper.Map<PropertyResponseDto>(CommercialProperty);
            }
            return null;
        }
    }

}