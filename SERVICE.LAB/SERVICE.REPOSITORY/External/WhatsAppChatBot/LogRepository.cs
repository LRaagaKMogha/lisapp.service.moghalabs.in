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
using System.Text;

namespace Dev.Repository.External.WhatsAppChatBot
{
    public class LogRepository : ILogRepository
    {
        private IConfiguration _config;
        public LogRepository(IConfiguration config) { _config = config; }

        public int InsertLog(InsertLogRequest objReq)
        {
            int logRefNo = 0;
            try
            {
                using (var context = new LogContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MobileNo = new SqlParameter("MobileNo", objReq.MobileNo);
                    var _MessageType = new SqlParameter("MessageType", objReq.MessageType);
                    var _Message = new SqlParameter("MessageData", objReq.Message);
                    var _JsonData = new SqlParameter("JsonData", string.IsNullOrEmpty(objReq.JsonObject) ? string.Empty : objReq.JsonObject);
                    var _IsBot = new SqlParameter("IsBot", objReq.IsBotMessage == true ? 1 : 0);
                    var _venueNo = new SqlParameter("VenueNo", objReq.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", objReq.VenueBranchNo);

                    var lst = context.InsertLogInformation.FromSqlRaw(
                       "Execute dbo.pro_CB_InsertLogInformation " +
                       "@MobileNo, @MessageType, @MessageData, @JsonData, @IsBot, @VenueNo, @VenueBranchNo",
                        _MobileNo, _MessageType, _Message, _JsonData, _IsBot, _venueNo, _venueBranchNo).ToList();

                    logRefNo = lst[0].LogRefNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LogRepository.InsertLog", ExceptionPriority.High, ApplicationType.REPOSITORY, objReq.VenueNo, objReq.VenueBranchNo, 0);
            }
            return logRefNo;
        }

        public List<FetchLogResponse> GetLogDetails(FetchLogRequest objLgReq)
        {
            List<FetchLogResponse> objResponse = new List<FetchLogResponse>();

            try
            {
                using (var context = new LogContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MobileNo = new SqlParameter("MobileNo", objLgReq.MobileNo);
                    var _DateFrom = new SqlParameter("DateFrom", string.IsNullOrEmpty(objLgReq.DateFrom) ? string.Empty : objLgReq.DateFrom);
                    var _DateTo = new SqlParameter("DateTo", string.IsNullOrEmpty(objLgReq.DateTo) ? string.Empty : objLgReq.DateTo);
                    var _MessageType = new SqlParameter("MessageType", string.IsNullOrEmpty(objLgReq.MessageType) ? string.Empty : objLgReq.MessageType);
                    var _IsBot = new SqlParameter("IsBot", objLgReq.IsBotMessage == true ? 1 : 0);
                    var _VenueNo = new SqlParameter("VenueNo", objLgReq.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objLgReq.VenueBranchNo);

                    objResponse = context.FetLogInformation.FromSqlRaw(
                           "Execute dbo.pro_CB_GetLogInformation" +
                           " @VenueNo, @VenueBranchNo, @MobileNo, @DateFrom, @DateTo, @MessageType, @IsBot",
                             _VenueNo, _VenueBranchNo, _MobileNo, _DateFrom, _DateTo, _MessageType, _IsBot).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LogRepository.GetLogDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objLgReq.VenueNo, objLgReq.VenueBranchNo, 0);
            }
            return objResponse;
        }
    }
}
