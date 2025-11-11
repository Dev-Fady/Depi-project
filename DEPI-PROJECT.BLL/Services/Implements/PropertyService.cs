using AutoMapper;
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
        private readonly IMapper _mapper;

        public PropertyService(
                ICommercialPropertyRepo commercialPropertyRepo,
                IResidentialPropertyRepo residentialPropertyRepo,
                IMapper mapper
            )
        {
            _commercialPropertyRepo = commercialPropertyRepo;
            _residentialPropertyRepo = residentialPropertyRepo;
            _mapper = mapper;
        }
        public async Task<ResponseDto<PagedResultDto<AllPropertyReadDto>>> GetAll(PropertyQueryDto propertyQueryDto)
        {
            var query1 = _commercialPropertyRepo.GetAllProperties();
            var query2 = _residentialPropertyRepo.GetAllResidentialProperty();

            var commercials = await query1
                        .IF(propertyQueryDto.City != null, a => a.City == propertyQueryDto.City)
                        .IF(propertyQueryDto.PropertyType != null, a => a.PropertyType == propertyQueryDto.PropertyType)
                        .IF(propertyQueryDto.PropertyStatus != null, a => a.PropertyStatus == propertyQueryDto.PropertyStatus)
                        .IF(propertyQueryDto.PropertyPurpose != null, a => a.PropertyPurpose == propertyQueryDto.PropertyPurpose)
                        .IF(propertyQueryDto.Address != null, a => a.Address.Contains(propertyQueryDto.Address))
                        .IF(propertyQueryDto.Description != null, a => a.Description.Contains(propertyQueryDto.Description))
                        .IF(propertyQueryDto.UpToPrice != null, a => a.Price <= propertyQueryDto.UpToPrice)
                        .IF(propertyQueryDto.UpToSquare != null, a => a.Square <= propertyQueryDto.UpToSquare)
                        .Paginate(new PagedQueryDto { PageNumber = propertyQueryDto.PageNumber, PageSize = propertyQueryDto.PageSize })
                        .OrderByExtended(new List<Tuple<bool, Expression<Func<CommercialProperty, object>>>>
                                    {
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Price, a => a.Price),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Sqaure, a => a.Square),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.DateListed, a => a.DateListed),
                                    }
                                ,
                                propertyQueryDto.IsDesc
                        ).ToListAsync();

            var residentials = await query2
                    .IF(propertyQueryDto.City != null, a => a.City == propertyQueryDto.City)
                    .IF(propertyQueryDto.PropertyType != null, a => a.PropertyType == propertyQueryDto.PropertyType)
                    .IF(propertyQueryDto.PropertyStatus != null, a => a.PropertyStatus == propertyQueryDto.PropertyStatus)
                    .IF(propertyQueryDto.PropertyPurpose != null, a => a.PropertyPurpose == propertyQueryDto.PropertyPurpose)
                    .IF(propertyQueryDto.Address != null, a => a.Address.Contains(propertyQueryDto.Address))
                    .IF(propertyQueryDto.Description != null, a => a.Description.Contains(propertyQueryDto.Description))
                    .IF(propertyQueryDto.UpToPrice != null, a => a.Price <= propertyQueryDto.UpToPrice)
                    .IF(propertyQueryDto.UpToSquare != null, a => a.Square <= propertyQueryDto.UpToSquare)
                    .Paginate(new PagedQueryDto { PageNumber = propertyQueryDto.PageNumber, PageSize = propertyQueryDto.PageSize })
                    .OrderByExtended(new List<Tuple<bool, Expression<Func<ResidentialProperty, object>>>>
                                {
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Price, a => a.Price),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.Sqaure, a => a.Square),
                                        new (propertyQueryDto.OrderBy == OrderByOptions.DateListed, a => a.DateListed),
                                }
                            ,
                            propertyQueryDto.IsDesc
                    ).ToListAsync();

            List<AllPropertyReadDto> propertyReadDto = new List<AllPropertyReadDto>
            {
                new AllPropertyReadDto{
                    CommercialProperties = _mapper.Map<IEnumerable<CommercialPropertyReadDto>>(commercials),
                    ResidentialProperties = _mapper.Map<IEnumerable<ResidentialPropertyReadDto>>(residentials),
                }
            };

            int totalCount = query1.Count() + query2.Count();
            var pagedResult = new PagedResultDto<AllPropertyReadDto>(propertyReadDto, propertyQueryDto.PageNumber, totalCount, propertyQueryDto.PageSize);

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
    }

}