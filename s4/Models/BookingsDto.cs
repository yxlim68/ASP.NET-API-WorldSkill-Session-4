namespace s4.Models.Entities
{
    public class BookingsDto
    {
        public long UserID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
