using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
    public class AppSettings
    {
        public string Secret { get; set; } = "";
        public JWTData JWT { get; set; } = new JWTData();
        
        
    }

    public class JWTData
    {
        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string LoginName { get; set; } = "";
    }
}