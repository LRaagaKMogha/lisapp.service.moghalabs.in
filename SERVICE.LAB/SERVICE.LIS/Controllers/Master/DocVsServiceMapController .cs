using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class DocVsServiceMapController : ControllerBase
    {
        private readonly IDocVsServiceMapRepository _DocVsServiceMapRepository;
        public DocVsServiceMapController(IDocVsServiceMapRepository noteRepository)
        {
            _DocVsServiceMapRepository = noteRepository;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/Getdoctorlst")]
        public List<DocVsSerResponse> Getdoctorlst(DocVsSerRequest Req)
        {
             List<DocVsSerResponse> result = new List<DocVsSerResponse>();
            try
            {               
                result= _DocVsServiceMapRepository.Getdoctorlst(Req);             
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.Getdoctorlst" + Req.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, 0, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/GetdocVsSerlst")]
        public List<DocVsSerGetRes> GetdocVsSerlst(DocVsSerGetReq Req)
        {
            List<DocVsSerGetRes> objresult = new List<DocVsSerGetRes>();
            try
            {
                objresult = _DocVsServiceMapRepository.GetdocVsSerlst(Req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.GetdocVsSerlst" + Req.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/InsertdocVsSer")]
        public int InsertdocVsSer(DocVsSerInsReq Req)
        {
            int DoctorServiceNo = 0;
            try
            {
                DoctorServiceNo = _DocVsServiceMapRepository.InsertdocVsSer(Req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.InsertdocVsSer" + Req.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venuebranchno, Req.userno);
            }
            return DoctorServiceNo;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/GetdocVsSerApproval")]
        public List<DocVsSerAppRes> GetdocVsSerApproval(DocVsSerAppReq Req)
        {
            List<DocVsSerAppRes> result = new List<DocVsSerAppRes>();
            try
            {
                result = _DocVsServiceMapRepository.GetdocVsSerApproval(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.GetdocVsSerApproval" + Req.ApprovedBy.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/GetdocVsSerAppDetails")]
        public List<DocVsSerAppdetailsRes> GetdocVsSerAppDetails(DocVsSerAppdetailsReq Req)
        {
            List<DocVsSerAppdetailsRes> result = new List<DocVsSerAppdetailsRes>();
            try
            {
                result = _DocVsServiceMapRepository.GetdocVsSerAppDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.GetdocVsSerAppDetails" + Req.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo,0, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/DocVsServiceMap/InsertdocVsSerProf")]
        public int InsertdocVsSerProf(DocVsSerProfInsReq Req)
        {
            int DoctorProfMastNo = 0;
            try
            {
                DoctorProfMastNo = _DocVsServiceMapRepository.InsertdocVsSerProf(Req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DocVsServiceMapController.InsertdocVsSerProf" + Req.DoctorNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venuebranchno, Req.userno);
            }
            return DoctorProfMastNo;
        }

    }
}