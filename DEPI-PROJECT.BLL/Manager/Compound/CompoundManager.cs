using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.DAL.Repository.Compound;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCompound = DEPI_PROJECT.DAL.Models.Compound;


namespace DEPI_PROJECT.BLL.Manager.Compound
{
    public class CompoundManager : ICompoundManager
    {
        private readonly ICompoundRepo _repo;
        private readonly IMapper _mapper;

        public CompoundManager(ICompoundRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public ResponseDto<PagedResult<CompoundReadDto>> GetAllCompounds(int pageNumber, int pageSize)
        {
            var result = _repo.GetAllCompounds(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<CompoundReadDto>>(result.Data);
            var pagedResult = new PagedResult<CompoundReadDto>
            {
                Data = mappedData,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };
            return new ResponseDto<PagedResult<CompoundReadDto>>
            {
                IsSuccess = true,
                Message = "Compounds retrieved successfully.",
                Data = pagedResult
            };
        }

        public ResponseDto<CompoundReadDto> GetCompoundById(Guid id)
        {
            var compound = _repo.GetCompoundById(id);
            if (compound == null)
            {
                return new ResponseDto<CompoundReadDto>
                {
                    IsSuccess = false,
                    Message = "Compound not found."
                };
            }
            var mapped = _mapper.Map<CompoundReadDto>(compound);
            return new ResponseDto<CompoundReadDto>
            {
                IsSuccess = true,
                Message = "Compound retrieved successfully.",
                Data = mapped
            };
        }

        public ResponseDto<bool> UpdateCompound(Guid id, CompoundUpdateDto compoundDto)
        {
            var existing = _repo.GetCompoundById(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Compound not found.",
                    Data = false
                };
            }
            _mapper.Map(compoundDto, existing);
            _repo.UpdateCompound(id, existing);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Compound updated successfully.",
                Data = true
            };
        }
        public ResponseDto<CompoundReadDto> AddCompound(CompoundAddDto compoundDto)
        {
            var compoundEntity = _mapper.Map<EntityCompound>(compoundDto);
            _repo.AddCompound(compoundEntity);
            return new ResponseDto<CompoundReadDto>
            {
                IsSuccess = true,
                Message = "Compound added successfully.",
                Data = _mapper.Map<CompoundReadDto>(compoundEntity)
            };
        }

        public ResponseDto<bool> DeleteCompound(Guid id)
        {
           var existing = _repo.GetCompoundById(id);
            if (existing == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Compound not found.",
                    Data = false
                };
            }
            _repo.DeleteCompound(id);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Compound deleted successfully.",
                Data = true
            };
        }

    }
}
