using AqarakDB.Models.Enums;

namespace AqarakDB.Models
{
    public class PropertyContractPhoto
    {
        public Guid Id { get; set; }
        public string PhotoUrl { get; set; }
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public ContractPhotoStatusEnum Status { get; set; } = ContractPhotoStatusEnum.Pending;
        public string StatusName => Status.ToString();

        public Guid? ReviewedBy { get; set; }
        public User? Reviewer { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Comment { get; set; }
    }
}
