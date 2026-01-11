using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class UserNavDTO
    {
        public Int64 Row_Num { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleIcon { get; set; }
        public string ModuleURL { get; set; }
        public int MenuNo { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string MenuURL { get; set; }
        public string MenuCode { get; set;  }
    }
    
    public class NavDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set;  }
        public BadgeDTO Badge { get; set; } 
        public List<MenuChildDTO> Children { get; set; }
    }
    public  class BadgeDTO
    {
        public string Variant { get; set; }
        public string Text { get; set; }
    }

    public  class MenuChildDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }

    }
    public class UserMenuCodeDTO
    {
        public string MenuCode { get; set; }
        public int MenuNo { get; set; }
        public string RoleName { get; set; }
    }
    public class UserRoleNameDTO
    {
        public string RoleName { get; set; }
    }
} 
