using Dev.IRepository.Audit;
using DEV.Common;
using DEV.Model;
using DEV.Model.Audit;
using DEV.Model.EF;
using DEV.Model.EF.Common;
using DEV.Model.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dev.Repository.Audit
{
    public class AuditRepository : IAuditRepository
    {
        private IConfiguration _config;
        public AuditRepository(IConfiguration config) 
        {
            _config = config;
        }
        public async Task<List<AuditLogEntry>> GetAuditDetails(AuditRequest request, UserClaimsIdentity user)
        {
            var response = new List<AuditLogEntry>();
            try
            {
                using (var _dbContext = new AuditContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    Expression<Func<AuditLogEntry, bool>> TabCode = e => e.SubMenuCode == request.TabCode;
                    Expression<Func<AuditLogEntry, bool>> LabAccessionNo = e => e.LabAccessionNo == request.LabAccessionNo;

                    var query = _dbContext.AuditLogs.Where(x => x.ParentMenuId == request.MenuCode && x.MenuId == request.SubMenuCode);
                    if (!string.IsNullOrEmpty(request.TabCode)) query = query.Where(TabCode);
                    if (!string.IsNullOrEmpty(request.LabAccessionNo)) query = query.Where(LabAccessionNo);

                    response = await query.ToListAsync();
                }
            }
            catch(Exception exp)
            {

            }
            
            return response;
        }
    }
}
