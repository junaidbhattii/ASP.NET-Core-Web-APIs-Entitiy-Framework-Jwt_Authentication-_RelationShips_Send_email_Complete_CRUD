namespace JwtAuthentication_Relations_Authorization.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string? AdminAdress {  get; set; }
        public string? AdminName { get; set; }
        public string? AdminEmail { get; set;}
        public int UserId { get; set; }
        public User user { get; set; }

    }
}
