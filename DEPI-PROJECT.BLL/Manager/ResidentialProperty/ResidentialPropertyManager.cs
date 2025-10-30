using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityResidentialProperty = DEPI_PROJECT.DAL.Models.ResidentialProperty;


namespace DEPI_PROJECT.BLL.Manager.ResidentialProperty
{
    public class ResidentialPropertyManager : IResidentialPropertyManager
    {
        private readonly IResidentialPropertyRepo _repo;
        private readonly IMapper _mapper;
        public ResidentialPropertyManager(IMapper mapper, IResidentialPropertyRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public ResponseDto<PagedResult<ResidentialPropertyReadDto>> GetAllResidentialProperty(int pageNumber, int pageSize)
        {
            var result = _repo.GetAllResidentialProperty(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<ResidentialPropertyReadDto>>(result.Data);

            var pagedResult = new PagedResult<ResidentialPropertyReadDto>
            {
                Data = mappedData,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };

            return new ResponseDto<PagedResult<ResidentialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Residential properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public ResponseDto<ResidentialPropertyReadDto> GetResidentialPropertyById(Guid id)
        {
            var property = _repo.GetResidentialPropertyById(id);
            if (property == null)
            {
                return new ResponseDto<ResidentialPropertyReadDto>
                {
                    IsSuccess = false,
                    Message = "Residential property not found."
                };
            }

            var mapped = _mapper.Map<ResidentialPropertyReadDto>(property);
            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property retrieved successfully.",
                Data = mapped
            };
        }

        public ResponseDto<ResidentialPropertyReadDto> AddResidentialProperty(ResidentialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<EntityResidentialProperty>(propertyDto);
            _repo.AddResidentialProperty(property);

            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                _repo.AddAmenity(amenity);
            }

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property added successfully.",
                Data = _mapper.Map<ResidentialPropertyReadDto>(property)
            };
        }

        public ResponseDto<bool> UpdateResidentialProperty(Guid id, ResidentialPropertyUpdateDto propertyDto)
        {
            var existing = _repo.GetResidentialPropertyById(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Residential property not found.",
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

            _repo.UpdateResidentialProperty(id, existing);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property updated successfully.",
                Data = true
            };
        }

        public ResponseDto<bool> DeleteResidentialProperty(Guid id)
        {
            var existing = _repo.GetResidentialPropertyById(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Residential property not found.",
                    Data = false
                };
            }

            _repo.DeleteResidentialProperty(id);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Residential property deleted successfully.",
                Data = true
            };
        }
    }

}
