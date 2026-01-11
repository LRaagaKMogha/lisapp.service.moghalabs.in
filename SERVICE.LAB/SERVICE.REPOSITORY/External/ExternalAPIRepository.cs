using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Sample;
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
    public class ExternalAPIRepository : IExternalAPIRepository
    {
        private IConfiguration _config;
        public ExternalAPIRepository(IConfiguration config) { _config = config; }
        public ExternalLoginResponse Login(ExternalLogin req)
        {
            ExternalLoginResponse result = new ExternalLoginResponse();
            bool isresult = false;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUser.Where(a => a.LoginName == req.UserName && a.Status == true && a.IsLogin == true && a.IsRider == true).Select(x => new { x.UserName, x.UserNo, x.Password, x.VenueBranchNo, x.VenueNo, x.IsSuperAdmin }).FirstOrDefault();
                    if (userresult != null)
                    {
                        var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                        if (userresult.Password == encodingPassword)
                        {
                            isresult = true;
                            result.VenueNo = userresult.VenueNo;
                            result.VenueBranchNo = userresult.VenueBranchNo;
                            result.UserNo = userresult.UserNo;
                            result.UserName = userresult.UserName;
                            result.Status = 1;
                            result.Message = "Sign In Successfully";
                            result.IsOffline = 0;
                            result.IsPreprintedBarcode = 0;
                        }
                    }
                    if (!isresult)
                    {

                        result.Status = 0;
                        result.Message = "Invalid Credentials";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.Login", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public ExternalCommonResponse LocationUpdate(Location results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            result.Status = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var userresult = context.TblUser.Where(a => a.UserNo == results.UserNo && a.VenueNo == results.VenueNo && a.VenueBranchNo == results.VenueBranchNo).FirstOrDefault();
                    if (userresult != null)
                    {
                        userresult.Latitude = results.Latitude;
                        userresult.Longitude = results.Longitude;
                        context.Entry(userresult).State = EntityState.Modified;
                        context.SaveChanges();
                        result.Status = 1;
                        result.Message = "Location Updated Successfully";
                    }
                    else
                    {
                        result.Message = "Something went wrong please try again";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.LocationUpdate", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalAppointmentResponse LoadAppointment(AppointmentRequest results)
        {
            ExternalAppointmentResponse result = new ExternalAppointmentResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BookingID = new SqlParameter("BookingID", results.BookingID.ValidateEmpty());
                    var _type = new SqlParameter("type", results.type);
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    var _MobileNumber = new SqlParameter("MobileNumber", results.MobileNumber);
                    List<ExternalPatientTempResponse> objresult = context.HCPatientTempResponse.FromSqlRaw(
                        "Execute dbo.Pro_GetHCTransaction @BookingID,@type,@UserNo,@VenueNo,@VenueBranchNo,@MobileNumber",
                    _BookingID, _type, _UserNo, _VenueNo, _VenueBranchNo, _MobileNumber).ToList();

                    if (objresult.Count > 0)
                    {
                        int oldBookingNo = 0;
                        int newBookingNo = 0;
                        result.lstPatientResponse = new List<ExternalPatientResponse>();
                        foreach (var hclist in objresult)
                        {
                            ExternalPatientResponse item = new ExternalPatientResponse();
                            List<LstTestDetail> LstTestDetail = new List<LstTestDetail>();
                            List<LstTestSampleWise> LstTestSampleWise = new List<LstTestSampleWise>();

                            newBookingNo = hclist.BookingNo;
                            var TestItem = objresult.Where(x => x.BookingNo == newBookingNo).Select(x => new { x.TestNo, x.TestName, x.Amount, x.SampleName, x.ContainerName, x.TestType, x.SampleNo, x.TestCode }).ToList();
                            if (newBookingNo != oldBookingNo)
                            {
                                item.BookingNo = hclist.BookingNo;
                                item.BookingId = hclist.BookingId;
                                item.PatientName = hclist.PatientName;
                                item.Age = hclist.Age;
                                item.Gender = hclist.Gender;
                                item.MobileNumber = hclist.MobileNumber;
                                item.Address = hclist.Address;
                                item.Area = hclist.Area;
                                item.Pincode = hclist.Pincode;
                                item.Latitude = hclist.Latitude;
                                item.Longitude = hclist.Longitude;
                                item.TestNames = hclist.TestNames;
                                item.NoofTest = hclist.NoofTest;
                                item.RegisteredDateTime = hclist.RegisteredDateTime;
                                item.PatientNo = hclist.PatientNo;
                                item.PatientVisitNo = hclist.PatientVisitNo;
                                item.CurrentStatusNo = hclist.CurrentStatusNo;
                                item.CurrentStatusName = hclist.CurrentStatusName;
                                item.FromSlot = hclist.FromSlot;
                                item.ToSlot = hclist.ToSlot;
                                item.UserImage = hclist.UserImage;
                                item.TotalTestAmount = hclist.TotalTestAmount;
                                item.PhysicianNo = hclist.PhysicianNo;
                                item.IsStart = hclist.IsStart;
                                item.IsPaid = hclist.IsPaid;
                                item.UserNo = hclist.UserNo;
                                item.VenueNo = hclist.VenueNo;
                                item.VenueBranchNo = hclist.VenueBranchNo;
                                int oldTestNo = 0;
                                int newTestNo = 0;
                                oldBookingNo = hclist.BookingNo;
                                oldTestNo = 0;

                                foreach (var Item in TestItem)
                                {
                                    newTestNo = Item.TestNo;
                                    if (oldTestNo != newTestNo)
                                    {
                                        LstTestDetail objTest = new LstTestDetail()
                                        {
                                            BookingId = hclist.BookingId,
                                            BookingNo = hclist.BookingNo,
                                            TestNo = Item.TestNo,
                                            TestName = Item.TestName,
                                            TestCode = Item.TestCode,
                                            TestType = Item.TestType,
                                            Amount = Item.Amount,
                                            SampleNo = Item.SampleNo,
                                            SampleName = Item.SampleName,
                                            ContainerName = Item.ContainerName
                                        };
                                        LstTestSampleWise objsample = new LstTestSampleWise()
                                        {
                                            BookingId = hclist.BookingId,
                                            BookingNo = hclist.BookingNo,
                                            TestNo = Item.TestNo,
                                            TestName = Item.TestName,
                                            TestCode = Item.TestCode,
                                            TestType = Item.TestType,
                                            Amount = Item.Amount,
                                            SampleNo = Item.SampleNo,
                                            SampleName = Item.SampleName,
                                            ContainerName = Item.ContainerName
                                        };
                                        oldTestNo = newTestNo;
                                        LstTestDetail.Add(objTest);
                                        LstTestSampleWise.Add(objsample);
                                    }
                                    item.NoofTest = LstTestDetail.Count;
                                    item.TotalTestAmount = LstTestDetail.Sum(x => x.Amount);
                                    item.lstTestDetails = LstTestDetail;
                                    item.lstTestSampleWise = LstTestSampleWise;
                                }
                                result.lstPatientResponse.Add(item);
                            }
                        }
                        result.Status = 1;
                        result.Message = "Data Fetched Successfully";
                        result.TotalCountTest = objresult.Count;

                    }
                    else
                    {
                        result.Status = 0;
                        result.Message = "No records found";
                        result.TotalCountTest = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.LoadAppointment", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalCommonResponse UploadPrescription(ExternalPrescription results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            result.Status = 0;
            try
            {
                foreach (var item in results.PrescriptionImglst)
                {
                    if (item.base64.Length > 0)
                    {
                        objAppSettingResponse = new AppSettingResponse();
                        string AppHCPrescriptionPath = "HCPrescriptionPath";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppHCPrescriptionPath);
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.HCPrescriptionPath);
                        path = path + results.VenueNo.ToString() + "/" + results.VenueNo.ToString() + "/" + results.BookingID.ToString() + "/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string imgPath = Path.Combine(path, item.ImageName + "." + item.ImageType);
                        byte[] imageBytes = Convert.FromBase64String(item.base64);
                        File.WriteAllBytes(imgPath, imageBytes);
                        result.Status = 1;
                        result.Message = "Prescription Uploaded Successfully";
                    }
                    else
                    {
                        result.Message = "Invalid File";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UploadPrescription", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }
        public ExternalAddTestResponse AddNewTest(ExternalAddTest results)
        {
            ExternalAddTestResponse result = new ExternalAddTestResponse();
            result.Status = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument TestXML = new XDocument(new XElement("TestXML", from Item in results.lstCartList
                                                                              select
                     new XElement("TestList",
                     new XElement("TestNo", Item.TestNo),
                     new XElement("TestCode", Item.TestCode),
                     new XElement("TestName", Item.TestName),
                     new XElement("TestType", Item.TestType),
                     new XElement("IsFasting", Item.IsFasting),
                      new XElement("Remarks", Item.Remarks)
                     )));

                    var _BookingID = new SqlParameter("BookingID", results.BookingID);
                    var _TestXML = new SqlParameter("TestXML", TestXML.ToString());
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);

                    var output = context.ExternalResult.FromSqlRaw(
                    "Execute dbo.Pro_GetHCInsertTest @BookingID,@TestXML,@UserNo,@VenueNo,@VenueBranchNo",
                    _BookingID, _TestXML, _UserNo, _VenueNo, _VenueBranchNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.result == 1)
                        {
                            result.Status = 1;
                            result.Message = "Test Added Successfully";
                        }
                        else
                        {
                            result.Message = "Failed";
                        }
                    }
                    else
                    {
                        result.Message = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.AddNewTest", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalCommonResponse DeleteServiceTest(ExternalDeleteTestRequest results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            result.Status = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _BookingID = new SqlParameter("BookingID", results.BookingID);
                    var _TestNo = new SqlParameter("TestNo", results.BookingTestNo);
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);

                    var output = context.ExternalDeleteTest.FromSqlRaw(
                    "Execute dbo.Pro_GetHCDeleteTest @BookingID,@TestNo,@UserNo,@VenueNo,@VenueBranchNo",
                    _BookingID, _TestNo, _UserNo, _VenueNo, _VenueBranchNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.result == 1)
                        {
                            result.Status = 1;
                            result.Message = "Test Deleted Successfully";
                        }
                        else
                        {
                            result.Message = "Failed";
                        }
                    }
                    else
                    {
                        result.Message = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.DeleteServiceTest", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalCommonResponse ValidatePrePrintedBarcode(ExternalBarcode results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument BarcodeXML = new XDocument(new XElement("BarcodeXML", from Item in results.lstBarcode
                                                                                    select
                           new XElement("BarcodeList",
                           new XElement("SampleNo", Item.SampleNo),
                           new XElement("BarcodeNo", Item.BarcodeNo)
                           )));

                    var _BookingID = new SqlParameter("BookingID", results.BookingID);
                    var _TestXML = new SqlParameter("BarcodeXML", BarcodeXML.ToString());
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);

                    var output = context.ExternalBarcodeTest.FromSqlRaw(
                    "Execute dbo.Pro_GetHCInsertBarcode @BookingID,@BarcodeXML,@VenueNo,@VenueBranchNo",
                    _BookingID, _TestXML, _VenueNo, _VenueBranchNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.result == 1)
                        {
                            result.Status = 1;
                            result.Message = "Barcode Added Successfully";
                        }
                        else
                        {
                            result.Message = "Failed";
                        }
                    }
                    else
                    {
                        result.Message = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.ValidatePrePrintedBarcode", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }
        public List<ExternalHCResponse> GetHcData(AppointmentRequest results)
        {
            List<ExternalHCResponse> result = new List<ExternalHCResponse>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BookingID = new SqlParameter("BookingID", results.BookingID);
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    result = context.ExternalHCResponse.FromSqlRaw(
                        "Execute dbo.Pro_GetHCPatient @BookingID,@UserNo,@VenueNo,@VenueBranchNo",
                    _BookingID, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.LoadAppointment", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalCommonResponse InsertPayment(ExternalBookingPayment results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            result.Status = 0;
            try
            {
                AppointmentRequest appointmentRequest = new AppointmentRequest();
                appointmentRequest.BookingID = results.BookingID;
                appointmentRequest.VenueNo = results.VenueNo;
                appointmentRequest.VenueBranchNo = results.VenueBranchNo;
                appointmentRequest.UserNo = results.UserNo;
                List<ExternalHCResponse> bookingitem = GetHcData(appointmentRequest);
                int index = 0;
                if (bookingitem.Count > 0)
                {
                    IFrontOfficeRepository _frontOfficeRepository = new FrontOfficeRepository(_config);
                    FrontOffficeDTO objDTO = new FrontOffficeDTO();
                    List<FrontOfficeOrderList> objTestlst = new List<FrontOfficeOrderList>();
                    objDTO.Orders = new List<FrontOfficeOrderList>();
                    int oldTestNo = 0;
                    int newTestNo = 0;
                    foreach (var Item in bookingitem)
                    {
                        newTestNo = Item.ServiceNo;
                        if (oldTestNo != newTestNo)
                        {
                            var testlist = _frontOfficeRepository.GetServiceDetails(Item.ServiceNo, Item.ServiceType, bookingitem[index].CustomerNo, results.VenueNo, results.VenueBranchNo, 0, 0);
                            FrontOfficeOrderList objTest = new FrontOfficeOrderList()
                            {
                                TestNo = Item.ServiceNo,
                                TestName = Item.ServiceName,
                                TestType = Item.ServiceType,
                                Amount = testlist.Amount,
                                RateListNo = testlist.RateListNo
                            };
                            oldTestNo = newTestNo;
                            objTestlst.Add(objTest);
                        }
                        objDTO.Orders = objTestlst;
                    }
                    objDTO.Payments = new List<FrontOfficePayment>();
                    List<FrontOfficePayment> lstpay = new List<FrontOfficePayment>();
                    foreach (var item in results.lstPaymentDetails)
                    {
                        FrontOfficePayment objitem = new FrontOfficePayment();
                        objitem.ModeOfPayment = item.PaymentType;
                        objitem.Amount = item.Amount;
                        objitem.Description = String.Empty;
                        objitem.ModeOfType = item.PaymentType;
                        lstpay.Add(objitem);
                    }
                    objDTO.Payments = lstpay;
                    objDTO.TitleCode = bookingitem[index].TitleCode;
                    objDTO.FirstName = bookingitem[index].FirstName;
                    objDTO.MiddleName = bookingitem[index].MiddleName;
                    objDTO.LastName = bookingitem[index].LastName;
                    objDTO.Age = bookingitem[index].Age;
                    objDTO.AgeType = bookingitem[index].AgeType;
                    objDTO.DOB = bookingitem[index].DOB;
                    objDTO.Gender = bookingitem[index].Gender;
                    objDTO.MobileNumber = bookingitem[index].MobileNumber;
                    objDTO.EmailID = bookingitem[index].EmailID;
                    objDTO.SecondaryEmailID = bookingitem[index].SecondaryEmailID;
                    objDTO.Address = bookingitem[index].Address;
                    objDTO.CountryNo = bookingitem[index].CountryNo;
                    objDTO.StateNo = bookingitem[index].StateNo;
                    objDTO.CityNo = bookingitem[index].CityNo;
                    objDTO.AreaName = bookingitem[index].AreaName;
                    objDTO.Pincode = bookingitem[index].Pincode;
                    objDTO.RefferralTypeNo = bookingitem[index].RefferralTypeNo;
                    objDTO.CustomerNo = bookingitem[index].CustomerNo;
                    objDTO.PhysicianNo = bookingitem[index].PhysicianNo;
                    objDTO.RiderNo = bookingitem[index].RiderNo;
                    objDTO.VenueNo = bookingitem[index].VenueNo;
                    objDTO.VenueBranchNo = bookingitem[index].VenueBranchNo;
                    objDTO.UserNo = results.UserNo;
                    objDTO.registrationDT = DateTime.Now.ToString();
                    objDTO.RegisteredType = "APP";
                    objDTO.ExternalVisitID = results.BookingID;
                    objDTO.GrossAmount = results.GrossAmount;
                    objDTO.NetAmount = results.NetAmount;
                    objDTO.DiscountAmount = results.DiscountAmount;
                    objDTO.CollectedAmount = results.CollectedAmount;
                    var data = _frontOfficeRepository.InsertFrontOfficeMaster(objDTO);
                    if (data.patientvisitno > 0)
                    {
                        IManageSampleRepository _IManageSampleRepository = new ManageSampleRepository(_config);
                        List<CreateManageSampleRequest> objsample = new List<CreateManageSampleRequest>();
                        CreateManageSampleRequest sampleitem = new CreateManageSampleRequest();
                        var itemlist = GetBarcodeSampleDetails(results.BookingID, data.patientvisitno, results.VenueNo, results.VenueBranchNo);
                        foreach (var item in itemlist)
                        {
                            var userresult = bookingitem.Where(a => a.ServiceNo == item.ServiceNo).Select(x => new { x.BarcodeNo }).First();
                            sampleitem.barcodeno = userresult.BarcodeNo;
                            sampleitem.testNo = item.ServiceNo;
                            sampleitem.ispreprinted = true;
                            sampleitem.sampleNo = item.SampleNo;
                            sampleitem.containerNo = item.ContainerNo;
                            sampleitem.visitNo = data.patientvisitno;
                            sampleitem.collectedBy = results.UserNo;
                            sampleitem.venueNo = results.VenueNo;
                            sampleitem.venueBranchNo = results.VenueBranchNo;
                            sampleitem.userNo = results.UserNo;
                            sampleitem.collectedDateTime = DateTime.Now.ToString();
                            sampleitem.isnotgiven = false;
                            sampleitem.ishigtemprature = false;
                            sampleitem.isbarcodenotreq = false;
                            sampleitem.higTempValue = String.Empty;
                            sampleitem.ordersNo = item.OrderNo;
                            sampleitem.orderListNo = item.OrderListNo;
                            objsample.Add(sampleitem);
                        }
                        _IManageSampleRepository.CreateManageSample(objsample);
                        result.Status = 1;
                        result.Message = "Payment processed successfully";
                    }
                }
                else
                {
                    result.Message = "Failed";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.InsertPayment", ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public List<ExternalSampleList> GetBarcodeSampleDetails(string BookingID, long visitNo, int VenueNo, int VenueBranchNo)
        {
            List<ExternalSampleList> objresult = new List<ExternalSampleList>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _VisitNo = new SqlParameter("VisitNo", visitNo);
                    var _BookingID = new SqlParameter("BookingID", BookingID);
                    objresult = context.ExternalSampleList.FromSqlRaw(
                        "Execute dbo.Pro_HCGetPatientDetails @VenueNo,@VenueBranchNo,@VisitNo,@BookingID",
                     _VenueNo, _VenueBranchNo, _VisitNo, _BookingID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.GetBarcodeSampleDetails/visitNo-" + visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public ExternalCommonResponse SignOut(ExternalSignout results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            try
            {
                result.Status = 1;
                result.Message = "You have SignOut Successfully";
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.SignOut", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        public ExternalApiReferralResponse GetReferralDetails(ExternalApiReferralRequest results)
        {
            ExternalApiReferralResponse result = new ExternalApiReferralResponse();
            try
            {
                IMasterRepository masterRepository = new MasterRepository(_config);
                var data = masterRepository.GetCommonMasterList(results.VenueNo, results.VenueBranchNo, results.SearchType);
                if (data.Count > 0)
                {
                    result.lstReferral = new List<CommonMasterDto>();
                    if (String.IsNullOrEmpty(results.SearchKey))
                    {
                        result.lstReferral = data;
                    }
                    else
                    {
                        result.lstReferral = data.Where(a => a.CommonKey == results.SearchKey).ToList();
                    }
                    result.Status = 1;
                    result.Message = "Data Fetched Successfully";
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Failed";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.GetReferralDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }

        public ExternalApiServiceResponse GetServiceDetails(ExternalServiceRequest serviceRequest)
        {
            ExternalApiServiceResponse objresult = new ExternalApiServiceResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", serviceRequest.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", serviceRequest.VenueBranchNo.ToString());
                    var _ClientNo = new SqlParameter("ClientNo", serviceRequest.ClientNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", serviceRequest.PhysicianNo);
                    var _PageIndex = new SqlParameter("PageIndex", serviceRequest.PageIndex);
                    var _ServiceName = new SqlParameter("ServiceName", serviceRequest.ServiceName);
                    objresult.lstService = context.ExternalServiceResponse.FromSqlRaw(
                        "Execute dbo.pro_HCServiceDetails @VenueNo,@VenueBranchNo,@ClientNo,@PhysicianNo,@PageIndex,@ServiceName",
                     _VenueNo, _VenueBranchNo, _ClientNo, _PhysicianNo, _PageIndex, _ServiceName).ToList();
                    if (objresult.lstService.Count > 0)
                    {
                        objresult.Status = 1;
                        objresult.Message = "Records fetched Successfully";
                    }
                    else
                    {
                        objresult.Status = 0;
                        objresult.Message = "No Records found";
                    }
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.GetServiceDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, serviceRequest.VenueNo, serviceRequest.VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<ExternalHCAppointment> GetHCAppointsList(CommonFilterRequestDTO RequestItem)
        {
            List<ExternalHCAppointment> objresult = new List<ExternalHCAppointment>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _Registertype = new SqlParameter("Registertype", RequestItem.Registertype);
                    objresult = context.ExternalHCAppointment.FromSqlRaw(
                            "Execute dbo.Pro_GetHCAppointment @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PageIndex,@Registertype",
                        _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _pageIndex, _Registertype).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.GetHCAppointsList", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        public ExternalCommonResponse InsertBooking(ExternalBookingDto objDTO)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            result.Status = 0;
            result.BookingID = "";
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TitleCode = new SqlParameter("TitleCode", objDTO.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO.AgeType.Substring(0, 1));
                    var _MobileNumber = new SqlParameter("MobileNumber", objDTO.MobileNumber.ValidateEmpty());
                    var _AltMobileNumber = new SqlParameter("WhatsappNo", objDTO.WhatsappNo.ValidateEmpty());
                    var _EmailID = new SqlParameter("EmailID", objDTO.EmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO.Pincode.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());
                    var _ReferralTypeNo = new SqlParameter("ReferralTypeNo", objDTO.RefferralTypeNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", objDTO.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", objDTO.PhysicianNo);
                    var _registeredDateTime = new SqlParameter("registeredDateTime", objDTO.registeredDateTime);
                    var _registerform = new SqlParameter("registeredfrom", objDTO.registeredfrom);
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);

                    XDocument TestXML = new XDocument(new XElement("TestXML", from Item in objDTO.lstCartList
                                                                              select
                     new XElement("TestList",
                     new XElement("TestNo", Item.TestNo),
                     new XElement("TestCode", Item.TestCode),
                     new XElement("TestName", Item.TestName),
                     new XElement("TestType", Item.TestType),
                     new XElement("RateListNo", Item.RateListNo),
                     new XElement("Amount", Item.Amount),
                     new XElement("IsFasting", Item.IsFasting),
                      new XElement("Remarks", Item.Remarks)
                     )));

                    var _TestXML = new SqlParameter("TestXML", TestXML.ToString());

                    var _result = context.ExternalBookingRider.FromSqlRaw(
                   "Execute dbo.pro_InsertHCPatient @TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@MobileNumber,@WhatsappNo," +
                   "@EmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@TestXML,@VenueNo,@VenueBranchNo,@userNo,@CustomerNo,@PhysicianNo,@ReferralTypeNo,@registeredDateTime,@registeredfrom,@PatientNo",
                   _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _MobileNumber, _AltMobileNumber, _EmailID,
                    _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _TestXML, _VenueNo, _VenueBranchNo, _UserID, _CustomerNo, _PhysicianNo, _ReferralTypeNo, _registeredDateTime,_registerform, _PatientNo).AsEnumerable().FirstOrDefault();
                    result.Status = 1;
                    result.Message = "Appointment Booked Successfully";
                    result.BookingID = _result?.result;
                    result.PatientNo = _result.PatientNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.InsertBooking - HC Appointment ", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        public ExternalCommonResponse UpdateRiderStatus(ExternalRiderStatusRequest results)
        {
            ExternalCommonResponse objresult = new ExternalCommonResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo.ToString());
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _RiderNo = new SqlParameter("RiderNo", results.RiderNo);
                    var _BookingId = new SqlParameter("BookingId", results.BookingNo);
                    var _IsOffline = new SqlParameter("IsOffline", results.IsOffline);
                    context.ExternalRiderStatus.FromSqlRaw(
                        "Execute dbo.Pro_UpdateRiderStatus @VenueNo,@VenueBranchNo,@UserNo,@RiderNo,@BookingId,@IsOffline",
                     _VenueNo, _VenueBranchNo, _UserNo, _RiderNo, _BookingId, _IsOffline).AsEnumerable().FirstOrDefault();
                    objresult.Status = 1;
                    objresult.Message = "Rider Status Updated Successfully";
                }
            }
            catch (Exception ex)    
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UpdateRiderStatus", ExceptionPriority.Medium, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return objresult;
        }
        public ExternalCommonResponse UpdatePatientStatus(ExternalPatientStatusRequest results)
        {
            ExternalCommonResponse objresult = new ExternalCommonResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo.ToString());
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _Reason = new SqlParameter("Reason", results.Reason);
                    var _BookingID = new SqlParameter("BookingID", results.BookingID);
                    var _BookingStatus = new SqlParameter("BookingStatus", results.BookingStatus);
                    context.ExternalPatientStatus.FromSqlRaw(
                        "Execute dbo.Pro_UpdatePatientStatus @VenueNo,@VenueBranchNo,@UserNo,@Reason,@BookingID,@BookingStatus",
                     _VenueNo, _VenueBranchNo, _UserNo, _Reason, _BookingID, _BookingStatus).FirstOrDefault();
                    objresult.Status = 1;
                    objresult.Message = "Patient Status Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UpdatePatientStatus", ExceptionPriority.Medium, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return objresult;
        }

        public ExternalupdateCommonResponse UpdateHCPatientDetails(UpdateHcpatient results)
        {
            ExternalupdateCommonResponse result = new ExternalupdateCommonResponse();

            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _bioHCPatientNo = new SqlParameter("bioHCPatientNo", results.bioHCPatientNo);
                    var _biotitleCode = new SqlParameter("biotitleCode", results.biotitleCode.ValidateEmpty());
                    var _fName = new SqlParameter("fName", results.fName.ValidateEmpty());
                    var _mName = new SqlParameter("mName ", results.mName.ValidateEmpty());
                    var _lName = new SqlParameter("lName", results.lName.ValidateEmpty());
                    //var _biodOB = new SqlParameter("biodOB", results.biodOB.ValidateEmpty());
                    DateTime parsedDob;
                    if (!DateTime.TryParseExact(results.biodOB, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
                    {
                        throw new Exception("Invalid Date Format. Please use dd-MM-yyyy.");
                    }
                    var _biodOB = new SqlParameter("biodOB", parsedDob);
                    var _biogender = new SqlParameter("biogender", results.biogender.ValidateEmpty());
                    var _bioage = new SqlParameter("bioage", results.bioage);
                    var _bioAgeType = new SqlParameter("bioAgeType", results.bioAgeType.ValidateEmpty());
                    var _biomobileNumber = new SqlParameter("biomobileNumber", results.biomobileNumber.ValidateEmpty());
                    var _bioemailID = new SqlParameter("bioemailID", results.bioemailID.ValidateEmpty());
                    var _bioaddress = new SqlParameter("bioaddress", results.bioaddress.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    var _userNo = new SqlParameter("userNo", results.userNo.ToString());
                    result = context.UpdateHCPatientDetails.FromSqlRaw(
                    "Execute dbo.Pro_UpdateHcPatientBIO @bioHCPatientNo,@biotitleCode,@fName,@mName,@lName,@biodOB,@biogender,@bioage,@bioAgeType,@biomobileNumber,@bioemailID," +
                    "@bioaddress,@VenueNo,@VenueBranchNo,@userNo",
                    _bioHCPatientNo, _biotitleCode, _fName, _mName, _lName, _biodOB, _biogender, _bioage, _bioAgeType, _biomobileNumber, _bioemailID,
                     _bioaddress, _VenueNo, _VenueBranchNo, _userNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UpdateHcpatient", ExceptionPriority.High, ApplicationType.REPOSITORY, (int)results.VenueNo, (int)results.VenueBranchNo, (int)results.userNo);
            }
            return result;
        }

        public UpdateStatusApptDateResponse UpdateStatusApptDate(UpdateStatusApptDateRequest results)
        {
            UpdateStatusApptDateResponse objresult = new UpdateStatusApptDateResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo.ToString());
                    var _UserNo = new SqlParameter("UserNo", results.UserNo);
                    var _HCPatientNo = new SqlParameter("HCPatientNo", results.HCPatientNo);
                    var _AppointDDTM = new SqlParameter("AppointDDTM", results.AppointDDTM);
                    var _IsCancelled = new SqlParameter("IsCancelled", results.IsCancelled);
                    var _ApptDateChangeDesc = new SqlParameter("ApptDateChangeDesc", results.ApptDateChangeDesc);
                    var _CancelledDesc = new SqlParameter("CancelledDesc", results.CancelledDesc);
                    context.UpdateStatusApptDate.FromSqlRaw(
                        "Execute dbo.Pro_UpdateStatusApptDate @HCPatientNo,@AppointDDTM,@IsCancelled,@VenueNo,@VenueBranchNo,@UserNo,@ApptDateChangeDesc,@CancelledDesc",
                     _HCPatientNo,_AppointDDTM,_IsCancelled,_VenueNo,_VenueBranchNo,_UserNo,_ApptDateChangeDesc,_CancelledDesc).AsEnumerable().FirstOrDefault();
                    objresult.Status = 1;
                    objresult.Message = results.AppointDDTM != null && results.AppointDDTM != "" ? "Appointment Date got changed Successfully": "Appointment got cancelled Successfully";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UpdateStatusApptDate", ExceptionPriority.Medium, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return objresult;
        }

        public List<TestSlotBookingDTO> GetSlotBooking(CommonFilterRequestDTO RequestItem)
        {
            List<TestSlotBookingDTO> objresult = new List<TestSlotBookingDTO>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    objresult = context.TestSlotBookingDTO.FromSqlRaw(
                            "Execute dbo.Pro_GetSlotBooking @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PageIndex",
                        _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _pageIndex).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.GetSlotBooking", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        public TestSlotCommonResponse InsertTestSlotBooking(ExternalBookingDTO objDTO)
        {
            TestSlotCommonResponse result = new TestSlotCommonResponse();
            result.Status = 0;
            result.BookingID = "";
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TitleCode = new SqlParameter("TitleCode", objDTO.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO.Age == null ? 0 : objDTO.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO.AgeType.Substring(0, 1));
                    var _MobileNumber = new SqlParameter("MobileNumber", objDTO.MobileNumber.ValidateEmpty());
                    var _AltMobileNumber = new SqlParameter("WhatsappNo", objDTO.WhatsappNo.ValidateEmpty());
                    var _EmailID = new SqlParameter("EmailID", objDTO.EmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO.CountryNo == null ? 0 : objDTO.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO.StateNo == null ? 0 : objDTO.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO.CityNo == null ? 0 : objDTO.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO.Pincode.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());
                    var _ReferralTypeNo = new SqlParameter("ReferralTypeNo", objDTO.RefferralTypeNo == null ? 1 : objDTO.RefferralTypeNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", objDTO.CustomerNo == null ? 0 : objDTO.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", objDTO.PhysicianNo == null ? 0 : objDTO.PhysicianNo);
                    var _registeredDateTime = new SqlParameter("registeredDateTime", objDTO.registeredDateTime.ValidateEmpty());

                    XDocument TestXML = new XDocument(new XElement("TestXML", from Item in objDTO.lstCartList
                                                                              select
                     new XElement("TestList",
                     new XElement("TestNo", Item.TestNo == null ? 0 : Item.TestNo),
                     new XElement("TestCode", Item.TestCode.ValidateEmpty()),
                     new XElement("TestName", Item.TestName.ValidateEmpty()),
                     new XElement("TestType", Item.TestType.ValidateEmpty()),
                     new XElement("RateListNo", Item.RateListNo == null ? 0 : Item.RateListNo),
                     new XElement("Amount", Item.Amount == null ? 0 : Item.Amount),
                      new XElement("Remarks", Item.Remarks.ValidateEmpty())
                     )));

                    var _TestXML = new SqlParameter("TestXML", TestXML.ToString());

                    result = context.InsertTestSlotBooking.FromSqlRaw(
                   "Execute dbo.pro_InsertSlotBooking @TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@MobileNumber,@WhatsappNo," +
                   "@EmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@TestXML,@VenueNo,@VenueBranchNo,@userNo,@CustomerNo,@PhysicianNo,@ReferralTypeNo,@registeredDateTime",
                   _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _MobileNumber, _AltMobileNumber, _EmailID,
                    _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _TestXML, _VenueNo, _VenueBranchNo, _UserID, _CustomerNo, _PhysicianNo, _ReferralTypeNo, _registeredDateTime).AsEnumerable().FirstOrDefault();
                    //result.Status = 1;
                    //result.Message = "Appointment Booked Successfully";
                    //result.BookingID = _result.result;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.InsertTestSlotBooking", ExceptionPriority.High, ApplicationType.REPOSITORY, (int)objDTO.VenueNo, (int)objDTO.VenueBranchNo, (int)objDTO.UserNo);
            }
            return result;
        }

        public SlotBookingupdateCResponse UpdateSlotBooking(UpdateHcpatient results)
        {
            SlotBookingupdateCResponse result = new SlotBookingupdateCResponse();

            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _bioPatientNo = new SqlParameter("bioPatientNo", results.bioHCPatientNo);
                    var _biotitleCode = new SqlParameter("biotitleCode", results.biotitleCode.ValidateEmpty());
                    var _fName = new SqlParameter("fName", results.fName.ValidateEmpty());
                    var _mName = new SqlParameter("mName ", results.mName.ValidateEmpty());
                    var _lName = new SqlParameter("lName", results.lName.ValidateEmpty());
                    var _biodOB = new SqlParameter("biodOB", results.biodOB.ValidateEmpty());
                    var _biogender = new SqlParameter("biogender", results.biogender.ValidateEmpty());
                    var _bioage = new SqlParameter("bioage", results.bioage);
                    var _bioAgeType = new SqlParameter("bioAgeType", results.bioAgeType.ValidateEmpty());
                    var _biomobileNumber = new SqlParameter("biomobileNumber", results.biomobileNumber.ValidateEmpty());
                    var _bioemailID = new SqlParameter("bioemailID", results.bioemailID.ValidateEmpty());
                    var _bioaddress = new SqlParameter("bioaddress", results.bioaddress.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    var _userNo = new SqlParameter("userNo", results.userNo.ToString());
                    result = context.UpdateSlotBooking.FromSqlRaw(
                    "Execute dbo.Pro_UpdateSlotBooking @bioPatientNo,@biotitleCode,@fName,@mName,@lName,@biodOB,@biogender,@bioage,@bioAgeType,@biomobileNumber,@bioemailID," +
                    "@bioaddress,@VenueNo,@VenueBranchNo,@userNo",
                    _bioPatientNo, _biotitleCode, _fName, _mName, _lName, _biodOB, _biogender, _bioage, _bioAgeType, _biomobileNumber, _bioemailID,
                     _bioaddress, _VenueNo, _VenueBranchNo, _userNo).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.UpdateSlotBooking", ExceptionPriority.High, ApplicationType.REPOSITORY, (int)results.VenueNo, (int)results.VenueBranchNo, (int)results.userNo);
            }
            return result;
        }

    }
}