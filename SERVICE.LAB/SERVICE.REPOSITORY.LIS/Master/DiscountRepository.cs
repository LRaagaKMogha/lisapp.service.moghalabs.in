using Dev.IRepository.Master;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Master
{
    public class DiscountRepository : IDiscountRepository
    {
        private IConfiguration _config;
        public DiscountRepository(IConfiguration config) { _config = config; }

        public List<GetDiscountDetails> GetDiscountMasters(DiscountMasterRequest discountItem)
        {
            List<GetDiscountDetails> objresult = new List<GetDiscountDetails>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _discountNo = new SqlParameter("discountNo", discountItem?.discountNo);
                    var _venueNo = new SqlParameter("venueNo", discountItem?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", discountItem?.venueBranchno);
                    var _pageIndex = new SqlParameter("pageIndex", discountItem?.pageIndex);

                    objresult = context.GetDiscountMasterData.FromSqlRaw(
                       "Execute dbo.pro_GetDiscountmaster @discountNo,@venueNo,@venueBranchno,@pageIndex",
                         _discountNo, _venueNo, _venueBranchno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiscountRepository.GetDiscountMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, discountItem.venueNo, discountItem.venueBranchno, 0);

            }
            return objresult;
        }
        public DiscountMasterReponse InsertDiscountMasters(DiscountInsertData disResponse)
        {
            DiscountMasterReponse result = new DiscountMasterReponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _discountNo = new SqlParameter("discountNo", disResponse?.discountNo);
                    var _discountName = new SqlParameter("discountName", disResponse?.discountName);
                    var _DiscountFor = new SqlParameter("DiscountFor", disResponse?.DiscountFor);
                    var _Amount = new SqlParameter("Amount", disResponse?.Amount);
                    var _Status = new SqlParameter("Status", disResponse?.Status);
                    var _Gender = new SqlParameter("Gender", disResponse?.Gender);
                    var _AgeRange = new SqlParameter("AgeRange", disResponse?.AgeRange);
                    var _AgeFrom = new SqlParameter("AgeFrom", disResponse?.AgeFrom);
                    var _AgeTo = new SqlParameter("AgeTo", disResponse?.AgeTo);
                    var _venueNo = new SqlParameter("venueNo", disResponse?.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", disResponse?.venueBranchNo);
                    var _IsRebate = new SqlParameter("IsRebate", disResponse?.IsRebate);
                    var _IsPercentage = new SqlParameter("IsPercentage", disResponse?.IsPercentage);
                    var _pageIndex = new SqlParameter("pageIndex", disResponse?.pageIndex);
                    var _UserNo= new SqlParameter("UserNo", disResponse?.UserNo);

                    var obj = context.InsertDiscountMasterData.FromSqlRaw(
                         "Execute dbo.pro_InsertDiscountmaster @discountNo,@discountName, @DiscountFor, @Amount, @Status," +
                         "@Gender,@AgeRange,@AgeFrom,@AgeTo,@venueNo,@venueBranchNo,@IsRebate,@IsPercentage,@pageIndex,@UserNo",
                           _discountNo, _discountName, _DiscountFor, _Amount, _Status, _Gender, _AgeRange, _AgeFrom,
                           _AgeTo, _venueNo, _venueBranchNo, _IsRebate, _IsPercentage, _pageIndex, _UserNo).ToList();
                    result.discountNo = obj[0].discountNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiscountRepository.InsertDiscountMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, disResponse.venueNo, disResponse.venueBranchNo, 0);
            }
            return result;
        }
    }
}