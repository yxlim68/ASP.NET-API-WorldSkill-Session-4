namespace s4.Model.Entities
{
    public class Coupons
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal MaximimDiscountAmount { get; set; }

    }
}
