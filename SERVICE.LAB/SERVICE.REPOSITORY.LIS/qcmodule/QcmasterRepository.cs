using DEV.Model;
using DEV.Model.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using Serilog;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class QcmasterRepository : IQcmasterRepository
    {
        private IConfiguration _config;
        public QcmasterRepository(IConfiguration config) { _config = config; }
        public List<GetTblqcmaster> GetqcmasterDetails(qcmasterRequest qcmaster)


        {
            List<GetTblqcmaster> qcresult = new List<GetTblqcmaster>();


            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _qcmasterNo = new SqlParameter("qcmasterNo", qcmaster?.qcmasterNo);
                    var _venueNo = new SqlParameter("venueNo", qcmaster?.venueNo);


                    qcresult = context.Getqcmaster.FromSqlRaw(
                       "Execute dbo.pro_GetQCMaster @qcmasterNo,@venueNo",
                        _qcmasterNo, _venueNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.GetqcmasterDetails" + qcmaster?.qcmasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, qcmaster?.venueNo, 0, 0);
            }
            return qcresult;
        }


        public saveqcDTO editqcmasterDetails(EditqcDTO req)
        {
           
           saveqcDTO lstv = new saveqcDTO();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                   
                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _lotNo=new SqlParameter("lotNo", req?.lotNo);
                    var _analyzerNo = new SqlParameter("analyzerNo", req?.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req?.paramNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", req?.venueBranchno);
                  


                    var datalstv = context.updateqcmaster.FromSqlRaw(
                       "Execute dbo.pro_GeteditQCMaster @venueNo,@lotNo,@analyzerNo,@paramNo,@venueBranchno",
                        _venueNo,_lotNo,_analyzerNo,_paramNo,_venueBranchno).ToList();

                    string lotNo = "";
                    
;                    int init = 0;

                    if (datalstv != null && datalstv.Count > 0)
                    {
                        lstv.newlst = new List<Fetchlot>();
                        lstv.levellst = new List<Fetchlevel>();
                        foreach (var v in datalstv)
                        {
                            if (init == 0)
                            {
                               
                                lstv.venueNo = v.venueNo;
                                lstv.userNo = v.userNo;
                                lstv.venueBranchno = v.venueBranchno;
                                lstv.analyzerNo = v.analyzerNo;
                                lstv.paramNo = v.paramNo;
                                lstv.lotNo = v.lotNo;
                               
                            }
                            init = 1;                           
                            if (lotNo != v.lotNo)
                            {
                                lotNo = v.lotNo;
                                Fetchlot objlot = new Fetchlot();
                                objlot.lotNo = v.lotNo;
                                objlot.status = v.status;
                                lstv.newlst.Add(objlot);
                            }
                            Fetchlevel objfetchlevel = new Fetchlevel();
                            objfetchlevel.qcmasterNo = v.qcmasterNo;
                            objfetchlevel.lotNo = v.lotNo;
                            objfetchlevel.levelNo = v.levelNo;
                            objfetchlevel.meanValue = v.meanValue;
                            objfetchlevel.lowValue = v.lowValue;
                            objfetchlevel.highValue = v.highValue;
                            lstv.levellst.Add(objfetchlevel);
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






        public QcMasterResponse InsertqcmasterDetails(saveqcDTO req)
        {
            QcMasterResponse objresult = new QcMasterResponse();
            CommonHelper commonUtility = new CommonHelper();
            var lotXML = commonUtility.ToXML(req?.newlst);
            var levelXML = commonUtility.ToXML(req?.levellst);

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
                    var _lotXML = new SqlParameter("lotXML", lotXML);
                    var _levelXML = new SqlParameter("levelXML", levelXML);


                    var obj = context.Insertqcmaster.FromSqlRaw(
                           "Execute dbo.pro_InsertQcmaster  @venueNo,@userNo,@venueBranchno,@analyzerNo,@paramNo,@lotNo,@lotXML,@levelXML",
                              _venueNo,_userNo,_venueBranchno,_analyzerNo,_paramNo,_lotNo,_lotXML, _levelXML).ToList();

                    objresult = obj[0];
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, " QcmasterRepository.InsertQcmaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }
            return objresult;
        }

        public List<Qclotresponse> Getqclot(Qclotreq req)
        {
            List<Qclotresponse> objlot = new List<Qclotresponse>();
                    

            try                                 
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _analyzerNo = new SqlParameter("analyzerNo", req?.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req?.paramNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", req?.venueBranchno);
                   

                    var obj = context.Qclotlist.FromSqlRaw(
                           "Execute dbo.pro_GetQClot  @venueNo,@analyzerNo,@paramNo,@venueBranchno",
                              _venueNo,_analyzerNo,_paramNo,_venueBranchno).ToList();

                    objlot = obj;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, " QcmasterRepository.Getqclot", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }
            return objlot;
        }

        public List<Qclevelresponse> Getqclevel(Qclevelreq req)
        {
            List<Qclevelresponse> objlevel = new List<Qclevelresponse>();


            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _analyzerNo = new SqlParameter("analyzerNo", req?.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req?.paramNo);
                    var _lotNo = new SqlParameter("lotNo", req?.lotNo);
                    var _venueBranchno = new SqlParameter("venueBranchno",req?.venueBranchno);


                    
                    var obj = context.Qclevellist.FromSqlRaw(
                           "Execute pro_GetQClevel  @venueNo,@analyzerNo,@paramNo,@lotNo,@venueBranchno",
                              _venueNo,_analyzerNo,_paramNo,_lotNo,_venueBranchno).ToList();

                    objlevel = obj;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, " QcmasterRepository.Getqclevel", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }
            return objlevel;
        }

        public List<Qclowhighresponse> Getqclowhighvalue(Qclowhighreq req)
        {
            List<Qclowhighresponse> objvalue= new List<Qclowhighresponse>();


            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("venueNo", req?.venueNo);
                    var _analyzerNo = new SqlParameter("analyzerNo", req?.analyzerNo);
                    var _paramNo = new SqlParameter("paramNo", req?.paramNo);
                    var _lotNo = new SqlParameter("lotNo", req?.lotNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", req?.venueBranchno);
                    var _levelNo = new SqlParameter("levelNo", req?.levelNo);



                    var obj = context.Qclowhighlist.FromSqlRaw(
                           "Execute pro_GetQClowhighvalue @venueNo,@analyzerNo,@paramNo,@lotNo,@venueBranchno,@levelNo",
                              _venueNo,_analyzerNo,_paramNo,_lotNo,_venueBranchno,_levelNo).ToList();

                    objvalue = obj;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, " QcmasterRepository.Getqclowhighvalue", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueNo, 0, 0);
            }
            return objvalue;
        }



    }




}

