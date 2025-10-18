namespace DEPI_PROJECT.DAL.Models
{
    public class CommercialProperty : Property
    {
        public string BusinessType { get; set; }
        public int? FloorNumber { get; set; }
        public bool? HasStorage { get; set; }
    }
}
