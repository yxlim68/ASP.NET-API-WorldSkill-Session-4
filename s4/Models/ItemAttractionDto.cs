namespace s4.Models
{
    public class ItemAttractionDto
    {
        public long ItemID { get; set; }
        public long AttractionID { get; set; }
        public decimal Distance { get; set; }
        public long DurationOnFoot { get; set; }
        public long DurationByCar { get; set; }
    }
}
