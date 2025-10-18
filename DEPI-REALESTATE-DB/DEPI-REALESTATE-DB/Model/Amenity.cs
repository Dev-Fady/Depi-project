using System.ComponentModel.DataAnnotations;

namespace DEPI_REALESTATE_DB.Model
{
    public class Amenity
    {
        [Key]
        public Guid PropertyId { get; set; }

        public bool HasElectricityLine { get; set; }
        public bool HasWaterLine { get; set; }
        public bool HasGasLine { get; set; }

        public Property Property { get; set; }
    }

}
