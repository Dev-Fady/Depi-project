using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.DAL.Models
{
    public class ResidentialProperty : Property
    {
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType KitchenType { get; set; } 
    }
}
