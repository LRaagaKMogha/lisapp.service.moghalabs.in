using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dev.IRepository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(UserResponseEntity response);
        UserClaimsIdentity ValidateToken(string token);
        bool ValidateSession(string token);
        //bool ValidateMenu(string token, string MenuCode);
    }
}
