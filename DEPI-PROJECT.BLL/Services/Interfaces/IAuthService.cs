using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Response;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseDto<AuthResponseDto>> RegisterAsync(AuthRegisterDto authRegisterDto);
        public Task<ResponseDto<AuthResponseDto>> LoginAsync(AuthLoginDto authLoginDto);
        public Task<ResponseDto<bool>> LogoutAsync(Guid userId);
    }
}