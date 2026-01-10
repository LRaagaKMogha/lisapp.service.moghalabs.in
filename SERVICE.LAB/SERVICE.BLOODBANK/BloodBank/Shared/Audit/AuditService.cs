using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public class AuditService : IAuditService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User User { get; set; }
        private string ParentMenuId { get; set; } = string.Empty;
        private string ContextId { get; set; } = string.Empty;
        private string MenuId { get; set; } = string.Empty;
        public AuditService(IDbConnection dbConnection, IHttpContextAccessor httpContextAccessor)
        {
            _dbConnection = dbConnection;
            _httpContextAccessor = httpContextAccessor;
            User = httpContextAccessor.HttpContext!.Items["User"] as User;
            if (httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("SelectedMenu", out StringValues menuDetails))
            {
                var items = menuDetails[0] != null ? menuDetails[0].Split(',') : new string[] {"", ""};
                ParentMenuId = items[0];
                if(items.Length > 1)
                {
                    MenuId = items[1];
                }
            }
            if (httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("ContextId", out StringValues contextId))
            {
                var contextIdValue = contextId[0] != null ? contextId[0] : "";
                ContextId = contextIdValue;
            }
        }
        public void LogChanges(List<AuditLogEntry> entries)
        {
            entries.ForEach(x => x.ContextId = string.IsNullOrEmpty(x.ContextId) ? ContextId : x.ContextId);
            var query = @$"INSERT INTO AuditLogs(EntityId, TableName, ColumnName, OldValue, NewValue, MetadataJson, ParentMenuId, MenuId, SubMenuCode, ModifiedBy, ModifiedByUserName, ModifiedOn, ContextId, UserAction, VisitNo, LabAccessionNo) 
                        VALUES (@EntityId, @TableName, @ColumnName, @OldValue, @NewValue, @MetadataJson, '{ParentMenuId}', '{MenuId}', @SubMenuCode, { User.UserNo}, '{User.UserName}', @ModifiedOn, @ContextId, @UserAction, @VisitNo, @LabAccessionNo)";
            try
            {
                _dbConnection.Execute(query, entries);
            }
            catch (Exception ex)
            {

            }
        }

        public Dictionary<string, Dictionary<string, object>> Query(string query, string entityIdProperty, object ids)
        {

           
            try
            {
                var result = _dbConnection.Query(query, ids);
                var group = result
                    .Cast<IDictionary<string, object>>()
                    .GroupBy(row => row[entityIdProperty].ToString());
                var response = group.ToDictionary(
                        group => group.Key,
                        group => group.First().ToDictionary(
                            row => row.Key.ToLower(),
                            row => row.Value
                        )
                );
                return response;
            }
            catch (Exception ex)
            {

            }
            return new Dictionary<string, Dictionary<string, object>>();
        }
    }
}
