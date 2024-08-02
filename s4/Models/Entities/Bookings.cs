namespace s4.Models.Entities
{
    public class Bookings
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long UserID { get; set; }
        public DateTime BookingDate { get; set; }
        public long? CouponID { get; set; }
        public long? TransactionID { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
