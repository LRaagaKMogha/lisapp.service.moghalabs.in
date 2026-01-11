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


namespace Dev.Repository
{
    public class MethodRepository : IMethodRepository
    {
        private IConfiguration _config;
        public MethodRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Method Details
        /// </summary>
        /// <returns></returns>
        public List<TblMethod> GetMethods1(GetCommonMasterRequest masterRequest)
        {
            List<TblMethod> objresult = new List<TblMethod>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    if (masterRequest.masterNo > 0)
                    {
                        objresult = context.TblMethod.Where(x => x.VenueNo == masterRequest.venueno && x.MethodNo == masterRequest.masterNo).ToList();
                    }
                    else
                    {
                        objresult = context.TblMethod.Where(x => x.VenueNo == masterRequest.venueno).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetMethods1", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest?.venueno, masterRequest?.venuebranchno, 0);
            }
            return objresult;
        }

        public List<TblMethod> GetMethods(GetCommonMasterRequest masterRequest)
        {
            List<TblMethod> objresult = new List<TblMethod>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MethodNo = new SqlParameter("MethodNo", masterRequest?.MethodNo);
                    var _venueno = new SqlParameter("VenueNo", masterRequest?.venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", masterRequest?.venuebranchno);
                    var _pageIndex = new SqlParameter("pageIndex", masterRequest?.pageIndex);

                    objresult = context.GetMethods.FromSqlRaw(
                        "Execute dbo.pro_GetMethod @MethodNo,@venueNo,@venueBranchNo,@pageIndex",
                        _MethodNo, _venueno, _venuebranchno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MethodRepository.GetMethods" + masterRequest.MethodNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest?.venueno, masterRequest?.venuebranchno, 0);
            }
            return objresult;
        }


        /// <summary>
        /// Insert Method Details
        /// </summary>
        /// <param name="Methoditem"></param>
        /// <returns></returns>
        public int InsertMethodDetails1(TblMethod Methoditem)
        {
            int result = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (Methoditem.MethodNo > 0)
                    {
                        Methoditem.ModifiedOn = DateTime.Now;
                        context.Entry(Methoditem).State = EntityState.Modified;
                    }
                    else
                    {
                        Methoditem.CreatedOn = DateTime.Now;
                        Methoditem.ModifiedOn = DateTime.Now;
                        context.TblMethod.Add(Methoditem);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertMethodDetails1", ExceptionPriority.Low, ApplicationType.REPOSITORY, Methoditem?.VenueNo, Methoditem?.VenueBranchNo, 0);
            }
            return result;
        }
        public List<MethodResponse> InsertMethodDetails(TblMethod Methoditem)
        {
            List<MethodResponse> objresult = new List<MethodResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MethodNo = new SqlParameter("MethodNo", Methoditem?.MethodNo);
                    var _MethodName = new SqlParameter("MethodName", Methoditem?.MethodName);
                    var _MethodDisplayText = new SqlParameter("MethodDisplayText", Methoditem?.MethodDisplayText);
                    var _Status = new SqlParameter("Status", Methoditem?.Status);
                    var _venueno = new SqlParameter("VenueNo", Methoditem?.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", Methoditem?.VenueBranchNo);
                    var _userno = new SqlParameter("userno", Methoditem?.CreatedBy);


                    objresult = context.InsertMethodDetails.FromSqlRaw(
                        "Execute dbo.pro_InsertMethod @MethodNo,@MethodName,@MethodDisplayText,@Status,@VenueNo,@VenueBranchNo,@userno",
                        _MethodNo, _MethodName, _MethodDisplayText, _Status, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MethodRepository.InsertMethodDetails" + Methoditem?.MethodNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Methoditem?.VenueNo, Methoditem?.VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}