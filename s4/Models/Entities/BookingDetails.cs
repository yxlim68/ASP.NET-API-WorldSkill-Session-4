namespace s4.Models.Entities
{
    public class BookingDetails
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long BookingID { get; set; }
        public long? ItemPriceID { get; set; }
        public Boolean isRefund { get; set; }
    }
}
