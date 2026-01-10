using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class AnaParamRepository : IAnaParamRepository
    {
        private IConfiguration _config;
        public AnaParamRepository(IConfiguration config) { _config = config; }
        /// <summary>
        /// Insert Analyzer Parameters
        /// </summary>
        /// <param name="AnaParamobj"></param>
        /// <returns></returns>
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
                    var lst = context.InsertAnalyzerParameter.FromSqlRaw(
                       "Execute dbo.pro_InsertAnalyzerVsParameters @analyzerMasterNo,@description,@sequenceNo, @sampleNo,@status,@venueNo,@userNo,@analyzerParamNo",
                        _analyzerMasterNo, _description, _sequenceNo, _sampleNo, _status, _venueNo, _userno, _analyzerParamNo).ToList();
                    result.AnalyzerMasterNo = lst[0].AnalyzerMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.InsertAnaParam", ExceptionPriority.Low, ApplicationType.REPOSITORY, AnaParamobj.VenueNo, 0, 0);
            }
            return result;
        }

        public List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno)
        {
            List<AnaParamGetDto> objresult = new List<AnaParamGetDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _analyzerno = new SqlParameter("Analyzerno", Analyzerno);
                    var _sampleno = new SqlParameter("Sampleno", Sampleno);

                    objresult = context.GetAnalyzerParameter.FromSqlRaw("Execute dbo.pro_GetAnalyzerVsParameters @VenueNo,@VenueBranchNo,@Analyzerno,@Sampleno", _venueno, _venuebranchno, _analyzerno, _sampleno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.GetAnaParamDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<FetchAnaParamDto> FetchAnalyzerParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno)
        {
            List<FetchAnaParamDto> objresult = new List<FetchAnaParamDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _analyzerno = new SqlParameter("Analyzerno", Analyzerno);
                    var _sampleno = new SqlParameter("Sampleno", Sampleno);

                    objresult = context.FetchAnalyzerParameter.FromSqlRaw("Execute dbo.pro_FetchAnalyzerVsParametersMapping " +
                        "@VenueNo, @VenueBranchNo, @Analyzerno, @Sampleno", 
                        _venueno, _venuebranchno, _analyzerno, _sampleno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnaParamRepository.FetchAnalyzerParameter", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}
