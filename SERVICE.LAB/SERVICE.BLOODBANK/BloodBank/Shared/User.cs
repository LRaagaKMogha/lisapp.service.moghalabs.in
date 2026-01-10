using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
    public class User
    {
        public bool IsAdmin {  get; set; }
        public bool IsSuperAdmin { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public bool IsProvisional { get; set; }
        public List<string>? Roles { get; set; } = null;

        public User()
        {

        }
        public User(bool isAdmin, bool isSuperAdmin, int venueNo, int venueBranchNo, int userNo,string userName, bool isProvisional, List<string> roles = null)
        {
            IsAdmin = isAdmin; 
            IsSuperAdmin = isSuperAdmin;
            VenueNo = venueNo;
            VenueBranchNo = venueBranchNo;
            UserNo = userNo;
            UserName = userName;
            IsProvisional = isProvisional;
            Roles = roles;

        }
    }
   
}