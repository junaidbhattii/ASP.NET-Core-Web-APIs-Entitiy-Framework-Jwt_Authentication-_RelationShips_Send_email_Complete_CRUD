using AutoMapper;
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
        private readonly IMapper _mapper;
        public VendorService(JwtAuthentication jwtAuthentication , LatitudeLongitude latitudeLongitude , IMapper mapper)
        {
            _JwtAuthentication = jwtAuthentication;
            _LatitudeLongitude = latitudeLongitude;
            _mapper = mapper;
        }
        public async Task<vendorResponse> VendorRegistration(VendorBodyRequest vendorBodyRequest)
        {
            var vendor = await _JwtAuthentication.Users.FirstOrDefaultAsync(u => u.Email == vendorBodyRequest.Email);
            if (vendor == null)
            {
                //var userRecord = new User
                //{
                //    Email = vendorBodyRequest.Email,
                //    Password = BCrypt.Net.BCrypt.HashPassword( vendorBodyRequest.Password ),
                //    Name = vendorBodyRequest.Name,  
                //    Country = vendorBodyRequest.VendorAdress,
                //    RoleID = 2,
                //};
                var user = _mapper.Map<User>(vendorBodyRequest);
                user.Country = vendorBodyRequest.VendorAdress;
                user.RoleID = 2;
                 await _JwtAuthentication.Users.AddAsync(user);
                var RESULTLAT = _LatitudeLongitude.GetCoordinatesFromAddress(user.Country);
                var Entry = await _JwtAuthentication.SaveChangesAsync();
                if(Entry > 0)
                {
                    //var vendorRecord = new Vendor
                    //{
                    //    NoOFDrivers = vendorBodyRequest.NoOFDrivers,
                    //    NoOfVehicles = vendorBodyRequest.NoOfVehicles,
                    //    ServiceArea = vendorBodyRequest.ServiceArea,
                    //    VendorAdress = vendorBodyRequest.VendorAdress,
                    //    UserId = user.Id,
                    //    Latitude = RESULTLAT.Result.Latitude,
                    //    Longitude = RESULTLAT.Result.Longitude,
                    //};
                    var vendorRecord = _mapper.Map<Vendor>(vendorBodyRequest);
                    vendorRecord.UserId = user.Id;
                    vendorRecord.Latitude = RESULTLAT.Result.Latitude;
                    vendorRecord.Longitude = RESULTLAT.Result.Longitude;
                    await _JwtAuthentication.Vendors.AddAsync(vendorRecord);
                    var EntryVendor = await _JwtAuthentication.SaveChangesAsync();
                    if(EntryVendor > 0)
                    {
                        //var Response = new vendorResponse
                        //{
                        //    NoOFDrivers = vendorRecord.NoOFDrivers,
                        //    NoOfVehicles = vendorRecord.NoOfVehicles,
                        //    VendorAdress = vendorRecord.VendorAdress,
                        //    ServiceArea = vendorRecord.ServiceArea,
                        //    Latitude = vendorRecord.Latitude,
                        //    Longitude = vendorRecord.Longitude,

                        //    userResponce = new UserResponce
                        //    {
                        //        Email = user.Email,
                        //        Name = user.Name,
                        //        Country = user.Country,
                        //        Role = new RoleResponse
                        //        {
                        //            Id = user.RoleID,
                        //            Name = _JwtAuthentication.Roles.FirstOrDefault(r => r.Id == user.RoleID)?.Name
                        //        }
                        //    }
                        //};
                        var Response = _mapper.Map< vendorResponse>(vendorRecord);


                        var role = _JwtAuthentication.Roles.Find(user.RoleID);
                       
                        
                        var roleresponse = _mapper.Map<RoleResponse>(role);
                        Response.user.Role = roleresponse;

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
                    user  = new UserResponce
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
