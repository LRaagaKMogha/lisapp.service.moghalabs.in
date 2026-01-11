using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace Service.Model.Audit
{
    [DtoMapping(typeof(InsertRoleReq))]
    public class TblRoleMasterMapping : DtoToTableMapping<InsertRoleReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_Role";
            EntityIdProperty = nameof(InsertRoleReq.RoleId);
            SubMenuCode = "Role Master";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<InsertRoleReq, object>>>
            {
                x => x.UserNo
            };
        }
    }

    [DtoMapping(typeof(ReqRoleMenu), "ROLEMENU")]
    public class TblRoleMunuMapping : DtoToTableMapping<ReqRoleMenu>
    {
        public override void SetUp()
        {
            IsAutoMap = false;
            TableName = "tbl_MenuMapping";
            EntityIdProperty = nameof(ReqRoleMenu.RoleId);
            AddProperty(x => x.usermenuitem, "", null, dto =>
            {
                return dto.usermenuitem.Select(menu =>
                {
                    return new { menuno = menu.menuno, taskno = menu.taskNo };
                }).ToList();
            });
        }
    }
}
