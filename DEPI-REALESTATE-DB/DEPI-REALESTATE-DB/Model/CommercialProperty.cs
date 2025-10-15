namespace DEPI_REALESTATE_DB.Model
{
    public class CommercialProperty : Property
    {
        public string BusinessType { get; set; }
        public int? FloorNumber { get; set; }
        public bool? HasStorage { get; set; }
    }
}
