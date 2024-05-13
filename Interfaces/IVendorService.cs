using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IVendorService
    {
        public Task<vendorResponse> VendorRegistration(VendorBodyRequest vendorBodyRequest);
        public List<vendorResponse> GetAllVendorRecord();
        public vendorResponse UpdateVendorRecord(VendorBodyRequest vendorBodyRequest ,int id);
    }
}
