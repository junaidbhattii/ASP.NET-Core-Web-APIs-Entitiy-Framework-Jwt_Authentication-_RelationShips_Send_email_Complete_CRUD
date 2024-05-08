using JwtAuthentication_Relations_Authorization.Context;
using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using JwtAuthentication_Relations_Authorization.Models;
using JwtAuthentication_Relations_Authorization.Utils;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication_Relations_Authorization.Services
{
    public class VendorService : IVendorService
    {
        private readonly JwtAuthentication _JwtAuthentication;
        private readonly LatitudeLongitude _LatitudeLongitude;
        public VendorService(JwtAuthentication jwtAuthentication , LatitudeLongitude latitudeLongitude)
        {
            _JwtAuthentication = jwtAuthentication;
            _LatitudeLongitude = latitudeLongitude;
        }
        public vendorResponse VendorRegistration(VendorBodyRequest vendorBodyRequest)
        {
            var vendor = _JwtAuthentication.Users.FirstOrDefault(u => u.Email == vendorBodyRequest.Email);
            if (vendor == null)
            {
                var userRecord = new User
                {
                    Email = vendorBodyRequest.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword( vendorBodyRequest.Password ),
                    Name = vendorBodyRequest.Name,  
                    Country = vendorBodyRequest.VendorAdress,
                    RoleID = 2,
                };
                _JwtAuthentication.Users.Add(userRecord);
                var Entry = _JwtAuthentication.SaveChanges();
                var RESULTLAT = _LatitudeLongitude.GetCoordinatesFromAddress(userRecord.Country);
                if(Entry > 0)
                {
                    var vendorRecord = new Vendor
                    {
                        NoOFDrivers = vendorBodyRequest.NoOFDrivers,
                        NoOfVehicles = vendorBodyRequest.NoOfVehicles,
                        ServiceArea = vendorBodyRequest.ServiceArea,
                        VendorAdress = vendorBodyRequest.VendorAdress,
                        UserId = userRecord.Id,
                        Latitude = RESULTLAT.Result.Latitude,
                        Longitude = RESULTLAT.Result.Longitude,
                    };

                    _JwtAuthentication.Vendors.Add(vendorRecord);
                    var EntryVendor = _JwtAuthentication.SaveChanges();
                    if(EntryVendor > 0)
                    {
                        var Response = new vendorResponse
                        {
                            NoOFDrivers = vendorRecord.NoOFDrivers,
                            NoOfVehicles = vendorRecord.NoOfVehicles,
                            VendorAdress = vendorRecord.VendorAdress,
                            ServiceArea = vendorRecord.ServiceArea,
                            Latitude = vendorRecord.Latitude,
                            Longitude = vendorRecord.Longitude,

                            userResponce = new UserResponce
                            {
                                Email = userRecord.Email,
                                Name = userRecord.Name,
                                Country = userRecord.Country,
                                Role = new RoleResponse
                                {
                                    Id = userRecord.RoleID,
                                    Name = _JwtAuthentication.Roles.FirstOrDefault(r => r.Id == userRecord.RoleID)?.Name
                                }
                            }
                        };
                        return Response;
                    }
                    else
                    {
                        throw new Exception("Vendor Record Not Save");
                    }

                }
                else
                {
                    throw new Exception("User Record Not Save");
                }
            }
            else
            {
                throw new Exception("User Already Found");
            }
        }

        public List<vendorResponse> GetAllVendorRecord()
        {
            var vendors = _JwtAuthentication.Vendors.ToList();
            if(vendors.Count <= 0)
            {
                throw new Exception("User Not Found");
            }
            else
            {
                List<vendorResponse> vendorList = _JwtAuthentication.Vendors
                .Include(v => v.user)
                .ThenInclude(u => u.Role)
                .Select(vendor => new vendorResponse
                {
                    NoOFDrivers = vendor.NoOFDrivers,
                    NoOfVehicles = vendor.NoOfVehicles,
                    VendorAdress = vendor.VendorAdress,
                    ServiceArea = vendor.ServiceArea,
                    Latitude = vendor.Latitude,
                    Longitude = vendor.Longitude,
                    userResponce = new UserResponce
                    {
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

        public vendorResponse UpdateVendorRecord(VendorBodyRequest vendorBodyRequest ,int id)
        {
            var vendor = _JwtAuthentication.Vendors.FirstOrDefault(v => v.user.Email == vendorBodyRequest.Email);
            if (vendor == null)
            {
                throw new Exception(" Vendor Not Found ");
            }
            else
            {
                _JwtAuthentication.Vendors.Update(vendor);
                var Entry = _JwtAuthentication.SaveChanges();
                if(Entry > 0)
                {
                    var Response = new vendorResponse
                    {
                        NoOFDrivers = vendor.NoOFDrivers,
                        NoOfVehicles = vendor.NoOfVehicles,
                        VendorAdress = vendor.VendorAdress,
                        ServiceArea = vendor.ServiceArea,
                    };
                    return Response;
                }
                else
                {
                    throw new Exception("User Not Save Something Went Wrong");
                }
            }
        }
    }
}
