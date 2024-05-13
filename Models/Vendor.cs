namespace JwtAuthentication_Relations_Authorization.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string NoOfVehicles { get; set; }
        public string NoOFDrivers { get; set;}
        public string VendorAdress {  get; set;}
        public string ServiceArea { get; set;}
        public string? Latitude { get; set;}
        public string? Longitude { get; set;}
        public int UserId { get; set;}
        public User user { get; set;}

    }
}
