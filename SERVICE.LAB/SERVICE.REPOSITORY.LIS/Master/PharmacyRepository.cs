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
using Serilog;

namespace Dev.Repository
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private IConfiguration _config;
        public PharmacyRepository(IConfiguration config) { _config = config; }
    

        public List<TblGeneric> GetGeneric(reqgeneric req)
        {
            List<TblGeneric> lst = new List<TblGeneric>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
      
               
                    var _genericNo = new SqlParameter("genericNo", req?.genericNo);
                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _pageIndex=new SqlParameter("pageIndex", req?.pageIndex);

                    lst = context.GetGeneric.FromSqlRaw(
                      "Execute dbo.pro_GetGenericMaster @genericNo,@venueNo,@pageIndex",
                       _genericNo,_venueNo,_pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyRepository.GetGeneric" + req.genericNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo ,0,0);
            }
            return lst;
        }

        public GenericMasterResponse InsertGeneric(TblGeneric tblGeneric)
        {
            GenericMasterResponse objresult = new GenericMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _genericNo = new SqlParameter("genericNo", tblGeneric?.genericNo);
                    var _venueNo = new SqlParameter("venueNo", tblGeneric?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblGeneric?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblGeneric?.userNo);
                    var _genericName = new SqlParameter("genericName", tblGeneric?.genericName);
                    var _isNotified = new SqlParameter("isNotified", tblGeneric?.isNotified);
                    var _sequenceNo = new SqlParameter("sequenceNo", tblGeneric?.sequenceNo);
                    var _status = new SqlParameter("status", tblGeneric?.status);

                    var obj = context.InsertGeneric.FromSqlRaw(
                           "Execute dbo.pro_InsertGenericMaster @genericNo,@venueNo,@venueBranchno,@userNo," +
                            "@genericName,@isNotified, @sequenceNo,@status",
                              _genericNo, _venueNo, _venueBranchno, _userNo, _genericName,
                               _isNotified, _sequenceNo, _status).ToList();

                    objresult.genericNo = obj[0].genericNo;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, "PharmacyRepository.InsertGeneric" + tblGeneric.genericNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblGeneric.venueNo, tblGeneric.venueBranchno, 0);
            }
            return objresult;

        }

        public List<TblMedtype> GetMedicinetype(reqmedtype medtype)
        {
            List<TblMedtype> lst = new List<TblMedtype>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {


                    var _medicineTypeNo = new SqlParameter("medicineTypeNo", medtype?.medicineTypeNo);
                    var _venueNo = new SqlParameter("venueNo", medtype?.venueNo);
                    var _unitNo = new SqlParameter("unitNo", medtype?.unitNo);
                    var _pageIndex = new SqlParameter("pageIndex", medtype?.pageIndex);
                   



                    lst = context.GetMedicinetype.FromSqlRaw(
                      "Execute dbo.pro_GetMedicineType @medicineTypeNo,@venueNo,@unitNo,@pageIndex",
                       _medicineTypeNo,_venueNo,_unitNo,_pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PharmacyRepository.GetMedicinetype" + medtype.medicineTypeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, medtype.venueNo, 0, 0);
            }
            return lst;
        }


        public MedtypeMasterResponse InsertMedtype(TblMedtype tblmedtype)
        {
            MedtypeMasterResponse objresult = new MedtypeMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _medicineTypeNo = new SqlParameter("medicineTypeNo", tblmedtype?.medicineTypeNo);
                    var _venueNo = new SqlParameter("venueNo", tblmedtype?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblmedtype?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblmedtype?.userNo);
                    var _description = new SqlParameter("description", tblmedtype?.description);
                    var _unitNo = new SqlParameter("unitNo", tblmedtype?.unitNo);
                    var _sequenceNo = new SqlParameter("sequenceNo", tblmedtype?.sequenceNo);
                    var _status = new SqlParameter("status", tblmedtype?.status);

                    var obj = context.InsertMedtype.FromSqlRaw(
                           "Execute dbo.pro_InsertMedicineType @medicineTypeNo,@venueNo,@venueBranchno,@userNo," +
                            "@description,@unitNo,@sequenceNo,@status",
                              _medicineTypeNo, _venueNo, _venueBranchno, _userNo, _description,
                               _unitNo,_sequenceNo, _status).ToList();

                    objresult.medicineTypeNo = obj[0].medicineTypeNo;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, "PharmacyRepository.InsertMedtype" + tblmedtype.medicineTypeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblmedtype.venueNo, tblmedtype.venueBranchno, 0);
            }
            return objresult;

        }

        public List<TblMedstr> GetMedstr(reqmedstr medstr)
        {
            List<TblMedstr> lst = new List<TblMedstr>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {


                    var _strengthNo = new SqlParameter("strengthNo", medstr?.strengthNo);
                    var _venueNo = new SqlParameter("venueNo", medstr?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", medstr?.pageIndex);



                    lst = context.GetMedstr.FromSqlRaw(
                      "Execute  dbo.pro_GetMedicineStrength @strengthNo,@venueNo,@pageIndex",
                       _strengthNo, _venueNo,_pageIndex).ToList();
                }
            }
            catch (Exception ex)
             {
                MyDevException.Error(ex, "PharmacyRepository. GetMedstr" + medstr.strengthNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, medstr.venueNo, 0, 0);
            }
            return lst;
        }

        public MedstrMasterResponse InsertMedstr(TblMedstr tblmedstr)
        {
            MedstrMasterResponse objresult = new MedstrMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _strengthNo = new SqlParameter("strengthNo", tblmedstr?.strengthNo);
                    var _venueNo = new SqlParameter("venueNo", tblmedstr?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblmedstr?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblmedstr?.userNo);
                    var _strengthName = new SqlParameter("strengthName", tblmedstr?.strengthName);
                    var _strengthValue = new SqlParameter("strengthValue", tblmedstr?.strengthValue);
                    var _sequenceNo = new SqlParameter("sequenceNo", tblmedstr?.sequenceNo);
                    var _status = new SqlParameter("status", tblmedstr?.status);

                    var obj = context.InsertMedstr.FromSqlRaw(
                           "Execute dbo.pro_InsertMedicineStrength @strengthNo,@venueNo,@venueBranchno,@userNo," +
                            "@strengthName,@strengthValue, @sequenceNo,@status",
                              _strengthNo, _venueNo, _venueBranchno, _userNo, _strengthName,
                               _strengthValue, _sequenceNo, _status).ToList();

                    objresult.strengthNo = obj[0].strengthNo;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, "PharmacyRepository.InsertMedstr" + tblmedstr.strengthNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblmedstr.venueNo, tblmedstr.venueBranchno, 0);
            }
            return objresult;

        }
    }
}
