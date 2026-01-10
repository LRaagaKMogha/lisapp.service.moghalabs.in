using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class DocVsServiceMapRepository : IDocVsServiceMapRepository
    {
        private IConfiguration _config;
        public DocVsServiceMapRepository(IConfiguration config) { _config = config; }

        public List<DocVsSerResponse> Getdoctorlst(DocVsSerRequest Req)
        {
            List<DocVsSerResponse> objresult = new List<DocVsSerResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DoctorNo = new SqlParameter("DoctorNo", Req?.DoctorNo);
                    var _venueNo = new SqlParameter("venueNo", Req?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", Req?.pageIndex);

                    objresult = context.Getdoctorlst.FromSqlRaw(
                        "Execute dbo.pro_GetDoctorDetails @DoctorNo,@venueNo,@pageIndex",
                         _DoctorNo, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.Getdoctorlst" + Req?.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.venueNo ?? 0, 0);
            }
            return objresult;
        }
        public List<DocVsSerGetRes> GetdocVsSerlst(DocVsSerGetReq Req)
        {
            List<DocVsSerGetRes> objresult = new List<DocVsSerGetRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", Req?.venueNo);
                    var _DoctorNo = new SqlParameter("DoctorNo", Req?.DoctorNo);
                    var _DeptNo = new SqlParameter("DeptNo", Req?.DeptNo);
                    var _ServiceType = new SqlParameter("ServiceType", Req?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", Req?.ServiceNo);

                     objresult = context.GetdocVsSerlst.FromSqlRaw(
                        "Execute dbo.pro_GetDocVsSerDetails @DoctorNo,@DeptNo,@ServiceType,@ServiceNo,@VenueNo",
                          _venueNo, _DoctorNo, _DeptNo, _ServiceType, _ServiceNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.GetdocVsSerlst" + Req?.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.venueNo,0, 0);
            }
            return objresult;
        }

        public int InsertdocVsSer(DocVsSerInsReq Req)
        {
          
            CommonHelper commonUtility = new CommonHelper();
            string DocVsSerXML = commonUtility.ToXML(Req.getdoclst);
            int i = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DocVsSerXML = new SqlParameter("DocVsSerXML",DocVsSerXML);

                    var _DoctorNo = new SqlParameter("DoctorNo", Req?.DoctorNo);                    
                    var _venuebranchno = new SqlParameter("venuebranchno", Req?.venuebranchno);
                    var _venueNo = new SqlParameter("venueNo", Req?.venueNo);
                    var _userno = new SqlParameter("userno", Req?.userno);

                    var obj = context.InsertdocVsSer.FromSqlRaw(
                        "Execute dbo.pro_InsertDocVsSerDetails @DocVsSerXML,@DoctorNo,@venueNo,@venuebranchno,@userno",
                         _DocVsSerXML, _DoctorNo, _venueNo, _venuebranchno,_userno).ToList();
                     i= obj[0].DoctorServiceNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.InsertdocVsSer" + Req?.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.venueNo, Req?.venuebranchno, Req?.userno);
            }
            return i;
        }
        public List<DocVsSerAppRes> GetdocVsSerApproval(DocVsSerAppReq Req)
        {
            List<DocVsSerAppRes> objresult = new List<DocVsSerAppRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ApprovedBy = new SqlParameter("ApprovedBy", Req?.ApprovedBy);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req?.VenueBranchNo);
                    var _Type = new SqlParameter("Type", Req?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", Req?.VenueNo);
                    var _Fromdate = new SqlParameter("Fromdate", Req?.Fromdate);
                    var _ToDate = new SqlParameter("ToDate", Req?.ToDate);

                    objresult = context.GetdocVsSerApproval.FromSqlRaw(
                        "Execute dbo.pro_getInternalDoctorVsAppraisalVsTransaction @ApprovedBy,@VenueNo,@VenueBranchNo,@Type,@Fromdate,@ToDate",
                         _ApprovedBy, _VenueNo, _VenueBranchNo, _Type, _Fromdate, _ToDate).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.GetdocVsSerApproval" + Req?.ApprovedBy.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.VenueNo, Req?.VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<DocVsSerAppdetailsRes> GetdocVsSerAppDetails(DocVsSerAppdetailsReq Req)
        {
            List<DocVsSerAppdetailsRes> objresult = new List<DocVsSerAppdetailsRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DoctorNo = new SqlParameter("DoctorNo", Req?.DoctorNo);
                    var _VenueNo = new SqlParameter("VenueNo", Req?.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", Req?.pageIndex);

                    objresult = context.GetdocVsSerAppDetails.FromSqlRaw(
                        "Execute dbo.pro_GetDocVsSerVsProfCharge @DoctorNo,@VenueNo,@pageIndex",
                         _DoctorNo, _VenueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.GetdocVsSerAppDetails" + Req?.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.VenueNo, 0, 0);
            }
            return objresult;
        }
        public int InsertdocVsSerProf(DocVsSerProfInsReq Req)
        {

            int i = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string DocVsSerProfXML = commonUtility.ToXML(Req?.getdocVsSerlst);
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DocVsSerProfXML = new SqlParameter("DocVsSerProfXML", DocVsSerProfXML);
                    var _DoctorNo = new SqlParameter("DoctorNo", Req?.DoctorNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", Req?.venuebranchno);
                    var _venueNo = new SqlParameter("venueNo", Req?.venueNo);
                    var _userno = new SqlParameter("userno", Req?.userno);

                    var obj = context.InsertdocVsSerProf.FromSqlRaw(
                        "Execute dbo.pro_InsertDocVsSerProfDetails @DocVsSerProfXML,@DoctorNo,@venueNo,@venuebranchno,@userno",
                         _DocVsSerProfXML, _DoctorNo, _venueNo, _venuebranchno, _userno).FirstOrDefault();
                    i = obj?.DoctorProfMastNo ?? 0;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapRepository.InsertdocVsSerProf" + Req?.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req?.venueNo, Req?.venuebranchno, Req?.userno);
            }
            return i;
        }

    }
}