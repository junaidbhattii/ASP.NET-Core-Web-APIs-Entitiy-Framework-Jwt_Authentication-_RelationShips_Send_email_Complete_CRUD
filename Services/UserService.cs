using JwtAuthentication_Relations_Authorization.Context;
using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using JwtAuthentication_Relations_Authorization.Models;
using JwtAuthentication_Relations_Authorization.Utils;

namespace JwtAuthentication_Relations_Authorization.Services
{
    public class UserService : IUserService
    {
        private readonly JwtAuthentication _JwtAuthentication;
        private readonly IConfiguration _Configuration;
        private readonly JWT jwt;
        private readonly IEmailSendService _EmailSendService;
        public UserService(JwtAuthentication jwtAuthentication , IConfiguration iConfiguration , JWT jwt, IEmailSendService emailSendService)
        {
            _JwtAuthentication = jwtAuthentication;     
            _Configuration = iConfiguration;
            this.jwt = jwt;
            _EmailSendService = emailSendService;

        }
        public UserResponce LoginUser(LoginRequest loginRequest)
        {
            var Request  = _JwtAuthentication.Users.FirstOrDefault(r => r.Email ==  loginRequest.Email);
            try
            {
                if (Request != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, Request.Password))
                {
                    string token = jwt.generateToken(Request.Email);
                    _EmailSendService.SendEmailToUser(Request.Email, "Wellcom", "This Email Is Only For Testing So PLZ Ignore:");
                    var Response = new UserResponce
                    {
                        Email = Request.Email,
                        Name = Request.Name,
                        Country = Request.Country,
                        PassCode = Request.PassCode,
                        Token = token,
                        Role = new RoleResponse
                        {
                            Id = Request.Id,
                            Name = _JwtAuthentication.Roles.FirstOrDefault(u => u.Id == Request.RoleID)?.Name,
                        }
                    };
                    return Response;
                }
                else
                {
                    throw new Exception("Password Not Match");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Requested Password and Body Password Not Match", ex);
            }
        }

        public UserResponce RegisterUser(UserRequestResponse userRequestResponse)
        {
            var IsUserExist = _JwtAuthentication.Employees.FirstOrDefault(u => u.Email == userRequestResponse.Email);
            if (IsUserExist == null)
            {
                var user = new User
                {
                    Email = userRequestResponse.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userRequestResponse.Password),
                    Name = userRequestResponse.Name,
                    Country = userRequestResponse.Country,
                    PassCode = userRequestResponse.PassCode,
                    RoleID = 1,
                };
                _JwtAuthentication.Users.Add(user);
                var Entry = _JwtAuthentication.SaveChanges();
                try
                {
                    if (Entry > 0)
                    {
                        string token = jwt.generateToken(user.Email);
                        var Response = new UserResponce
                        {
                            Email = user.Email,
                            Name = user.Name,
                            Country = user.Country,
                            PassCode = user.PassCode,
                            Token = token,
                            Role = new RoleResponse
                            {
                                Id = user.Id,
                            }
                        };
                        return Response;
                    }
                    else
                    {
                        throw new Exception("User Not Save");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("User Not Register, Something Went Wrong", ex);
                }
            }
            else
            {
                throw new Exception("User Already Exist");
            }
        }
    }
}
