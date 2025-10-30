using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
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

        public ResponseDto<PagedResult<CommercialPropertyReadDto>> GetAllProperties(int pageNumber, int pageSize)
        {
            var result = _repo.GetAllProperties(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<CommercialPropertyReadDto>>(result.Data);
            var pagedResult = new PagedResult<CommercialPropertyReadDto>
            {
                Data = mappedData,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };

            return new ResponseDto<PagedResult<CommercialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public ResponseDto<CommercialPropertyReadDto> GetPropertyById(Guid id)
        {
            var property = _repo.GetPropertyById(id);
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

        public ResponseDto<bool> UpdateCommercialProperty(Guid id, CommercialPropertyUpdateDto propertyDto)
        {
            var existing = _repo.GetPropertyById(id);
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
                    _repo.AddAmenity(existing.Amenity);
                }
                else
                {
                    _mapper.Map(propertyDto.Amenity, existing.Amenity);
                    _repo.UpdateAmenity(existing.Amenity);
                }
            }
            _repo.UpdateCommercialProperty(id, existing);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Commercial property updated successfully.",
                Data = true
            };
        }
        public ResponseDto<CommercialPropertyReadDto> AddProperty(CommercialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<CommercialProperty>(propertyDto);
            _repo.AddCommercialProperty(property);
            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                _repo.AddAmenity(amenity);
            }
            return new ResponseDto<CommercialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Commercial property added successfully.",
                Data = _mapper.Map<CommercialPropertyReadDto>(property)
            };
        }

        public ResponseDto<bool> DeleteCommercialProperty(Guid id)
        {
            var existing = _repo.GetPropertyById(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Property not found.",
                    Data = false
                };
            }

            _repo.DeleteCommercialProperty(id);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Property deleted successfully.",
                Data = true
            };

        }
    }
}
