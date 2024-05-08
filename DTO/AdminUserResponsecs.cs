namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class AdminUserResponsecs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PassCode { get; set; }
        public RoleResponse Role { get; set; }
    }
}
