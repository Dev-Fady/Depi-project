using DEPI_PROJECT.BLL.DTOs.Authentication;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public AuthResponseDto Register(AuthRegisterDto authRegisterDto);
        
    }
}