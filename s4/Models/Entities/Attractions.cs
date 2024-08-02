namespace s4.Models.Entities
{
    public class Attractions
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long AreaID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
