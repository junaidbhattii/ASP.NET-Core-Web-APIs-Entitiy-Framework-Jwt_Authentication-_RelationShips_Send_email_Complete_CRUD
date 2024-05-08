using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication_Relations_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        public AdminResponse AddAdmin(AdminRequestBody adminRequestBody)
        {
            var AdminResult = _adminService.AddAdminRecord(adminRequestBody);
            return AdminResult; 
        }
        [HttpGet]
        public List<AdminResponse> GetAdmins()
        {
            var AdminResult = _adminService.GetAllAdmins();
            return AdminResult;
        }
        [HttpGet("GetAllUsers")]
        public List<AdminUserResponsecs> GetAllUsersRecords()
        {
            var AllUsers = _adminService.GetAllUsers();
            return AllUsers;
        }
        [HttpGet("GetAllVendors")]
        public List<AdminVendorResponse> GetAllVendorRecords()
        {
            var AllVendors = _adminService.GetAllVendorsByAdmin();
            return AllVendors;
        }
    }
}
