using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
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

            // defualt response
            var response = new ResponseDto<ToggleResult>()
            {
                IsSuccess = true,
                Message = "Property liked successfully",
                Data = ToggleResult.Added
            };

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
            }
            else
            {
                var statusDelete = await _LikePropertyRepo.DeleteLikeProperty(existingLike);
                if (!statusDelete)
                {
                    throw new Exception("An error occured while disliking the property, please try again");

                }
                response = new ResponseDto<ToggleResult>()
                {
                    IsSuccess = true,
                    Message = "Property disliked successfully",
                    Data = ToggleResult.Deleted
                };
            }

            // invalidate cache in the end
            if(ResidentialProperty == null)
            {
                _cacheService.InvalidateCache(CacheConstants.COMMERCIAL_PROPERTY_CACHE);
            }
            else
            {
                _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);
            }

            return response; 
        }
        public async Task AddLikesCountAndIsLike(Guid UserId, List<PropertyResponseDto> mappedData)
        {
            var PropertiesIds = mappedData.Select(p => p.PropertyId).ToList();
            var CountPropertyDic = await _LikePropertyRepo.GetAllLikesByPropertyIds(PropertiesIds)
                                    .GroupBy(lc => lc.PropertyId)
                                    .Select(n => new
                                    {
                                        PropertyId = n.Key,
                                        Count = n.Count()
                                    })
                                    .ToDictionaryAsync(n => n.PropertyId, n => n.Count);

            var IsLikedHash = await _LikePropertyRepo.GetAllLikesByPropertyIds(PropertiesIds)
                                    .Where(lc => lc.UserID == UserId)
                                    .Select(n => n.PropertyId)
                                    .ToHashSetAsync();

            mappedData.ForEach(p => p.IsLiked = IsLikedHash.Contains(p.PropertyId));
            mappedData.ForEach(p => p.LikesCount = CountPropertyDic.GetValueOrDefault(p.PropertyId));
        }
    }
}
