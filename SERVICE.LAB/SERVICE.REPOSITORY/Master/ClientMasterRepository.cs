using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Serilog;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Service.Model.Integration;

namespace Dev.Repository
{
    public class ClientMasterRepository : IClientMasterRepository
    {
        private IConfiguration _config;
        public ClientMasterRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get ClientMaster Details
        /// </summary>
        /// <returns></returns>
        public List<CustomerResponse> GetClientMasterDetails(GetCustomerRequest getCustomerRequest)
        {
            List<CustomerResponse> objresult = new List<CustomerResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getCustomerRequest.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getCustomerRequest.venueBranchNo);
                    var _VisitNo = new SqlParameter("CustomerNo", getCustomerRequest.customerNumber);
                    var _IsFranchisee = new SqlParameter("IsFranchisee", getCustomerRequest.IsFranchisee);
                    var _PageIndex = new SqlParameter("PageIndex", getCustomerRequest.pageIndex);
                    var _custType = new SqlParameter("custType", getCustomerRequest.custType);
                    var _PayType = new SqlParameter("PayType", getCustomerRequest.PayType);
                    var _IsApproval = new SqlParameter("IsApproval", getCustomerRequest.IsApproval);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", getCustomerRequest?.viewvenuebranchno == null ? 0 : getCustomerRequest.viewvenuebranchno);

