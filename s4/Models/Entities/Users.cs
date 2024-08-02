namespace s4.Models.Entities
{
    public class Users
    {
        public long ID { get; set; }
        public Guid GUID { get; set; }
        public long UserTypeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int FamilyCount { get; set; }
    }
}
