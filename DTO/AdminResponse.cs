namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class AdminResponse
    {
        public string? AdminAdress { get; set; }
        public string? AdminName { get; set; }
        public string? AdminEmail { get; set; }
        public UserResponce userResponce { get; set; }

    }
}
