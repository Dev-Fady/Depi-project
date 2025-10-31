using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Pagination;
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
using System.Text;
using System.Threading.Tasks;
using EntityResidentialProperty = DEPI_PROJECT.DAL.Models.ResidentialProperty;


namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class ResidentialPropertyService : IResidentialPropertyService
    {
        private readonly IResidentialPropertyRepo _repo;
        private readonly IMapper _mapper;
        public ResidentialPropertyService(IMapper mapper, IResidentialPropertyRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>> GetAllResidentialPropertyAsync(ResidentialPropertyQueryDto queryDto)
        {
            var query = _repo.GetAllResidentialProperty();

            var result = await query.IF(queryDto.Bedrooms != null, a => a.Bedrooms == queryDto.Bedrooms)
                                    .IF(queryDto.Bathrooms != null, a => a.Bathrooms == queryDto.Bathrooms)
                                    .IF(queryDto.Floors != null, a => a.Floors == queryDto.Floors)
                                    .IF(queryDto.KitchenType != null, a => a.KitchenType == queryDto.KitchenType)
                                    .Paginate(new PagedQueryDto { PageNumber = queryDto.PageNumber, PageSize = queryDto.PageSize })
                                    .ToListAsync();
            
            var mappedData = _mapper.Map<List<ResidentialPropertyReadDto>>(result);

            var pagedResult = new PagedResultDto<ResidentialPropertyReadDto>(mappedData, queryDto.PageNumber, query.Count(), queryDto.PageSize);
            

            return new ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>
            {
                IsSuccess = true,
                Message = "Residential properties retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<ResidentialPropertyReadDto>> GetResidentialPropertyByIdAsync(Guid id)
        {
            var property = await _repo.GetResidentialPropertyByIdAsync(id);
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

        public async Task<ResponseDto<ResidentialPropertyReadDto>> AddResidentialPropertyAsync(ResidentialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<EntityResidentialProperty>(propertyDto);
            await _repo.AddResidentialPropertyAsync(property);

            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                await _repo.AddAmenityAsync(amenity);
            }

            return new ResponseDto<ResidentialPropertyReadDto>
            {
                IsSuccess = true,
                Message = "Residential property added successfully.",
                Data = _mapper.Map<ResidentialPropertyReadDto>(property)
            };
        }

        public async Task<ResponseDto<bool>> UpdateResidentialPropertyAsync(Guid id, ResidentialPropertyUpdateDto propertyDto)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(id);
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

        public async Task<ResponseDto<bool>> DeleteResidentialPropertyAsync(Guid id)
        {
            var existing = await _repo.GetResidentialPropertyByIdAsync(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Residential property not found.",
                    Data = false
                };
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
