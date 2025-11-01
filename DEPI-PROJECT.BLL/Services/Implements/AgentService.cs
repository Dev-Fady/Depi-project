using System.Linq.Expressions;
using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Agent;
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
    public class AgentService : IAgentService
    {
        private readonly IAgentRepo _agentRepo;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public AgentService(IAgentRepo agentRepo,
                            IMapper mapper,
                            IUserRoleService userRoleService,
                            IRoleService roleService)
        {
            _agentRepo = agentRepo;
            _mapper = mapper;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public async Task<ResponseDto<PagedResultDto<AgentResponseDto>>> GetAllAsync(AgentQueryDto agentQueryDto)
        {
            var query = _agentRepo.GetAll();

            var result = await query.IF(agentQueryDto.AgencyName != null, a => a.AgencyName.Contains(agentQueryDto.AgencyName))
                                    .IF(agentQueryDto.MinRating != null, a => a.Rating >= agentQueryDto.MinRating)
                                    .IF(agentQueryDto.MinexperienceYears != null, a => a.ExperienceYears >= agentQueryDto.MinexperienceYears)
                                    .Paginate(new PagedQueryDto { PageNumber = agentQueryDto.PageNumber, PageSize = agentQueryDto.PageSize })
                                    .OrderByExtended(new List<Tuple<bool, Expression<Func<Agent, object>>>>
                                                {
                                                    new (agentQueryDto.OrderByOption == OrderByAgentOptions.Rating, a => a.Rating),
                                                    new (agentQueryDto.OrderByOption == OrderByAgentOptions.experienceYears, a => a.ExperienceYears)
                                                },
                                            agentQueryDto.IsDesc
                                        )
                                    .ToListAsync();
            var AgentResponseDto = _mapper.Map<IEnumerable<Agent>, IEnumerable<AgentResponseDto>>(result);

            var PagedResult = new PagedResultDto<AgentResponseDto>(AgentResponseDto, agentQueryDto.PageNumber, query.Count(), agentQueryDto.PageSize);

            return new ResponseDto<PagedResultDto<AgentResponseDto>>
            {
                Data = PagedResult,
                Message = "Agents Retrived Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<AgentResponseDto>> GetByIdAsync(Guid AgentId)
        {
            var agent = await _agentRepo.GetByIdAsync(AgentId);

            if (agent == null)
            {
                return new ResponseDto<AgentResponseDto>
                {
                    Message = $"No Agent found with userId {AgentId}",
                    IsSuccess = false
                };
            }

            var AgentResponseDto = _mapper.Map<Agent, AgentResponseDto>(agent);

            return new ResponseDto<AgentResponseDto>
            {
                Data = AgentResponseDto,
                Message = "Agent Returned successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<AgentResponseDto>> CreateAsync(AgentCreateDto agentCreateDto)
        {
            var agent = _mapper.Map<AgentCreateDto, Agent>(agentCreateDto);
            agent = await _agentRepo.CreateAsync(agent);

            if (agent == null)
            {
                return new ResponseDto<AgentResponseDto>
                {
                    Message = "An error occurred while creating agent",
                    IsSuccess = false
                };
            }

            // Assignning user to role Agent
            var roleResponse = await _roleService.GetByName(UserRoleOptions.Agent.ToString());
            var result = await _userRoleService.AssignUserToRole(new UserRoleDto { UserId = agent.UserId, RoleId = roleResponse.Data.RoleId });

            if (!result.IsSuccess)
            {
                return new ResponseDto<AgentResponseDto>
                {
                    Message = result.Message,
                    IsSuccess = false
                };
            }
            var agentResponseDto = _mapper.Map<Agent, AgentResponseDto>(agent);

            return new ResponseDto<AgentResponseDto>
            {
                Data = agentResponseDto,
                Message = "Agent Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(AgentUpdateDto agentUpdateDto)
        {
            var agent = await _agentRepo.GetByIdAsync(agentUpdateDto.AgentId);
            if (agent == null)
            {
                return new ResponseDto<bool>
                {
                    Message = $"No agent found with ID {agentUpdateDto.AgentId}",
                    IsSuccess = false
                };
            }

            _mapper.Map<AgentUpdateDto, Agent>(agentUpdateDto, agent);
            
            bool result = await _agentRepo.UpdateAsync(agent);
            if (!result)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occurred while updating agent",
                    IsSuccess = false
                };
            }
            return new ResponseDto<bool>
            {
                Message = "Agent Updated Successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<bool>> DeleteAsync(Guid AgentId)
        {
            bool result = await _agentRepo.DeleteAsync(AgentId);

            if (!result)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occurred while deleting agent",
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