namespace s4.Models.Entities
{
    public class ItemAmenities
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long ItemID { get; set; }
        public long AmenityID { get; set; }
    }
}
