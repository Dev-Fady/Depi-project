using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repository.CommercialProperty;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCommercialProperty = DEPI_PROJECT.DAL.Models.CommercialProperty;

namespace DEPI_PROJECT.BLL.Manager.CommercialProperty
{
    public class CommercialPropertyManager : ICommercialPropertyManager
    {
        private readonly IMapper _mapper;
        private readonly ICommercialPropertyRepo _repo;

        public CommercialPropertyManager(IMapper mapper,ICommercialPropertyRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public PagedResult<CommercialPropertyReadDto> GetAllProperties(int pageNumber, int pageSize)
        {
            var result = _repo.GetAllProperties(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<CommercialPropertyReadDto>>(result.Data);
            return new PagedResult<CommercialPropertyReadDto>
            {
                Data = mappedData,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };
        }

        public CommercialPropertyReadDto GetPropertyById(Guid id)
        {
            var property = _repo.GetPropertyById(id);
            if (property == null)
            {
                throw new Exception("Commercial Property not found");
            }
            return _mapper.Map<CommercialPropertyReadDto>(property);
        }

        public bool UpdateCommercialProperty(Guid id, CommercialPropertyUpdateDto propertyDto)
        {
            var existing = _repo.GetPropertyById(id);
            if (existing == null)
            {
                return false;
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
            return true;
        }
        public CommercialPropertyReadDto AddProperty(CommercialPropertyAddDto propertyDto)
        {
            var property = _mapper.Map<EntityCommercialProperty>(propertyDto);
            _repo.AddCommercialProperty(property);
            if (propertyDto.Amenity != null)
            {
                var amenity = _mapper.Map<Amenity>(propertyDto.Amenity);
                amenity.PropertyId = property.PropertyId;
                _repo.AddAmenity(amenity);
            }
            return _mapper.Map<CommercialPropertyReadDto>(property);
        }

        public bool DeleteCommercialProperty(Guid id)
        {
            var existing = _repo.GetPropertyById(id);
            if (existing == null)
            {
                return false;
            }

            _repo.DeleteCommercialProperty(id);
            return true;
            
        }
    }
}
