using Dev.IRepository.External.WhatsAppChatBot;
using DEV.Common;
using Service.Model.EF.External.WhatsAppChatBot;
using Service.Model.External.WhatsAppChatBot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;

namespace Dev.Repository.External.WhatsAppChatBot
{
    public class BranchMasterRepository : IBranchMasterRepository
    {
        private IConfiguration _config;
        public BranchMasterRepository(IConfiguration config) { _config = config; }
        public List<lstBranch> GetBranchList(int pVenueNo, int pVenueBranchNo)
        {
            List<lstBranch> objResponse = new List<lstBranch>();
            
            try
            {
                using (var context = new BranchContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetBranchList.FromSqlRaw(
                           "Execute dbo.pro_CB_GetBranchDetails" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "BranchMasterRepository.GetBranchList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}