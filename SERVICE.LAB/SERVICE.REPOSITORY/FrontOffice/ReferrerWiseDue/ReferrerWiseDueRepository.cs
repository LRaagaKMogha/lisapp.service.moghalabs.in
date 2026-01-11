using Dev.IRepository;
using Dev.IRepository.FrontOffice;
using DEV.Common;
using Service.Model;
using Service.Model.EF.ReferrerWiseDue;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.FrontOffice.ReferrerWiseDue
{
    public class ReferrerWiseDueRepository : IReferrerWiseDueRepository
    {
        private IConfiguration _configuration;

        public ReferrerWiseDueRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RefWiseDueResponse GetRefWiseDueResponses(RefWiseDueRequest request)
        {
            RefWiseDueResponse response = new RefWiseDueResponse();
            List<RefWiseDueResponseList> responseList = new List<RefWiseDueResponseList>();
            List<RefWiseDueResponseData> responseData = new List<RefWiseDueResponseData>();

            try
            {
                using (var dbcontext = new ReferrerWiseDueContext(_configuration.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RefTypeNo = new SqlParameter("@RefTypeNo", request.RefTypeNo);
                    var _ReferrerNo = new SqlParameter("@ReferrerNo", request.ReferrerNo);
                    var _PageIndex = new SqlParameter("@PageIndex", request.PageIndex);
                    var _UserNo = new SqlParameter("@UserNo", request?.UserNo);
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _BranchNo = new SqlParameter("@BranchNo", request.BranchNo);
                    var _FilterBranchNo = new SqlParameter("@FilterBranchNo", request.FilterBranchNo);

                    responseData = dbcontext.FetchRefWiseDueResponse.FromSqlRaw(
                    "EXECUTE pro_LB_ReferrerWise_Due_Details " +
                    "@RefTypeNo, @ReferrerNo, @PageIndex, @UserNo, @VenueNo, @BranchNo, @FilterBranchNo",
                    _RefTypeNo, _ReferrerNo, _PageIndex, _UserNo, _VenueNo, _BranchNo, _FilterBranchNo).ToList();

                    if (responseData.Count > 0)
                    {
                        response.TotalRecords = responseData[0].TotalRecords;
                        response.PageIndex = responseData[0].PageIndex;
                        response.PageVisitCount = responseData[0].PageVisitCount;
                        response.PageDueAmount = responseData[0].PageDueAmount;
                        response.ReportVisitCount = responseData[0].ReportVisitCount;
                        response.ReportDueAmount = responseData[0].ReportDueAmount;

                        foreach(var item in responseData) {
                            RefWiseDueResponseList objResult = new RefWiseDueResponseList();
                            objResult.RowNo = item.RowNo;
                            objResult.BranchNo = item.BranchNo;
                            objResult.BranchName = item.BranchName;
                            objResult.RefTypeNo = item.RefTypeNo;
                            objResult.ReferralType  = item.ReferralType;
                            objResult.ReferrerNo = item.ReferrerNo;
                            objResult.ReferrerName = item.ReferrerName;
                            objResult.VisitCount = item.VisitCount;
                            objResult.DueAmount = item.DueAmount;

                            responseList.Add(objResult);
                        }

                        response.RefWiseDueResponseList = responseList;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReferrerWiseDueRepository.GetRefWiseDueResponses", ExceptionPriority.High, ApplicationType.REPOSITORY, request.VenueNo, request.BranchNo, request.UserNo);
            }

            return response;
        }
    }
}
