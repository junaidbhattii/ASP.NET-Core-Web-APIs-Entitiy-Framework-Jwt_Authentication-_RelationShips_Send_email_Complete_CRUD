using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IUserService
    {
        public UserResponce LoginUser(LoginRequest loginRequest);
        public UserResponce RegisterUser(UserRequestResponse userRequestResponse);
    }
}
