using DEPI_PROJECT.BLL.DTOs.Broker;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IBrokerService
    {
        public Task<ResponseDto<PagedResultDto<BrokerResponseDto>>> GetAllAsync(BrokerQueryDto BrokerQueryDto);
        public Task<ResponseDto<BrokerResponseDto>> GetByIdAsync(Guid BrokerId);
        public Task<ResponseDto<BrokerResponseDto>> CreateAsync(BrokerCreateDto roleCreateDto);
        public Task<ResponseDto<bool>> UpdateAsync(BrokerUpdateDto roleUpdateDto);
        public Task<ResponseDto<bool>> DeleteAsync(Guid BrokerId);

    }
}