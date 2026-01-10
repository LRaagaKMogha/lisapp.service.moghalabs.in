using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using System.Data;
using Serilog;
using System.Text.RegularExpressions;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using DEV.Model.Sample;
using System.Xml.Linq;

namespace Dev.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private IConfiguration _config;
        public InsuranceRepository(IConfiguration config) { _config = config; }
        public List<NetworkMasterDTO> GetNetworkMasterDetails(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<NetworkMasterDTO> objresult = new List<NetworkMasterDTO>();
            try
            {
                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _PageIndex = new SqlParameter("PageIndex", pageIndex.ToString());
                    objresult = context.GetNetworkMasters.FromSqlRaw
                        ("Execute dbo.Pro_GetInsuranceNetwork @venueNo,@venueBranchNo,@pageIndex", _VenueNo, _VenueBranchNo, _PageIndex).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetNetworkMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        public NetworkMasterDTOResponse InsertNetworkMasterDetails(NetworkMasterRequest objDTO)
        {
            NetworkMasterDTOResponse result = new NetworkMasterDTOResponse();
            try
            {              

                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _NetworkNo = new SqlParameter("NetworkNo", objDTO.NetworkNo);
                    var _PayerName = new SqlParameter("PayerName", objDTO.PayerName);
                    var _PayerCode = new SqlParameter("PayerCode", objDTO.PayerCode.ValidateEmpty());
                    var _FollowupDays = new SqlParameter("FollowupDays", objDTO.FollowupDays);
                    var _ContactPerson = new SqlParameter("ContactPerson", objDTO.ContactPerson.ValidateEmpty());
                    var _Telephone = new SqlParameter("Telephone", objDTO.Telephone.ValidateEmpty());
                    var _Fax = new SqlParameter("Fax", objDTO.Fax.ValidateEmpty());
                    var _Email = new SqlParameter("Email", objDTO.Email.ValidateEmpty());
                    var _Status = new SqlParameter("Status", objDTO.Status);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());

                    result = context.InsertNetworkMaster.FromSqlRaw(
                   "Execute dbo.Pro_InsertInsuranceNetwork @NetworkNo,@PayerName,@PayerCode,@FollowupDays,@ContactPerson,@Telephone,@Fax,@Email,@Status,@VenueNo,@VenueBranchNo,@UserNo",
                   _NetworkNo, _PayerName, _PayerCode, _FollowupDays, _ContactPerson, _Telephone, _Fax, _Email, _Status, _VenueNo, _VenueBranchNo, _UserID).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertNetworkMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }

        public List<CompanyMasterDTO> GetCompanyMasterDetails(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<CompanyMasterDTO> objresult = new List<CompanyMasterDTO>();
            try
            {
                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _PageIndex = new SqlParameter("PageIndex", pageIndex.ToString());
                    objresult = context.GetCompanyMaster.FromSqlRaw
                        ("Execute dbo.Pro_GetInsuranceCompany @venueNo,@venueBranchNo,@pageIndex", _VenueNo, _VenueBranchNo, _PageIndex).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetCompanyMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        public CompanyMasterDTOResponse InsertCompanyMasterDetails(CompanyMasterRequest objDTO)
        {
            CompanyMasterDTOResponse result = new CompanyMasterDTOResponse();
            try
            {
                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _InsuranceCompanyNo = new SqlParameter("InsuranceCompanyNo", objDTO.InsuranceCompanyNo);
                    var _TPAName = new SqlParameter("TPAName", objDTO.TPAName);
                    var _ReceiverCode = new SqlParameter("ReceiverCode", objDTO.ReceiverCode.ValidateEmpty());
                    var _InsuranceCode = new SqlParameter("InsuranceCode", objDTO.InsuranceCode.ValidateEmpty());
                    var _ContactPerson = new SqlParameter("ContactPerson", objDTO.ContactPerson.ValidateEmpty());
                    var _Telephone = new SqlParameter("Telephone", objDTO.Telephone.ValidateEmpty());
                    var _Email = new SqlParameter("Email", objDTO.Email.ValidateEmpty());
                    var _VSTRegNo = new SqlParameter("VSTRegNo", objDTO.VSTRegNo.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO.CountryNo);
                    var _EM = new SqlParameter("EM", objDTO.EM);
                    var _CPT = new SqlParameter("CPT", objDTO.CPT);
                    var _OpeningBalance = new SqlParameter("OpeningBalance", objDTO.OpeningBalance);
                    var _TopupInsurance = new SqlParameter("TopupInsurance", objDTO.TopupInsurance);
                    var _Remarks = new SqlParameter("Remarks", objDTO.Remarks.ValidateEmpty());
                    var _Status = new SqlParameter("Status", objDTO.Status);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());

                    result = context.InsertCompanyMaster.FromSqlRaw(
                   "Execute dbo.Pro_InsertInsuranceCompany @InsuranceCompanyNo,@TPAName,@ReceiverCode,@InsuranceCode,@ContactPerson,@Telephone," +
                   "@Email,@VSTRegNo,@Address,@CountryNo,@EM,@CPT,@OpeningBalance,@TopupInsurance,@Remarks,@Status,@VenueNo,@VenueBranchNo,@UserNo",
                   _InsuranceCompanyNo, _TPAName, _ReceiverCode, _InsuranceCode, _ContactPerson, _Telephone, _Email, _VSTRegNo, _Address, _CountryNo, _EM, _CPT,
                   _OpeningBalance, _TopupInsurance, _Remarks,_Status, _VenueNo, _VenueBranchNo, _UserID).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertNetworkMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        public DeductionDTOResponse InsertDeductionMaster(DeductionMasterDTO objDTO)
        {
            DeductionDTOResponse result = new DeductionDTOResponse();
            try
            {

                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DeductionMasterNo = new SqlParameter("DeductionMasterNo", objDTO.DeductionMasterNo);
                    var _DeductionName = new SqlParameter("DeductionName", objDTO.DeductionName);
                    var _Status = new SqlParameter("Status", objDTO.Status);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());

                    XDocument DeductionXML = new XDocument(new XElement("DeductionXML", from Item in objDTO.Deductionlist
                                                                                    select
                    new XElement("DeductionList",
                    new XElement("DeductionHeaderNo", Item.deductionHeaderNo),
                    new XElement("DeductionType", Item.deductionType),
                    new XElement("DeductionValue", Item.deductionValue),
                    new XElement("DeductionLimit", Item.deductionLimit)
                    )));
                  
                    var _DeductionXML = new SqlParameter("DeductionXML", DeductionXML.ToString());

                    result = context.InsertDeductionMaster.FromSqlRaw(
                   "Execute dbo.Pro_InsertInsuranceDeduction @DeductionMasterNo,@DeductionName,@DeductionXML,@Status,@VenueNo,@VenueBranchNo,@UserNo",
                   _DeductionMasterNo, _DeductionName, _DeductionXML, _Status, _VenueNo, _VenueBranchNo, _UserID).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertDeductionMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        public List<DeductionResponse> GetDeductionMaster(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<DeductionResponse> lstDeductionDTO = new List<DeductionResponse>();
            try
            {
                using (var context = new InsuranceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _PageIndex = new SqlParameter("PageIndex", pageIndex.ToString());
                    var Deductionlist = context.GetDeductionMaster.FromSqlRaw
                        ("Execute dbo.Pro_GetInsuranceDeductionDetails @venueNo,@venueBranchNo,@pageIndex", _VenueNo, _VenueBranchNo, _PageIndex).ToList();

                    lstDeductionDTO = GetDeductionResponse(Deductionlist, venueNo, venueBranchNo);

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDeductionMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return lstDeductionDTO;
        }
        public List<DeductionResponse> GetDeductionResponse(List<Deductionresult> _Deductionresult, int VenueNo, int VenueBranchNo)
        {

            List<DeductionResponse> objresult = new List<DeductionResponse>();
            try
            {
                int oldDeductionMasterNo = 0;
                int newDeductionMasterNo = 0;
                //int OldDeductionHeaderNo = 0;
                int newDeductionHeaderNo = 0;
                foreach (var deduList in _Deductionresult)
                {
                    DeductionResponse Responseitem = new DeductionResponse();
                    List<DeductionDetail> lstDetail = new List<DeductionDetail>();
                    newDeductionMasterNo = deduList.DeductionMasterNo;
                    var DeductionItem = _Deductionresult.Where(x => x.DeductionMasterNo == newDeductionMasterNo).Select(x => new
                    {
                        x.DeductionHeaderNo,
                        x.HeaderName,
                        x.DeductionType,
                        x.DeductionValue,
                        x.DeductionLimit
                    }).ToList();    
                    if (newDeductionMasterNo != oldDeductionMasterNo)
                    {
                        Responseitem.DeductionMasterNo = deduList.DeductionMasterNo;
                        Responseitem.DeductionName = deduList.DeductionName;
                        Responseitem.Status = deduList.Status;
                        oldDeductionMasterNo = deduList.DeductionMasterNo;
                        Responseitem.Sno = deduList.Sno;
                        Responseitem.PageIndex = deduList.PageIndex;
                        Responseitem.TotalRecords = deduList.TotalRecords;
                        int OldDeductionHeaderNo = 0;
                        foreach (var Item in DeductionItem)
                        {
                            newDeductionHeaderNo = (int)Item.DeductionHeaderNo;
                            if (OldDeductionHeaderNo != newDeductionHeaderNo)
                            {
                                DeductionDetail objaudit = new DeductionDetail()
                                {
                                    DeductionHeaderNo = Item.DeductionHeaderNo,
                                    DeductionHeaderName = Item.HeaderName,
                                    DeductionType = Item.DeductionType,
                                    DeductionLimit = Item.DeductionLimit,
                                    DeductionValue = Item.DeductionValue
                                };
                                OldDeductionHeaderNo = newDeductionHeaderNo;
                                lstDetail.Add(objaudit);

                            }
                            Responseitem.Deductionlist = lstDetail;
                        }
                        objresult.Add(Responseitem);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDeductionResponse", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}
