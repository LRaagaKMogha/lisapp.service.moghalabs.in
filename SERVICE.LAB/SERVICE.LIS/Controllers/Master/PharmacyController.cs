using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyRepository _PharmacyRepository;
        public PharmacyController(IPharmacyRepository noteRepository)
        {
            _PharmacyRepository = noteRepository;
        }
        
        [HttpPost]
        [Route("api/Pharmacy/GetGeneric")]
        public List<TblGeneric> GetGeneric(reqgeneric req)
        {
            List<TblGeneric> lst = new List<TblGeneric>();
            try
            {
                lst = _PharmacyRepository.GetGeneric(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.GetGeneric", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Pharmacy/InsertGeneric")]
        public GenericMasterResponse InsertGeneric(TblGeneric tblGeneric)
        {
            GenericMasterResponse objresult = new GenericMasterResponse();
            try
            {
                objresult = _PharmacyRepository.InsertGeneric(tblGeneric);
                string _CacheKey = CacheKeys.CommonMaster + "GENERIC" + tblGeneric.venueNo + tblGeneric.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.InsertGeneric", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblGeneric.venueNo, tblGeneric.venueBranchno, 0);
            }
            return objresult;
        }


        [HttpPost]
        [Route("api/Pharmacy/GetMedicinetype")]
        public List<TblMedtype> GetMedicinetype(reqmedtype  medtype)
        {
            List<TblMedtype> lst = new List<TblMedtype>();
            try
            {
                lst = _PharmacyRepository.GetMedicinetype(medtype);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.GetMedicinetype", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Pharmacy/InsertMedtype")]
        public MedtypeMasterResponse InsertMedtype(TblMedtype tblmedtype)
        {
            MedtypeMasterResponse objresult = new MedtypeMasterResponse();
            try
            {
                objresult = _PharmacyRepository.InsertMedtype(tblmedtype);
                string _CacheKey = CacheKeys.CommonMaster + "MEDICINETYPE" + tblmedtype.venueNo + tblmedtype.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.InsertMedtype", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblmedtype.venueNo, tblmedtype.venueBranchno, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Pharmacy/GetMedstr")]
        public List<TblMedstr> GetMedstr(reqmedstr medstr)
        {
            List<TblMedstr> lst = new List<TblMedstr>();
            try
            {
                lst = _PharmacyRepository.GetMedstr(medstr);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.GetMedstr", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lst;
        }


        [HttpPost]
        [Route("api/Pharmacy/InsertMedstr")]
        public MedstrMasterResponse InsertMedstr(TblMedstr tblmedstr)
        {
            MedstrMasterResponse objresult = new MedstrMasterResponse();
            try
            {
                objresult = _PharmacyRepository.InsertMedstr(tblmedstr);
                string _CacheKey = CacheKeys.CommonMaster + "MEDICINESTRENGTH" + tblmedstr.venueNo + tblmedstr.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyController.InsertMedstr", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblmedstr.venueNo, tblmedstr.venueBranchno, 0);
            }
            return objresult;
        }

    }


}