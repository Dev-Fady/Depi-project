using AutoMapper;
using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using DataModel = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class PropertyGalleryService : IPropertyGalleryService
    {
        private readonly IPropertyGalleryRepo _repo;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public PropertyGalleryService(IPropertyGalleryRepo repo,
                                      IPropertyService propertyService,
                                      IMapper mapper,
                                      ICacheService cacheService)
        {
            _repo = repo;
            _propertyService = propertyService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ResponseDto<string>> AddAsync(Guid UserId, PropertyGalleryAddDto dto)
        {
            if (await CheckAuthorizedAgent(UserId, dto.PropertyId) == false)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    Message = "Unauthorized upload"
                };
            }
            if (dto.MediaFiles == null || !dto.MediaFiles.Any())
            {
                throw new BadRequestException("Expected media files");
            }

            var existing = await _propertyService.GetPropertyById(dto.PropertyId);
            if(existing == null)
            {
                throw new NotFoundException($"No property found with ID {dto.PropertyId}");
            }

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var galleryList = new List<PropertyGallery>();

            foreach (var file in dto.MediaFiles)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

                var fileUrl = $"/uploads/{fileName}";

                var gallery = new PropertyGallery
                {
                    MediaId = Guid.NewGuid(),
                    PropertyId = dto.PropertyId,
                    UploadedAt = DateTime.UtcNow
                };

                if (ext == ".mp4" || ext == ".mov" || ext == ".avi")
                    gallery.VideoUrl = fileUrl;
                else
                    gallery.ImageUrl = fileUrl;

                galleryList.Add(gallery);
            }

            await _repo.AddRangeAsync(galleryList);

            if(existing.PropertyType == PropertyType.Commercial)
            {
                _cacheService.InvalidateCache(CacheConstants.COMMERCIAL_PROPERTY_CACHE);
            }
            else
            {
                _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);
            }

            return new ResponseDto<string>
            {
                IsSuccess = true,
                Message = $"{dto.MediaFiles.Count} file(s) uploaded successfully.",
                Data = "Upload completed."
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(Guid UserId, Guid id)
        {
            var gallery = await _repo.GetByIdAsync(id);
            if (gallery == null)
            {
                throw new NotFoundException($"No media file found with Id {id}");
            }

            CommonFunctions.EnsureAuthorized(gallery.Property.Agent.UserId);

            string? filePath = null;
            if (!string.IsNullOrEmpty(gallery.ImageUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.ImageUrl.TrimStart('/'));
            else if (!string.IsNullOrEmpty(gallery.VideoUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.VideoUrl.TrimStart('/'));

            if (filePath != null && File.Exists(filePath))
                File.Delete(filePath);

            await _repo.DeleteAsync(id);

            if(gallery.Property.PropertyType == PropertyType.Residential)
            {
                _cacheService.InvalidateCache(CacheConstants.COMMERCIAL_PROPERTY_CACHE);
            }
            else
            {
                _cacheService.InvalidateCache(CacheConstants.RESIDENTIAL_PROPERTY_CACHE);
            }

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Gallery deleted successfully.",
                Data = true
            };
        }

        public async Task<ResponseDto<IEnumerable<PropertyGalleryReadDto>>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            var mapped = data.Select(g => new PropertyGalleryReadDto
            {
                MediaId = g.MediaId,
                PropertyId = g.PropertyId,
                ImageUrl = g.ImageUrl,
                VideoUrl = g.VideoUrl,
                UploadedAt = g.UploadedAt
            });

            return new ResponseDto<IEnumerable<PropertyGalleryReadDto>>
            {
                IsSuccess = true,
                Message = "Gallery items retrieved successfully.",
                Data = mapped
            };
        }

        public async Task<ResponseDto<PropertyGalleryReadDto>> GetByIdAsync(Guid id)
        {
            var g = await _repo.GetByIdAsync(id);
            if (g == null)
            {
                throw new NotFoundException($"No media file found with Id {id}");
            }

            return new ResponseDto<PropertyGalleryReadDto>
            {
                IsSuccess = true,
                Message = "Gallery item retrieved successfully.",
                Data = new PropertyGalleryReadDto
                {
                    MediaId = g.MediaId,
                    PropertyId = g.PropertyId,
                    ImageUrl = g.ImageUrl,
                    VideoUrl = g.VideoUrl,
                    UploadedAt = g.UploadedAt
                }
            };
        }

        public async Task<ResponseDto<IEnumerable<PropertyGalleryReadDto>>> GetByPropertyIdAsync(Guid propertyId)
        {
            var data = await _repo.GetByPropertyIdAsync(propertyId);

            return new ResponseDto<IEnumerable<PropertyGalleryReadDto>>
            {
                IsSuccess = true,
                Message = $"Gallery items for property {propertyId} retrieved successfully.",
                Data = _mapper.Map<IEnumerable<PropertyGalleryReadDto>>(data)
            };
        }
        private async Task<bool> CheckAuthorizedAgent(Guid UserId, Guid propertyId)
        {
            var property = await _propertyService.GetPropertyById(propertyId);
            if (property == null)
            {
                throw new NotFoundException($"No property found with ID {propertyId}");
            }
            return property.UserId == UserId;
        }
    }

}
