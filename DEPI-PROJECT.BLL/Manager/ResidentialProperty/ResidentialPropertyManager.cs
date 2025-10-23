using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
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
        public ResidentialPropertyManager(IMapper mapper, IResidentialPropertyRepo repo) {
            _mapper = mapper;
            _repo = repo;
        }

        public PagedResult<ResidentialPropertyReadDto> GetAllResidentialProperty(int pageNumber, int pageSize)
        {
            var result = _repo.GetAllResidentialProperty(pageNumber, pageSize);

            var mappedData = _mapper.Map<List<ResidentialPropertyReadDto>>(result.Data);

            return new PagedResult<ResidentialPropertyReadDto>
            {
                Data = mappedData,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };
        }


        public ResidentialPropertyReadDto GetResidentialPropertyById(Guid id)
        {
            var property = _repo.GetResidentialPropertyById(id);
            if (property == null)
            {
                throw new Exception("Residential Property not found");
            }
            return _mapper.Map<ResidentialPropertyReadDto>(property);
        }

        public ResidentialPropertyReadDto AddResidentialProperty(ResidentialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<EntityResidentialProperty>(propertyDto);
            _repo.AddResidentialProperty(property);
            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                _repo.AddAmenity(amenity);
            }
            return _mapper.Map<ResidentialPropertyReadDto>(property);
        }

        public bool DeleteResidentialProperty(Guid id)
        {
            var existing = _repo.GetResidentialPropertyById(id);
            if (existing == null)
                return false;

            _repo.DeleteResidentialProperty(id);
            return true;
        }

        public bool UpdateResidentialProperty(Guid id, ResidentialPropertyUpdateDto propertyDto)
        {
            var existing = _repo.GetResidentialPropertyById(id);
            if (existing == null)
                return false;

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
            
            return true;
        }
    }
}
