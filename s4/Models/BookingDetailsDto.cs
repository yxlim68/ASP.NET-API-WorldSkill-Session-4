namespace s4.Models.Entities
{
    public class BookingDetailsDto
    {
        public long bookingID { get; set; }
        public long ItemPriceID { get; set; }
        public Boolean isRefund { get; set; }
        public DateTime RefundDate { get; set; }
        public long RefundCancellationPolicyID { get; set; }
    }
}
