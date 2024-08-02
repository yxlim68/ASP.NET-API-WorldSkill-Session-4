namespace s4.Models
{
    public class ItemDto
    {
        public long UserID { get; set; }
        public long ItemTypeID { get; set; }
        public long AreaID { get; set; }
        public string Title { get; set; }
        public int capacity { get; set; }
        public int NumberOfBeds { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string ExactAddress { get; set; }
        public string ApproximateAddress { get; set; }
        public string Description { get; set; }
        public string HostRules { get; set; }
        public int MaximumNights { get; set; }
        public int MinimumNights { get; set; }
    }
}
