using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Serilog;

namespace Dev.Repository
{
    public class OutSourceAPIRepository : IOutSourceAPIRepository
    {
        private IConfiguration _config;
        public OutSourceAPIRepository(IConfiguration config) { _config = config; }

        public List<OutSourceAPIDTOResponse> GetOutSourceAPIList(OutSourceAPIDTORequest results)
        {
            List<OutSourceAPIDTOResponse> lst = new List<OutSourceAPIDTOResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {                    
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    lst = context.GetOutsourceDetailsAPI.FromSqlRaw(
                         "Execute dbo.pro_GetOutsourceDetails_API @VenueNo,@VenueBranchNo",
                      _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OutSourceAPIRepository.GetOutSourceAPIList", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return lst;
        }

        public int AckOutSourceAPIList(AckOutSourceAPIDTORequest results)
        {
            int OutStatus = 0;
            int ackno = 0;
            ackno = results.APIOutsourceSendNo != null ? Convert.ToInt32(results.APIOutsourceSendNo):0;
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _APIOutsourceSendNo = new SqlParameter("APIOutsourceSendNo", results.APIOutsourceSendNo);
                    var _Ackstatus = new SqlParameter("Ackstatus", results.Ackstatus);
                    var obj = context.AckOutSourceAPIList.FromSqlRaw(
                         "Execute dbo.pro_AckOutsourceDetails_API @APIOutsourceSendNo,@ackstatus",
                      _APIOutsourceSendNo, _Ackstatus).ToList();
                    OutStatus = obj != null && obj.Count>0 ? obj[0].OutStatus : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OutSourceAPIRepository.AckOutSourceAPIList", ExceptionPriority.High, ApplicationType.REPOSITORY, ackno, 0, 0);
            }
            return OutStatus;
        }
    }
}
