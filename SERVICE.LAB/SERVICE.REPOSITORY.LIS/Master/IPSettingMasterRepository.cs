using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class IPSettingMasterRepository : IIPSettingMasterRepository
    {
        private IConfiguration _config;
        public IPSettingMasterRepository(IConfiguration config) { _config = config; }

        public InsertTariffMasterResponse InsertIpSettingMasterDetails(List<IPSettingRequest> ipSettingRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string ipsettingXML = commonUtility.ToXML(ipSettingRequest);
                    var _ipsettingXML = new SqlParameter("ipsettingXML", ipsettingXML);

                    var objresult = context.InsertIPSetting.FromSqlRaw("Execute dbo.pro_InsertIPSettingDetails @ipsettingXML", _ipsettingXML).AsEnumerable().FirstOrDefault();
                    result.resultStatus = objresult?.resultStatus ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "IPSettingMasterRepository.InsertIpSettingMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, ipSettingRequest[0].VenueNo, ipSettingRequest[0].VenueBranchNo, 0);
            }
            return result;
        }        
                    
        public InsertTariffMasterResponse InsertIpSettingMasterDetailsOld(List<IPSettingRequest> ipSettingRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    ipSettingRequest.ForEach(x =>
                    {
                        if (context.TblIPSettings.Where(y => y.RCPNo == x.RCPNo).ToList().Any())
                        {
                            var ipUpdate = context.TblIPSettings.Where(y => y.RCPNo == x.RCPNo).FirstOrDefault();
                            if(ipUpdate != null)
                            {
                                ipUpdate.RCNo = x.RCNo;
                                ipUpdate.RCPNo = x.RCPNo;
                                ipUpdate.PhysicianNo = x.PhysicianNo;
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
                        }
                        else
                        {
                            TblIPSetting iPSetting = new TblIPSetting()
                            {
                                RCNo = x.RCNo,
                                RCPNo = x.RCPNo,
                                IPSettingNo = x.IPSettingNo,
                                PhysicianNo = x.PhysicianNo,
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
                            };
                            context.TblIPSettings.Add(iPSetting);
                            result.resultStatus = context.SaveChanges();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "IPSettingMasterRepository.InsertIpSettingMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, ipSettingRequest[0].VenueNo, ipSettingRequest[0].VenueBranchNo, 0);
            }
            return result;
        }

        public List<GetIPSettingResponse> GetIpSettings(int venueNo, int venueBranchNo, int pageIndex, int IPSettingNo)
        {
            List<GetIPSettingResponse> objresult = new List<GetIPSettingResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _PageIndex = new SqlParameter("PageIndex", pageIndex.ToString());
                    var _IPSettingNo = new SqlParameter("IPSettingNo", IPSettingNo.ToString());

                    objresult = context.GetIPSettingMasterDTO.FromSqlRaw
                        ("Execute dbo.pro_GetIPSettings @venueNo,@venueBranchNo,@pageIndex,@IPSettingNo", _VenueNo, _VenueBranchNo, _PageIndex,_IPSettingNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "IPSettingMasterRepository.GetSubCustomerDetailbyCustomer", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }

        public List<RCPriceList> GetEditIpSettings(int venueNo, int venueBranchNo, int physicianNo, int rcNo)
        {
            List<RCPriceList> objresult = new List<RCPriceList>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var iPSettings = context.TblIPSettings.Where(x => x.PhysicianNo == physicianNo && x.RCNo == rcNo && x.VenueNo == venueNo && x.VenueBranchNo == venueBranchNo).ToList();
                    objresult = context.RCPriceLists.Where(x => x.RCNo == rcNo && x.VenueNo == venueNo && x.VenueBranchNo == venueBranchNo).ToList();
                    objresult.ForEach(x =>
                    {
                        var rclist = iPSettings.Where(y => y.RCPNo == x.RCPNo).ToList();
                        if (rclist.Any())
                        {
                            x.IPPrice = rclist?.FirstOrDefault()?.IPPrice;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "IPSettingMasterRepository.GetEditIpSettings", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
    }
}
