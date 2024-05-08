namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class VendorBodyRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NoOfVehicles { get; set; }
        public string NoOFDrivers { get; set; }
        public string VendorAdress { get; set; }
        public string ServiceArea { get; set; }
    }
}
