using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Audit
{
    public class AuditRequest
    {
        public string MenuCode {  get; set; }
        public string SubMenuCode {  get; set; }
        public string TabCode {  get; set; }
        public string AttributeName {  get; set; }
        public string LabAccessionNo {  get; set; }

    }
}
