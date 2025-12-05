using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Implements;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICacheService _cacheService;

        public LikePropertyService(ILikePropertyRepo LikePropertyRepo, 
                                   IResidentialPropertyRepo ResidentialPropertyRepo, 
                                   ICommercialPropertyRepo commercialPropertyRepo,
                                   ICacheService cacheService)
        {
            _LikePropertyRepo = LikePropertyRepo;
            _ResidentialPropertyRepo = ResidentialPropertyRepo;
            _commercialPropertyRepo = commercialPropertyRepo;
            _cacheService = cacheService;
        }
        public async Task<ResponseDto<ToggleResult>> ToggleLikeProperty(Guid userId, Guid propertyId)
        {
            if (userId == Guid.Empty || propertyId == Guid.Empty)
            {
                throw new BadRequestException("User id and property Id both cannot be null");

            }
            var ResidentialProperty = await _ResidentialPropertyRepo.GetResidentialPropertyByIdAsync(userId, propertyId);
            var commercialProperty = await _commercialPropertyRepo.GetPropertyByIdAsync(userId, propertyId);
            if (ResidentialProperty == null && commercialProperty == null)
            {
                throw new NotFoundException($"No property found with Id {propertyId}");
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
                    throw new Exception("An error occured while liking the property, please try again");

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
                throw new Exception("An error occured while disliking the property, please try again");

            }

            if(ResidentialProperty == null)
            {
                _cacheService.InvalidateCache(CacheConstants.COMMERCIAL_PROPERTY_CACHE);
            }
            else
            {
                _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);
            }
            return new ResponseDto<ToggleResult>()
            {
                IsSuccess = true,
                Message = "Property disliked successfully",
                Data = ToggleResult.Deleted
            };
        }
    }
}
