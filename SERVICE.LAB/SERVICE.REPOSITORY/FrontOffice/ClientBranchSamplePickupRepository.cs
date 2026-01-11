using System;
using System.Collections.Generic;
using System.Text;
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
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Serilog;
using System.Globalization;
using Service.Model.Sample;
using Service.Model.FrontOffice;
using Dev.IRepository.FrontOffice;
using Microsoft.Data.SqlClient;
namespace Dev.Repository.FrontOffice
{
    public class ClientBranchSamplePickupRepository : IClientBranchSamplePickupRepository
    {
        private IConfiguration _config;
        public ClientBranchSamplePickupRepository(IConfiguration config) { _config = config; }

        public List<ClientBranchSamplePickupResponse> GetClientBranchSamplePickup(ClientBranchSamplePickupRequest RequestItem)
        {
            List<ClientBranchSamplePickupResponse> objresult = new List<ClientBranchSamplePickupResponse>();

            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _SPType = new SqlParameter("SPType", RequestItem.SPType);
                    var _SPTypeNo = new SqlParameter("SPTypeNo", RequestItem.SPTypeNo);
                    var _RiderNo = new SqlParameter("RiderNo", RequestItem.RiderNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.PageIndex);

                    objresult = context.GetClientBranchSamplePickup.FromSqlRaw(
                        "EXEC dbo.Pro_GetClientBranchSamplePickup @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@SPType,@SPTypeNo,@RiderNo,@PageIndex",
                        _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _SPType, _SPTypeNo, _RiderNo, _PageIndex
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupRepository.GetClientBranchSamplePickup",
                    ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        public ClientBranchSamplePickupInsertResponse InsertClientBranchSamplePickup(ClientBranchSamplePickupInsertRequest request)
        {
            ClientBranchSamplePickupInsertResponse objResult = new ClientBranchSamplePickupInsertResponse();

            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SamplePickupNo = new SqlParameter("SamplePickupNo", request.SamplePickupNo);
                    var _SPType = new SqlParameter("SPType", request.SPType);
                    var _SPTypeNo = new SqlParameter("SPTypeNo", request.SPTypeNo);
                    var _SampleCount = new SqlParameter("SampleCount", request.SampleCount);
                    var _PickupDateTime = new SqlParameter("PickupDateTime", request.PickupDateTime);
                    var _RequesterInfo = new SqlParameter("RequesterInfo", request.RequesterInfo);
                    var _Status = new SqlParameter("Status", request.Status);
                    var _VenueNo = new SqlParameter("VenueNo", request.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", request.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", request.UserNo);

                    objResult = context.InsertClientBranchSamplePickup
                        .FromSqlRaw(
                            "EXEC dbo.Pro_InsertCilentBranchSamplePickup @SamplePickupNo, @SPType, @SPTypeNo, @SampleCount, @PickupDateTime, @RequesterInfo, @Status, @VenueNo, @VenueBranchNo, @UserNo",
                            _SamplePickupNo, _SPType, _SPTypeNo, _SampleCount, _PickupDateTime, _RequesterInfo, _Status, _VenueNo, _VenueBranchNo, _UserNo
                        ).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupRepository.InsertClientBranchSamplePickup",
                    ExceptionPriority.Medium, ApplicationType.REPOSITORY, request.VenueNo, request.VenueBranchNo, request.UserNo);
            }

            return objResult;
        }
        public ClientBranchSamplePickupRiderInsertResponse InsertRiderClientBranchSamplePickup(ClientBranchSamplePickupRiderInsertRequest request)
        {
            ClientBranchSamplePickupRiderInsertResponse objResult = new ClientBranchSamplePickupRiderInsertResponse();

            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SamplePickupNo = new SqlParameter("SamplePickupNo", request.SamplePickupNo);
                    var _RiderNo = new SqlParameter("RiderNo", request.RiderNo);
                    var _VenueNo = new SqlParameter("VenueNo", request.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", request.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", request.UserNo);

                    objResult = context.InsertRiderClientBranchSamplePickup
                        .FromSqlRaw(
                            "EXEC dbo.Pro_InsertAssignRiderToSamplePickup @SamplePickupNo, @RiderNo, @VenueNo, @VenueBranchNo, @UserNo",
                            _SamplePickupNo, _RiderNo, _VenueNo, _VenueBranchNo, _UserNo
                        ).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupRepository.InsertRiderClientBranchSamplePickup",
                    ExceptionPriority.Medium, ApplicationType.REPOSITORY, request.VenueNo, request.VenueBranchNo, request.UserNo);
            }

            return objResult;
        }
    }
}
