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
    public class TermsRepository : ITermsRepository
    {
        private IConfiguration _config;
        public TermsRepository(IConfiguration config) { _config = config; }
        public List<TblTerms> GettermsDetails(TermsmasterRequest termsmaster)
        {
            List<TblTerms> termsresult = new List<TblTerms>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _termsNo = new SqlParameter("termsNo", termsmaster?.termsNo);
                    var _venueNo = new SqlParameter("venueNo", termsmaster?.venueNo);
                    var _TermsType = new SqlParameter("termstype ", termsmaster?.termstype);
                    var _pageIndex = new SqlParameter("pageIndex", termsmaster?.pageIndex);

                    termsresult = context.GetTermsmaster.FromSqlRaw(
                    "Execute dbo.pro_GetTerms @termsNo,@venueNo,@termstype,@pageIndex",
                    _termsNo, _venueNo, _TermsType, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TermsRepository.GettermsDetails" + termsmaster?.termsNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, termsmaster.venueNo, 0, 0);
            }
            return termsresult;
        }

        public Termsmasterresponse InsertTermsmaster(TblTerms tblterms)
        {
            Termsmasterresponse objresult = new Termsmasterresponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _termsNo = new SqlParameter("termsNo", tblterms?.termsNo);
                    var _venueNo = new SqlParameter("venueNo", tblterms?.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", tblterms?.venuebranchno);
                    var _userno = new SqlParameter("userno", tblterms?.userno);
                    var _termstype = new SqlParameter("termstype", tblterms?.termstype);
                    var _termsname = new SqlParameter("termsname ", tblterms?.termsname);
                    var _termsdescription = new SqlParameter("termsdescription", tblterms?.termsdescription);
                    var _sequenceno = new SqlParameter("sequenceno", tblterms?.sequenceno);
                    var _status = new SqlParameter("status", tblterms?.status);

                    var obj = context.InsertTermsmaster.FromSqlRaw(
                    "Execute dbo.pro_InsertTerms @termsNo,@venueNo,@venuebranchno,@userno,@termstype," +
                    "@termsname,@termsdescription, @sequenceno,@status",
                    _termsNo, _venueNo, _venuebranchno, _userno, _termstype, _termsname,
                    _termsdescription, _sequenceno, _status).ToList();

                    if (obj[0].termsNo == -1)
                    {
                        objresult.termsNo = -1; 
                    }
                    else if (obj[0].termsNo == 0)
                    {
                        objresult.termsNo = 0;
                    }
                    else if (obj[0].termsNo > 0)
                    {
                        objresult.termsNo = 1;
                    }

                    //objresult.termsNo = (obj[0].termsNo < 0) ? 0 : obj[0].termsNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TermsRepository.InsertTermsmaster" + tblterms.termsNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblterms.venueNo, tblterms.venuebranchno, 0);
            }
            return objresult;
        }
    }
}


 

