﻿namespace JwtAuthentication_Relations_Authorization.DTO
{
    public class vendorResponse
    {
        public string NoOfVehicles { get; set; }
        public string NoOFDrivers { get; set; }
        public string VendorAdress { get; set; }
        public string ServiceArea { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public UserResponce userResponce { get; set; }
    }
}
