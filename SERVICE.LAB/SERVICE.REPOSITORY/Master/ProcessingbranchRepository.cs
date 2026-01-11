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
    public class ProcessingbranchRepository : IProcessingbranchRepository
    {
        private IConfiguration _config;
        public ProcessingbranchRepository(IConfiguration config) { _config = config; }


        public List<responsebranch> GetProcessingbranch(reqbranch req)
        {
            List<responsebranch> lst = new List<responsebranch>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueNo", req?.venueNo);
                    var _processingBranchMapNo = new SqlParameter("processingBranchMapNo", req?.processingBranchMapNo);
                    var _pageIndex = new SqlParameter("pageIndex", req?.pageIndex);
                    var _billedBranchNo = new SqlParameter("billedBranchNo", req?.billedBranchNo);
                    var _processingNo = new SqlParameter("processingNo", req?.processingNo);
                    var _deptNo = new SqlParameter("deptNo", req?.deptNo);
                    var _testNo = new SqlParameter("testNo", req?.testNo);
                    lst = context.GetProcessingbranch.FromSqlRaw(
                        "Execute dbo.pro_GetProcessingBranch @venueNo,@processingBranchMapNo,@PageIndex,@billedBranchNo,@processingNo,@deptNo,@testNo",
                        _venueno, _processingBranchMapNo, _pageIndex, _billedBranchNo, _processingNo, _deptNo, _testNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProcessingbranchRepository.GetProcessingbranch" + req.processingBranchMapNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return lst;
        }
        public Storeprocessingbranch InsertProcessingbranch(insertbranch obj1)
        {
            Storeprocessingbranch objresult = new Storeprocessingbranch();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _processingBranchMapNo = new SqlParameter("processingBranchMapNo", obj1?.processingBranchMapNo);
                    var _billedBranchNo = new SqlParameter("billingBranchNo", obj1?.billedBranchNo);
                    var _processingNo = new SqlParameter("processingBranchNo", obj1?.processingNo);
                    var _testNo = new SqlParameter("testNo", obj1?.testNo);
                    var _venueNo = new SqlParameter("venueNo", obj1?.venueNo);
                    var _status = new SqlParameter("status", obj1?.status);
                    var _userNo = new SqlParameter("userNo", obj1?.userNo);

                    var obj = context.InsertProcessingbranch.FromSqlRaw(
                    "Execute dbo.pro_InsertProcessingBranch @ProcessingBranchMapNo,@BillingBranchNo,@ProcessingBranchNo,@TestNo,@VenueNo,@Status,@userNo",
                    _processingBranchMapNo, _billedBranchNo, _processingNo, _testNo, _venueNo, _status, _userNo).ToList();
                    objresult.processingBranchMapNo = obj[0].processingBranchMapNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProcessingbranchRepository.InsertProcessingbranch" + obj1.processingBranchMapNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, obj1.venueNo, obj1.userNo, 0);
            }
            return objresult;
        }



    }
}