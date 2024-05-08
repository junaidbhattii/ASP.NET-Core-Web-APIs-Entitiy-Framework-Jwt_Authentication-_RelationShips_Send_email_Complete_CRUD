using JwtAuthentication_Relations_Authorization.DTO;

namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IAdminService
    {
        public AdminResponse AddAdminRecord(AdminRequestBody adminRequestBody);
        public List<AdminResponse> GetAllAdmins();
        public List<AdminUserResponsecs> GetAllUsers();
        public List<AdminVendorResponse> GetAllVendorsByAdmin();
    }
}
