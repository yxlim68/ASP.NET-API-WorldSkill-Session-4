namespace s4.Models.Entities
{
    public class ItemAttractions
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long ItemID { get; set; }
        public long AttractionID { get; set; }
        public decimal Distance { get; set; }
        public long DurationOnFoot{ get; set; }
        public long DurationByCar { get; set; }
    }
}
