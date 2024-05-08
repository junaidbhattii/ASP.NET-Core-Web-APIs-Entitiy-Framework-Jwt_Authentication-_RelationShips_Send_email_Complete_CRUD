using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IVendorService
    {
        public vendorResponse VendorRegistration(VendorBodyRequest vendorBodyRequest);
    }
}
