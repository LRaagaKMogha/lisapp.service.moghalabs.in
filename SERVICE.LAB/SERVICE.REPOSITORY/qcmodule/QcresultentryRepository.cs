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

namespace Dev.Repository
{
    public class QcresultentryRepository : IQcresultentryRepository
    {
        private IConfiguration _config;
        public QcresultentryRepository(IConfiguration config) { _config = config; }
        public List<GetTblqcresult> GetqcresultDetails(QcresultRequest req)


        {
            List<GetTblqcresult> qcresult = new List<GetTblqcresult>();


            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _qcresultNo = new SqlParameter("qcresultNo", req.qcresultNo);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);


                    qcresult = context.Getqcresult.FromSqlRaw(
                       "Execute dbo.pro_GetQCResultEntry @qcresultNo,@venueNo",
                        _qcresultNo, _venueNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcresultentryRepository.Getqcresult" + req.qcresultNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return qcresult;
        }

        public QcresultResponse InsertqcresultDetails(SaveqcresDTO req)
        {
            QcresultResponse objresult = new QcresultResponse();
            CommonHelper commonUtility = new CommonHelper();
           
            var resultXML = commonUtility.ToXML(req?.resultlst);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _userNo = new SqlParameter("userNo ", req?.userNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", req?.venueBranchno);
                    var _analyzerNo = new SqlParameter("analyzerNo", req?.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req?.paramNo);
                    var _lotNo = new SqlParameter("lotNo", req?.lotNo);
                    var _resultDate = new SqlParameter("resultDate", req?.resultDate);
                    var _resultXML = new SqlParameter("resultXML", resultXML);


                    var obj = context.Insertqcresult.FromSqlRaw(
                           "Execute dbo.pro_InsertQcresultentry  @venueNo,@userNo,@venueBranchno,@analyzerNo,@paramNo,@lotNo,@resultDate,@resultXML",
                              _venueNo,_userNo,_venueBranchno,_analyzerNo,_paramNo,_lotNo,_resultDate,_resultXML).ToList();

                    objresult = obj[0];
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, " QcresultentryRepository.InsertQcmaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }
            return objresult;
        }



        public SaveqcresDTO EditqcresultDetails(EditqcresDTO req)
        {

            SaveqcresDTO lstv = new SaveqcresDTO();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _analyzerNo = new SqlParameter("analyzerNo", req.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req.paramNo);
                    var _lotNo = new SqlParameter("lotNo", req.lotNo);
                    var _resultDate = new SqlParameter("resultDate", req.resultDate);
                    var _venueBranchno = new SqlParameter("venueBranchno", req.venueBranchno);


                    var datalst = context.editqcresult.FromSqlRaw(
                       "Execute dbo.pro_GeteditQcresultentry @venueNo,@analyzerNo,@paramNo,@lotNo,@resultDate,@venueBranchno",
                        _venueNo, _analyzerNo, _paramNo, _lotNo, _resultDate, _venueBranchno).ToList();

                    string lotNo = "";

                    ; int init = 0;

                    if (datalst != null && datalst.Count > 0)
                    {
                      
                        lstv.resultlst = new List<Fetchresult>();
                        foreach (var v in datalst)
                        {
                            if (init == 0)
                            {

                                lstv.venueNo = v.venueNo;
                                lstv.venueBranchno = v.venueBranchno;
                                lstv.analyzerNo = v.analyzerNo;
                                lstv.paramNo = v.paramNo;
                                lstv.lotNo = v.lotNo;
                                lstv.resultDate = v.resultDate;
                                
                            }
                            init = 1;
                            if (lotNo != v.lotNo)
                            {
                                Fetchresult objfetchlevel = new Fetchresult();
                                objfetchlevel.qcresultNo = v.qcresultNo;
                                objfetchlevel.levelNo = v.levelNo;
                                objfetchlevel.result = v.result;
                                objfetchlevel.resultTime = v.resultTime;
                                objfetchlevel.status = v.status;
                                lstv.resultlst.Add(objfetchlevel);
                            }
                            
                        }

                    }

                }



            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.geteditqcmasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }

            return lstv;
        }
    }




}

