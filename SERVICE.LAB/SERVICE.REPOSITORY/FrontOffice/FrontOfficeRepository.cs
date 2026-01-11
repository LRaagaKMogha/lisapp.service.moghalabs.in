using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dev.Repository
{
    public class FrontOfficeRepository : IFrontOfficeRepository
    {
        private IConfiguration _config;
        public FrontOfficeRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Country
        /// /// </summary>
        /// <returns></returns>
        public List<TblCountryList> GetCountry(int VenueNo)
        {
            List<TblCountryList> objresult = new List<TblCountryList>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblCountry.Where(x => x.VenueNo == VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCountry", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        /// <summary>
        /// Get State
        /// </summary>
        /// <param name="countryNo"></param>
        /// <returns></returns>
        public List<TblState> GetState(int VenueNo)
        {
            List<TblState> objresult = new List<TblState>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblState.Where(x => x.VenueNo == VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetState", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        /// <summary>
        /// Get City
        /// </summary>
        /// <param name="StateNo"></param>
        /// <returns></returns>
        public List<TblCity> GetCity(int VenueNo)
        {
            List<TblCity> objresult = new List<TblCity>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblCity.Where(x => x.VenueNo == VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCity", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        public GetDetailsByPincode GetDetailsByPincode(int VenueNo, int VenueBranchNo, string PinCode)
        {
            GetDetailsByPincode objresult = new GetDetailsByPincode();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _Pincode = new SqlParameter("Pincode", PinCode);

                    var result = context.GetDetailsByPincodeDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetDetailsByPinCode @VenueNo, @VenueBranchNo, @Pincode",
                     _VenueNo, _VenueBranchNo, _Pincode).ToList();
                    objresult = result?.AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetDetailsByPincode", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        /// <summary>
        /// GetCurrency
        /// </summary>
        /// <returns></returns>
        public List<TblCurrency> GetCurrency(int VenueNo)
        {
            List<TblCurrency> objresult = new List<TblCurrency>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblCurrency.Where(a => a.VenueNo == VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCurrency", ExceptionPriority.Medium, ApplicationType.REPOSITORY, VenueNo, 0, 0);
            }
            return objresult;
        }
        /// <summary>
        /// GetCustomers
        /// </summary>
        /// <returns></returns>
        public List<CustomerList> GetCustomers(int VenueNo, int VenueBranchNo, int UserNo, int IsFranchisee, bool ExcludePostpaid = false, bool ExcludePrepaid = false, bool ExcludeCash = false, bool IsApproval = false, int IsClinical = -1, int clientType = 0, bool IsMapping = false)
        {
            List<CustomerList> objresult = new List<CustomerList>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", UserNo);
                    var _IsFranchisee = new SqlParameter("IsFranchisee", IsFranchisee);
                    var _ExcludePostpaid = new SqlParameter("ExcludePostpaid", ExcludePostpaid == true ? 1 : 0);
                    var _ExcludePrepaid = new SqlParameter("ExcludePrepaid", ExcludePrepaid == true ? 1 : 0);
                    var _ExcludeCash = new SqlParameter("ExcludeCash", ExcludeCash == true ? 1 : 0);
                    var _IsApproval = new SqlParameter("IsApproval", IsApproval == true ? 1 : 0);
                    var _IsClinical = new SqlParameter("IsClinic", IsClinical);
                    var _IsMapping = new SqlParameter("IsMapping", IsMapping);                    
                    var _clientType = new SqlParameter("ClientType", clientType);

                    objresult = context.CustomerList.FromSqlRaw(
                    "Execute dbo.Pro_GetSearchCustomer @VenueNo, @VenueBranchNo, @UserNo, @IsFranchisee, " +
                    "@ExcludePostpaid, @ExcludePrepaid, @ExcludeCash, @IsApproval, @IsClinic, @ClientType, @IsMapping",
                     _VenueNo, _VenueBranchNo, _UserNo, _IsFranchisee, _ExcludePostpaid, _ExcludePrepaid, _ExcludeCash, _IsApproval, _IsClinical, _clientType,_IsMapping).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCustomers", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        /// <summary>
        /// GetCustomers Details
        /// </summary>
        /// <returns></returns>
        public CustomerList GetCustomerDetails(long Customerno, int VenueNo, int VenueBranchNo)
        {
            CustomerList objresult = new CustomerList();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var result = context.CustomerList.FromSqlRaw(
                         "Execute dbo.Pro_GetSearchCustomer @VenueNo,@VenueBranchNo",
                      _VenueNo, _VenueBranchNo).ToList();

                    objresult = result.Where(a => a.customerNo == Customerno).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCustomerDetails/Customerno-" + Customerno, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Customer CurrentBalance Details
        /// </summary>
        /// <returns></returns>
        public CustomerCurrentBalance GetCustomerCurrentBalance(long Customerno, int VenueNo, int VenueBranchNo)
        {
            CustomerCurrentBalance objresult = new CustomerCurrentBalance();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Customerno = new SqlParameter("Customerno", Customerno);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    objresult = context.CustomerCurrentBalance.FromSqlRaw(
                        "Execute dbo.Pro_GetCustomerBalance @Customerno,@VenueNo,@VenueBranchNo",
                     _Customerno, _VenueNo, _VenueBranchNo).AsEnumerable()?.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetCustomerCurrentBalance/Customerno-" + Customerno, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        /// <summary>
        /// GetDiscountMaster
        /// </summary>
        /// <returns></returns>
        public List<TblDiscount> GetDiscountMaster(int VenueNo, int VenueBranchNo)
        {
            List<TblDiscount> objresult = new List<TblDiscount>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    //objresult = context.TblDiscount.Where(a => a.VenueNo == VenueNo
                    //&& a.VenueBranchNo == VenueBranchNo).ToList();
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);                                        

                    objresult = context.GetDiscountMaster.FromSqlRaw(
                         "Execute dbo.pro_GetDiscountMaster_All @VenueNo,@VenueBranchNo",
                      _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetDiscountMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        /// <summary>
        /// GetPhysicianDetails
        /// </summary>
        /// <returns></returns>
        public List<TblPhysician> GetPhysicianDetails(int VenueNo, int VenueBranchNo)
        {
            List<TblPhysician> objresult = new List<TblPhysician>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblPhysician.Where(a => a.VenueNo == VenueNo
                    && a.VenueBranchNo == VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPhysicianDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        /// <summary>
        /// GetPhysicianDetails
        /// </summary>
        /// <returns></returns>
        public List<TblPhysicianSearch> GetPhysicianDetailsbyName(int VenueNo, int VenueBranchNo, string physicianName,int type=0)
        {
            List<TblPhysicianSearch> objresult = new List<TblPhysicianSearch>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _physicianName = new SqlParameter("physicianName", physicianName.ValidateEmpty());
                    var _type = new SqlParameter("type", type);
                    
                    objresult = context.Physiciandetails.FromSqlRaw(
                         "Execute dbo.Pro_SearchPhysicianDetail @VenueNo,@VenueBranchNo,@physicianName,@type",
                      _VenueNo, _VenueBranchNo, _physicianName, _type).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPhysicianDetailsbyName", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        /// <summary>
        /// Get Service 
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public List<ServiceSearchDTO> GetService(int VenueNo, int VenueBranchNo, int IsApproval)
        {
            List<ServiceSearchDTO> objresult = new List<ServiceSearchDTO>();
            try
            {
                string _CacheKey = CacheKeys.ServiceList + VenueNo;// + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<ServiceSearchDTO>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                        var _IsApproval = new SqlParameter("IsApproval", IsApproval);
                        objresult = context.ServiceSearchDTO.FromSqlRaw(
                            "Execute dbo.pro_SearchService @VenueNo,@VenueBranchNo,@IsApproval",
                         _VenueNo, _VenueBranchNo, _IsApproval).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetService", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<OptionalTestDTO> GetOptionalSelectedInPackages(int ServiceNo, int VenueNo, int VenueBranchNo, int PatientVisitNo)
        {
            List<OptionalTestDTO> objresult = new List<OptionalTestDTO>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", ServiceNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    objresult = context.GetOptionalSelectedInPackages.FromSqlRaw(
                        "Execute dbo.Pro_GetOptionalSelectedInPackages @VenueNo,@VenueBranchNo,@ServiceNo,@PatientVisitNo",
                        _VenueNo, _VenueBranchNo, _ServiceNo, _PatientVisitNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetGrouptest", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<GroupTestDTO> GetGrouptest(int ServiceNo, string ServiceType, int VenueNo, int VenueBranchNo)
        {
            List<GroupTestDTO> objresult = new List<GroupTestDTO>();
            try
            {
                string _CacheKey = CacheKeys.tblGroupList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<GroupTestDTO>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                        var _ServiceNo = new SqlParameter("ServiceNo", ServiceNo);
                        var _ServiceType = new SqlParameter("ServiceType", ServiceType);
                        objresult = context.GroupServiceDTO.FromSqlRaw(
                            "Execute dbo.pro_GrouptestDetails @VenueNo,@VenueBranchNo,@ServiceNo,@ServiceType",
                         _VenueNo, _VenueBranchNo, _ServiceNo, _ServiceType).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetGrouptest", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public FrontOffficeValidatetest getvalidatetest(List<ServiceParamDTO> req)
        {
            FrontOffficeValidatetest objresult = new FrontOffficeValidatetest();
            try
            {
                XElement XMLNode = new XElement("ServiceXML", req.Select(kv => new XElement("Service",
                new XAttribute("key", kv.TestNo), new XAttribute("value", kv.TestType))));

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req[0].venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req[0].venueBranchNo);
                    var _ServiceXML = new SqlParameter("ServiceXML", XMLNode.ToString());
                    objresult = context.validatetestresult.FromSqlRaw(
                        "Execute dbo.pro_ValidateService @VenueNo,@VenueBranchNo,@ServiceXML",
                     _VenueNo, _VenueBranchNo, _ServiceXML).AsEnumerable()?.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetGrouptest", ExceptionPriority.High, ApplicationType.REPOSITORY, req[0].venueNo, req[0].venueBranchNo, 0);
            }
            return objresult;
        }
        public ServiceRateList GetServiceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo, int physicianNo, int splratelisttype)
        {
            ServiceRateList objresult = new ServiceRateList();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ServiceNo = new SqlParameter("ServiceNo", ServiceNo);
                    var _ServiceType = new SqlParameter("ServiceType", ServiceType);
                    var _ClientNo = new SqlParameter("ClientNo", ClientNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _physicianNo = new SqlParameter("PhysicianNo", physicianNo);
                    var _splratelisttype = new SqlParameter("SplRateListType", splratelisttype);

                    objresult = context.ServiceRateList.FromSqlRaw(
                    "Execute dbo.pro_ServiceDetails @VenueNo, @VenueBranchNo, @ServiceNo, @ServiceType, @ClientNo, @physicianNo, @SplRateListType",
                    _VenueNo, _VenueBranchNo, _ServiceNo, _ServiceType, _ClientNo, _physicianNo, _splratelisttype)?.AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetServiceDetails/ServiceN0/ServiceType/ClientNo-" + ServiceNo + "/" + ServiceType + "/" + ClientNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<CreateManageSampleResponse> PrePrintManageSample(List<PrePrintBarcodeRequest> createManageSample)
        {
            //CreateManageSampleRequest sampleRequest = new CreateManageSampleRequest();
            List<CreateManageSampleResponse> response = new List<CreateManageSampleResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createManageSample);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createManageSample?.FirstOrDefault()?.collectedBy);
                    var _UserNo = new SqlParameter("UserNo", createManageSample?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createManageSample?.FirstOrDefault()?.venueNo);
                    var _pagecode = new SqlParameter("pagecode", createManageSample?.FirstOrDefault()?.pagecode);

                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createManageSample?.FirstOrDefault()?.venueBranchNo);
                    response = context.CreateManageSamples.FromSqlRaw(
                        "Execute dbo.Pro_PrePrintBarcodeInsertSamples @SampleXML,@CreatedBy,@VenueBranchNo,@VenueNo,@UserNo,@pagecode",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID, _pagecode).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PrePrintManageSample", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return response;
        }
        public List<TestPrePrintDetailsResponse> GetTestPrePrintDetails(TestPrePrintDetailsRequest req)
        {
            List<TestPrePrintDetailsResponse> lstresult = new List<TestPrePrintDetailsResponse>();
            List<TestPrePrintDetailsResponse> lstresultNew = new List<TestPrePrintDetailsResponse>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ServiceNo = new SqlParameter("ServiceNo", req.ServiceNo);
                    var _ServiceType = new SqlParameter("ServiceType", req.ServiceType);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    lstresult = context.TestPrePrintDetails.FromSqlRaw(
                        "Execute dbo.pro_GetTestPrePrintDetails @VenueNo,@VenueBranchNo,@UserNo,@ServiceNo,@ServiceType",
                    _VenueNo, _VenueBranchNo, _UserNo, _ServiceNo, _ServiceType).ToList();

                    foreach (var obj in lstresult)
                    {
                        TestPrePrintDetailsResponse objResponse = new TestPrePrintDetailsResponse();

                        objResponse.ContainerName = obj.ContainerName;
                        objResponse.ContainerNo = obj.ContainerNo;
                        objResponse.DeptName = obj.DeptName;
                        objResponse.DeptNo = obj.DeptNo;
                        objResponse.ID = obj.ID;
                        objResponse.MethodName = obj.MethodName;
                        objResponse.MethodNo = obj.MethodNo;
                        objResponse.Prefix = obj.Prefix;
                        objResponse.ResultType = obj.ResultType;
                        objResponse.SampleName = obj.SampleName;
                        objResponse.SampleNo = obj.SampleNo;
                        objResponse.SeqNo = obj.SeqNo;
                        objResponse.Suffix = obj.Suffix;
                        objResponse.TestName = obj.TestName;
                        objResponse.TestNo = obj.TestNo;
                        objResponse.UnitsName = obj.UnitsName;
                        objResponse.UnitsNo = obj.UnitsNo;
                        objResponse.IsMultiSampleSelect = obj.IsMultiSampleSelect;
                        objResponse.lstMultiSampleList = new List<MultiSampleList>();
                        if (objResponse.IsMultiSampleSelect)
                        {
                            GetMultiplsSampleRequest objSampRequest = new GetMultiplsSampleRequest();
                            objSampRequest.TestNo = objResponse.TestNo;
                            objSampRequest.VenueNo = req.VenueNo;
                            objSampRequest.VenueBranchNo = req.VenueBranchNo;
                            objSampRequest.Type = req.ServiceType.ToString();
                            //GetMultiplsSampleResponse objSampResponse = new GetMultiplsSampleResponse();
                            //objSampResponse = GetMultiplsSampleByTestId(objSampRequest);
                            //objResponse.lstMultiSampleList = JsonConvert.DeserializeObject<List<MultiSamplesList>>(objSampResponse.SelectMultiSampleJson);

                            List<GetMultiplsSampleResponse> lstSampResponse = new List<GetMultiplsSampleResponse>();
                            lstSampResponse = GetMultiplsSampleByTestId(objSampRequest);
                            foreach (var multiSample in lstSampResponse)
                            {
                                MultiSampleList objRes = new MultiSampleList();
                                objRes.SampleNo = multiSample.SampleNo;
                                objRes.SampleName = multiSample.SampleName;
                                objRes.ContainerNo = multiSample.ContainerNo;
                                objRes.ContainerName = multiSample.ContainerName;
                                objRes.MethodNo = multiSample.MethodNo;
                                objRes.MethodName = multiSample.MethodName;
                                objResponse.lstMultiSampleList.Add(objRes);
                            }
                        }
                        lstresultNew.Add(objResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetTestPrePrintDetails/ServiceN0/ServiceType-" + req.ServiceNo + "/" + req.ServiceType, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lstresultNew;
        }
        public List<GetMultiplsSampleResponse> GetMultiplsSampleByTestId(GetMultiplsSampleRequest req)
        {
            List<GetMultiplsSampleResponse> lstMulti = new List<GetMultiplsSampleResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("TestNo", req.TestNo);
                    var _type = new SqlParameter("Type", req.Type);
                    var _venueno = new SqlParameter("VenueNo", req.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    lstMulti = context.GetMultiplsSampleByTestId.FromSqlRaw(
                        "Execute dbo.Pro_GetMultiSampleByTestId @Type,@VenueNo,@VenueBranchNo,@TestNo",
                           _type, _venueno, _venuebranchno, _testno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetMultiplsSampleByTestId" + req.TestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lstMulti;
        }
        public ExternalVisitDetailsResponse CheckExternalVistIdExists(ExternalVisitDetails req)
        {
            ExternalVisitDetailsResponse objresult = new ExternalVisitDetailsResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Value = new SqlParameter("Value", req.Value);
                    var _ValueType = new SqlParameter("ValueType", req.ValueType);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _VisitNo = new SqlParameter("VisitNo", req.VisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    objresult = context.CheckExternalVistIdExists.FromSqlRaw(
                        "Execute dbo.pro_CheckExternalVistIdExists @VenueNo,@VenueBranchNo,@UserNo,@Value,@ValueType,@VisitNo",
                    _VenueNo, _VenueBranchNo, _UserNo, _Value, _ValueType, _VisitNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.CheckExternalVistIdExists/VisitId-" + req.Value, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }

        /// <summary>
        /// Insert Front OfficeMaster
        /// </summary>
        /// <param name="objDTO"></param>
        /// <returns></returns>
        public FrontOffficeResponse InsertFrontOfficeMaster(FrontOffficeDTO objDTO)
        {
            int PatientVisitNo = 0;
            FrontOffficeResponse result = new FrontOffficeResponse(); ;
            try
            {
                int agedays = objDTO.ageDays != null ? objDTO.ageDays :0;
                int agemonth = objDTO.ageMonths != null ? objDTO.ageMonths : 0;
                int ageyear = objDTO.ageYears != null ? objDTO.ageYears : objDTO.Age;
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string Password = Guid.NewGuid().ToString("N").Substring(0, 7);
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);
                    var _TitleCode = new SqlParameter("TitleCode", objDTO.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO.AgeType.Substring(0, 1));
                    var _AgeDays = new SqlParameter("ageDays", agedays);
                    var _AgeMonths = new SqlParameter("ageMonths", agemonth);
                    var _AgeYears = new SqlParameter("ageYears", ageyear);
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
                    var _RouteNo = new SqlParameter("RouteNo", objDTO.RouteNo);
                    var _IsStat = new SqlParameter("IsStat", objDTO.IsStat);
                    var _ClinicalHistory = new SqlParameter("ClinicalHistory", objDTO.ClinicalHistory.ValidateEmpty());
                    var _registeredType = new SqlParameter("registeredType", objDTO.RegisteredType.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserID", objDTO.UserNo.ToString());
                    var _Password = new SqlParameter("Pass", CommonSecurity.EncodePassword(Password, CommonSecurity.GeneratePassword(1)));

                    if (objDTO.RegisteredType != "LIMS" && objDTO.RegisteredType != "HIMS" && objDTO.RegisteredType != "APP")
                    {
                        string registrationDate = objDTO?.registrationDT?.Split('T')[0];
                        string registrationTime = objDTO?.registrationDT?.Split('T')[1];
                        objDTO.registrationDT = registrationDate + ' ' + registrationTime;
                    }
                    var _registrationDT = new SqlParameter("registrationDT", objDTO.registrationDT.ValidateEmpty());
                    var _IsEmail = new SqlParameter("IsAutoEmail", objDTO.IsAutoEmail);
                    var _IsSMS = new SqlParameter("IsAutoSMS", objDTO.IsAutoSMS);
                    var _isAutoWhatsApp = new SqlParameter("isAutoWhatsApp", objDTO.IsAutoWhatsApp);

                    var _isSelf = new SqlParameter("IsSelf", objDTO.isSelf);
                    var _ExternalVisitID = new SqlParameter("ExtenalVisitID", objDTO.ExternalVisitID.ValidateEmpty());
                    var _VaccinationType = new SqlParameter("VaccinationType", objDTO.VaccinationType.ValidateEmpty());
                    var _VaccinationDate = new SqlParameter("VaccinationDate", objDTO.VaccinationDate.ValidateEmpty());
                    var _IsFranchise = new SqlParameter("IsFranchise", objDTO.IsFranchise);

                    //var _PassportNo = new SqlParameter("PassportNo", objDTO.PassportNo.ValidateEmpty());                    
                    var _NURNID = new SqlParameter("NURNID", objDTO.NURNID.ValidateEmpty());
                    var _NURNType = new SqlParameter("NURNType", objDTO.NURNType.ValidateEmpty());
                    var _Deliverymode = new SqlParameter("Deliverymode", objDTO.Deliverymode);
                    var _ExternalVisitIdentity = new SqlParameter("ExternalVisitIdentity", objDTO.ExternalVisitIdentity.ValidateEmpty());
                    var _WardNo = new SqlParameter("WardNo", objDTO.WardNo);
                    var _WardName = new SqlParameter("WardName", objDTO.WardName.ValidateEmpty());
                    //var _IPOPNumber = new SqlParameter("IPOPNumber", objDTO.IPOPNumber.ValidateEmpty());
                    var _maritalStatus = new SqlParameter("maritalStatus", objDTO.maritalStatus);
                    var _HCPatientNo = new SqlParameter("HCPatientNo", objDTO.HCPatientNo);

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
                    var _HomePhoneNo = new SqlParameter("HomePhoneNo", objDTO.HomePhoneNo.ValidateEmpty());
                    var _ClinicalDiagnosis = new SqlParameter("ClinicalDiagnosis", objDTO.ClinicalDiagnosis);
                    var _ClinicalDiagnosisOthers = new SqlParameter("ClinicalDiagnosisOthers", objDTO.ClinicalDiagnosisOthers.ValidateEmpty());
                    var _SampleCollectionDT = new SqlParameter("SampleCollectionDT", objDTO.SampleCollectionDT.ValidateEmpty());
                    var _InternalComments = new SqlParameter("InternalComments", objDTO.InternalComments.ValidateEmpty());
                    var _IsFasting = new SqlParameter("IsFasting", objDTO.isFasting);
                    var _ExternalPatientID = new SqlParameter("ExternalPatientID", objDTO.ExternalPatientID.ValidateEmpty());
                    var _OPDAppoinmentNo = new SqlParameter("OPDAppoinmentNo", objDTO?.OPDAppoinmentNo.ValidateEmpty());
                    //var _loyalcardno = new SqlParameter("loyalcardno", objDTO?.loyalcardno.ValidateEmpty());
                    var FrontOffficePatientResponse = context.FrontOffficePatient.FromSqlRaw(
                    "Execute dbo.Pro_InsertPatientRegistration @PatientNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@ageDays,@ageMonths,@ageYears,@MobileNumber,@AltMobileNumber," +
                    "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@SecondaryAddress," +
                    "@URNID,@URNType,@RefferralTypeNo,@CustomerNo,@PhysicianNo,@RiderNo,@MarketingNo,@RouteNo,@IsStat,@ClinicalHistory,@registeredType,@VenueNo,@VenueBranchNo,@UserID,@Pass," +
                    "@registrationDT,@IsAutoEmail,@IsAutoSMS,@IsSelf,@ExtenalVisitID,@VaccinationType,@VaccinationDate,@IsFranchise,@NURNID,@NURNType,@Deliverymode,@ExternalVisitIdentity," +
                    "@WardNo,@WardName,@maritalStatus,@isAutoWhatsApp,@NRICNumber,@AllergyInfo,@PatientBlock,@PatientUnitNo,@PatientFloor," +
                    "@PatientBuilding,@PatientHomeNo,@PhysicianNo2,@VipIndication,@BedNo,@NationalityNo,@RaceNo,@CompanyNo,@CaseNumber,@AlternateIdType, " +
                    "@AlternateId,@PatientOfficeNumber,@IsPregnant,@Remarks,@HomePhoneNo,@ClinicalDiagnosis,@ClinicalDiagnosisOthers,@SampleCollectionDT," +
                    "@InternalComments, @HCPatientNo, @IsFasting, @ExternalPatientID",
                    _PatientNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _AgeDays, _AgeMonths, _AgeYears, _MobileNumber, _AltMobileNumber, _EmailID,
                    _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _SecondaryAddress, _URNID, _URNType, _RefferralTypeNo,
                    _CustomerNo, _PhysicianNo, _RiderNo, _MarketingNo, _RouteNo, _IsStat, _ClinicalHistory, _registeredType, _VenueNo, _VenueBranchNo, _UserID, _Password, _registrationDT,
                    _IsEmail, _IsSMS, _isSelf, _ExternalVisitID, _VaccinationType, _VaccinationDate, _IsFranchise, _NURNID, _NURNType, _Deliverymode,
                    _ExternalVisitIdentity, _WardNo, _WardName, _maritalStatus, _isAutoWhatsApp, _NRICNumber,
                    _AllergyInfo, _PatientBlock, _PatientUnitNo, _PatientFloor, _PatientBuilding, _PatientHomeNo, _PhysicianNo2, _VipIndication, _BedNo, _NationalityNo, _RaceNo,
                    _CompanyNo, _CaseNumber, _AlternateIdType, _AlternateId, _PatientOfficeNumber, _IsPregnant, _Remarks, _HomePhoneNo, _ClinicalDiagnosis, _ClinicalDiagnosisOthers,
                    _SampleCollectionDT, _InternalComments, _HCPatientNo, _IsFasting, _ExternalPatientID).AsEnumerable().ToList();

                    PatientVisitNo = FrontOffficePatientResponse[0].patientvisitno;
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
                     new XElement("ServiceName", Item.TestName),
                     new XElement("ServiceType", Item.TestType),
                     new XElement("Rate", Item.Rate),
                     new XElement("Quantity", Item.Quantity),
                     new XElement("Amount", Item.Amount),
                     new XElement("DiscountType", Item.DiscountType),
                     new XElement("DiscountValue", Item.discount),
                     new XElement("DiscountAmount", Item.DiscountAmount),
                     new XElement("RateListNo", Item.RateListNo),
                     new XElement("ClientServiceCode", Item.ClientServiceCode)
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

                    var vDueRemarks = objDTO.DueRemarks != null ? objDTO.DueRemarks : "";
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _orderxml = new SqlParameter("orderxml", ServiceXML.ToString());
                    var _paymentxml = new SqlParameter("paymentxml", PaymentXML.ToString());
                    var _NetAmount = new SqlParameter("NetAmount", objDTO.NetAmount);
                    var _GrossAmount = new SqlParameter("GrossAmount", objDTO.GrossAmount);
                    var _discountno = new SqlParameter("discountno", objDTO.discountno);
                    var _DiscountAmount = new SqlParameter("DiscountAmount", objDTO.TdiscountAmt);
                    var _DiscountApprovedBy = new SqlParameter("DiscountApprovedBy", objDTO.DiscountApprovedBy);
                    var _discountDescription = new SqlParameter("DiscountReason", objDTO.discountDescription.ValidateEmpty());

                    var _DueAmount = new SqlParameter("DueAmount", objDTO.DueAmount);
                    var _CollectedAmount = new SqlParameter("CollectedAmount", objDTO.CollectedAmount);
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _PUserID = new SqlParameter("UserID", objDTO.UserNo.ToString());
                    var _DueRemarks = new SqlParameter("DueRemarks", vDueRemarks);
                    var _givenAmount = new SqlParameter("GivenAmount", objDTO.GivenAmount);
                    var _toBeReturn = new SqlParameter("ToBeReturn", objDTO.ToBeReturn);
                    var _IsDiscountApprovalReq = new SqlParameter("IsDiscountApprovalReq", objDTO.IsDiscountApprovalReq);

                    var result1 = context.FrontOffficeTransaction.FromSqlRaw(
                    "Execute dbo.Pro_InsertPatientOrders " +
                    "@PatientVisitNo, @orderxml, @paymentxml, @NetAmount, @GrossAmount, @discountno, @DiscountAmount," +
                    "@DiscountApprovedBy, @DiscountReason, @DueAmount, @CollectedAmount, @VenueNo, @VenueBranchNo, @UserID, @DueRemarks, " +
                    "@GivenAmount, @ToBeReturn, @IsDiscountApprovalReq, @OPDAppoinmentNo",
                    _PatientVisitNo, _orderxml, _paymentxml, _NetAmount, _GrossAmount, _discountno, _DiscountAmount, 
                    _DiscountApprovedBy, _discountDescription, _DueAmount, _CollectedAmount, _PVenueNo, _PVenueBranchNo, _PUserID, _DueRemarks, 
                    _givenAmount, _toBeReturn, _IsDiscountApprovalReq, _OPDAppoinmentNo).AsEnumerable().ToList();

                    result = result1?.AsEnumerable()?.FirstOrDefault();

                    PushMessage(result.patientvisitno, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo, Password);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertFrontOfficeMaster/PatientVisitNo-" + PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _PUserID = new SqlParameter("UserNo", objDTO.UserNo.ToString());
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    
                    context.FrontOffficeReset.FromSqlRaw(
                    "Execute dbo.pro_ResetRegistration @PatientVisitNo,@UserNo,@VenueNo,@VenueBranchNo",
                    _PatientVisitNo, _PUserID, _PVenueNo, _PVenueBranchNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            return result;
        }

        public FrontOffficeResponse InsertFrontOfficeRegistration(FrontOffficeDTO objDTO)
        {
            int PatientVisitNo = 0;
            FrontOffficeResponse result = new FrontOffficeResponse(); ;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string Password = Guid.NewGuid().ToString("N").Substring(0, 7);
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);
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
                    var _RouteNo = new SqlParameter("RouteNo", objDTO.RouteNo);
                    var _IsStat = new SqlParameter("IsStat", objDTO.IsStat);
                    var _ClinicalHistory = new SqlParameter("ClinicalHistory", objDTO.ClinicalHistory.ValidateEmpty());
                    var _registeredType = new SqlParameter("registeredType", objDTO.RegisteredType.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserID", objDTO.UserNo.ToString());
                    var _Password = new SqlParameter("Pass", CommonSecurity.EncodePassword(Password, CommonSecurity.GeneratePassword(1)));
                    var _OPDAppoinmentNo = new SqlParameter("OPDAppoinmentNo", objDTO?.OPDAppoinmentNo.ValidateEmpty());

                    if (objDTO.RegisteredType != "LIMS" && objDTO.RegisteredType != "HIMS" && objDTO.RegisteredType != "APP")
                    {
                        string registrationDate = objDTO?.registrationDT?.Split('T')[0];
                        string registrationTime = objDTO?.registrationDT?.Split('T')[1];
                        objDTO.registrationDT = registrationDate + ' ' + registrationTime;
                    }
                    var _registrationDT = new SqlParameter("registrationDT", objDTO.registrationDT.ValidateEmpty());
                    var _IsEmail = new SqlParameter("IsAutoEmail", objDTO.IsAutoEmail);
                    var _IsSMS = new SqlParameter("IsAutoSMS", objDTO.IsAutoSMS);
                    var _isAutoWhatsApp = new SqlParameter("isAutoWhatsApp", objDTO.IsAutoWhatsApp);

                    var _isSelf = new SqlParameter("IsSelf", objDTO.isSelf);
                    var _ExternalVisitID = new SqlParameter("ExtenalVisitID", objDTO.ExternalVisitID.ValidateEmpty());
                    var _VaccinationType = new SqlParameter("VaccinationType", objDTO.VaccinationType.ValidateEmpty());
                    var _VaccinationDate = new SqlParameter("VaccinationDate", objDTO.VaccinationDate.ValidateEmpty());
                    var _IsFranchise = new SqlParameter("IsFranchise", objDTO.IsFranchise);

                    //var _PassportNo = new SqlParameter("PassportNo", objDTO.PassportNo.ValidateEmpty());                    
                    var _NURNID = new SqlParameter("NURNID", objDTO.NURNID.ValidateEmpty());
                    var _NURNType = new SqlParameter("NURNType", objDTO.NURNType.ValidateEmpty());
                    var _Deliverymode = new SqlParameter("Deliverymode", objDTO.Deliverymode);
                    var _ExternalVisitIdentity = new SqlParameter("ExternalVisitIdentity", objDTO.ExternalVisitIdentity.ValidateEmpty());
                    var _WardNo = new SqlParameter("WardNo", objDTO.WardNo);
                    var _WardName = new SqlParameter("WardName", objDTO.WardName.ValidateEmpty());
                    //var _IPOPNumber = new SqlParameter("IPOPNumber", objDTO.IPOPNumber.ValidateEmpty());
                    var _maritalStatus = new SqlParameter("maritalStatus", objDTO.maritalStatus);
                    var _HCPatientNo = new SqlParameter("HCPatientNo", objDTO.HCPatientNo);

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
                    var _HomePhoneNo = new SqlParameter("HomePhoneNo", objDTO.HomePhoneNo.ValidateEmpty());
                    var _ClinicalDiagnosis = new SqlParameter("ClinicalDiagnosis", objDTO.ClinicalDiagnosis);
                    var _ClinicalDiagnosisOthers = new SqlParameter("ClinicalDiagnosisOthers", objDTO.ClinicalDiagnosisOthers.ValidateEmpty());
                    var _SampleCollectionDT = new SqlParameter("SampleCollectionDT", objDTO.SampleCollectionDT.ValidateEmpty());
                    var _InternalComments = new SqlParameter("InternalComments", objDTO.InternalComments.ValidateEmpty());
                    var _IsFasting = new SqlParameter("IsFasting", objDTO.isFasting);
                    var _ExternalPatientID = new SqlParameter("ExternalPatientID", objDTO.ExternalPatientID.ValidateEmpty());
                    var _C2PReportSMSPatient = new SqlParameter("C2PReportSMSPatient", objDTO.C2PReportSMSPatient);
                    var _C2PReportEmailPatient = new SqlParameter("C2PReportEmailPatient", objDTO.C2PReportEmailPatient);
                    var _C2PReportWhatsappPatient = new SqlParameter("C2PReportWhatsappPatient", objDTO.C2PReportWhatsappPatient);
                    var _C2PBillSMSPatient = new SqlParameter("C2PBillSMSPatient", objDTO.C2PBillSMSPatient);
                    var _C2PBillEmailPatient = new SqlParameter("C2PBillEmailPatient", objDTO.C2PBillEmailPatient);
                    var _C2PBillWhatsappPatient = new SqlParameter("C2PBillWhatsappPatient", objDTO.C2PBillWhatsappPatient);
                    var _loyalcardno = new SqlParameter("loyalcardno", objDTO.loyalcardno.ValidateEmpty());

                    var FrontOffficePatientResponse = context.FrontOffficeRegistration.FromSqlRaw(
                    "Execute dbo.Pro_InsertPatientRegistration @PatientNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@ageDays,@ageMonths,@ageYears,@MobileNumber,@AltMobileNumber," +
                    "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@SecondaryAddress," +
                    "@URNID,@URNType,@RefferralTypeNo,@CustomerNo,@PhysicianNo,@RiderNo,@MarketingNo,@RouteNo,@IsStat,@ClinicalHistory,@registeredType,@VenueNo,@VenueBranchNo,@UserID,@Pass," +
                    "@registrationDT,@IsAutoEmail,@IsAutoSMS,@IsSelf,@ExtenalVisitID,@VaccinationType,@VaccinationDate,@IsFranchise,@NURNID,@NURNType,@Deliverymode,@ExternalVisitIdentity," +
                    "@WardNo,@WardName,@maritalStatus,@isAutoWhatsApp,@NRICNumber,@AllergyInfo,@PatientBlock,@PatientUnitNo,@PatientFloor," +
                    "@PatientBuilding,@PatientHomeNo,@PhysicianNo2,@VipIndication,@BedNo,@NationalityNo,@RaceNo,@CompanyNo,@CaseNumber,@AlternateIdType, " +
                    "@AlternateId,@PatientOfficeNumber,@IsPregnant,@Remarks,@HomePhoneNo,@ClinicalDiagnosis,@ClinicalDiagnosisOthers,@SampleCollectionDT," +
                    "@InternalComments, @HCPatientNo, @IsFasting, @ExternalPatientID,@C2PReportSMSPatient,@C2PReportEmailPatient,@C2PReportWhatsappPatient,@C2PBillSMSPatient,@C2PBillEmailPatient,@C2PBillWhatsappPatient,@loyalcardno",
                    _PatientNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _AgeDays, _AgeMonths, _AgeYears, _MobileNumber, _AltMobileNumber, _EmailID,
                    _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _SecondaryAddress, _URNID, _URNType, _RefferralTypeNo,
                    _CustomerNo, _PhysicianNo, _RiderNo, _MarketingNo, _RouteNo, _IsStat, _ClinicalHistory, _registeredType, _VenueNo, _VenueBranchNo, _UserID, _Password, _registrationDT,
                    _IsEmail, _IsSMS, _isSelf, _ExternalVisitID, _VaccinationType, _VaccinationDate, _IsFranchise, _NURNID, _NURNType, _Deliverymode,
                    _ExternalVisitIdentity, _WardNo, _WardName, _maritalStatus, _isAutoWhatsApp, _NRICNumber,
                    _AllergyInfo, _PatientBlock, _PatientUnitNo, _PatientFloor, _PatientBuilding, _PatientHomeNo, _PhysicianNo2, _VipIndication, _BedNo, _NationalityNo, _RaceNo,
                    _CompanyNo, _CaseNumber, _AlternateIdType, _AlternateId, _PatientOfficeNumber, _IsPregnant, _Remarks, _HomePhoneNo, _ClinicalDiagnosis, _ClinicalDiagnosisOthers,
                    _SampleCollectionDT, _InternalComments, _HCPatientNo, _IsFasting, _ExternalPatientID, _C2PReportSMSPatient, _C2PReportEmailPatient, _C2PReportWhatsappPatient,
                    _C2PBillSMSPatient, _C2PBillEmailPatient, _C2PBillWhatsappPatient, _loyalcardno).AsEnumerable().ToList();

                    PatientVisitNo = FrontOffficePatientResponse[0].patientvisitno;
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
                     new XElement("ServiceName", Item.TestName),
                     new XElement("ServiceType", Item.TestType),
                     new XElement("Rate", Item.Rate),
                     new XElement("Quantity", Item.Quantity),
                     new XElement("Amount", Item.Amount),
                     new XElement("DiscountType", Item.DiscountType),
                     new XElement("DiscountValue", Item.discount),
                     new XElement("DiscountAmount", Item.DiscountAmount),
                     new XElement("RateListNo", Item.RateListNo),
                     new XElement("ClientServiceCode", Item.ClientServiceCode)
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
                    var vDueRemarks = objDTO.DueRemarks != null ? objDTO.DueRemarks : "";
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _orderxml = new SqlParameter("orderxml", ServiceXML.ToString());
                    var _paymentxml = new SqlParameter("paymentxml", PaymentXML.ToString());
                    var _NetAmount = new SqlParameter("NetAmount", objDTO.NetAmount);
                    var _GrossAmount = new SqlParameter("GrossAmount", objDTO.GrossAmount);
                    var _discountno = new SqlParameter("discountno", objDTO.discountno);
                    var _DiscountAmount = new SqlParameter("DiscountAmount", objDTO.TdiscountAmt);
                    var _DiscountApprovedBy = new SqlParameter("DiscountApprovedBy", objDTO.DiscountApprovedBy);
                    var _discountDescription = new SqlParameter("DiscountReason", objDTO.discountDescription.ValidateEmpty());

                    var _DueAmount = new SqlParameter("DueAmount", objDTO.DueAmount);
                    var _CollectedAmount = new SqlParameter("CollectedAmount", objDTO.CollectedAmount);
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _PUserID = new SqlParameter("UserID", objDTO.UserNo.ToString());
                    var _DueRemarks = new SqlParameter("DueRemarks", vDueRemarks);
                    var _givenAmount = new SqlParameter("GivenAmount", objDTO.GivenAmount);
                    var _toBeReturn = new SqlParameter("ToBeReturn", objDTO.ToBeReturn);
                    var _IsDiscountApprovalReq = new SqlParameter("IsDiscountApprovalReq", objDTO.IsDiscountApprovalReq);

                    var result1 = context.FrontOffficeTransaction.FromSqlRaw(
                    "Execute dbo.Pro_InsertPatientOrders " +
                    "@PatientVisitNo, @orderxml, @paymentxml, @NetAmount, @GrossAmount, @discountno, @DiscountAmount," +
                    "@DiscountApprovedBy, @DiscountReason, @DueAmount, @CollectedAmount, @VenueNo, @VenueBranchNo, @UserID, @DueRemarks, " +
                    "@GivenAmount, @ToBeReturn, @IsDiscountApprovalReq, @OPDAppoinmentNo",
                    _PatientVisitNo, _orderxml, _paymentxml, _NetAmount, _GrossAmount, _discountno, _DiscountAmount, 
                    _DiscountApprovedBy, _discountDescription, _DueAmount, _CollectedAmount, _PVenueNo, _PVenueBranchNo, _PUserID, _DueRemarks, 
                    _givenAmount, _toBeReturn, _IsDiscountApprovalReq, _OPDAppoinmentNo).AsEnumerable().ToList();

                    result = result1?.AsEnumerable()?.FirstOrDefault();

                    PushMessage(result.patientvisitno, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo, Password);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertFrontOfficeMaster/PatientVisitNo-" + PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", PatientVisitNo);
                    var _PUserID = new SqlParameter("UserNo", objDTO.UserNo.ToString());
                    var _PVenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _PVenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    context.FrontOffficeReset.FromSqlRaw(
                         "Execute dbo.pro_ResetRegistration @PatientVisitNo,@UserNo,@VenueNo,@VenueBranchNo",
                         _PatientVisitNo, _PUserID, _PVenueNo, _PVenueBranchNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            return result;
        }
        private int PushMessage(int patientVisitNo, int venueno, int venuebranchno, int userno, string Password)
        {
            int result = 0;
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                objAppSettingResponse = new AppSettingResponse();
                string AppPatientportalURL = "PatientportalURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppPatientportalURL);
                string URL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.PatientportalURL);

                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    var lst = context.GetCustomerMsgDetails.FromSqlRaw(
                   "Execute dbo.Pro_GetPatientNotification @PatientVisitNo,@VenueNo,@VenueBranchNo", _PatientVisitNo, _venueno, _venuebranchno).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            NotificationDto objDTO = new NotificationDto();
                            CommonRepository objCommonRepository = new CommonRepository(_config);
                            objDTO.Address = item.Address;
                            objDTO.MessageType = item.MessageType;
                            objDTO.TemplateKey = "Patient_PORTAL_" + item.MessageType + "";
                            objDTO.VenueNo = venueno;
                            objDTO.VenueBranchNo = venuebranchno;
                            objDTO.UserNo = userno;
                            objDTO.ScheduleTime = DateTime.Now;
                            Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                            objMessageItem.Add("#Address#", item.Address);
                            objMessageItem.Add("#UserName#", item.VisitID);
                            objMessageItem.Add("#Password#", Password);
                            objMessageItem.Add("#FullName#", item.FullName);
                            objMessageItem.Add("#URL#", URL);
                            objDTO.MessageItem = objMessageItem;
                            objDTO.IsAttachment = false;
                            objDTO.PatientVisitNo = patientVisitNo;
                            objCommonRepository.SendMessage(objDTO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.PushMessage/PatientVisitNo-" + patientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebranchno, userno);
            }
            return result;
        }

        /// <summary>
        /// Print Bill
        /// </summary>
        /// <param name="VisitNo"></param>
        /// <returns></returns>
        public async Task<ReportOutput> PrintBill(ReportRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PatientVisitNo", req.visitNo.ToString());
                objdictionary.Add("UserNo", req.userNo.ToString());
                objdictionary.Add("VenueNo", req.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", req.VenueBranchNo.ToString());
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (req.print == "" || req.print == null)
                    {
                        req.print = ReportKey.PATIENTBILL;
                    }
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == req.print && x.VenueNo == req.VenueNo
                    && x.VenueBranchNo == req.VenueBranchNo).AsEnumerable()?.FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                    }
                }
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);

                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster?.ReportPath;
                objitem.ExportPath = tblReportMaster?.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                objitem.ExportFormat = FileFormat.PDF;
                string ReportParam = JsonConvert.SerializeObject(objitem);
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppReportServiceURL = "ReportServiceURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                string reportservceurl = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";
                //

                string filename = await ExportReportService.ExportPrint(ReportParam, reportservceurl);
                result.PatientExportFile = tblReportMaster?.ExportURL + filename; //CommonHelper.URLShorten(tblReportMaster?.ExportURL + filename, _config.GetConnectionString(ConfigKeys.FireBaseAPIkey));
                result.ExportURL = tblReportMaster?.ExportURL + filename;
                result.PatientExportFolderPath = objitem?.ExportPath;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.PrintBill/visitNo-" + req.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        /// <summary>
        /// Get Patient Details
        /// </summary>
        /// <returns></returns>
        public GetPatientDetailsWithServices GetPatientDetails(long visitNo, int VenueNo, int VenueBranchNo, string searchType = null, int PatientNo = 0, int Isprocedure = 0)
        {
            GetPatientDetailsWithServices objresult = new GetPatientDetailsWithServices();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _VisitNo = new SqlParameter("VisitNo", visitNo);
                    var _PatientNo = new SqlParameter("PatientNo", PatientNo);
                    var _Isprocedure = new SqlParameter("Isprocedure", Isprocedure);

                    var _SearchType = new SqlParameter();
                    if (string.IsNullOrEmpty(searchType))
                        _SearchType = new SqlParameter("SearchType", DBNull.Value);
                    else
                        _SearchType = new SqlParameter("SearchType", searchType);

                    var response = context.GetPatientDetailsDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetPatientDetails @VenueNo, @VenueBranchNo, @VisitNo, @SearchType, @PatientNo, @Isprocedure",
                     _VenueNo, _VenueBranchNo, _VisitNo, _SearchType, _PatientNo, _Isprocedure).ToList();

                    List<ServiceSearchDTO> serviceRateLists = new List<ServiceSearchDTO>();
                    if (response != null)
                    {
                        if (response.Any())
                        {
                            objresult.PatientNo = (int)(response?.FirstOrDefault()?.PatientNo ?? 0);
                            objresult.FullName = response?.FirstOrDefault()?.FullName;
                            objresult.PatientID = response?.FirstOrDefault()?.PatientID;
                            objresult.TitleCode = response?.FirstOrDefault()?.TitleCode;
                            objresult.FirstName = response?.FirstOrDefault()?.FirstName;
                            objresult.MiddleName = response?.FirstOrDefault()?.MiddleName;
                            objresult.LastName = response?.FirstOrDefault()?.LastName;
                            objresult.Age = (int)(response?.FirstOrDefault()?.Age ?? 0);
                            objresult.AgeType = response?.FirstOrDefault()?.AgeType;
                            objresult.ageDays = (int)(response?.FirstOrDefault()?.ageDays ?? 0);
                            objresult.ageMonths = (int)(response?.FirstOrDefault()?.ageMonths ?? 0);
                            objresult.ageYears = (int)(response?.FirstOrDefault()?.ageYears ?? 0);
                            objresult.dOB = response?.FirstOrDefault()?.dOB;
                            objresult.Gender = (int)(response?.FirstOrDefault()?.Gender ?? 0);
                            objresult.MobileNumber = response?.FirstOrDefault()?.MobileNumber;
                            objresult.AltMobileNumber = response?.FirstOrDefault()?.AltMobileNumber;
                            objresult.EmailID = response?.FirstOrDefault()?.EmailID;
                            objresult.SecondaryEmailID = response?.FirstOrDefault()?.SecondaryEmailID;
                            objresult.Address = response?.FirstOrDefault()?.Address;
                            objresult.CountryNo = (int)response?.FirstOrDefault()?.CountryNo;
                            objresult.StateNo = (int)response?.FirstOrDefault()?.StateNo;
                            objresult.CityNo = (int)response?.FirstOrDefault()?.CityNo;
                            objresult.AreaName = response?.FirstOrDefault()?.AreaName;
                            objresult.Pincode = response?.FirstOrDefault()?.Pincode;
                            objresult.SecondaryAddress = response?.FirstOrDefault()?.SecondaryAddress;
                            objresult.maritalStatus = (short)(response?.FirstOrDefault()?.maritalStatus);
                            objresult.uRNID = response?.FirstOrDefault()?.uRNID;
                            objresult.uRNType = response?.FirstOrDefault()?.uRNType;
                            objresult.ExternalVisitID = response?.FirstOrDefault()?.ExternalVisitID;
                            objresult.RefferralTypeNo = (int)response?.FirstOrDefault()?.RefferralTypeNo;
                            objresult.PhysicianNo = (int)response?.FirstOrDefault()?.PhysicianNo;
                            objresult.WardNo = (int)response?.FirstOrDefault()?.WardNo;
                            objresult.RefferralName = response?.FirstOrDefault()?.RefferralName;
                            objresult.VaccinationDate = response?.FirstOrDefault()?.VaccinationDate;
                            objresult.VaccinationType = response?.FirstOrDefault()?.VaccinationType;
                            objresult.HCPatientNo = (int)response?.FirstOrDefault()?.HCPatientNo;

                            objresult.NRICNumber = response?.FirstOrDefault()?.NRICNumber;
                            objresult.RaceNo = response.FirstOrDefault().RaceNo;
                            objresult.PatientBlock = response?.FirstOrDefault()?.PatientBlock;
                            objresult.PatientUnitNo = response?.FirstOrDefault()?.PatientUnitNo;
                            objresult.PatientFloor = response?.FirstOrDefault()?.PatientFloor;
                            objresult.PatientBuilding = response?.FirstOrDefault()?.PatientBuilding;
                            objresult.PatientHomeNo = response?.FirstOrDefault()?.PatientHomeNo;
                            objresult.AlternateId = response?.FirstOrDefault()?.AlternateId;
                            objresult.AlternateIdType = response?.FirstOrDefault()?.AlternateIdType;
                            objresult.NationalityNo = response?.FirstOrDefault()?.NationalityNo;
                            objresult.Amount = response.FirstOrDefault().Amount;
                            //
                            objresult.loyalcardno = response.FirstOrDefault().loyalcardno;
                        }
                        foreach (var patientdetail in response)
                        {
                            ServiceSearchDTO serviceRateList = new ServiceSearchDTO();
                            serviceRateList.TestType = patientdetail.ServiceType;
                            serviceRateList.TestNo = patientdetail.ServiceNo;
                            serviceRateList.TestCode = patientdetail.ServiceCode;
                            serviceRateList.TestName = patientdetail.ServiceName;
                            serviceRateLists.Add(serviceRateList);
                        }
                    }
                    objresult.serviceRateLists = serviceRateLists;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPatientDetails - visitNo : " + visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<QueueOrderDTO> GetQueueOrderDetails(CommonFilterRequestDTO RequestItem)
        {
            List<QueueOrderDTO> lstQueueOrderDTO = new List<QueueOrderDTO>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _QueueNo = new SqlParameter("QueueNo", RequestItem.SearchKey);
                    var _QueueStatus = new SqlParameter("QueueStatus", RequestItem.orderStatus);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    lstQueueOrderDTO = context.GetQueueOrder.FromSqlRaw(
                        "Execute dbo.Pro_GetQueueOrder @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PatientNo,@VisitNo,@QueueNo,@QueueStatus,@PageIndex",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _PatientNo, _VisitNo, _QueueNo, _QueueStatus, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetQueueOrderDetails/QueueNo-" + RequestItem.serviceNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstQueueOrderDTO;
        }
        public FrontOffficeQueueResponse UpdateQueueOrder(CommonFilterRequestDTO RequestItem)
        {
            FrontOffficeQueueResponse result = new FrontOffficeQueueResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", RequestItem.visitNo);
                    var _QueueNo = new SqlParameter("QueueNo", RequestItem.SearchKey);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _userNo = new SqlParameter("userNo", RequestItem.userNo);
                    result = context.QueueResponse.FromSqlRaw(
                         "Execute dbo.Pro_UpdateQueueOrder @PatientVisitNo,@QueueNo,@VenueNo,@VenueBranchNo,@userNo",
                     _PatientVisitNo, _QueueNo, _VenueNo, _VenueBranchNo, _userNo).AsEnumerable()?.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.UpdateQueueOrder/QueueNo-" + RequestItem.SearchKey, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return result;
        }
        public List<rescheckExists> checkExists(reqcheckExists req)
        {
            List<rescheckExists> lst = new List<rescheckExists>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _check = new SqlParameter("check", req.check);
                    var _checkType = new SqlParameter("checkType", req.checkType);
                    var _checkValue = new SqlParameter("checkValue", req.checkValue);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", req.venueBranchNo);

                    var response = context.checkExists.FromSqlRaw(
                    "Execute dbo.pro_CheckExists @check, @checkType, @checkValue, @venueNo, @venueBranchNo",
                    _check, _checkType, _checkValue, _venueNo, _venueBranchNo).ToList();

                    lst = response;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.checkExists : checkValue - " + req.checkValue, ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public DoctorDetails InsertDoctor(DoctorDetails objDTO)
        {
            DoctorDetails objresult = new DoctorDetails();
            int VenueNo = objDTO.VenueNo;
            int venueBranchNo = objDTO.VenueBranchNo;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _doctorName = new SqlParameter("DoctorName", !string.IsNullOrEmpty(objDTO.DoctorName) ? objDTO.DoctorName : string.Empty);
                    var _doctorQualification = new SqlParameter("DoctorQualification", !string.IsNullOrEmpty(objDTO.DoctorQualification) ? objDTO.DoctorQualification : string.Empty);
                    var _doctorMobile = new SqlParameter("DoctorMobile", !string.IsNullOrEmpty(objDTO.DoctorMobile) ? objDTO.DoctorMobile : string.Empty);
                    var _venueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _userNo = new SqlParameter("UserNo", objDTO.userNo);

                    objresult = context.DoctorDetails.FromSqlRaw(
                        "Execute dbo.Pro_InsertPhysician @DoctorName, @DoctorQualification, @DoctorMobile, @VenueNo, @VenueBranchNo, @UserNo",
                    _doctorName, _doctorQualification, _doctorMobile, _venueNo, _venueBranchNo, _userNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertDoctor/", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.userNo);
            }
            return objresult;
        }
        //from result entry/patient info screen,push the message send to patient regarding late result entry
        public int PushNotifyMessage(int patientVisitNo, int venueno, int venuebranchno, int userno, string messagetype, string message)
        {
            int result = 0;
            NotificationResponse output = new NotificationResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    var lst = context.GetCustomerMsgDetails.FromSqlRaw(
                   "Execute dbo.Pro_GetPatientNotification @PatientVisitNo,@VenueNo,@VenueBranchNo", _PatientVisitNo, _venueno, _venuebranchno).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            if (((messagetype == "SMS" || messagetype == "WSMS") && item.MessageType == "SMS") ||
                                (messagetype == "Email" && item.MessageType == "Email"))
                            {
                                NotificationDto objDTO = new NotificationDto();
                                CommonRepository objCommonRepository = new CommonRepository(_config);
                                objDTO.Address = item.Address;
                                objDTO.MessageType = messagetype;
                                objDTO.TemplateKey = "Patient_Notify_" + messagetype + "";
                                objDTO.VenueNo = venueno;
                                objDTO.VenueBranchNo = venuebranchno;
                                objDTO.UserNo = userno;
                                objDTO.ScheduleTime = DateTime.Now;
                                Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                                objMessageItem.Add("#Content#", message);
                                objDTO.MessageItem = objMessageItem;
                                objDTO.IsAttachment = false;
                                objDTO.PatientVisitNo = patientVisitNo;
                                output = objCommonRepository.SendMessage(objDTO);
                                result = output != null ? output.Status : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.PushNotifyMessage/PatientVisitNo-" + patientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebranchno, userno);
            }
            return result;
        }
        public int InsertPatientNotifyLog(PatientNotifyLog objDTO)
        {
            int result = 0;
            PatientNotifyLogResponse objresult = new PatientNotifyLogResponse();
            int VenueNo = objDTO != null ? objDTO.VenueNo : 0;
            int venueBranchNo = objDTO != null ? objDTO.VenueBranchNo : 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", objDTO?.PatientVisitNo);
                    var _VisitTestNo = new SqlParameter("VisitTestNo", objDTO?.VisitTestNo);
                    var _LogType = new SqlParameter("LogType", objDTO?.LogType);
                    var _Content = new SqlParameter("Content", objDTO?.NotifyContent);
                    var _UserType = new SqlParameter("UserType", objDTO?.LogUserType);
                    var _UserNo = new SqlParameter("UserNo", objDTO?.LogUserNo);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);
                    var _LogUserName = new SqlParameter("LogUserName", objDTO?.LogUserName ?? string.Empty);
                    DateTime? notifyLogDTTM = string.IsNullOrEmpty(objDTO.NotifyLogDTTM) ? null : DateTime.ParseExact(objDTO.NotifyLogDTTM, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var _NotifyLogDTTM = new SqlParameter("NotifyLogDTTM", SqlDbType.DateTime);
                    _NotifyLogDTTM.Value = notifyLogDTTM.HasValue ? notifyLogDTTM.Value : DBNull.Value;

                    objresult = context.InsertPatientNotifyLogDetails.FromSqlRaw(
                        "Execute dbo.Pro_InsertPatientNotifyLog " +
                        "@PatientVisitNo, @VisitTestNo, @LogType, @Content, @UserType, @UserNo, @VenueNo, @VenueBranchNo, @LogUserName, @NotifyLogDTTM ",
                        _PatientVisitNo, _VisitTestNo, _LogType, _Content, _UserType, _UserNo, _VenueNo, _VenueBranchNo, _LogUserName, _NotifyLogDTTM).AsEnumerable()?.FirstOrDefault();

                    result = objresult != null ? objresult.PatientNotifyLogNo : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertPatientNotifyLog/", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, venueBranchNo, 0);
            }
            return result;
        }

        public ClinicalSummary GetPatientClinicalSummary(PatientNotifyLog objDTO)
        {
            ClinicalSummary clinicalSummary = new ClinicalSummary();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", objDTO?.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);

                    var response = context.GetPatientClinicalSummary.FromSqlRaw(
                        "Execute dbo.Pro_GetClinicalSummary @PatientVisitNo,@VenueNo,@VenueBranchNo",
                        _PatientVisitNo, _VenueNo, _VenueBranchNo).ToList();
                    clinicalSummary = response.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPatientClinicalSummary", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO?.VenueNo, objDTO?.VenueBranchNo, 0);
            }
            return clinicalSummary;
        }
        public List<PatientNotifyLog> GetPatientNotifyLog(PatientNotifyLog objDTO)
        {
            List<PatientNotifyLog> lst = new List<PatientNotifyLog>();
            PatientNotifyLog objPatientNotifyLog = new PatientNotifyLog();
            int VenueNo = objDTO != null ? objDTO.VenueNo : 0;
            int venueBranchNo = objDTO != null ? objDTO.VenueBranchNo : 0;
            string LogType = objDTO != null ? objDTO.LogType : "";
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", objDTO?.PatientVisitNo);
                    var _VisitTestNo = new SqlParameter("VisitTestNo", objDTO?.VisitTestNo);
                    var _LogType = new SqlParameter("LogType", LogType);
                    var _LogUserNo = new SqlParameter("LogUserNo", objDTO?.LogUserNo);
                    var _LogUserType = new SqlParameter("LogUserType", objDTO?.LogUserType);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);

                    var rtndblst = context.GetPatientNotifyLog.FromSqlRaw(
                        "Execute dbo.Pro_GetPatientNotifyLog @PatientVisitNo,@VisitTestNo,@LogType,@LogUserNo,@LogUserType,@VenueNo,@VenueBranchNo",
                        _PatientVisitNo, _VisitTestNo, _LogType, _LogUserNo, _LogUserType, _VenueNo, _VenueBranchNo).ToList();

                    rtndblst = rtndblst.OrderBy(a => a.LogDTTM).ToList();
                    foreach (var v in rtndblst)
                    {
                        objPatientNotifyLog = new PatientNotifyLog();
                        objPatientNotifyLog.NotifyLogDTTM = v.NotifyLogDTTM;
                        objPatientNotifyLog.LogType = v.LogType;
                        objPatientNotifyLog.LogUserName = v.LogUserName;
                        objPatientNotifyLog.NotifyContent = v.NotifyContent;
                        objPatientNotifyLog.LogUserTyped = v.LogUserTyped;
                        objPatientNotifyLog.LogUserNo = v.LogUserNo;
                        objPatientNotifyLog.PatientVisitNo = v.PatientVisitNo;
                        objPatientNotifyLog.PatientNotifyLogNo = v.PatientNotifyLogNo;
                        lst.Add(objPatientNotifyLog);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPatientNotifyLog", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, venueBranchNo, 0);
            }
            return lst;
        }
        #region Discount Approval
        ////
        ///
        public List<GetDiscountApprovalResponse> GetDiscountApprovalDetails(GetDiscountApprovalDto objDTO)
        {
            List<GetDiscountApprovalResponse> lstGetDetails = new List<GetDiscountApprovalResponse>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", objDTO.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", objDTO.UserNo);
                    var _DiscountStatus = new SqlParameter("DiscountStatus", objDTO.DiscountStatus);
                    lstGetDetails = context.GetDiscountApprovalDetails.FromSqlRaw(
                         "Execute dbo.Pro_GetDiscountApprovalDetails @PatientVisitNo,@VenueNo,@VenueBranchNo,@UserNo,@DiscountStatus",
                      _PatientVisitNo, _VenueNo, _VenueBranchNo, _UserID, _DiscountStatus).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetDiscountApprovalDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return lstGetDetails;
        }
        public SaveDiscountApprovalResponse InsertDiscountApprovalDetails(SaveDiscountApprovalDto objDTO)
        {
            SaveDiscountApprovalResponse objOutput = new SaveDiscountApprovalResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", objDTO?.UserNo);
                    var _DiscountStatus = new SqlParameter("DiscountStatus", objDTO?.DiscountStatus);
                    var _DiscountApprovalNo = new SqlParameter("DiscountApprovalNo", objDTO?.DiscountApprovalNo);
                    var _ApproveReason = new SqlParameter("ApproveReason", objDTO?.ApproveReason);
                    var _FromScreen = new SqlParameter("FromScreen", objDTO?.FromScreen);
                    objOutput = context.InsertDiscountApprovalDetails.FromSqlRaw(
                        "Execute dbo.Pro_InsertDiscountApprovalDetails @VenueNo,@VenueBranchNo,@UserNo,@DiscountStatus,@DiscountApprovalNo,@ApproveReason,@FromScreen",
                    _VenueNo, _VenueBranchNo, _UserNo, _DiscountStatus, _DiscountApprovalNo, _ApproveReason, _FromScreen).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertDiscountApprovalDetails/", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return objOutput;
        }
        public dynamic ValidateNricNo(int ServiceNo, string ServiceType, string NricNo, int VenueNo, int VenueBranchNo, bool IsNonConcurrent = false)
        {
            int objresult = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ServiceNo = new SqlParameter("ServiceNo", ServiceNo);
                    var _ServiceType = new SqlParameter("ServiceType", ServiceType);
                    var _NricNo = new SqlParameter("NricNo", NricNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _IsNonConcurrent = new SqlParameter("IsNonConcurrent", IsNonConcurrent);
                    var finalResult = context.ValidateNricNo.FromSqlRaw(
                        "Execute dbo.pro_ValidateNricNo @VenueNo,@VenueBranchNo,@ServiceNo,@ServiceType,@NricNo,@IsNonConcurrent",
                    _VenueNo, _VenueBranchNo, _ServiceNo, _ServiceType, _NricNo, _IsNonConcurrent).ToList();

                    objresult = finalResult.FirstOrDefault().status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.ValidateNricNo/ServiceNo/ServiceType/NricNo - " + ServiceNo + "/" + ServiceType + "/" + NricNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        #endregion Discount Approval

        public MassRegistrationResponse InsertMassRegistration([FromBody] ExternalBulkFile objDTO)
        {
            MassRegistrationResponse result = new MassRegistrationResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument PatientXML = new XDocument(new XElement("PatientXML", from Item in objDTO.patientlst
                                                                                    select
                          new XElement("PatientList",
            new XElement("patientname", Item.patientname),
            new XElement("idNo", Item.idNo),
            new XElement("gender", Item.gender),
            new XElement("dob", Item.dob == "" ? Item.dob : Item.dob.Split('T')[0]),
            new XElement("Email", Item.email),
            new XElement("Contact", Item.contact),
            new XElement("Street", Item.street),
            new XElement("Block", Item.block),
            new XElement("BuildinName", Item.buildingname),
            new XElement("PostalCode", Item.postalcode),
            new XElement("Alternate_email", Item.alternate_email),
            new XElement("Nationality", Item.nationality.ValidateEmpty() == "" ? "UNKNOWN" : Item.nationality)
            )));

                    var _CustomerNo = new SqlParameter("CustomerNo", objDTO.customerno);
                    var _physicianno = new SqlParameter("physicianno", objDTO.physicianno);
                    var _contractno = new SqlParameter("contractno", objDTO.contractno);
                    var _ServiceNo = new SqlParameter("ServiceNo", objDTO.testno);
                    var _ServiceName = new SqlParameter("ServiceName", objDTO.testname);
                    var _ServiceType = new SqlParameter("ServiceType", objDTO.testype);
                    var _FileName = new SqlParameter("FileName", objDTO.filename); ;
                    var _ValidFrom = new SqlParameter("ValidFrom", objDTO.validfrom);
                    var _ValidTo = new SqlParameter("ValidTo", objDTO.validto);
                    var _PatientXML = new SqlParameter("PatientXML", PatientXML.ToString());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", objDTO.UserNo);
                    var _iadditionalrecords = new SqlParameter("additionalrecords", objDTO.iadditionalrecords);


                    result = context.MassRegistrationResponse.FromSqlRaw(
                        "Execute dbo.pro_InsertMassRegistration @CustomerNo,@physicianno,@contractno,@ServiceNo,@ServiceType,@ServiceName,@FileName,@ValidFrom,@ValidTo,@PatientXML,@VenueNo,@VenueBranchNo,@userNo,@additionalrecords",
                    _CustomerNo, _physicianno, _contractno, _ServiceNo, _ServiceType, _ServiceName, _FileName, _ValidFrom, _ValidTo, _PatientXML, _VenueNo, _VenueBranchNo, _UserNo, _iadditionalrecords)?.AsEnumerable()?.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertMassRegistration", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return result;

        }
        public List<MassFileDTO> GetMassFileRegistration(CommonFilterRequestDTO RequestItem)
        {
            List<MassFileDTO> result = new List<MassFileDTO>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    result = context.GetMassFileResponse.FromSqlRaw(
                        "Execute dbo.Pro_GetMassFileRegistration @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PageIndex",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetMassFileRegistration", ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return result;
        }
        public List<massPatientBarcode> DownloadMassFile(int MassFileNo, int VenueNo, int VenueBranchNo)
        {
            List<massPatientBarcode> result = new List<massPatientBarcode>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MassFileNo = new SqlParameter("MassFileNo", MassFileNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    result = context.massPatientBarcodeResponse.FromSqlRaw(
                        "Execute dbo.Pro_GetMassTransactionDownload @MassFileNo,@VenueNo,@VenueBranchNo",
                    _MassFileNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.DownloadMassFile", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        public List<ClinicalHistory> GetClinicalHistory(int venueNo, int venueBranchNo, int patientVisitNo)
        {
            List<ClinicalHistory> lstClinicalHistory = new List<ClinicalHistory>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);

                    var clinicalHistories = context.GetClinicalHistories.FromSqlRaw(
                        "Execute dbo.Pro_GetClinicalHistory @VenueNo, @VenueBranchNo, @PatientVisitNo",
                    _VenueNo, _VenueBranchNo, _PatientVisitNo).ToList();

                    if (clinicalHistories != null || clinicalHistories.Any())
                    {
                        lstClinicalHistory = clinicalHistories?.GroupBy(ch => new { ch.GroupNo, ch.GroupName })
                    .Select(clinicalHistory => new ClinicalHistory
                    {
                        GroupNo = clinicalHistory.Key.GroupNo,
                        GroupName = clinicalHistory.Key.GroupName,
                        ClinicalHistoryMasters = clinicalHistory.Select(ch => new ClinicalHistoryMaster
                        {
                            MasterNo = ch.MasterNo,
                            MasterName = ch.MasterName,
                            ControlType = ch.ControlType,
                            MasterValue = ch.MasterValue
                        }).ToList()

                    }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetClinicalHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return lstClinicalHistory;
        }

        public CommonAdminResponse InsertClinicalHistory(InsertClinicalHistory insertClinicalHistory)
        {
            CommonAdminResponse commonAdminResponse = new CommonAdminResponse();

            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string clinicalHistoryXML = commonUtility.ToXML(insertClinicalHistory);

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClinicalHistoryXML = new SqlParameter("ClinicalHistoryXML", clinicalHistoryXML);

                    commonAdminResponse = context.InsertClinicalHistories.FromSqlRaw("Execute dbo.Pro_InsertClinicalHistory @ClinicalHistoryXML",
                   _ClinicalHistoryXML).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertClinicalHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertClinicalHistory.VenueNo, insertClinicalHistory.VenueBranchNo, insertClinicalHistory.PatientVisitNo);
            }
            return commonAdminResponse;
        }
        public List<Tblloyal> getloyalcard(TblloyalReq req)
        {
            List<Tblloyal> objresult = new List<Tblloyal>();
            if (!string.IsNullOrEmpty(req.loyalcardno))
            {
                if (req.loyalcardno.StartsWith("LTM"))
                {
                    req.loyalcardno = req.loyalcardno.Substring(3);
                }
                // If it not starts with "LTM", no need to modify it
            }
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _loyalcardno = new SqlParameter("loyalcardno", req.loyalcardno);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);

                    objresult = context.getloyalcard.FromSqlRaw(
                        "Execute dbo.Pro_GetLoyaltyType @loyalcardno, @venueNo, @venuebranchno",
                   _loyalcardno, _venueNo, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.getloyalcard - " + req.loyalcardno, ExceptionPriority.High, ApplicationType.REPOSITORY, (int)req.venueNo, (int)req.venuebranchno, 0);
            }
            return objresult;
        }

        public PatientVisitPatternIDGenRes GetVisitPatternID(PatientVisitPatternIDGenReq req)
        {
            PatientVisitPatternIDGenRes objresult = new PatientVisitPatternIDGenRes();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatternType = new SqlParameter("PatternType", req.PatternType);                 
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    objresult = context.GetVisitPatternID.FromSqlRaw(
                        "Execute dbo.pro_GetPatternIDNew @PatternType,@VenueNo,@VenueBranchNo",
                    _PatternType, _VenueNo, _VenueBranchNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetVisitPatternID/", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<PreBookingtDTO> GetPreBookingDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PreBookingtDTO> lstPreBookingtDTO = new List<PreBookingtDTO>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate.ValidateEmpty());
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate.ValidateEmpty());
                    var _Type = new SqlParameter("Type", RequestItem.Type.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _BookingType = new SqlParameter("BookingType", RequestItem.BookingType);
                    var _ResourceNo = new SqlParameter("ResourceNo", RequestItem.ResourceNo);
                    var _BookingStatus = new SqlParameter("BookingStatus", RequestItem.BookingStatus);
                    var _ViewType = new SqlParameter("ViewType", RequestItem.ViewType);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    lstPreBookingtDTO = context.GetPreBookingDetails.FromSqlRaw(
                        "Execute dbo.pro_GetPreBookingDetails @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@BookingType,@ResourceNo,@BookingStatus,@ViewType,@PageIndex",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _BookingType, _ResourceNo, _BookingStatus, _ViewType, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetPreBookingDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPreBookingtDTO;
        }
        public PreBookingtResponse InsertPreBookingDetails(PreBookingtRequest objDTO)
        {
            PreBookingtResponse resultPreBookingtResponse = new PreBookingtResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PreBookingQueueNo = new SqlParameter("PreBookingQueueNo", objDTO.PreBookingQueueNo);
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
                    var _SecondaryEmailID = new SqlParameter("SecondaryEmailID", objDTO.SecondaryEmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO.Pincode.ValidateEmpty());
                    var _BookingType = new SqlParameter("BookingType", objDTO.BookingType);
                    var _ResourceTypeNo = new SqlParameter("ResourceTypeNo", objDTO.ResourceTypeNo);
                    var StartDatetime = new SqlParameter("StartDatetime", objDTO.StartDatetime.ValidateEmpty());
                    var _EndDateTime = new SqlParameter("EndDateTime", objDTO.EndDateTime.ValidateEmpty());
                    var _BookingStatus = new SqlParameter("BookingStatus", objDTO.BookingStatus);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", objDTO.userNo);

                    resultPreBookingtResponse = context.PreBookingtResponse.FromSqlRaw("Execute dbo.pro_InsertPreBooking @PreBookingQueueNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@MobileNumber,@WhatsappNo," +
                   "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@BookingType,@ResourceTypeNo,@StartDatetime,@EndDateTime,@BookingStatus,@VenueNo,@VenueBranchNo,@UserNo",
                   _PreBookingQueueNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _MobileNumber, _AltMobileNumber, _EmailID,
                   _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _BookingType, _ResourceTypeNo, StartDatetime, _EndDateTime, _BookingStatus, _VenueNo, _VenueBranchNo, _UserNo)?.AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.InsertPreBookingDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.userNo);
            }
            return resultPreBookingtResponse;
        }

        public AutoLoyaltyIDGenResponse GetLoyaltyCardPatternID(AutoLoyaltyIDGenRequest req)
        {
            AutoLoyaltyIDGenResponse objresult = new AutoLoyaltyIDGenResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _DiscountNo = new SqlParameter("DiscountNo", req.DiscountNo);

                    var result = context.GetLoyaltyCardPatternID.FromSqlRaw(
                        "Execute dbo.pro_GetLoyaltyPatternID @DiscountNo,@VenueNo, @VenueBranchNo",
                     _DiscountNo,_VenueNo, _VenueBranchNo).ToList();
                    if (objresult != null)
                    {
                        objresult = result?.AsEnumerable()?.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetLoyaltyCardPatternID", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
    }
}
