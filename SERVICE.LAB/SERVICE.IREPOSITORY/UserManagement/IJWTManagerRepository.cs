using Service.Model;

namespace Service.IRepository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(UserResponseEntity response);
        UserClaimsIdentity ValidateToken(string token);
        bool ValidateSession(string token);
    }
}
