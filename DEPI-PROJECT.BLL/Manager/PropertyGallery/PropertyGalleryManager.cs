using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DataModel = DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repository.PropertyGallery;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Manager.PropertyGallery
{
    public class PropertyGalleryManager : IPropertyGalleryManager
    {
        private readonly IPropertyGalleryRepo _repo;
        public PropertyGalleryManager(IPropertyGalleryRepo repo)
        {
            _repo = repo;
        }

        public async Task Add(PropertyGalleryAddDto dto)
        {
            if (dto.MediaFiles == null || !dto.MediaFiles.Any())
                throw new ArgumentException("No media files uploaded.");

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

            _repo.AddRange(galleryList);
        }

        public bool Delete(Guid id)
        {
            var gallery = _repo.GetById(id);
            if (gallery == null)
                return false;

            string? filePath = null;
            if (!string.IsNullOrEmpty(gallery.ImageUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.ImageUrl.TrimStart('/'));
            else if (!string.IsNullOrEmpty(gallery.VideoUrl))
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", gallery.VideoUrl.TrimStart('/'));

            if (filePath != null && File.Exists(filePath))
                File.Delete(filePath);

            _repo.Delete(id);
            return true;
        }
        public IEnumerable<PropertyGalleryReadDto> GetAll()
        {
            var data = _repo.GetAll();
            return data.Select(g => new PropertyGalleryReadDto
            {
                MediaId = g.MediaId,
                PropertyId = g.PropertyId,
                ImageUrl = g.ImageUrl,
                VideoUrl = g.VideoUrl,
                UploadedAt = g.UploadedAt
            });
        }
        public PropertyGalleryReadDto GetById(Guid id)
        {
            var g = _repo.GetById(id);
            if (g == null)
                return null;

            return new PropertyGalleryReadDto
            {
                MediaId = g.MediaId,
                PropertyId = g.PropertyId,
                ImageUrl = g.ImageUrl,
                VideoUrl = g.VideoUrl,
                UploadedAt = g.UploadedAt
            };
        }

        public IEnumerable<DataModel.PropertyGallery> GetByPropertyId(Guid propertyId)
        {
            return _repo.GetByPropertyId(propertyId);
        }
    }
}
