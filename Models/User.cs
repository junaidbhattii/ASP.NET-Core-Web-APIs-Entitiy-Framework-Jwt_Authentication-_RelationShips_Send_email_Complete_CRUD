namespace JwtAuthentication_Relations_Authorization.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Country { get; set; }
        public string? PassCode { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public Vendor vendor { get; set; }
        public Admin admin { get; set; }

    }
}
