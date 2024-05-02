namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class UserResponce
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PassCode { get; set; }
        public RoleResponse Role { get; set; }
        public string Token { get; set; }


    }
}
