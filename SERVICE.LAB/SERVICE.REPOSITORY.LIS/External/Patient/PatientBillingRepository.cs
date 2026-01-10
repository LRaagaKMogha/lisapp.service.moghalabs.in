using Dev.IRepository.External.Patient;
using DEV.Common;
using DEV.Model.EF.External.Patient;
using DEV.Model.External.Billing;
using DEV.Model.External.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.Patient
{
    public class PatientBillingRepository : IPatientBillingRepository
    {
        private IConfiguration _config;
        public PatientBillingRepository(IConfiguration config) { _config = config; }

        public LstPatientBillingInfo GetPatientBillInfo(int pVenueNo, int pVenueBranchNo, int pVisitNo)
        {
            List<LstPatientBillingInfo> objResponse = new List<LstPatientBillingInfo>();
            LstPatientBillingInfo objFinalResponse = new LstPatientBillingInfo();

            BillServiceDetails billService = new BillServiceDetails();
            List<BillServiceDetails> lstBillService = new List<BillServiceDetails>();

            BillPaymentDetails billPaymentDetails = new BillPaymentDetails();
            List<BillPaymentDetails> lstbillPaymentDetails = new List<BillPaymentDetails>();

            try
            {
                using (var context = new PatientBillingContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _visitNo = new SqlParameter("PatientVisitNo", pVisitNo);

                    var response = context.FetchPatientBillingInformation.FromSqlRaw(
                           "Execute dbo.Pro_Ex_GetPatientBillingInfo " +
                           " @VenueNo, @VenueBranchNo, @PatientVisitNo",
                             _venueNo, _venueBranchNo, _visitNo).ToList();

                    objResponse = response;

                    string patientDetails = JsonConvert.SerializeObject(objResponse.FirstOrDefault());
                    objFinalResponse = JsonConvert.DeserializeObject<LstPatientBillingInfo>(patientDetails);

                    foreach (var patientdetail in response)
                    {
                        billService = new BillServiceDetails();
                        billService.serviceNo = patientdetail.serviceNo;
                        billService.serviceName = patientdetail.serviceName;
                        billService.serviceType = patientdetail.serviceType;
                        billService.amount = patientdetail.rate;
                        billService.discount = patientdetail.discount;
                        billService.net = patientdetail.net;

                        lstBillService.Add(billService);
                    }
                    objFinalResponse.servicelist = lstBillService;

                    var paymentResponse = context.FetchPatientBillingPayInformation.FromSqlRaw(
                       "Execute dbo.Pro_Ex_GetPatientBillPaymentInfo " +
                       "@VenueNo,@VenueBranchNo,@PatientVisitNo",
                        _venueNo, _venueBranchNo, _visitNo).ToList();

                    lstbillPaymentDetails = paymentResponse;
                    objFinalResponse.payments = lstbillPaymentDetails;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientBillingRepository.GetPatientBillInfo", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objFinalResponse;
        }

        public LstPatientCancelBillingInfo GetPatientCancelBillInfo(int pVenueNo, int pVenueBranchNo, int pVisitNo)
        {
            List<LstPatientCancelBillingInfo> objResponse = new List<LstPatientCancelBillingInfo>();
            LstPatientCancelBillingInfo objFinalResponse = new LstPatientCancelBillingInfo();

            CancelBillServiceDetails billService = new CancelBillServiceDetails();
            List<CancelBillServiceDetails> lstBillService = new List<CancelBillServiceDetails>();

            CancelBillPaymentDetails billPaymentDetails = new CancelBillPaymentDetails();
            List<CancelBillPaymentDetails> lstbillPaymentDetails = new List<CancelBillPaymentDetails>();

            try
            {
                using (var context = new PatientBillingContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _visitNo = new SqlParameter("PatientVisitNo", pVisitNo);

                    var response = context.FetchPatientCancelBillingInformation.FromSqlRaw(
                           "Execute dbo.Pro_Ex_GetPatientCancelBillingInfo" +
                           " @VenueNo, @VenueBranchNo, @PatientVisitNo",
                             _venueNo, _venueBranchNo, _visitNo).ToList();

                    objResponse = response;

                    string patientDetails = JsonConvert.SerializeObject(objResponse.FirstOrDefault());
                    objFinalResponse = JsonConvert.DeserializeObject<LstPatientCancelBillingInfo>(patientDetails);

                    foreach (var patientdetail in response)
                    {
                        billService = new CancelBillServiceDetails();
                        billService.serviceName = patientdetail.serviceName;
                        billService.serviceType = patientdetail.serviceType;
                        billService.amount = patientdetail.rate;
                        billService.discount = patientdetail.discount;
                        billService.net = patientdetail.net;

                        lstBillService.Add(billService);
                    }
                    objFinalResponse.servicelist = lstBillService;

                    var paymentResponse = context.FetchPatientCancelBillingPayInformation.FromSqlRaw(
                       "Execute dbo.Pro_Ex_GetPatientCancelBillPaymentInfo " +
                       " @VenueNo,@VenueBranchNo,@PatientVisitNo",
                        _venueNo, _venueBranchNo, _visitNo).ToList();

                    lstbillPaymentDetails = paymentResponse;
                    objFinalResponse.payments = lstbillPaymentDetails;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientBillingRepository.GetPatientCancelBillInfo", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objFinalResponse;
        }

        public List<LstCancelPatientInfo> GetCancelServiceDetails(int pVenueNo, int pVenueBranchNo, string pDtFrom, string pDtTo)
        {
            List<LstCancelPatientInfo> objFinalResponse = new List<LstCancelPatientInfo>();
            List<LstCancelPatientInfo> objListProcess = new List<LstCancelPatientInfo>();
            LstCancelPatientInfo objProcess = new LstCancelPatientInfo();

            try
            {
                using (var context = new PatientBillingContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _dtFrom = new SqlParameter("FromDate", pDtFrom);
                    var _dtTo = new SqlParameter("ToDate", pDtTo);

                    var response = context.FetchCancelBillingInformation.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetCancelServiceInfo" +
                           " @VenueNo, @VenueBranchNo, @FromDate, @ToDate ",
                             _venueNo, _venueBranchNo, _dtFrom, _dtTo).ToList();

                    string patientDetails = JsonConvert.SerializeObject(objListProcess);

                    foreach (var patientdetail in response.ToList())
                    {
                        objProcess = new LstCancelPatientInfo();
                        objProcess.patientId = patientdetail.patientId;
                        objProcess.patientName = patientdetail.patientName;
                        objProcess.visitNo = patientdetail.visitNo;
                        objProcess.visitId = patientdetail.visitId;
                        objProcess.visitDtTm = patientdetail.visitDtTm;
                        objProcess.referredBy = patientdetail.referredBy;
                        objProcess.discount = patientdetail.discount;
                        objProcess.net = patientdetail.net;
                        objProcess.servicesList = JsonConvert.DeserializeObject<List<LstCancelServiceList>>(patientdetail.listOfServices);

                        objListProcess.Add(objProcess);
                    }
                    objFinalResponse = objListProcess;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientBillingRepository.GetPatientCancelBillInfo", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objFinalResponse;
        }
    }
}
