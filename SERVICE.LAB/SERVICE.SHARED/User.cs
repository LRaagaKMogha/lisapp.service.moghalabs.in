using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
     public record User(
         bool IsAdmin,
         bool IsSuperAdmin, 
         int VenueNo,
         int VenueBranchNo,
         int UserNo,
         string UserName,
         bool IsProvisional,
         List<string>? Roles = null
     );
   
}