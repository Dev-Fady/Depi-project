using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
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
using EntityCompound = DEPI_PROJECT.DAL.Models.Compound;


namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class CompoundService : ICompoundService
    {
        private readonly ICompoundRepo _repo;
        private readonly IMapper _mapper;

        public CompoundService(ICompoundRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ResponseDto<PagedResultDto<CompoundReadDto>>> GetAllCompoundsAsync(CompoundQueryDto compoundQueryDto)
        {
            var query = _repo.GetAllCompounds();

            var result = await query.IF(compoundQueryDto.City != null, a => a.City == compoundQueryDto.City)
                                    .IF(compoundQueryDto.Address != null, a => a.Address.Contains(compoundQueryDto.Address ?? ""))
                                    .IF(compoundQueryDto.Description != null, a => a.Description.Contains(compoundQueryDto.Description ?? ""))
                                    .Paginate(new PagedQueryDto { PageNumber = compoundQueryDto.PageNumber, PageSize = compoundQueryDto.PageSize })
                                    .ToListAsync();

            var mappedData = _mapper.Map<List<CompoundReadDto>>(result);
            var pagedResult = new PagedResultDto<CompoundReadDto>(mappedData, compoundQueryDto.PageNumber, result.Count, compoundQueryDto.PageSize);
            
            return new ResponseDto<PagedResultDto<CompoundReadDto>>
            {
                IsSuccess = true,
                Message = "Compounds retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<CompoundReadDto>> GetCompoundByIdAsync(Guid id)
        {
            var compound = await GetCompoundIfExistsAsync(id);
            var mapped = _mapper.Map<CompoundReadDto>(compound);
            return new ResponseDto<CompoundReadDto>
            {
                IsSuccess = true,
                Message = "Compound retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<bool>> UpdateCompoundAsync(Guid id, CompoundUpdateDto compoundDto)
        {
            var existing = await GetCompoundIfExistsAsync(id);
            _mapper.Map(compoundDto, existing);
            await _repo.UpdateCompoundAsync(id, existing);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Compound updated successfully.",
                Data = true
            };
        }
        public async Task<ResponseDto<CompoundReadDto>> AddCompoundAsync(CompoundAddDto compoundDto)
        {
            var compoundEntity = _mapper.Map<EntityCompound>(compoundDto);
            await _repo.AddCompoundAsync(compoundEntity);
            return new ResponseDto<CompoundReadDto>
            {
                IsSuccess = true,
                Message = "Compound added successfully.",
                Data = _mapper.Map<CompoundReadDto>(compoundEntity)
            };
        }

        public async Task<ResponseDto<bool>> DeleteCompoundAsync(Guid id)
        {
            await GetCompoundIfExistsAsync(id);

            await _repo.DeleteCompoundAsync(id);
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Compound deleted successfully.",
                Data = true
            };
        }

        public async Task<Compound> GetCompoundIfExistsAsync(Guid id)
        {
            return await _repo.GetCompoundByIdAsync(id) ?? throw new NotFoundException($"No compund found with Id {id}");
        }

    }
}
