using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public class WrapperInput<T>
    {
        public List<T> Details { get; set; } = new List<T>();
        public Dictionary<int, List<T>> InputCollections { get; set; }
    }
    public class AuditLogEntry
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }  
        public string OldValue {  get; set; }
        public string NewValue { get; set; }
        public string MetadataJson { get; set; }
        public string ParentMenuId {  get; set; }
        public string MenuId {  get; set; }
        public DateTime ModifiedOn {  get; set; }
        public Int64 ModifiedBy {  get; set; }
        public string ModifiedByUserName { get; set; }  
        public string UserAction { get; set; }
        public string ContextId { get; set; }   
        public string VisitNo {  get; set; }
        public string LabAccessionNo {  get; set; }
        public string SubMenuCode {  get; set; }
        public AuditLogEntry() { }
    }
}
