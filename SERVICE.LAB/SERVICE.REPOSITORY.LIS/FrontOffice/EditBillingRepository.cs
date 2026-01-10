using Azure;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Dev.Repository
{
    public class EditBillingRepository : IEditBillingRepository
    {
        private IConfiguration _config;
        public EditBillingRepository(IConfiguration config) { _config = config; }


        /// <summary>
        /// Insert Front OfficeMaster
        /// </summary>
        /// <param name="objDTO"></param>
        /// <returns></returns>
        public FrontOffficeResponse InsertEditBilling(FrontOffficeDTO objDTO)
        {
            int PatientVisitNo = 0;
            FrontOffficeResponse result = new FrontOffficeResponse(); ;
            try
            {

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string Password = Guid.NewGuid().ToString("N").Substring(0, 7);
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", objDTO.PatientVisitNo);
                    var _TitleCode = new SqlParameter("TitleCode", objDTO.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO.AgeType?.Length > 0 ? objDTO.AgeType.Substring(0, 1) : "");
                    var _AgeDays = new SqlParameter("ageDays", objDTO.ageDays);
                    var _AgeMonths = new SqlParameter("ageMonths", objDTO.ageMonths);
                    var _AgeYears = new SqlParameter("ageYears", objDTO.ageYears);
                    var _MobileNumber = new SqlParameter("MobileNumber", objDTO.MobileNumber.ValidateEmpty());
                    var _AltMobileNumber = new SqlParameter("AltMobileNumber", objDTO.AltMobileNumber.ValidateEmpty());
                    var _EmailID = new SqlParameter("EmailID", objDTO.EmailID.ValidateEmpty());
                    var _SecondaryEmailID = new SqlParameter("SecondaryEmailID", objDTO.SecondaryEmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO.Pincode.ValidateEmpty());
                    var _SecondaryAddress = new SqlParameter("SecondaryAddress", objDTO.SecondaryAddress.ValidateEmpty());
                    var _URNID = new SqlParameter("URNID", objDTO.URNID.ValidateEmpty());
                    var _URNType = new SqlParameter("URNType", objDTO.URNType.ValidateEmpty());
                    var _RefferralTypeNo = new SqlParameter("RefferralTypeNo", objDTO.RefferralTypeNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", objDTO.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", objDTO.PhysicianNo);
                    var _RiderNo = new SqlParameter("RiderNo", objDTO.RiderNo);
                    var _MarketingNo = new SqlParameter("MarketingNo", objDTO.MarketingNo);
                    var _IsStat = new SqlParameter("IsStat", objDTO.IsStat);
                    var _ClinicalHistory = new SqlParameter("ClinicalHistory", objDTO.ClinicalHistory.ValidateEmpty());
                    var _registeredType = new SqlParameter("registeredType", objDTO.RegisteredType.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserID", objDTO.UserNo.ToString());
                    var _Password = new SqlParameter("Pass", CommonSecurity.EncodePassword(Password, CommonSecurity.GeneratePassword(1)));

                    var _registrationDT = new SqlParameter("registrationDT", objDTO.registrationDT.ValidateEmpty());
                    var _IsEmail = new SqlParameter("IsAutoEmail", objDTO.IsAutoEmail);
                    var _IsSMS = new SqlParameter("IsAutoSMS", objDTO.IsAutoSMS);
                    var _ExternalVisitID = new SqlParameter("ExtenalVisitID", objDTO.ExternalVisitID.ValidateEmpty());
                    var _VaccinationType = new SqlParameter("VaccinationType", objDTO.VaccinationType.ValidateEmpty());
                    var _VaccinationDate = new SqlParameter("VaccinationDate", objDTO.VaccinationDate.ValidateEmpty());

                    var _NURNID = new SqlParameter("NURNID", objDTO.NURNID.ValidateEmpty());
                    var _NURNType = new SqlParameter("NURNType", objDTO.NURNType.ValidateEmpty());
                    var _Deliverymode = new SqlParameter("Deliverymode", objDTO.Deliverymode);
                    var _ExternalVisitIdentity = new SqlParameter("ExternalVisitIdentity", objDTO.ExternalVisitIdentity.ValidateEmpty());
                    var _WardNo = new SqlParameter("WardNo", objDTO.WardNo);
                    var _WardName = new SqlParameter("WardName", objDTO.WardName.ValidateEmpty());
                    //var _IPOPNumber = new SqlParameter("IPOPNumber", objDTO.IPOPNumber.ValidateEmpty());
                    var _maritalStatus = new SqlParameter("maritalStatus", objDTO.maritalStatus);
                    var _HCPatientNo = new SqlParameter("HCPatientNo", objDTO.HCPatientNo);
                    var _isAutoWhatsApp = new SqlParameter("isAutoWhatsApp", objDTO.IsAutoWhatsApp);
                    var _NRICNumber = new SqlParameter("NRICNumber", objDTO.NRICNumber.ValidateEmpty());
                    var _NationalityNo = new SqlParameter("NationalityNo", objDTO.NationalityNo);
                    var _RaceNo = new SqlParameter("RaceNo", objDTO.RaceNo);
                    var _AllergyInfo = new SqlParameter("AllergyInfo", objDTO.AllergyInfo.ValidateEmpty());
                    var _PatientBlock = new SqlParameter("PatientBlock", objDTO.PatientBlock.ValidateEmpty());
                    var _PatientUnitNo = new SqlParameter("PatientUnitNo", objDTO.PatientUnitNo.ValidateEmpty());
                    var _PatientFloor = new SqlParameter("PatientFloor", objDTO.PatientFloor.ValidateEmpty());
                    var _PatientBuilding = new SqlParameter("PatientBuilding", objDTO.PatientBuilding.ValidateEmpty());
                    var _PatientHomeNo = new SqlParameter("PatientHomeNo", objDTO.PatientHomeNo.ValidateEmpty());
                    var _PhysicianNo2 = new SqlParameter("PhysicianNo2", objDTO.PhysicianNo2);
                    var _VipIndication = new SqlParameter("VipIndication", objDTO.IsVipIndication);
                    var _BedNo = new SqlParameter("BedNo", objDTO.BedNo);
                    var _CompanyNo = new SqlParameter("CompanyNo", objDTO.CompanyNo);
                    var _CaseNumber = new SqlParameter("CaseNumber", objDTO.CaseNumber.ValidateEmpty());
                    var _AlternateIdType = new SqlParameter("AlternateIdType", objDTO.AlternateIdType.ValidateEmpty());
                    var _AlternateId = new SqlParameter("AlternateId", objDTO.AlternateId.ValidateEmpty());
                    var _PatientOfficeNumber = new SqlParameter("PatientOfficeNumber", objDTO.PatientOfficeNumber.ValidateEmpty());
                    var _IsPregnant = new SqlParameter("IsPregnant", objDTO.IsPregnant);
                    var _Remarks = new SqlParameter("Remarks", objDTO.Remarks.ValidateEmpty());

                    var editBillingResponse = context.EditBillingPatientDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertEditBilling @PatientNo,@VisitNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@ageDays,@ageMonths,@ageYears,@MobileNumber,@AltMobileNumber," +
                    "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@SecondaryAddress," +
                    "@URNID,@URNType,@RefferralTypeNo,@CustomerNo,@PhysicianNo,@RiderNo,@MarketingNo,@IsStat,@ClinicalHistory,@registeredType,@VenueNo,@VenueBranchNo,@UserID," +
                    "@Pass,@registrationDT,@IsAutoEmail,@IsAutoSMS,@ExtenalVisitID,@VaccinationType,@VaccinationDate,@NURNID,@NURNType,@Deliverymode,@ExternalVisitIdentity," +
                    "@WardNo,@WardName,@maritalStatus,@isAutoWhatsApp,@NRICNumber,@AllergyInfo,@PatientBlock,@PatientUnitNo,@PatientFloor," +
                    "@PatientBuilding,@PatientHomeNo,@PhysicianNo2,@VipIndication,@BedNo,@NationalityNo,@RaceNo,@CompanyNo,@CaseNumber,@AlternateIdType," +
                    "@AlternateId,@PatientOfficeNumber,@IsPregnant,@Remarks",
                    _PatientNo, _VisitNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _AgeDays, _AgeMonths, _AgeYears, _MobileNumber, _AltMobileNumber, _EmailID,
                    _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _SecondaryAddress, _URNID, _URNType, _RefferralTypeNo,
                    _CustomerNo, _PhysicianNo, _RiderNo, _MarketingNo, _IsStat, _ClinicalHistory, _registeredType, _VenueNo, _VenueBranchNo, _UserID, _Password, _registrationDT,
                    _IsEmail, _IsSMS, _ExternalVisitID, _VaccinationType, _VaccinationDate, _NURNID, _NURNType, _Deliverymode,
                    _ExternalVisitIdentity, _WardNo, _WardName, _maritalStatus, _isAutoWhatsApp, _NRICNumber,
                    _AllergyInfo, _PatientBlock, _PatientUnitNo, _PatientFloor, _PatientBuilding, _PatientHomeNo, _PhysicianNo2, _VipIndication, _BedNo, _NationalityNo, _RaceNo,
                    _CompanyNo, _CaseNumber, _AlternateIdType, _AlternateId, _PatientOfficeNumber, _IsPregnant, _Remarks).AsEnumerable().FirstOrDefault();

                    PatientVisitNo = editBillingResponse != null ? editBillingResponse.patientvisitno : 0;
                    //Check if exists or not mobileno and passportno in  Client portal 
                    if (PatientVisitNo == -4)
                    {
                        result.patientvisitno = PatientVisitNo;
                        return result;
                    }
                    XDocument ServiceXML = new XDocument(new XElement("Orders", from Item in objDTO.Orders
                                                                                select
                     new XElement("ServiceList",
                     new XElement("ServiceNo", Item.TestNo),
                     new XElement("ServiceCode", Item.TestCode),
                     new XElement("ServiceShortCode", Item.TestShortCode),
                     new XElement("ServiceName", Item.TestName),
                     new XElement("ServiceType", Item.TestType),
                     new XElement("Rate", Item.Rate),
                     new XElement("Quantity", Item.Quantity),
                     new XElement("Amount", Item.Amount),
                     new XElement("DiscountType", Item.DiscountType),
                     new XElement("DiscountAmount", Item.DiscountAmount),
                     new XElement("RateListNo", Item.RateListNo),
                     new XElement("Status", Item.status)
                     )));

                    XDocument PaymentXML = new XDocument(new XElement("PaymentXML", from Item in objDTO.Payments
                                                                                    select
                    new XElement("PaymentList",
                    new XElement("ModeOfPayment", Item.ModeOfPayment),
                    new XElement("Amount", Item.Amount),
                    new XElement("Description", Item.Description),
                    new XElement("ModeOfType", Item.ModeOfType),
                    new XElement("CurrencyNo", Item.CurrencyNo),
                    new XElement("CurrencyRate", Item.CurrencyRate),
                    new XElement("CurrencyAmount", Item.CurrencyAmount)
                    )));

                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _orderxml = new SqlParameter("orderxml", ServiceXML.ToString());
                    var _paymentxml = new SqlParameter("paymentxml", PaymentXML.ToString());
                    var _NetAmount = new SqlParameter("NetAmount", objDTO.NetAmount);
                    var _GrossAmount = new SqlParameter("GrossAmount", objDTO.GrossAmount);
                    var _discountno = new SqlParameter("discountno", objDTO.discountno);
                    var _DiscountAmount = new SqlParameter("DiscountAmount", objDTO.TdiscountAmt);
                    var _DiscountApprovedBy = new SqlParameter("DiscountApprovedBy", objDTO.DiscountApprovedBy);
                    var _DueAmount = new SqlParameter("DueAmount", objDTO.DueAmount);
                    var _CollectedAmount = new SqlParameter("CollectedAmount", objDTO.CollectedAmount);
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _PUserID = new SqlParameter("UserID", objDTO.UserNo.ToString());

                    result = context.EditBillingTransaction.FromSqlRaw(
                   "Execute dbo.Pro_InsertEditBillingOrders @PatientVisitNo,@orderxml,@paymentxml,@NetAmount,@GrossAmount,@discountno,@DiscountAmount," +
                   "@DiscountApprovedBy,@DueAmount,@CollectedAmount,@VenueNo,@VenueBranchNo,@UserID",
                   _PatientVisitNo, _orderxml, _paymentxml, _NetAmount, _GrossAmount, _discountno, _DiscountAmount, _DiscountApprovedBy,
                   _DueAmount, _CollectedAmount, _PVenueNo, _PVenueBranchNo, _PUserID).AsEnumerable().FirstOrDefault();

                    // PushMessage(result.patientvisitno, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo, Password);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _PUserID = new SqlParameter("UserNo", objDTO.UserNo.ToString());
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    context.FrontOffficeReset.FromSqlRaw(
                         "Execute dbo.pro_ResetRegistration @PatientVisitNo,@UserNo,@VenueNo,@VenueBranchNo",
                         _PatientVisitNo, _PUserID, _PVenueNo, _PVenueBranchNo).FirstOrDefault();
                }

            }
            return result;
        }

        /// <summary>
        /// Get Patient Details
        /// </summary>
        /// <returns></returns>
        public GetEditPatientDetailsFinalResponse GetEditPatientDetails(long visitNo, int VenueNo, int VenueBranchNo)
        {
            List<GetEditPatientDetailsResponse> objresult = new List<GetEditPatientDetailsResponse>();
            GetEditPatientDetailsFinalResponse finalResponse = new GetEditPatientDetailsFinalResponse();
            EditBillServiceDetails editBillService = new EditBillServiceDetails();
            List<EditBillServiceDetails> lstEditBillService = new List<EditBillServiceDetails>();
            GetEditBillPaymentDetails getEditBillPaymentDetails = new GetEditBillPaymentDetails();
            List<GetEditBillPaymentDetails> lstGetEditBillPaymentDetails = new List<GetEditBillPaymentDetails>();

            try
            {

                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _VisitNo = new SqlParameter("VisitNo", visitNo);


                    var exists = context.GetBillInvoiceExists.FromSqlRaw(
                        "Execute dbo.Pro_CheckBillExistsInInvoice @VenueNo,@VenueBranchNo,@VisitNo",
                     _VenueNo, _VenueBranchNo, _VisitNo).ToList();

                    finalResponse.IsExistsInvoice = exists.FirstOrDefault().IsExists;

                    if(finalResponse.IsExistsInvoice) { return finalResponse; }

                    var response = context.GetEditBillingPatientDetailsDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetEditBillingPatientDetails @VenueNo,@VenueBranchNo,@VisitNo",
                     _VenueNo, _VenueBranchNo, _VisitNo).ToList();

                    objresult = response;

                    string patientDetails = JsonConvert.SerializeObject(objresult.FirstOrDefault());
                    finalResponse = JsonConvert.DeserializeObject<GetEditPatientDetailsFinalResponse>(patientDetails);

                    foreach (var patientdetail in response)
                    {
                        if (patientdetail != null && patientdetail.ServiceNo > 0)
                        {
                            if (!lstEditBillService.Exists(x => x.Serviceno == patientdetail.ServiceNo && x.servicetype == patientdetail.ServiceType))
                            {
                                editBillService = new EditBillServiceDetails();
                                editBillService.servicename = patientdetail.ServiceName;
                                editBillService.Serviceno = patientdetail.ServiceNo;
                                editBillService.servicecode = patientdetail.ServiceCode;
                                editBillService.discounttype = patientdetail.DiscountType;
                                editBillService.discount = patientdetail.DiscountAmount;
                                editBillService.Quantity = patientdetail.Quantity;
                                editBillService.Rate = patientdetail.Rate;
                                editBillService.servicetype = patientdetail.ServiceType;
                                editBillService.Amount = patientdetail.Amount;
                                editBillService.isAmountEditable = false;
                                editBillService.Netamount = patientdetail.SNetAmount.GetValueOrDefault();
                                editBillService.rateListNo = 0;
                                editBillService.Status = patientdetail.Status;
                                editBillService.OrderListStatus = patientdetail.OrderListStatus;
                                editBillService.IsEdit = patientdetail.IsEdit;
                                editBillService.CancelledFlag = patientdetail.CancelledFlag;
                                editBillService.serviceCodeNo = patientdetail.serviceCodeNo;
                                editBillService.isIncludeInstruction = patientdetail.isIncludeInstruction;
                                editBillService.includeInstruction = patientdetail.includeInstruction;

                                lstEditBillService.Add(editBillService);
                            }
                        }                        
                    }
                    finalResponse.servicelist = lstEditBillService;

                    var paymentResponse = context.GetEditBillPaymentDetailsDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetEditPatientPaymentDetails @VenueNo,@VenueBranchNo,@VisitNo",
                     _VenueNo, _VenueBranchNo, _VisitNo).ToList();

                    lstGetEditBillPaymentDetails = paymentResponse;
                    finalResponse.payments = lstGetEditBillPaymentDetails;

                    

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return finalResponse;
        }

        public dynamic ValidatePTTTest(int ServiceNo, string ServiceType, int VisitNo, int VenueNo, int VenueBranchNo)
        {
            int objresult = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ServiceNo = new SqlParameter("ServiceNo", ServiceNo);
                    var _ServiceType = new SqlParameter("ServiceType", ServiceType);
                    var _VisitNo = new SqlParameter("VisitNo", VisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var finalResult = context.ValidatePTTTestDTO.FromSqlRaw(
                        "Execute dbo.pro_ValidatePTTTest @VenueNo,@VenueBranchNo,@ServiceNo,@ServiceType,@VisitNo",
                    _VenueNo, _VenueBranchNo, _ServiceNo, _ServiceType, _VisitNo).ToList();

                    objresult = finalResult.FirstOrDefault().status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "EditBillingRepository.ValidatePTTTest/ServiceNo/ServiceType/VisitNo - " + ServiceNo + "/" + ServiceType + "/" + VisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

    }
}
