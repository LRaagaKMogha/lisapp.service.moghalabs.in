using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class RCMasterRepository : IRCMasterRepository
    {
        private IConfiguration _config;
        public RCMasterRepository(IConfiguration config) { _config = config; }

        public InsertTariffMasterResponse InsertRCMaster(InsertRCMasterRequest rcRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            CommonHelper commonUtility = new CommonHelper();
            string rcPriceListXML = "";
            if (rcRequest?.rcPriceList != null && rcRequest?.rcPriceList?.Count > 0)
            {
                rcPriceListXML = commonUtility.ToXML(rcRequest?.rcPriceList);
            }

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _rcno = new SqlParameter("rcno", rcRequest?.RCNo);
                    var _rcpricelistXML = new SqlParameter("rcpricelistXML", rcPriceListXML);
                    var _userno = new SqlParameter("userno", rcRequest?.CreatedBy);
                    var _VenueNo = new SqlParameter("venueNo", rcRequest?.VenueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", rcRequest?.VenueBranchNo);
                    var _rcname = new SqlParameter("rcname", rcRequest?.RCName);
                    var _rcstatus = new SqlParameter("rcstatus", rcRequest?.Status);

                    var lst = context.InsertRcMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertRcMaster @rcno,@rcpricelistXML,@userno,@venueNo,@venueBranchNo,@rcname,@rcstatus",
                     _rcno,_rcpricelistXML,_userno,_VenueNo,_venueBranchNo,_rcname,_rcstatus).ToList();
                    result.resultStatus = (lst[0]?.resultStatus)?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterRepository.InsertRCMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, (rcRequest?.VenueNo), (rcRequest?.VenueBranchNo), 0);
            }
            return result;
        }

        public InsertTariffMasterResponse InsertRCMaster1(InsertRCMasterRequest rcRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            CommonHelper commonUtility = new CommonHelper();
            Dictionary<string, string> error = new Dictionary<string, string>();

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var rcExists = context.TblRCs.Where(x => x.RCName == rcRequest.RCName && x.VenueNo == rcRequest.VenueNo && x.VenueBranchNo == rcRequest.VenueBranchNo && x.RCNo != rcRequest.RCNo).ToList();

                    if (rcExists.Any())
                    {
                        error.Add("RCName", "RC Name can not be duplicated");
                        throw new Exception(JsonConvert.SerializeObject(error));
                    }

                    TblRC tblRC = new TblRC
                    {
                        RCNo = rcRequest.RCNo,
                        RCName = rcRequest.RCName,
                        Status = rcRequest.Status,
                        CreatedOn = DateTime.Now,
                        CreatedBy = rcRequest.CreatedBy,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = rcRequest.ModifiedBy,
                        VenueNo = rcRequest.VenueNo,
                        VenueBranchNo = rcRequest.VenueBranchNo
                    };

                    if (tblRC.RCNo != 0)
                    {
                        var rcUpdate = context.TblRCs.Where(y => y.RCNo == rcRequest.RCNo).FirstOrDefault();
                        if (rcUpdate != null)
                        {
                            rcUpdate.RCName = tblRC.RCName;
                            rcUpdate.Status = tblRC.Status;
                            rcUpdate.CreatedOn = tblRC.CreatedOn;
                            rcUpdate.CreatedBy = tblRC.CreatedBy;
                            rcUpdate.ModifiedOn = tblRC.ModifiedOn;
                            rcUpdate.ModifiedBy = tblRC.ModifiedBy;
                            rcUpdate.VenueNo = tblRC.VenueNo;
                            rcUpdate.VenueBranchNo = tblRC.VenueBranchNo;
                            result.resultStatus = context.SaveChanges();
                        }                        
                    }
                    else
                    {
                        context.TblRCs.AddRange(tblRC);
                        result.resultStatus = context.SaveChanges();
                    }

                    List<RCPriceList> rcPriceLists = rcRequest.rcPriceList.Where(y => y.IsEdit == false).Select(x => new RCPriceList
                    {
                        RCPNo = x.RCPNo,
                        RCNo = tblRC.RCNo,
                        DepartmentNo = x.DepartmentNo,
                        ServiceNo = x.ServiceNo,
                        ServiceType = x.ServiceType,
                        MRPPrice = x.MRPPrice,
                        IPPrice = x.IPPrice,
                        IPPercentage = x.IPPercentage,
                        Status = x.Status,
                        CreatedOn = DateTime.Now,
                        CreatedBy = x.CreatedBy,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = x.ModifiedBy,
                        VenueNo = x.VenueNo,
                        VenueBranchNo = x.VenueBranchNo
                    }).ToList();
                    context.RCPriceLists.AddRange(rcPriceLists);
                    result.resultStatus = context.SaveChanges();

                    rcRequest.rcPriceList.Where(y => y.IsEdit == true && y.IsDelete == false).OrderBy(x => x.RCPNo).ToList().ForEach(x =>
                    {
                        var ipUpdate = context.RCPriceLists.Where(y => y.RCPNo == x.RCPNo).FirstOrDefault();
                        if(ipUpdate != null)
                        {
                            ipUpdate.RCNo = x.RCNo;
                            ipUpdate.DepartmentNo = x.DepartmentNo;
                            ipUpdate.ServiceNo = x.ServiceNo;
                            ipUpdate.ServiceType = x.ServiceType;
                            ipUpdate.MRPPrice = x.MRPPrice;
                            ipUpdate.IPPrice = x.IPPrice;
                            ipUpdate.IPPercentage = x.IPPercentage;
                            ipUpdate.Status = x.Status;
                            ipUpdate.CreatedOn = DateTime.Now;
                            ipUpdate.CreatedBy = x.CreatedBy;
                            ipUpdate.ModifiedOn = DateTime.Now;
                            ipUpdate.ModifiedBy = x.ModifiedBy;
                            ipUpdate.VenueNo = x.VenueNo;
                            ipUpdate.VenueBranchNo = x.VenueBranchNo;
                            result.resultStatus = context.SaveChanges();
                        }
                          
                    });                    
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterRepository.InsertIpSettingMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, rcRequest.VenueNo, rcRequest.VenueBranchNo, 0);
            }
            return result;
        }
        public List<RCPriceList> GetEditRCMaster(int venueNo, int venueBranchNo, int rcNo)
        {
            List<RCPriceList> objresult = new List<RCPriceList>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.RCPriceLists.Where(x => x.RCNo == rcNo && x.VenueNo == venueNo && x.VenueBranchNo == venueBranchNo).OrderBy(x => x.RCPNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterRepository.GetEditRCMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        public List<GetRCMasterResponse> GetRCDetails(int venueNo, int venueBranchNo, int pageIndex, int RcNo)
        {
            List<GetRCMasterResponse> objresult = new List<GetRCMasterResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _PageIndex = new SqlParameter("PageIndex", pageIndex.ToString());
                    var _RcNo = new SqlParameter("RcNo", RcNo.ToString());
                    objresult = context.GetRCMasterDTO.FromSqlRaw(
                    "Execute dbo.pro_GetRCDetails @venueNo,@venueBranchNo,@pageIndex,@RcNo", _VenueNo, _VenueBranchNo, _PageIndex, _RcNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterRepository.GetRCDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblRC> GetRCMasterDetails(GetCommonMasterRequest masterRequest)
        {
            List<TblRC> objresult = new List<TblRC>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblRCs.Where(x => x.VenueNo == masterRequest.venueno && x.VenueBranchNo == masterRequest.venuebranchno && x.Status == true).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterRepository.GetRCMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueno, masterRequest.venuebranchno, 0);
            }
            return objresult;
        }
    }
}
