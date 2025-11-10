using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Implements;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class LikePropertyService : ILikePropertyService
    {
        private readonly ILikePropertyRepo _LikePropertyRepo;
        private readonly IResidentialPropertyRepo _ResidentialPropertyRepo;
        private readonly ICommercialPropertyRepo _commercialPropertyRepo;
        public LikePropertyService(ILikePropertyRepo LikePropertyRepo, IResidentialPropertyRepo ResidentialPropertyRepo , ICommercialPropertyRepo commercialPropertyRepo)
        {
            _LikePropertyRepo = LikePropertyRepo;
            _ResidentialPropertyRepo = ResidentialPropertyRepo;
            _commercialPropertyRepo = commercialPropertyRepo;
        }
        public async Task<ResponseDto<ToggleResult>> ToggleLikeProperty(Guid userId, Guid propertyId)
        {
            if (userId == Guid.Empty || propertyId == Guid.Empty)
            {
                return new ResponseDto<ToggleResult>()
                {
                    IsSuccess = false,
                    Message = "Invalid input data",
                    Data = ToggleResult.Failed
                };
            }
            var ResidentialProperty = await _ResidentialPropertyRepo.GetResidentialPropertyByIdAsync(propertyId);
            var commercialProperty = await _commercialPropertyRepo.GetPropertyByIdAsync(propertyId);  
            if (ResidentialProperty == null && commercialProperty == null) 
            {
                return new ResponseDto<ToggleResult>()
                {
                    IsSuccess = false,
                    Message = "Property not found",
                    Data = ToggleResult.NotFound
                };
            }

            var existingLike = await _LikePropertyRepo.GetLikePropertyByUserAndPropertyId(userId, propertyId);
            if (existingLike == null)
            {
                var newLike = new LikeProperty
                {
                    UserID = userId,
                    PropertyId = propertyId,
                    CreatedAt = DateTime.UtcNow
                };
                var statusAdd = await _LikePropertyRepo.AddLikeProperty(newLike);
                if (!statusAdd)
                {
                    return new ResponseDto<ToggleResult>()
                    {
                        IsSuccess = false,
                        Message = "Failed to like the property",
                        Data = ToggleResult.Failed
                    };
                }
                return new ResponseDto<ToggleResult>()
                {
                    IsSuccess = true,
                    Message = "Property liked successfully",
                    Data = ToggleResult.Added
                };
            }
            var statusDelete = await _LikePropertyRepo.DeleteLikeProperty(existingLike);
            if (!statusDelete)
            {
                return new ResponseDto<ToggleResult>()
                {
                    IsSuccess = false,
                    Message = "Failed to unlike the property",
                    Data = ToggleResult.Failed
                };
            }
            return new ResponseDto<ToggleResult>()
            {
                IsSuccess = true,
                Message = "Property unliked successfully",
                Data = ToggleResult.Deleted
            };
        }
    }
}
