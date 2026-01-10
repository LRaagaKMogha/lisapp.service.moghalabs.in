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
using System.Reflection;

namespace Dev.Repository
{
    public class AnalyzerMasterRepository : IAnalyzerMasterRepository
    {
        private IConfiguration _config;
        public AnalyzerMasterRepository(IConfiguration config) { _config = config; }

        public List<TblAnalyzer> GetAnalyzerMasterDetails(GetCommonMasterRequest getanalyzer)
        {
            List<TblAnalyzer> objresult = new List<TblAnalyzer>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    if (getanalyzer.masterNo > 0)
                    {
                        objresult = context.TblAnalyzer.Where(x => x.VenueNo == getanalyzer.venueno   && x.Status == true).ToList();
                    }
                    else
                    {
                        objresult = context.TblAnalyzer.Where(x => x.VenueNo == getanalyzer.venueno ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAnalyzerMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, getanalyzer.venueno, getanalyzer.venuebranchno, 0);
            }
            return objresult;
        }

        public TblAnalyzerdata InsertAnalyzerDetails(TblAnalyzerresponse TblAnalyzerresponse)
        {
            TblAnalyzerdata objresult = new TblAnalyzerdata();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", TblAnalyzerresponse?.analyzerMasterNo);
                    var _serialNo = new SqlParameter("serialNo", TblAnalyzerresponse?.serialNo);
                    var _assetCode = new SqlParameter("assetCode", TblAnalyzerresponse?.assetCode);
                    var _description = new SqlParameter("description", TblAnalyzerresponse?.description);
                    var _status = new SqlParameter("status", TblAnalyzerresponse?.status);
                    var _venueNo = new SqlParameter("venueNo", TblAnalyzerresponse?.venueNo);
                    var _userNo = new SqlParameter("userNo", TblAnalyzerresponse?.userNo);


                    var obj = context.InsertAnalyzerDetails.FromSqlRaw(
                        "Execute dbo.pro_InsertAnalyzerDetails @analyzerMasterNo,@serialNo,@assetCode,@description,@status,@venueNo,@userNo",
                        _analyzerMasterNo, _serialNo, _assetCode, _description, _status, _venueNo, _userNo).ToList();
                    objresult.analyzerMasterNo = obj[0].analyzerMasterNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.InsertAnalyzerDetails" + TblAnalyzerresponse.analyzerMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, TblAnalyzerresponse.venueNo, 0, TblAnalyzerresponse.userNo);
            }
            return objresult;
        }
        public AnaParamDtoResponse InsertAnaParam(AnaParamDto AnaParamobj)
        {
            AnaParamDtoResponse result = new AnaParamDtoResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", AnaParamobj.AnalyzerMasterNo);
                    var _description = new SqlParameter("description", AnaParamobj.Description);
                    var _sequenceNo = new SqlParameter("sequenceNo", AnaParamobj.SequenceNo);
                    var _sampleNo = new SqlParameter("sampleNo", AnaParamobj.SampleNo);
                    var _status = new SqlParameter("status", AnaParamobj.Status);
                    var _venueNo = new SqlParameter("venueNo", AnaParamobj.VenueNo);
                    var _userno = new SqlParameter("userNo", AnaParamobj.CreatedBy);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", AnaParamobj.AnalyzerParamNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", AnaParamobj.venuebranchno);
                    var lst = context.InsertAnalyzerParameter.FromSqlRaw(
                       "Execute dbo.pro_InsertAnalyzerVsParameters @analyzerMasterNo,@description,@sequenceNo, @sampleNo,@status,@venueNo,@userNo,@analyzerParamNo,@venuebranchno",
                        _analyzerMasterNo, _description, _sequenceNo, _sampleNo, _status, _venueNo, _userno, _analyzerParamNo, _venuebranchno).ToList();
                    result.AnalyzerMasterNo = lst[0].AnalyzerMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.InsertAnaParam", ExceptionPriority.Low, ApplicationType.REPOSITORY, AnaParamobj.VenueNo, 0, 0);
            }
            return result;
        }

        public List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int analyzerParamNo, int Analyzerno, int Sampleno)
        {
            List<AnaParamGetDto> objresult = new List<AnaParamGetDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", analyzerParamNo);
                    var _analyzerMasterNo = new SqlParameter("Analyzerno", Analyzerno);
                    var _Sampleno = new SqlParameter("Sampleno", Sampleno);
                    objresult = context.GetAnalyzerParameter.FromSqlRaw("Execute dbo.pro_GetAnalyzerVsParameters @VenueNo,@VenueBranchNo,@analyzerParamNo,@analyzerNo,@Sampleno",
                        _venueno, _venuebranchno, _analyzerParamNo, _analyzerMasterNo, _Sampleno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.GetAnaParamDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TbltestMap> GetAnalVsParamVsTest(testmapRequest testmapRequest)
        {
            List<TbltestMap> objresult = new List<TbltestMap>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", testmapRequest?.venueNo);
                    var _branchNo = new SqlParameter("branchNo", testmapRequest?.branchNo);
                    var _analyzerparamTestNo = new SqlParameter("analyzerparamTestNo", testmapRequest?.analyzerparamTestNo);
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", testmapRequest?.analyzerMasterNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", testmapRequest?.analyzerParamNo);
                    var _testNo = new SqlParameter("testNo", testmapRequest?.testNo);
                    var _subtestNo = new SqlParameter("subtestNo", testmapRequest?.subtestNo);
                    var _pageIndex = new SqlParameter("pageIndex", testmapRequest?.pageIndex);

                   objresult = context.GetAnalVsParamVsTest.FromSqlRaw(
                        "Execute dbo.pro_GetAnalVsParamVsTest @venueNo,@branchNo,@analyzerparamTestNo,@analyzerMasterNo,@analyzerParamNo,@testNo,@subtestNo,@pageIndex",
                       _venueNo, _branchNo, _analyzerparamTestNo, _analyzerMasterNo, _analyzerParamNo, _testNo, _subtestNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.GetAnalVsParamVsTest" + testmapRequest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, testmapRequest.venueNo, 0, 0);
            }
            return objresult;
        }
        public analVsparamVstestMap InsertAnalVsParamVsTest(responseTest responseTest)
        {
            analVsparamVstestMap objresult = new analVsparamVstestMap();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _analyzerparamTestNo = new SqlParameter("analyzerparamTestNo", responseTest?.analyzerparamTestNo);
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", responseTest?.analyzerMasterNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", responseTest?.analyzerParamNo);
                    var _testNo = new SqlParameter("testNo", responseTest?.testNo);
                    var _subtestNo = new SqlParameter("subtestNo", responseTest?.subtestNo);
                    var _tstatus = new SqlParameter("tstatus", responseTest?.tstatus);
                    var _venueNo = new SqlParameter("venueNo", responseTest?.venueNo);
                    var _branchNo = new SqlParameter("branchNo", responseTest?.venuebranchno);
                    var _userNo = new SqlParameter("userNo", responseTest?.userNo);
                    var _unitNo = new SqlParameter("unitNo", responseTest?.unitNo);
                    var _methodNo = new SqlParameter("methodNo", responseTest?.methodNo);
                    var _perunitconsumption = new SqlParameter("perUnitConsumption", responseTest?.perUnitConsumption);
                    var _ReagentName = new SqlParameter("ReagentName", responseTest?.ReagentName);
                    var _UnitName = new SqlParameter("UnitName", responseTest?.UnitName);

                    var obj = context.InsertAnalVsParamVsTest.FromSqlRaw(
                        "Execute dbo.pro_InsertAnalVsParamVsTest @analyzerparamTestNo,@analyzerMasterNo,@analyzerParamNo,@testNo,@subtestNo,@tstatus,@venueNo,@branchNo,@userNo,@unitNo,@methodNo,@PerUnitConsumption,@ReagentName,@UnitName",
                        _analyzerparamTestNo, _analyzerMasterNo, _analyzerParamNo, _testNo, _subtestNo, _tstatus, _venueNo, _branchNo, _userNo, _unitNo, _methodNo, _perunitconsumption, _ReagentName, _UnitName).ToList();
                    objresult.analyzerparamTestNo = obj[0].analyzerparamTestNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.InsertAnalVsParamVsTest" + responseTest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, responseTest.venueNo, 0, responseTest.userNo);
            }
            return objresult;
        }
        public List<subresponse> GetSubTest(subrequest subrequest)
        {
            List<subresponse> objresult = new List<subresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", subrequest?.venueNo);
                    var _testNo = new SqlParameter("testNo", subrequest?.testNo);

                    objresult = context.GetSubTest.FromSqlRaw(
                        "Execute dbo.pro_GetSubTest @venueNo,@testNo",
                       _venueNo, _testNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.GetSubTest", ExceptionPriority.Low, ApplicationType.REPOSITORY, subrequest.venueNo, 0, 0);
            }
            return objresult;
        }
    }
}
