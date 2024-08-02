namespace s4.Models
{
    public class TableDto
    {
        public long itemID { get; set; }
        public string? title { get; set; }
        public string? area { get; set; }
        public long? average { get; set; }
        public long? completed { get; set; }
        public decimal? payable { get; set; }
    }
}
