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
using System.IO;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class PhysicianRepository: IPhysicianRepository
    {
        private IConfiguration _config;
        public PhysicianRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Physician Details
        /// </summary>
        /// <returns></returns>
        public List<PhysicianDetailsResponse> GetPhysicianDetails(GetCommonMasterRequest masterRequest)
        {
            List<PhysicianDetailsResponse> objresult = new List<PhysicianDetailsResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", masterRequest.venuebranchno);
                    var _PageIndex = new SqlParameter("pageIndex", masterRequest.pageIndex);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", masterRequest.masterNo);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", masterRequest.viewvenuebranchno);

                    objresult = context.GetPhysicianDetailsDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetPhysicianDetails @VenueNo, @VenueBranchNo, @pageIndex, @PhysicianNo,@viewvenuebranchno",
                    _VenueNo, _VenueBranchNo, _PageIndex, _PhysicianNo, _viewvenuebranchno).ToList();
                }                                               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.GetPhysicianDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest?.venueno, masterRequest?.venuebranchno, 0);
            }
            return objresult;
        }
        ///
        public int SavePhysicianDetaile(TblPhysician Physicianitem)
        {
            int result = 0;
            int venueno = Physicianitem?.VenueNo ?? 0;
            int venuebno = Physicianitem?.VenueBranchNo ?? 0;
            int userno = Physicianitem?.CreatedBy ?? 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string physicianDetails = commonUtility.ToXML(Physicianitem);
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {                                                          
                    var _PhysicianDetails = new SqlParameter("physicianDetails", physicianDetails);
                    var dbResponse = context.SavePhysicianDetaile.FromSqlRaw(
                    "Execute dbo.Pro_InsertPhysicianMaster @physicianDetails",
                    _PhysicianDetails).AsEnumerable().FirstOrDefault();
                    
                    result = dbResponse?.physicianNo?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.SavePhysicianDetaile", ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebno, userno);
            }
            return result;
        }

        /// <summary>
        /// Insert Physician Details
        /// </summary>
        /// <param name="Physicianitem"></param>
        /// <returns></returns>
        public int InsertPhysicianDetails(TblPhysician Physicianitem)
        {
            int result = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (Physicianitem.PhysicianNo > 0)
                    {
                        Physicianitem.ModifiedOn = DateTime.Now;
                        Physicianitem.ModifiedBy = Physicianitem.CreatedBy;
                        Physicianitem.CustomerNo = 0;
                        context.Entry(Physicianitem).State = EntityState.Modified;
                    }
                    else
                    {
                        Physicianitem.CreatedOn = DateTime.Now;
                        Physicianitem.CreatedBy = Physicianitem.CreatedBy;
                        Physicianitem.CustomerNo = 0;
                        context.TblPhysician.Add(Physicianitem);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.InsertPhysicianDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, Physicianitem.VenueNo, Physicianitem.VenueBranchNo, 0);
            }
            return result;
        }

        public int PhysicianMerging(List<TblPhysician> mergingPhysicians, int VenueNo, int VenueBranchNo, int UserID, int physicianNo)
        {
            int result = 0;
            
            PhysicianNo physician = new PhysicianNo();
            List<PhysicianNo> physicianList = new List<PhysicianNo>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    var lstphysicians = mergingPhysicians?.Select(x => x.PhysicianNo)?.ToList();
                   
                    if(lstphysicians != null)
                    foreach(var phy in lstphysicians)
                    {
                        physician = new PhysicianNo();
                        physician.physicianNo = phy;
                        physicianList.Add(physician);
                    }                   

                    string physicianDetails = commonUtility.ToXML(physicianList);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _UserID = new SqlParameter("CreatedBy", UserID);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", physicianNo);
                    var _PhysicianDetails = new SqlParameter("PhysicianDetailsXML", physicianDetails);
                    
                    var dbResponse = context.PhysicianMerging.FromSqlRaw(
                    "Execute dbo.Pro_PhysicianMerging @VenueNo,@VenueBranchNo,@CreatedBy,@PhysicianNo,@PhysicianDetailsXML",
                    _VenueNo, _VenueBranchNo, _UserID, _PhysicianNo, _PhysicianDetails).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.InsertSubClientMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, UserID);
            }
            return result;
        }

        public int PhysicianHaveVisits(TblPhysician tblPhysician)
        {
            int iOutput = 0;
            try
            {

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", tblPhysician.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", tblPhysician.VenueBranchNo);                    
                    var _PhysicianNo = new SqlParameter("PhysicianNo", tblPhysician.PhysicianNo);

                    var objresult = context.PhysicianHaveVisits.FromSqlRaw(
                    "Execute dbo.Pro_PhysicianHaveVisits @VenueNo,@VenueBranchNo,@PhysicianNo",
                    _VenueNo, _VenueBranchNo, _PhysicianNo).ToList();

                    iOutput =  objresult[0]?.status?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.PhysicianHaveVisits", ExceptionPriority.Low, ApplicationType.REPOSITORY, tblPhysician?.VenueNo, tblPhysician?.VenueBranchNo, 0);
            }
            return iOutput;
        }
        public int DocumentUploadDetails(List<DocumentUploadlst> PhysicianDocument, int VenueNo, int UserID, int physicianNo)
        {
            int result = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string physicianDocument = commonUtility.ToXML(PhysicianDocument);
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PhysicianDocument = new SqlParameter("physicianDocument", physicianDocument);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _UserID = new SqlParameter("CreatedBy", UserID);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", physicianNo);

                    var dbResponse = context.DocumentUploadDetails.FromSqlRaw(
                    "Execute dbo.Pro_InsertPhysicianDocument @physicianDocument, @VenueNo, @CreatedBy, @PhysicianNo",
                    _PhysicianDocument, _VenueNo, _UserID, _PhysicianNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.DocumentUploadDetails - " + physicianNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, 0, UserID);
            }
            return result;
        }
        public List<PhysicianDocUploadDetailRes> GetPhysicianDocumentDetails(PhysicianDocUploadReq Req)
        {
            List<PhysicianDocUploadDetailRes> lstOutput = new List<PhysicianDocUploadDetailRes>();
            List<PhysicianDocUploadRes> objresult = new List<PhysicianDocUploadRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _EntityType = new SqlParameter("EntityType", Req?.EntityType);
                    var _EntityNo = new SqlParameter("EntityNo", Req?.EntityNo);
                    var _venueNo = new SqlParameter("venueNo", Req?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req?.venueBranchNo);

                    objresult = context.GetPhysicianDocumentDetails.FromSqlRaw(
                    "Execute dbo.pro_GetEntityDocument @EntityType,@EntityNo,@venueNo,@venueBranchNo",
                    _EntityType, _EntityNo, _venueNo, _VenueBranchNo).ToList();
                    
                    if (objresult != null && objresult.Count > 0)
                    {
                        foreach (var v in objresult)
                        {
                            PhysicianDocUploadDetailRes obj = new PhysicianDocUploadDetailRes();
                            obj.documentNo = v.documentNo;
                            obj.documentType = v.documentType;
                            obj.documentTypeCode = v.documentTypeCode;
                            obj.physicianNo = v.physicianNo;
                            obj.physicianfileUpload = new List<PhysicianFileUpload>();
                            obj.physicianfileUpload = GetPhysicianUploadDetail(Req?.venueNo ?? 0, Req?.venueBranchNo ?? 0, v.physicianNo, v.documentTypeCode);
                            lstOutput.Add(obj);
                        }                         
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.GetPhysicianDocumentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, Req.venueNo, Req.venueBranchNo, 0);
            }
            return lstOutput;
        }
        public List<PhysicianFileUpload> GetPhysicianUploadDetail(int venueNo, int venueBranchNo, int physicianNo,int documentTypeCode)
        {
            List<PhysicianFileUpload> lstresult = new List<PhysicianFileUpload>();
            PhysicianFileUpload result = new PhysicianFileUpload();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            string FilePath = string.Empty;
            try
            {
                objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadPhysicianDoc");
                string Pathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";
                var visitId = documentTypeCode;
                var venueno = venueNo;
                var venuebNo = venueBranchNo;
                var visitno = physicianNo;
                string folderName = venueno + "\\" + venuebNo + "\\" + visitno + "\\" + visitId;
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new PhysicianFileUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            result.ActualFileName = filePaths[f].ToString().Split("$$")[3];
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[4];
                            result.ExternalVisitID = "";
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.GetPhysicianUploadDetail", ExceptionPriority.High, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return lstresult;
        }

        public List<OPDMachineRes> GetMachineTimeDetails(OPDMachineReq Req)
        {
            List<OPDMachineRes> objresult = new List<OPDMachineRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", Req?.VenueNo);
                    var _Venuebno = new SqlParameter("Venuebno", Req?.Venuebno);
                    var _MachineNo = new SqlParameter("MachineNo", Req?.MachineNo);

                    objresult = context.GetOPDMachineRes.FromSqlRaw(
                    "Execute dbo.pro_GetMachineTimelist @VenueNo,@Venuebno,@MachineNo",
                    _VenueNo, _Venuebno, _MachineNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.GetMachineTimeDetails" + Req.MachineNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, (int)Req.VenueNo, (int)Req.Venuebno, (int)Req.MachineNo);
            }
            return objresult;
        }
        public List<OPDPhysicianRes> GetPhysicianOPDDetails(OPDPhysicianReq Req)
        {
            List<OPDPhysicianRes> objresult = new List<OPDPhysicianRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", Req?.VenueNo);
                    var _Venuebno = new SqlParameter("Venuebno", Req?.Venuebno);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", Req?.PhysicianNo);
                    var _PhysicianBranchNo = new SqlParameter("PhysicianBranchNo", Req?.PhysicianBranchNo);

                    objresult = context.GetPhysicianOPDDetails.FromSqlRaw(
                    "Execute dbo.pro_GetPhysicianOPDlist @VenueNo,@Venuebno,@PhysicianNo,@PhysicianBranchNo",
                    _VenueNo, _Venuebno, _PhysicianNo, _PhysicianBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.GetPhysicianOPDDetails - " + Req.PhysicianNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, (int)Req.VenueNo, (int)Req.Venuebno, (int)Req.PhysicianNo);
            }
            return objresult;
        }
        public int OPDPatientDetails(List<OPDPhysicianDetail> opdPhysiciandetail, TblPhysician tblPhysician)
        {
            int result = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string OPDPhysicianitem = commonUtility.ToXML(opdPhysiciandetail);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDPhysicianitem = new SqlParameter("OPDPhysicianitem", OPDPhysicianitem);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", tblPhysician.PhysicianNo);
                    var _VenueNo = new SqlParameter("VenueNo", tblPhysician.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", tblPhysician.VenueBranchNo);
                    var _CreatedBy = new SqlParameter("CreatedBy", tblPhysician.CreatedBy);
                    var _PhysicianBranchNo = new SqlParameter("PhysicianBranchNo", tblPhysician.PhysicianBranchNo);
                    var _ApptCount = new SqlParameter("apptCount", tblPhysician.apptCount);
                    var _OPDTiming = new SqlParameter("OPDTiming", tblPhysician.OPDTiming);
                    var _ApptDuration = new SqlParameter("apptDuration", tblPhysician.apptDuration);
                    var _ApptAmount = new SqlParameter("apptAmount", tblPhysician.apptAmount);
                    var _ApptVIPAmount = new SqlParameter("apptVIPAmount", tblPhysician.apptVIPAmount);
                    var _ApptFollowUpAmount = new SqlParameter("apptFollowUpAmount", tblPhysician.apptFollowUpAmount);
                    var _ApptAmountPercentage = new SqlParameter("apptAmountPercentage", tblPhysician.apptAmtPerc);
                    var _ApptVIPAmountPercentage = new SqlParameter("apptVIPAmountPercentage", tblPhysician.apptVIPAmtPerc);
                    var _ApptFollowUpAmountPercentage = new SqlParameter("apptFollowUpAmountPercentage", tblPhysician.apptFollowUpAmtPerc);


                    var dbResponse = context.OPDPatientDetails.FromSqlRaw(
                    "Execute dbo.Pro_InsertOPDPhysicianMaster @OPDPhysicianitem, @PhysicianNo, @VenueNo, @VenueBranchNo, @CreatedBy," +
                    "@PhysicianBranchNo, @apptCount, @OPDTiming, @apptDuration, @apptAmount, @apptVIPAmount, @apptFollowUpAmount,@apptAmountPercentage,@apptVIPAmountPercentage,@apptFollowUpAmountPercentage",
                    _OPDPhysicianitem, _PhysicianNo, _VenueNo, _VenueBranchNo, _CreatedBy, _PhysicianBranchNo, _ApptCount, _OPDTiming,
                    _ApptDuration, _ApptAmount, _ApptVIPAmount, _ApptFollowUpAmount, _ApptAmountPercentage, _ApptVIPAmountPercentage, _ApptFollowUpAmountPercentage).ToList();

                    result = dbResponse[0]?.status ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianRepository.OPDPatientDetails" + tblPhysician.PhysicianNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, tblPhysician.VenueNo, tblPhysician.VenueBranchNo, tblPhysician.CreatedBy);
            }
            return result;
        }
        public List<PhysicianOrClientCodeResponse> GetLastPhysicianCode(int VenueNo, int VenueBranchNo,string CodeType, string CodeToCheck = null)
        {
            List<PhysicianOrClientCodeResponse> objResult = new List<PhysicianOrClientCodeResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _Venuebranchno = new SqlParameter("Venuebranchno", VenueBranchNo);
                    var _CodeType = new SqlParameter("CodeType", CodeType);
                    var _CodeToCheck = new SqlParameter("CodeToCheck", (object)CodeToCheck ?? DBNull.Value);

                    objResult = context.GetLastPhysicianCode.FromSqlRaw(
                    "Execute dbo.pro_GetPhysicianOrClinetCode @VenueNo,@VenueBranchNo,@CodeType,@CodeToCheck", _VenueNo, _Venuebranchno, _CodeType,_CodeToCheck).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPhysicianOrClientCode", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, 0, 0);
            }
            return objResult;

        }
        public List<consultantdetails> GetConsultant(getconsultant getconsultant)
        {
            List<consultantdetails> objresult = new List<consultantdetails>();
            try
            {

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", getconsultant?.venueNo);
                    var _venuebranchNo = new SqlParameter("venuebranchNo", getconsultant?.venuebranchNo);
                    var _consultantNo = new SqlParameter("consultantNo", getconsultant?.consultantNo);
                    objresult = context.GetConsultant.FromSqlRaw(
                        "Execute dbo.pro_GetConsultant @venueNo,@venuebranchNo,@consultantNo",
                      _venueNo, _venuebranchNo, _consultantNo).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetConsultant", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        public int SaveConsultant(saveConsultant saveConsultant)
        {
            CommonHelper commonUtility = new CommonHelper();
            string saveconsultantXML = commonUtility.ToXML(saveConsultant.consultantdetails);
            int i = 0;
            try
            {

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", saveConsultant?.venueNo);
                    var _venuebranchNo = new SqlParameter("venuebranchNo", saveConsultant?.venuebranchNo);
                    var _userNo = new SqlParameter("userNo", saveConsultant?.userNo);
                    var _saveconsultantXML = new SqlParameter("saveconsultantXML", saveconsultantXML);

                    var lst = context.SaveConsultant.FromSqlRaw(
                        "Execute dbo.pro_SaveConsultant @venueNo,@venuebranchNo,@userNo,@saveconsultantXML",
                      _venueNo, _venuebranchNo, _userNo, _saveconsultantXML).ToList();

                    i = lst[0].consultantNo;
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveConsultant", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return i;
        }
    }
}
