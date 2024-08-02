namespace s4.Models
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int FamilyCount { get; set; }
    }
}
