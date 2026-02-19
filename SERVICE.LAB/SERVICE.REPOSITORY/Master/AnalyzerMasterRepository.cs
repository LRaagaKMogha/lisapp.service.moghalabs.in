using Service.Model;
using Service.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Common;
using Microsoft.Data.SqlClient;
using Service.IRepository;

namespace Service.Repository
{
    public class AnalyzerMasterRepository : IAnalyzerMasterRepository
    {
        private IConfiguration _config;
        public AnalyzerMasterRepository(IConfiguration config) { _config = config; }

        public List<TblAnalyzer> GetAnalyzerMasterDetails(GetCommonMasterRequest getanalyzer)
        {
            List<TblAnalyzer> Objresult = new List<TblAnalyzer>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (getanalyzer.masterNo > 0)
                    {
                        Objresult = context.TblAnalyzer.Where(x => x.VenueNo == getanalyzer.venueno   && x.Status == true).ToList();
                    }
                    else
                    {
                        Objresult = context.TblAnalyzer.Where(x => x.VenueNo == getanalyzer.venueno ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAnalyzerMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, getanalyzer.venueno, getanalyzer.venuebranchno, 0);
            }
            return Objresult;
        }
        public TblAnalyzerdata InsertAnalyzerDetails(TblAnalyzerresponse TblAnalyzerresponse)
        {
            TblAnalyzerdata Objresult = new TblAnalyzerdata();
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
                    
                    Objresult.analyzerMasterNo = obj[0].analyzerMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.InsertAnalyzerDetails" + TblAnalyzerresponse.analyzerMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, TblAnalyzerresponse.venueNo, 0, TblAnalyzerresponse.userNo);
            }
            return Objresult;
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
            List<AnaParamGetDto> Objresult = new List<AnaParamGetDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", analyzerParamNo);
                    var _analyzerMasterNo = new SqlParameter("Analyzerno", Analyzerno);
                    var _Sampleno = new SqlParameter("Sampleno", Sampleno);
                    
                    Objresult = context.GetAnalyzerParameter.FromSqlRaw("Execute dbo.pro_GetAnalyzerVsParameters @VenueNo,@VenueBranchNo,@analyzerParamNo,@analyzerNo,@Sampleno",
                    _venueno, _venuebranchno, _analyzerParamNo, _analyzerMasterNo, _Sampleno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.GetAnaParamDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<TbltestMap> GetAnalVsParamVsTest(TestmapRequest TestmapRequest)
        {
            List<TbltestMap> Objresult = new List<TbltestMap>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", TestmapRequest?.venueNo);
                    var _branchNo = new SqlParameter("branchNo", TestmapRequest?.branchNo);
                    var _analyzerparamTestNo = new SqlParameter("analyzerparamTestNo", TestmapRequest?.analyzerparamTestNo);
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", TestmapRequest?.analyzerMasterNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", TestmapRequest?.analyzerParamNo);
                    var _testNo = new SqlParameter("testNo", TestmapRequest?.testNo);
                    var _subtestNo = new SqlParameter("subtestNo", TestmapRequest?.subtestNo);
                    var _pageIndex = new SqlParameter("pageIndex", TestmapRequest?.pageIndex);

                    Objresult = context.GetAnalVsParamVsTest.FromSqlRaw(
                    "Execute dbo.pro_GetAnalVsParamVsTest @venueNo,@branchNo,@analyzerparamTestNo,@analyzerMasterNo,@analyzerParamNo,@testNo,@subtestNo,@pageIndex",
                    _venueNo, _branchNo, _analyzerparamTestNo, _analyzerMasterNo, _analyzerParamNo, _testNo, _subtestNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.GetAnalVsParamVsTest" + TestmapRequest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, TestmapRequest.venueNo, 0, 0);
            }
            return Objresult;
        }
        public AnalVsparamVstestMap InsertAnalVsParamVsTest(ResponseTest ResponseTest)
        {
            AnalVsparamVstestMap Objresult = new AnalVsparamVstestMap();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _analyzerparamTestNo = new SqlParameter("analyzerparamTestNo", ResponseTest?.analyzerparamTestNo);
                    var _analyzerMasterNo = new SqlParameter("analyzerMasterNo", ResponseTest?.analyzerMasterNo);
                    var _analyzerParamNo = new SqlParameter("analyzerParamNo", ResponseTest?.analyzerParamNo);
                    var _testNo = new SqlParameter("testNo", ResponseTest?.testNo);
                    var _subtestNo = new SqlParameter("subtestNo", ResponseTest?.subtestNo);
                    var _tstatus = new SqlParameter("tstatus", ResponseTest?.tstatus);
                    var _venueNo = new SqlParameter("venueNo", ResponseTest?.venueNo);
                    var _branchNo = new SqlParameter("branchNo", ResponseTest?.venuebranchno);
                    var _userNo = new SqlParameter("userNo", ResponseTest?.userNo);
                    var _unitNo = new SqlParameter("unitNo", ResponseTest?.unitNo);
                    var _methodNo = new SqlParameter("methodNo", ResponseTest?.methodNo);
                    var _perunitconsumption = new SqlParameter("perUnitConsumption", ResponseTest?.perUnitConsumption);
                    var _ReagentName = new SqlParameter("ReagentName", ResponseTest?.ReagentName);
                    var _UnitName = new SqlParameter("UnitName", ResponseTest?.UnitName);

                    var obj = context.InsertAnalVsParamVsTest.FromSqlRaw(
                    "Execute dbo.pro_InsertAnalVsParamVsTest @analyzerparamTestNo,@analyzerMasterNo,@analyzerParamNo,@testNo,@subtestNo,@tstatus,@venueNo,@branchNo,@userNo,@unitNo,@methodNo,@PerUnitConsumption,@ReagentName,@UnitName",
                    _analyzerparamTestNo, _analyzerMasterNo, _analyzerParamNo, _testNo, _subtestNo, _tstatus, _venueNo, _branchNo, _userNo, _unitNo, _methodNo, _perunitconsumption, _ReagentName, _UnitName).ToList();
                    
                    Objresult.analyzerparamTestNo = obj[0].analyzerparamTestNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.InsertAnalVsParamVsTest" + ResponseTest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, ResponseTest.venueNo, 0, ResponseTest.userNo);
            }
            return Objresult;
        }
        public List<Subresponse> GetSubTest(Subrequest Subrequest)
        {
            List<Subresponse> Objresult = new List<Subresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", Subrequest?.venueNo);
                    var _testNo = new SqlParameter("testNo", Subrequest?.testNo);

                    Objresult = context.GetSubTest.FromSqlRaw(
                    "Execute dbo.pro_GetSubTest @venueNo,@testNo",_venueNo, _testNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzerMasterRepository.GetSubTest", ExceptionPriority.Low, ApplicationType.REPOSITORY, Subrequest.venueNo, 0, 0);
            }
            return Objresult;
        }
    }
}
