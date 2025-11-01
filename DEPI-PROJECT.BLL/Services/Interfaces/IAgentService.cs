using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IAgentService
    {
        public Task<ResponseDto<PagedResultDto<AgentResponseDto>>> GetAllAsync(AgentQueryDto agentQueryDto);
        public Task<ResponseDto<AgentResponseDto>> GetByIdAsync(Guid AgentId);
        public Task<ResponseDto<AgentResponseDto>> CreateAsync(AgentCreateDto roleCreateDto);
        public Task<ResponseDto<bool>> UpdateAsync(AgentUpdateDto roleUpdateDto);
        public Task<ResponseDto<bool>> DeleteAsync(Guid AgentId);

    }
}