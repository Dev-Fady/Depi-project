using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;

using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResponseDto<List<UserResponseDto>>> GetAllUsersAsync();
        public Task<ResponseDto<UserResponseDto>> GetUserByIdAsync(Guid UserId);
        public Task<ResponseDto<bool>> UpdateUserAsync(UserUpdateDto userUpdateDto);
        public Task<ResponseDto<bool>> DeleteUserAsync(Guid UserId);

    }
}