                    objresult = context.GetClientMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetCustomer @VenueNo, @VenueBranchNo, @CustomerNo, @IsFranchisee, @PageIndex, @custType, @PayType, @IsApproval,@viewvenuebranchno",
                    _VenueNo, _VenueBranchNo, _VisitNo, _IsFranchisee, _PageIndex, _custType, _PayType, _IsApproval, _viewvenuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetClientMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, getCustomerRequest.venueNo, getCustomerRequest.venueBranchNo, getCustomerRequest.userNo);
            }
            return objresult;
        }

        /// <summary>
        /// Search ClientMaster
        /// </summary>
        /// <param name="ClientMasterName"></param>
        /// <returns></returns>
        public List<TblCustomer> SearchClientMaster(string ClientMasterName)
        {
            List<TblCustomer> objresult = new List<TblCustomer>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblCustomer.Where(a => a.CustomerName == ClientMasterName).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.SearchClientMaster - " + ClientMasterName, ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Insert ClientMaster Details
        /// </summary>
        /// <param name="ClientMasteritem"></param>
        /// <returns></returns>
        public InsertCustomerResponse InsertClientMasterDetails(PostCustomerMaster postcustomerDTO)
        {
            InsertCustomerResponse result = new InsertCustomerResponse();
            TblCustomer ClientMasteritem = postcustomerDTO?.tblcustomer;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblCustomer.Where(a => a.UserName == ClientMasteritem.UserName && a.CustomerNo != ClientMasteritem.CustomerNo && a.Status == true && a.VenueBranchNo == ClientMasteritem.VenueBranchNo).Select(x => new { x.CustomerNo }).FirstOrDefault();
                    if (userresult != null)
                    {
                        result.CustomerNo = 0;
                        return result;
                    }
                }
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    var encodingPassword = CommonSecurity.EncodePassword(ConfigKeys.Defaultpassword, CommonSecurity.GeneratePassword(1));
                    ClientMasteritem.Password = encodingPassword;
                    string clientDetails = commonUtility.ToXML(ClientMasteritem);

                    var _ClientDetails = new SqlParameter("ClientDetails", clientDetails);
                    var _VenueNo = new SqlParameter("VenueNo", ClientMasteritem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", ClientMasteritem.VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", ClientMasteritem.CreatedBy);
                    var _dashBoardJson = new SqlParameter("dashBoardJson", postcustomerDTO?.dashBoardDetailsJson);
                    var _IsApproval = new SqlParameter("IsApproval", ClientMasteritem?.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", ClientMasteritem?.IsReject);
                    var _RejectReason = new SqlParameter("RejectReason", ClientMasteritem?.RejectReason);
                    var _OldCustomerNo = new SqlParameter("OldCustomerNo", ClientMasteritem?.OldCustomerNo);

                    var dbResponse = context.InsertClientMaster.FromSqlRaw(
                    "Execute dbo.Pro_InsertClientMaster @VenueNo, @VenueBranchNo, @UserNo, @ClientDetails, @dashBoardJson, @IsApproval, @IsReject, @RejectReason, @OldCustomerNo",
                    _VenueNo, _VenueBranchNo, _UserID, _ClientDetails, _dashBoardJson, _IsApproval, _IsReject, _RejectReason, _OldCustomerNo).AsEnumerable().FirstOrDefault();

                    result = dbResponse;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.InsertClientMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, ClientMasteritem.VenueNo, ClientMasteritem.VenueBranchNo, ClientMasteritem.CreatedBy);
            }
            return result;
        }
        public int InsertClientSubMaster(PostCustomersubuserMaster postcustomerDTO)
        {
            int result = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string clientdetails = string.Empty;
                    var customer = postcustomerDTO.tblsubcustomer.Where(x => x.isadd == true).Select(x => x.CustomerNo).ToList();
                    if (customer.Count > 0)
                    {
                        clientdetails = string.Join(",", customer);
                    }

                    var _CustomerSubUserNo = new SqlParameter("CustomerSubUserNo", postcustomerDTO?.CustomerSubUserNo);
                    var _LoginName = new SqlParameter("LoginName", postcustomerDTO?.LoginName);
                    var _userName = new SqlParameter("userName", postcustomerDTO?.userName);
                    var _Email = new SqlParameter("Email", postcustomerDTO?.Email);
                    var _PhoneNo = new SqlParameter("PhoneNo", postcustomerDTO?.PhoneNo);
                    var _status = new SqlParameter("Status", postcustomerDTO?.status);
                    var _ClientDetails = new SqlParameter("ClientDetails", clientdetails);
                    var _VenueNo = new SqlParameter("VenueNo", postcustomerDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", postcustomerDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", postcustomerDTO.CreatedBy);

                    result = context.InsertClientsubuserMaster.FromSqlRaw(
                    "Execute dbo.Pro_InsertClientsubuserMaster @CustomerSubUserNo,@LoginName,@userName,@Email,@PhoneNo,@Status,@ClientDetails,@VenueNo,@VenueBranchNo,@UserNo",
                    _CustomerSubUserNo, _LoginName, _userName, _Email, _PhoneNo, _status, _ClientDetails, _VenueNo, _VenueBranchNo, _UserID).AsEnumerable().FirstOrDefault().status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.InsertClientSubMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, postcustomerDTO.VenueNo, postcustomerDTO.VenueBranchNo, postcustomerDTO.CreatedBy);
            }
            return result;
        }
        public int InsertSubClientMasterDetails(List<CustomerMappingDTO> subclient, int VenueNo, int VenueBranchNo, int UserID, int CustomerNo, int IsApproval, bool IsReject)
        {
            int result = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    string clientDetails = commonUtility.ToXML(subclient);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", UserID);
                    var _CustomerNo = new SqlParameter("CustomerNo", CustomerNo);
                    var _ClientDetails = new SqlParameter("ClientDetails", clientDetails);
                    var _IsApproval = new SqlParameter("IsApproval", IsApproval);
                    var _IsReject = new SqlParameter("IsReject", IsReject);
                    
                    var dbResponse = context.InsertSubClientMapping.FromSqlRaw(
                    "Execute dbo.Pro_InsertSubClientMapping @VenueNo,@VenueBranchNo,@UserNo,@CustomerNo,@ClientDetails,@IsApproval,@IsReject",
                     _VenueNo, _VenueBranchNo, _UserID, _CustomerNo, _ClientDetails, _IsApproval, _IsReject).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.InsertSubClientMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, UserID);
            }
            return result;
        }

        /// <summary>
        /// GetSubCustomerDetailbyCustomer
        /// </summary>
        /// <param name="CustomerNo"></param>
        /// <param name="VenueNo"></param>
        /// <param name="VenueBranchNo"></param>
        /// <returns></returns>
        public List<CustomerMappingDTO> GetSubCustomerDetailbyCustomer(int CustomerNo, int VenueNo, int VenueBranchNo, int IsApproval)
        {
            List<CustomerMappingDTO> objresult = new List<CustomerMappingDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _CustomerNo = new SqlParameter("CustomerNo", CustomerNo);
                    var _IsApproval = new SqlParameter("IsApproval", IsApproval);

                    objresult = context.GetCustomerMapping.FromSqlRaw(
                    "Execute dbo.Pro_GetCustomerMapping @VenueNo,@VenueBranchNo,@CustomerNo,@IsApproval",
                    _VenueNo, _VenueBranchNo, _CustomerNo, _IsApproval).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetSubCustomerDetailbyCustomer", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<CustomerMappingDTO> GetSubClinic(int CustomerNo, int VenueNo, int VenueBranchNo)
        {
            List<CustomerMappingDTO> objresult = new List<CustomerMappingDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _CustomerNo = new SqlParameter("CustomerNo", CustomerNo);
                 
                    objresult = context.GetCLinic.FromSqlRaw(
                    "Execute dbo.Pro_GetCLinic @VenueNo,@VenueBranchNo,@CustomerNo",
                    _VenueNo, _VenueBranchNo, _CustomerNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetSubClinic", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<ClientSubClientMappingDTO> GetAllClientBySubClinic(int VenueNo, int VenueBranchNo)
        {
            List<ClientSubClientMappingDTO> objresult = new List<ClientSubClientMappingDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());

                    objresult = context.GetAllClients.FromSqlRaw(
                       "Execute dbo.Pro_GetAllClientBySubCLinic @VenueNo,@VenueBranchNo",
                    _VenueNo, _VenueBranchNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSubClinic", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<ClientSubUserResponse> GetclientSubUser(GetCustomerRequest request)
        {
            List<ClientSubUserResponse> objresult = new List<ClientSubUserResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueNo", request.venueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchno", request.venueBranchNo);

                    objresult = context.GetClientSubUserResponse.FromSqlRaw(
                    "Execute dbo.pro_GetClientSubUser @venueNo,@venueBranchno",
                    _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetclientSubUser", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueNo, request.venueBranchNo, 0);
            }
            return objresult;
        }
        public ClientRestrictionDayResponse GetClientRestrictionDayIsValid(ClientRestrictionDay ObjRequest)
        {
            int ClientNumber = ObjRequest.ClientNumber;
            int RestrictionDays = ObjRequest.RestrictionDays;
            int VenueNo = ObjRequest.VenueNo;
            int VenueBranchNo = ObjRequest.VenueBranchNo;
            ClientRestrictionDayResponse objresult = new ClientRestrictionDayResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClientNumber = new SqlParameter("ClientNumber", ClientNumber);
                    var _RestrictionDays = new SqlParameter("RestrictionDays", RestrictionDays);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    objresult = context.ClientRestriction.FromSqlRaw(
                    "Execute dbo.pro_GetClientRestrictionDayIsValid @ClientNumber,@RestrictionDays,@VenueNo,@VenueBranchNo",
                    _ClientNumber, _RestrictionDays, _VenueNo, _VenueBranchNo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetClientRestrictionDayIsValid/ClientNumber/RestrictionDays-" + ClientNumber + "/" + RestrictionDays, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public int DocumentUploadDetails(List<DocumentUploadlst> ClientDocument, int VenueNo, int VenueBranchNo, int UserID, int CustomerNo)
        {
            int result = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string clientDocument = commonUtility.ToXML(ClientDocument);
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClientDocument = new SqlParameter("clientDocument", clientDocument);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _UserID = new SqlParameter("CreatedBy", UserID);
                    var _CustomerNo = new SqlParameter("CustomerNo", CustomerNo);

                    var dbResponse = context.DocumentUploadDetails.FromSqlRaw(
                    "Execute dbo.Pro_InsertClientDocument @ClientDocument, @VenueNo, @VenueBranchNo, @CreatedBy, @CustomerNo",
                    _ClientDocument, _VenueNo, _VenueBranchNo, _UserID, _CustomerNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.DocumentUploadDetails - " + CustomerNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, 0, UserID);
            }
            return result;
        }
        public List<ClientDocUploadDetailRes> GetClientDocumentDetails(ClientDocUploadReq Req)
        {
            List<ClientDocUploadDetailRes> lstOutput = new List<ClientDocUploadDetailRes>();
            List<PhysicianDocUploadRes> objresult = new List<PhysicianDocUploadRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _EntityType = new SqlParameter("EntityType", Req.EntityType);
                    var _EntityNo = new SqlParameter("EntityNo", Req.EntityNo);
                    var _venueNo = new SqlParameter("venueNo", Req.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req.venueBranchNo);

                    objresult = context.GetClientDocumentDetails.FromSqlRaw(
                    "Execute dbo.pro_GetEntityDocument @EntityType, @EntityNo, @venueNo, @venueBranchNo",
                    _EntityType, _EntityNo, _venueNo, _VenueBranchNo).ToList();

                    if (objresult != null && objresult.Count > 0)
                    {
                        foreach (var v in objresult)
                        {
                            ClientDocUploadDetailRes obj = new ClientDocUploadDetailRes();
                            obj.documentNo = v.documentNo;
                            obj.documentType = v.documentType;
                            obj.documentTypeCode = v.documentTypeCode;
                            obj.CustomerNo = v.physicianNo;
                            obj.clientfileUpload = new List<ClientFileUpload>();
                            obj.clientfileUpload = GetClientUploadDetaile(Req.venueNo, Req.venueBranchNo, v.physicianNo, v.documentTypeCode);
                            lstOutput.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetClientDocumentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, Req.venueNo, Req.venueBranchNo, 0);
            }
            return lstOutput;
        }
        public List<ClientFileUpload> GetClientUploadDetaile(int venueNo, int venueBranchNo, int physicianNo, int documentTypeCode)
        {
            List<ClientFileUpload> lstresult = new List<ClientFileUpload>();
            ClientFileUpload result = new ClientFileUpload();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            string FilePath = string.Empty;
            try
            {
                objAppSettingResponse = new AppSettingResponse();
                string AppUploadClientDoc = "UploadClientDoc";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadClientDoc);
                string Pathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.UploadClientDoc);
                var visitId = documentTypeCode;
                var venueno = venueNo;
                var venuebNo = venueBranchNo;
                var visitno = physicianNo;
                var format = "";
                string folderName = venueno + "\\" + venuebNo + "\\" + visitno + "\\" + visitId;
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            //string FullPath = newPath + "\\" + venueNo + "_" + venuebNo + "_" + visitId + "." + format;
                            result = new ClientFileUpload();
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
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterRepository.GetClientUploadDetaile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return lstresult;
        }
    }
}

