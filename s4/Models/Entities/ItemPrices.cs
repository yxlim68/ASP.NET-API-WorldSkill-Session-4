namespace s4.Models.Entities
{
    public class ItemPrices
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long? ItemID { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
        public long CancellationPolicyID { get; set; }
    }
}
