using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
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

        public CommercialPropertyService(IMapper mapper,ICommercialPropertyRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ResponseDto<PagedResultDto<CommercialPropertyReadDto>>> GetAllPropertiesAsync(CommercialPropertyQueryDto queryDto)
        {
            var query = _repo.GetAllProperties();

            var result = await query.IF(queryDto.BusinessType != null, a => a.BusinessType.Contains(queryDto.BusinessType))
                                    .IF(queryDto.FloorNumber != null, a => a.FloorNumber == queryDto.FloorNumber)
                                    .IF(queryDto.HasStorage != null, a => a.HasStorage == queryDto.HasStorage)
                                    .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize })
                                        .ToListAsync();

            var mappedData = _mapper.Map<List<CommercialPropertyReadDto>>(result);
            var pagedResult = new PagedResultDto<CommercialPropertyReadDto>(mappedData, queryDto.PageNumber, query.Count(), queryDto.PageSize);

            return new ResponseDto<PagedResultDto<CommercialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<CommercialPropertyReadDto>> GetPropertyByIdAsync(Guid id)
        {
            var property = await _repo.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return new ResponseDto<CommercialPropertyReadDto>
                {
                    IsSuccess = false,
                    Message = "Commercial property not found."
                };
            }
            var mapped = _mapper.Map<CommercialPropertyReadDto>(property);
            return new ResponseDto<CommercialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Property retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<bool>> UpdateCommercialPropertyAsync(Guid id, CommercialPropertyUpdateDto propertyDto)
        {
            var existing = await _repo.GetPropertyByIdAsync(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Property not found.",
                    Data = false
                };
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
            await _repo.UpdateCommercialPropertyAsync(id, existing);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Commercial property updated successfully.",
                Data = true
            };
        }
        public async Task<ResponseDto<CommercialPropertyReadDto>> AddPropertyAsync(CommercialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<CommercialProperty>(propertyDto);
            await _repo.AddCommercialPropertyAsync(property);
            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                await _repo.AddAmenityAsync(amenity);
            }
            return new ResponseDto<CommercialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Commercial property added successfully.",
                Data = _mapper.Map<CommercialPropertyReadDto>(property)
            };
        }

        public async Task<ResponseDto<bool>> DeleteCommercialPropertyAsync(Guid id)
        {
            var existing = await _repo.GetPropertyByIdAsync(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Property not found.",
                    Data = false
                };
            }

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
