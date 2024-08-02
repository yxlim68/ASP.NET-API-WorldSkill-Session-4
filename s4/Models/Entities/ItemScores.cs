namespace s4.Models.Entities
{
    public class ItemScores
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long UserID { get; set; }
        public long? ItemID { get; set; }
        public long ScoreID { get; set; }
        public long? Value { get; set; }
    }
}
