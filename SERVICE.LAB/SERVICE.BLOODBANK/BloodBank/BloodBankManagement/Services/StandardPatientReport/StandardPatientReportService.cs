using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankRegistrations;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.StandardPatinetReport
{
    public class StandardPatientReportService : IStandardPatientReportService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IMapper mapper;
        protected readonly IConfiguration Configuration;

        public StandardPatientReportService(BloodBankDataContext dataContext, IMapper mapper, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.Configuration = configuration;
        }

        #region Standard Patient Report       
        public async Task<ErrorOr<List<Models.StandardPatinetReport>>> GetStandardPatientReport(Contracts.FetchPatientRequest request)
        {
            List<Models.StandardPatinetReport> lst = new List<Models.StandardPatinetReport>();
            List<Contracts.StandardPatinetReport> lstOutput = new List<Contracts.StandardPatinetReport>();
            using (var context = new BloodBankDataContext(Configuration))
            {
                var _registrationid = new SqlParameter("RegistrationID", request.RegistrationID);
                var _venueno = new SqlParameter("VenueNo", request.VenueNo);
                var _venuebranchno = new SqlParameter("VenueBranchNo", request.VenueBranchNo);
                var _masterNo = new SqlParameter("MasterNo", request.MasterNo);
                var _pageIndex = new SqlParameter("PageIndex", request.PageIndex);
                var _nRICNo = new SqlParameter("NRICNo", request.NRICNo);
                var _type = new SqlParameter("Type", request.Type);
                var _fromdate = new SqlParameter("FromDate", request.FromDate);
                var _todate = new SqlParameter("ToDate", request.ToDate);
                var _pvNo = new SqlParameter("PatientVisitNo", request.PatientVisitNo);
                var _eLab = new SqlParameter("IseLab", request.eLab);
                var _wardNo = new SqlParameter("WardNo", request.WardNo);
                var _caseNo = new SqlParameter("CaseNo", request.CaseNo);
                var _customerNo = new SqlParameter("CustomerNo", request.CustomerNo);
                var _physicianNo = new SqlParameter("PhysicianNo", request.PhysicianNo);
                var _testNo = new SqlParameter("TestNo", request.TestNo);

                lstOutput = await Task.Run(() => context.StandardPatientReport.FromSqlRaw(
                "Execute dbo.pro_GetStandardPatientReport @RegistrationID, @VenueNo, @VenueBranchNo, @MasterNo, @PageIndex, @NRICNo, @FromDate, @ToDate, @Type, @PatientVisitNo, " +
                "@IseLab, @WardNo, @CaseNo, @CustomerNo, @PhysicianNo, @TestNo",
                _registrationid, _venueno, _venuebranchno, _masterNo, _pageIndex, _nRICNo, _fromdate, _todate, _type, _pvNo, _eLab, _wardNo, _caseNo, _customerNo, _physicianNo, _testNo).ToList());
                
                if (lstOutput != null && lstOutput.Count > 0)
                {
                    foreach (var lstitem in lstOutput)
                    {
                        Models.StandardPatinetReport objmod = new Models.StandardPatinetReport();
                        objmod.RowNo = lstitem.RowNo;
                        objmod.RegistrationId = lstitem.RegistrationId;
                        objmod.LabAccessionNo = lstitem.LabAccessionNo;
                        objmod.PatientName = lstitem.PatientName;
                        objmod.VisitId = lstitem.VisitId;
                        objmod.NRICNumber = lstitem.NRICNumber;
                        objmod.Gender = lstitem.Gender;
                        objmod.DOB = lstitem.DOB;
                        objmod.RegDTTM = lstitem.RegDTTM;
                        objmod.ProductName = lstitem.ProductName;
                        objmod.TestId = lstitem.TestId;
                        objmod.TestName = lstitem.TestName;
                        objmod.Result = lstitem.Result;
                        objmod.DonorID = lstitem.DonorID;
                        objmod.Status = lstitem.Status;
                        objmod.IsActive = lstitem.IsActive;
                        objmod.ModifiedBy = lstitem.ModifiedBy;
                        objmod.ModifiedDate = lstitem.ModifiedDate;
                        objmod.TotalRecords = lstitem.TotalRecords;
                        objmod.PageIndex = lstitem.PageIndex;
                        objmod.InventoryDonationId = lstitem.InventoryDonationId;
                        objmod.CheckedBy = lstitem.CheckedBy;
                        objmod.ExpirationDateAndTime = lstitem.ExpirationDateAndTime;
                        objmod.CheckedBy = lstitem.CheckedBy;
                        objmod.Volume = lstitem.Volume;
                        objmod.InventoryAboOnLabel = lstitem.InventoryAboOnLabel;
                        objmod.NationalityId = lstitem.NationalityId;
                        objmod.RaceId = lstitem.RaceId;
                        objmod.ResidenceStatusId = lstitem.ResidenceStatusId;
                        objmod.Nationality = lstitem.Nationality;
                        objmod.Race = lstitem.Race;
                        objmod.Residence = lstitem.Residence;
                        objmod.GenderId = lstitem.GenderId;
                        objmod.CompatibilityResults = lstitem.CompatibilityResults;
                        objmod.Remarks = lstitem.Remarks;
                        objmod.CompatibilityValidTill = lstitem.CompatibilityValidTill;
                        objmod.IssuedDateAndTime = lstitem.IssuedDateAndTime;
                        objmod.PatientBloodGroup = lstitem.PatientBloodGroup;
                        objmod.PatientAntibodyScreen = lstitem.PatientAntibodyScreen;
                        objmod.PatientSpecialInstructions = lstitem.PatientSpecialInstructions;
                        objmod.ProductCode = lstitem.ProductCode;
                        objmod.Comments = lstitem.Comments;
                        objmod.PatientDOB = lstitem.PatientDOB;
                        objmod.PatientSex = lstitem.PatientSex;
                        objmod.BarCode = lstitem.BarCode;
                        objmod.TestShortName = lstitem.TestShortName;
                        objmod.IsBarcodeAvail = lstitem.IsBarcodeAvail;
                        objmod.SampleName = lstitem.SampleName;
                        objmod.Tube = lstitem.Tube;
                        objmod.UnitCount = lstitem.UnitCount;
                        objmod.IsRedCell = lstitem.IsRedCell;
                        objmod.BSamInvenId = lstitem.BSamInvenId;
                        objmod.BSamProductId = lstitem.BSamProductId;
                        objmod.UnitAttribute = lstitem.UnitAttribute;
                        objmod.IsUploadAvail = 0;
                        objmod.CaseOrVisitNumber = lstitem.CaseOrVisitNumber;
                        objmod.referraltype = lstitem.referraltype;
                        
                        //check the file avail for the current registration no
                        string Pathinit = this.Configuration.GetValue<string>(Constants.UploadPathInit);
                        if (Pathinit != null && Pathinit != "")
                        {
                            var visitId = objmod.RegistrationId.ToString();
                            var venueNo = request.VenueNo;
                            var venuebNo = request.VenueBranchNo;
                            string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                            string newPath = Path.Combine(Pathinit, folderName);
                            if (Directory.Exists(newPath))
                            {
                                string[] filePaths = Directory.GetFiles(newPath);
                                objmod.IsUploadAvail = filePaths != null && filePaths.Length > 0 ? 1 : 0;
                            }
                        }
                        //
                        objmod.IsTestUploadAvail = 0;
                       
                        //check the file avail for the test current registration no
                        Pathinit = String.Empty;
                        Pathinit = this.Configuration.GetValue<string>(Constants.UploadPathInit);
                        if (Pathinit != null && Pathinit != "")
                        {
                            var visitId = objmod.RegistrationId.ToString();
                            visitId = visitId + "_" + objmod.TestId.ToString();
                            var venueNo = request.VenueNo;
                            var venuebNo = request.VenueBranchNo;
                            string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                            string newPath = Path.Combine(Pathinit, folderName);
                            if (Directory.Exists(newPath))
                            {
                                string[] filePaths = Directory.GetFiles(newPath);
                                objmod.IsTestUploadAvail = filePaths != null && filePaths.Length > 0 ? 1 : 0;
                            }
                        }
                        //
                        objmod.objReportPrint = new GetPatientPrintReport();
                        
                        lst.Add(objmod);
                        if (objmod.IsRedCell == 1 && objmod.UnitCount > 1 && objmod.BSamInvenId == 0 && objmod.BSamProductId == 0)
                        {
                            int totl = objmod.UnitCount;
                            for (int h = 0; h < totl - 1; h++)
                            {
                                Models.StandardPatinetReport objmod1 = new Models.StandardPatinetReport();
                                objmod1.RegistrationId = lstitem.RegistrationId;
                                objmod1.ProductName = String.Empty;
                                objmod1.TestId = lstitem.TestId;
                                objmod1.TestName = lstitem.TestName;
                                objmod1.Result = lstitem.Result;
                                objmod1.DonorID = lstitem.DonorID;
                                objmod1.Status = lstitem.Status;
                                objmod1.IsActive = lstitem.IsActive;
                                objmod1.ModifiedBy = lstitem.ModifiedBy;
                                objmod1.ModifiedDate = lstitem.ModifiedDate;
                                objmod1.UnitAttribute = lstitem.UnitAttribute;
                                lst.Add(objmod1);
                            }
                        }
                    }
                }
            }
            return lst;
        }
        public async Task<ErrorOr<Contracts.UpdateStandardPatientResponse>> UpdateStandardPatientReport(UpdateStandardPatinetReport request)
        {
            List<Contracts.UpdateStandardPatientResponse> lst = new List<Contracts.UpdateStandardPatientResponse>();
            using (var context = new BloodBankDataContext(Configuration))
            {
                var _registrationid = new SqlParameter("RegistrationID", request.RegistrationID);
                var _venueno = new SqlParameter("VenueNo", request.VenueNo);
                var _venuebranchno = new SqlParameter("VenueBranchNo", request.VenueBranchNo);
                var _nRICNo = new SqlParameter("NRICNo", request.NRICNo);
                var _patientName = new SqlParameter("PatientName", request.PatientName);
                var _patientDOB = new SqlParameter("PatientDOB", request.PatientDOB);
                var _nationalityId = new SqlParameter("NationalityId", request.NationalityId);
                var _genderId = new SqlParameter("GenderId", request.GenderId);
                var _raceId = new SqlParameter("RaceId", request.RaceId);
                var _residenceStatusId = new SqlParameter("ResidenceStatusId", request.ResidenceStatusId);
                var _userNo = new SqlParameter("UserNo", request.UserNo);
                var _lastModifiedDateTime = new SqlParameter("LastModifiedDateTime", request.LastModifiedDateTime);
                var _result = new SqlParameter("Result", request.Result);
                var _comments = new SqlParameter("Comments", request.Comments);
                lst = await Task.Run(() => context.UpdateStandardPatientReport.FromSqlRaw(
                    "Execute dbo.pro_UpdateStandardPatientReport @RegistrationID,@VenueNo,@VenueBranchNo,@NRICNo,@PatientName,@PatientDOB,@GenderId,@UserNo,@NationalityId,@RaceId,@ResidenceStatusId,@Result,@Comments",
                      _registrationid, _venueno, _venuebranchno, _nRICNo, _patientName, _patientDOB, _genderId, _userNo, _nationalityId, _raceId, _residenceStatusId, _result, _comments).ToList());
            }
            return lst.FirstOrDefault();
        }
        public async Task<ErrorOr<GetPatientPrintReport>> GetStandardPatientReportPrint(Contracts.FetchPatientReportRequest request)
        {
            List<GetPatientPrintReport> lst = new List<GetPatientPrintReport>();
            using (var context = new BloodBankDataContext(Configuration))
            {
                var _registrationid = new SqlParameter("RegistrationID", request.RegistrationID);
                var _venueno = new SqlParameter("VenueNo", request.VenueNo);
                var _venuebranchno = new SqlParameter("VenueBranchNo", request.VenueBranchNo);
                var _testNo = new SqlParameter("TestNo", request.TestNo);
                lst = await Task.Run(() => context.PatientReportPrint.FromSqlRaw(
                    "Execute dbo.pro_GetStandardPatientReportPrint @RegistrationID,@VenueNo,@VenueBranchNo,@TestNo",
                      _registrationid, _venueno, _venuebranchno, _testNo).ToList());
            }
            return lst.FirstOrDefault();
        }
        #endregion
    }
}