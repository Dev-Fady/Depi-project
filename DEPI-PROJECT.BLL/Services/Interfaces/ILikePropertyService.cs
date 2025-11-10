using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ILikePropertyService
    {
        public Task<ResponseDto<ToggleResult>> ToggleLikeProperty(Guid userId, Guid propertyId);
    }
}
