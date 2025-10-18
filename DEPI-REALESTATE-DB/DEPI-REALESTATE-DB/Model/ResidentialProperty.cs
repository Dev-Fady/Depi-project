using DEPI_REALESTATE_DB.Model.Enums;

namespace DEPI_REALESTATE_DB.Model
{
    public class ResidentialProperty : Property
    {
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType KitchenType { get; set; } 
    }
}
