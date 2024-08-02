namespace s4.Models.Entities
{
    public class ItemPricesDto
    {
        public long ItemID { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public long CancellationPolicyID { get; set; }
    }
}
