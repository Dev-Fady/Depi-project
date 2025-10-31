using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using DataModel = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class PropertyGalleryService : IPropertyGalleryService
    {
        private readonly IPropertyGalleryRepo _repo;

        public PropertyGalleryService(IPropertyGalleryRepo repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDto<string>> AddAsync(PropertyGalleryAddDto dto)
        {
            if (dto.MediaFiles == null || !dto.MediaFiles.Any())
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    Message = "No media files uploaded.",
                    Data = null
                };
            }

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var galleryList = new List<DataModel.PropertyGallery>();

            foreach (var file in dto.MediaFiles)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

                var fileUrl = $"/uploads/{fileName}";

                var gallery = new DataModel.PropertyGallery
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

            return new ResponseDto<string>
            {
                IsSuccess = true,
                Message = $"{dto.MediaFiles.Count} file(s) uploaded successfully.",
                Data = "Upload completed."
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(Guid id)
        {
            var gallery = await _repo.GetByIdAsync(id);
            if (gallery == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = $"Gallery item with id {id} not found.",
                    Data = false
                };
            }

            string? filePath = null;
            if (!string.IsNullOrEmpty(gallery.ImageUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.ImageUrl.TrimStart('/'));
            else if (!string.IsNullOrEmpty(gallery.VideoUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.VideoUrl.TrimStart('/'));

            if (filePath != null && File.Exists(filePath))
                File.Delete(filePath);

            await _repo.DeleteAsync(id);

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
                return new ResponseDto<PropertyGalleryReadDto>
                {
                    IsSuccess = false,
                    Message = $"Gallery item with id {id} not found.",
                    Data = null
                };
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

        public async Task<ResponseDto<IEnumerable<DataModel.PropertyGallery>>> GetByPropertyIdAsync(Guid propertyId)
        {
            var data = await _repo.GetByPropertyIdAsync(propertyId);

            return new ResponseDto<IEnumerable<DataModel.PropertyGallery>>
            {
                IsSuccess = true,
                Message = $"Gallery items for property {propertyId} retrieved successfully.",
                Data = data
            };
        }
    }

}
