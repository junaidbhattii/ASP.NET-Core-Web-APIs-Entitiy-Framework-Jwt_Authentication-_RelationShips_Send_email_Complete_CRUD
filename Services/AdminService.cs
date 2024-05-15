using JwtAuthentication_Relations_Authorization.Context;
using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using JwtAuthentication_Relations_Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication_Relations_Authorization.Services
{
    public class AdminService : IAdminService
    {
        private readonly JwtAuthentication _JwtAuthentication;
        public AdminService(JwtAuthentication jwtAuthentication)
        {
            _JwtAuthentication = jwtAuthentication;
        }

        public AdminResponse AddAdminRecord(AdminRequestBody adminRequestBody)
        {
            var adminRecord = _JwtAuthentication.Users.FirstOrDefault(a => a.Email == adminRequestBody.Email);
            if (adminRecord == null)
            {
                var AdminUser = new User
                {
                    Email = adminRequestBody.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(adminRequestBody.Password),
                    Name = adminRequestBody.Name,
                    Country = adminRequestBody.AdminAdress,
                    RoleID = 3
                };
                _JwtAuthentication.Users.Add(AdminUser);
                var Entry = _JwtAuthentication.SaveChanges();
                if( Entry > 0)
                {
                    var AdminRe = new Admin
                    {
                        AdminAdress = adminRequestBody.AdminAdress,
                        AdminName = AdminUser.Name,
                        AdminEmail = AdminUser.Email,
                        UserId = AdminUser.Id
                    };
                    _JwtAuthentication.Admins.Add(AdminRe); 
                    var EntryAdmin = _JwtAuthentication.SaveChanges();
                    if( EntryAdmin > 0 )
                    {
                        var Response = new AdminResponse
                        {
                            AdminName = AdminRe.AdminName,
                            AdminEmail = AdminRe.AdminEmail,
                            AdminAdress = AdminRe.AdminAdress,
                            userResponce = new UserResponce
                            {
                                Email = AdminUser.Email,
                                Name = AdminUser.Name,
                                Country = AdminUser.Country,
                                Role = new RoleResponse
                                {
                                    Id = AdminUser.RoleID,
                                    Name = _JwtAuthentication.Roles.FirstOrDefault(r => r.Id == AdminUser.RoleID)?.Name
                                }
                            }
                        };
                        return Response;
                    }
                    else
                    {
                        throw new Exception("Admin Table Not Saved");
                    }
                }
                else
                {
                    throw new Exception("AdminUser Not Saved");
                }

            }
            else
            {
                throw new Exception("User With This Email Already Exist");
            }
        }
        public List<AdminResponse> GetAllAdmins()
        {
            var Admins = _JwtAuthentication.Admins.ToList();
            if( Admins.Count <= 0 )
            {
                throw new Exception("Admin Not Found");
            }
            else
            {
                List<AdminResponse> adminList =  _JwtAuthentication.Admins
                    .Include(u => u.user)
                    .ThenInclude(r => r.Role)
                    .Select(admin => new AdminResponse
                    {
                        AdminAdress = admin.AdminAdress,
                        AdminEmail = admin.AdminEmail,
                        AdminName = admin.AdminName,
                        userResponce = new UserResponce
                        {   
                            Email =admin.user.Email,
                            Name = admin.user.Name,
                            Country = admin.user.Country,
                            Role = new RoleResponse
                            {
                                Id = admin.user.RoleID,
                                Name = admin.user.Role.Name
                            }
                        }
                    })
                    .ToList();
                return adminList;

            }
        }
        public List<AdminUserResponsecs> GetAllUsers()
        {
            var Users = _JwtAuthentication.Users.ToList();
            if( Users.Count <= 0 )
            {
                throw new Exception("Users Not Found");
            }
            else
            {
                List<AdminUserResponsecs> userList = _JwtAuthentication.Users
                    .Include(r => r.Role)
                    .Where(User => User.RoleID == 1)
                    .Select(user => new AdminUserResponsecs
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Country = user.Country,
                        PassCode = user.PassCode,
                        Role = new RoleResponse
                        {
                            Id = user.RoleID,
                            Name = user.Role.Name
                        }


                    })
                    .ToList();
                return userList;
            }
        }

        public List<AdminVendorResponse> GetAllVendorsByAdmin()
        {
            var vendors = _JwtAuthentication.Vendors.ToList();
            if ( vendors.Count <= 0 )
            {
                throw new Exception("Vendors Not Found ");
            }
            else
            {
                List<AdminVendorResponse> vendorList = _JwtAuthentication.Vendors
                    .Include(u => u.user)
                    .ThenInclude(r => r.Role)
                    .Where(vendors => vendors.user.RoleID == 2)
                    .Select(vendor => new AdminVendorResponse
                    {
                        Id = vendor.Id,
                        NoOFDrivers = vendor.NoOFDrivers,
                        NoOfVehicles = vendor.NoOfVehicles,
                        VendorAdress = vendor.VendorAdress,
                        ServiceArea = vendor.ServiceArea,
                        Latitude = vendor.Latitude,
                        Longitude = vendor.Longitude,
                        adminUserResponsecs = new AdminUserResponsecs
                        {
                            Id = vendor.user.Id,
                            Name = vendor.user.Name,
                            Email = vendor.user.Email,
                            Country = vendor.user.Country,
                            PassCode = vendor.user.PassCode,
                            Role = new RoleResponse
                            {
                                Id = vendor.user.RoleID,
                                Name = vendor.user.Role.Name
                            }
                        }
                    })
                    .ToList();
                return vendorList;
            }
        }
    }
}
