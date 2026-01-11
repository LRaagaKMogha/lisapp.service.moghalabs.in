using Service.Model;
using Service.Model.EF;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Common;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Dev.Repository
{
    public class quotationRepository : IquotationRepository
    {
        private IConfiguration _config;
        public quotationRepository(IConfiguration config) { _config = config; }

        public List<returnquotationlst> Getquotation(requestquotation req)
        {
            List<returnquotationlst> lst = new List<returnquotationlst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req ?.venuebranchno);
                    var _quotationMasterNo = new SqlParameter("quotationMasterNo", req ?.quotationMasterNo);
                    var _pageIndex = new SqlParameter("pageIndex", req?.pageIndex);

                    lst = context.Getquotation.FromSqlRaw(
                        "Execute dbo.pro_GetServiceQuotation @venueno,@venuebranchno,@QuotationMasterNo,@pageIndex",
                         _venueno, _venuebranchno, _quotationMasterNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "quotationRepository.Getquotation" + req.quotationMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }


        public int Insertquotation(responselst req1)
        {
            CommonHelper commonUtility = new CommonHelper();
            string TestXML = commonUtility.ToXML(req1.gettestlst);
            int i = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _quotationMasterNo = new SqlParameter("quotationMasterNo", req1 ?.quotationMasterNo);
                    var _quotationDtTm = new SqlParameter("quotationDtTm", req1 ?.quotationDtTm);
                    var _quotationId = new SqlParameter("quotationId", req1 ?.quotationId);
                    var _patientNo = new SqlParameter("patientNo", req1 ?.patientNo);
                    var _titleCode = new SqlParameter("titleCode", req1?.titleCode);
                    var _firstName = new SqlParameter("firstName", req1?.firstName);
                    var _middleName = new SqlParameter("middleName", req1?.middleName);
                    var _lastName = new SqlParameter("lastName", req1?.lastName);
                    var _fullName = new SqlParameter("fullName", req1?.fullName);
                    var _gender = new SqlParameter("gender", req1?.gender);
                    var _dOB = new SqlParameter("dOB", req1?.dOB);
                    var _ageY = new SqlParameter("ageY", req1?.ageY);
                    var _ageM = new SqlParameter("ageM", req1?.ageM);
                    var _ageD = new SqlParameter("ageD", req1?.ageD);
                    var _mobileNumber = new SqlParameter("mobileNumber", req1?.mobileNumber);
                    var _emailID = new SqlParameter("emailID", req1?.emailID);
                    var _venueno = new SqlParameter("venueno", req1?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req1?.venuebranchno);
                    var _status = new SqlParameter("status", req1?.status);
                    var _userno = new SqlParameter("userno", req1?.userno);
                    var _refferralTypeNo = new SqlParameter("refferralTypeNo", req1?.refferralTypeNo);
                    var _customerNo = new SqlParameter("customerNo", req1?.customerNo);
                    var _physicianNo = new SqlParameter("physicianNo", req1?.physicianNo);
                    var _physicianName = new SqlParameter("physicianName", req1?.physicianName);
                    var _customerName = new SqlParameter("customerName", req1?.customerName);
                    var _TestXML = new SqlParameter("TestXML", TestXML);


                    var lst = context.Insertquotation.FromSqlRaw(
                       "Execute dbo.pro_InsertServiceQuotation @quotationMasterNo,@quotationDtTm,@quotationId,@patientNo," +
                       "@titleCode,@firstName,@middleName,@lastName,@fullName,@gender,@dOB ,@ageY,@ageM,@ageD,@mobileNumber,@emailId,@venueno,@venuebranchno,@status,@userno,@refferralTypeNo,@customerNo,@physicianNo,@physicianName,@customerName,@TestXML",
                       _quotationMasterNo, _quotationDtTm, _quotationId, _patientNo, _titleCode, _firstName, _middleName, _lastName, _fullName, _gender, _dOB,
                       _ageY, _ageM, _ageD, _mobileNumber, _emailID, _venueno, _venuebranchno, _status, _userno,
                       _refferralTypeNo, _customerNo, _physicianNo, _physicianName, _customerName, _TestXML).ToList();

                    i = lst[0].quotationMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "quotationRepository.Insertquotation" + req1.quotationMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req1.venueno, req1.venuebranchno, req1.userno);
            }
            return i;
        }





    }
}