using System.Linq.Expressions;
using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Broker;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class BrokerService : IBrokerService
    {
        private readonly IBrokerRepo _BrokerRepo;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public BrokerService(IBrokerRepo BrokerRepo,
                            IMapper mapper,
                            IUserRoleService userRoleService,
                            IRoleService roleService)
        {
            _BrokerRepo = BrokerRepo;
            _mapper = mapper;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public async Task<ResponseDto<PagedResultDto<BrokerResponseDto>>> GetAllAsync(BrokerQueryDto BrokerQueryDto)
        {
            var query = _BrokerRepo.GetAll();

            var result = await query.IF(BrokerQueryDto.LicenseID != null, a => a.LicenseID.Contains(BrokerQueryDto.LicenseID))
                                    .IF(BrokerQueryDto.NationalID != null, a => a.NationalID.Contains(BrokerQueryDto.NationalID))
                                    .Paginate(new PagedQueryDto { PageNumber = BrokerQueryDto.PageNumber, PageSize = BrokerQueryDto.PageSize })
                                    .OrderByExtended(new List<Tuple<bool, Expression<Func<Broker, object>>>>
                                                {
                                                    new (BrokerQueryDto.OrderByOption == OrderByBrokerOptions.LicenseID, a => a.LicenseID),
                                                    new (BrokerQueryDto.OrderByOption == OrderByBrokerOptions.NationalID, a => a.NationalID)
                                                },
                                            BrokerQueryDto.IsDesc
                                        )
                                    .ToListAsync();
            var BrokerResponseDto = _mapper.Map<IEnumerable<Broker>, IEnumerable<BrokerResponseDto>>(result);

            var PagedResult = new PagedResultDto<BrokerResponseDto>(BrokerResponseDto, BrokerQueryDto.PageNumber, query.Count(), BrokerQueryDto.PageSize);

            return new ResponseDto<PagedResultDto<BrokerResponseDto>>
            {
                Data = PagedResult,
                Message = "Brokers Retrived Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<BrokerResponseDto>> GetByIdAsync(Guid BrokerId)
        {
            var Broker = await _BrokerRepo.GetByIdAsync(BrokerId);

            if (Broker == null)
            {
                return new ResponseDto<BrokerResponseDto>
                {
                    Message = $"No Broker found with ID {BrokerId}",
                    IsSuccess = false
                };
            }

            var BrokerResponseDto = _mapper.Map<Broker, BrokerResponseDto>(Broker);

            return new ResponseDto<BrokerResponseDto>
            {
                Data = BrokerResponseDto,
                Message = "Broker Returned successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<BrokerResponseDto>> CreateAsync(BrokerCreateDto BrokerCreateDto)
        {
            var Broker = _mapper.Map<BrokerCreateDto, Broker>(BrokerCreateDto);
            Broker = await _BrokerRepo.CreateAsync(Broker);

            if (Broker == null)
            {
                return new ResponseDto<BrokerResponseDto>
                {
                    Message = "An error occurred while creating Broker",
                    IsSuccess = false
                };
            }

            // Assignning user to role Broker
            var roleResponse = await _roleService.GetByName(UserRoleOptions.Broker.ToString());
            var result = await _userRoleService.AssignUserToRole(new UserRoleDto { UserId = Broker.UserId, RoleId = roleResponse.Data.RoleId });

            if (!result.IsSuccess)
            {
                return new ResponseDto<BrokerResponseDto>
                {
                    Message = result.Message,
                    IsSuccess = false
                };
            }
            var BrokerResponseDto = _mapper.Map<Broker, BrokerResponseDto>(Broker);

            return new ResponseDto<BrokerResponseDto>
            {
                Data = BrokerResponseDto,
                Message = "Broker Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(BrokerUpdateDto BrokerUpdateDto)
        {
            var Broker = await _BrokerRepo.GetByIdAsync(BrokerUpdateDto.Id);
            if (Broker == null)
            {
                return new ResponseDto<bool>
                {
                    Message = $"No Broker found with ID {BrokerUpdateDto.Id}",
                    IsSuccess = false
                };
            }

            _mapper.Map<BrokerUpdateDto, Broker>(BrokerUpdateDto, Broker);
            
            bool result = await _BrokerRepo.UpdateAsync(Broker);
            if (!result)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occurred while updating Broker",
                    IsSuccess = false
                };
            }
            return new ResponseDto<bool>
            {
                Message = "Broker Updated Successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<bool>> DeleteAsync(Guid BrokerId)
        {
            bool result = await _BrokerRepo.DeleteAsync(BrokerId);

            if (!result)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occurred while deleting Broker",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                Message = "User Deleted Successfully",
                IsSuccess = true
            };
        }
        
    }
}