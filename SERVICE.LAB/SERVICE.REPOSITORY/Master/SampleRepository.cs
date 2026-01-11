using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;
using Serilog;
using Newtonsoft.Json;
using System.IO;

namespace Dev.Repository
{
    public class SampleRepository : ISampleRepository
    {
        private IConfiguration _config;
        public SampleRepository(IConfiguration config) { _config = config; }
        public int InsertSampleDetails1(TblSample Sampleitem)
        {
            int result = 0;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (Sampleitem.SampleNo > 0)
                    {
                        Sampleitem.ModifiedOn = DateTime.Now;
                        Sampleitem.ModifiedBy = Sampleitem.CreatedBy;
                        context.Entry(Sampleitem).State = EntityState.Modified;
                    }
                    else
                    {
                        Sampleitem.CreatedOn = DateTime.Now;
                        Sampleitem.ModifiedOn = DateTime.Now;
                        Sampleitem.ModifiedBy = Sampleitem.CreatedBy;

                        context.TblSamples.Add(Sampleitem);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
        public List<TblSample> GetSampleDetails1(GetCommonMasterRequest sampleMasterRequest)
        {
            List<TblSample> objresult = new List<TblSample>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    if (sampleMasterRequest.masterNo > 0)
                    {
                        objresult = context.TblSamples.Where(x => x.VenueNo == sampleMasterRequest.venueno && x.VenueBranchNo == sampleMasterRequest.venuebranchno && x.SampleNo == sampleMasterRequest.masterNo).ToList();
                    }
                    else
                    {
                        objresult = context.TblSamples.Where(x => x.VenueNo == sampleMasterRequest.venueno && x.VenueBranchNo == sampleMasterRequest.venuebranchno).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSampleDetails1", ExceptionPriority.Low, ApplicationType.REPOSITORY, sampleMasterRequest.venueno, sampleMasterRequest.venuebranchno, 0);
            }
            return objresult;
        }
        public List<TblSample> GetSampleDetails(GetCommonMasterRequest sampleMasterRequest)
        {
            List<TblSample> objresult = new List<TblSample>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _SampleNo = new SqlParameter("SampleNo", sampleMasterRequest?.SampleNo);
                    var _venueno = new SqlParameter("VenueNo", sampleMasterRequest?.venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", sampleMasterRequest?.venuebranchno);
                    var _pageIndex = new SqlParameter("pageIndex", sampleMasterRequest?.pageIndex);

                    objresult = context.GetSampleDetails.FromSqlRaw(
                        "Execute dbo.pro_GetSample @SampleNo,@venueNo,@venueBranchNo,@pageIndex",
                        _SampleNo, _venueno, _venuebranchno, _pageIndex).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SampleRepository.GetSampleDetails" + sampleMasterRequest.SampleNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, sampleMasterRequest.venueno, sampleMasterRequest.venuebranchno, 0);
            }
            return objresult;
        }
        public List<sampleMasterResponse> InsertSampleDetails(TblSample Sampleitem)
        {
            List<sampleMasterResponse> objresult = new List<sampleMasterResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _SampleNo = new SqlParameter("SampleNo", Sampleitem?.SampleNo);
                    var _SampleName = new SqlParameter("SampleName", Sampleitem?.SampleName);
                    var _SampleDisplayText = new SqlParameter("SampleDisplayText", Sampleitem?.SampleDisplayText);
                    var _SampleVolume = new SqlParameter("SampleVolume", Sampleitem?.SampleVolume);
                    var _Prefix = new SqlParameter("Prefix", Sampleitem?.Prefix);
                    var _Suffix = new SqlParameter("Suffix", Sampleitem?.Suffix);
                    var _Status = new SqlParameter("Status", Sampleitem?.Status);
                    var _userno = new SqlParameter("userno", Sampleitem?.CreatedBy);
                    var _venueno = new SqlParameter("VenueNo", Sampleitem?.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", Sampleitem?.VenueBranchNo);
                    var _sequenceNo = new SqlParameter("SequenceNo",Sampleitem?.SequenceNo);


                    objresult = context.InsertSampleDetails.FromSqlRaw(
                        "Execute dbo.pro_InsertSample @SampleNo,@SampleName,@SampleDisplayText,@SampleVolume,@Prefix,@Suffix,@Status,@userno,@venueNo,@venueBranchNo,@SequenceNo",
                        _SampleNo, _SampleName, _SampleDisplayText, _SampleVolume, _Prefix, _Suffix, _Status, _userno, _venueno, _venuebranchno,_sequenceNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SampleRepository.InsertSampleDetails" + Sampleitem.SampleNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Sampleitem.VenueNo, Sampleitem.VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblSample> SearchSampleDetails(string SampleName)
        {
            List<TblSample> objresult = new List<TblSample>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblSamples.Where(s => s.SampleName == SampleName).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SearchSampleDetails - " + SampleName, ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
    }
}