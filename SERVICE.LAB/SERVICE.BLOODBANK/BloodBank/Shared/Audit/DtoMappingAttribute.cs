using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]  
    public class DtoMappingAttribute : Attribute
    {
        public Type DtoType { get; }
        public string Profile { get; }
        public DtoMappingAttribute(Type dtoType)
        {
            DtoType = dtoType;
        }

        public DtoMappingAttribute(Type dtoType, string profile) : this(dtoType)
        {
            Profile = profile;
        }
    }
}
