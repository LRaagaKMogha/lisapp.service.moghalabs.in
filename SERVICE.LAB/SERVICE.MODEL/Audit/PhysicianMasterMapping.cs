using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(TblPhysician))]
    public class TblPhysicianMasterMapping : DtoToTableMapping<TblPhysician>
    {
        public override void SetUp()
        {
            TableName = "tbl_Physician";
            EntityIdProperty = nameof(TblPhysician.PhysicianNo);
            SubMenuCode = "Physician";
        }
    }

    [DtoMapping(typeof(Tblspecialization))]
    public class TblSpecializationMapping : DtoToTableMapping<Tblspecialization>
    {
        public override void SetUp()
        {
            TableName = "tbl_Specialization";
            EntityIdProperty = nameof(Tblspecialization.specializationNo);
            SubMenuCode = "Specialization";
            AddProperty(x => x.specialization, "Description");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<Tblspecialization, object>>>
            {
                x => x.userNo
            };
        }        
    }
}
