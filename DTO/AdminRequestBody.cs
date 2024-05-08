namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class AdminRequestBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? AdminAdress { get; set; }

    }
}
