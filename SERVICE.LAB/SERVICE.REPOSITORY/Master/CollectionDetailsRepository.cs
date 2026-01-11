using DEV.Common;
using Dev.IRepository;
using Service.Model.EF;
using Service.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dev.Repository
{
    public class CollectionDetailsRepository : ICollectionDetailsRepository
    {
        private IConfiguration _config;
        public CollectionDetailsRepository(IConfiguration config) { _config = config; }
        public List<lstCollectDTS> GetCollectionDetails(reqCollectDTS collectreq)

        {
            List<lstCollectDTS> lst = new List<lstCollectDTS>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _collectionNo = new SqlParameter("CollectionNo", collectreq.CollectionNo);
                    var _type = new SqlParameter("Type", collectreq.Type);
                    var _selectDate = new SqlParameter("SelectDate", collectreq.SelectDate);
                    var _venueNo = new SqlParameter("VenueNo", collectreq.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", collectreq.VenueBranchNo);
                    lst = context.GetCollectionDetails.FromSqlRaw(
                    "Execute dbo.pro_GetCollectionDetails @collectionNo, @type, @selectDate, @venueNo, @venueBranchNo",
                    _collectionNo, _type, _selectDate, _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CollectionDetailsRepository.GetCollectionDetails - " + collectreq.CollectionNo, ExceptionPriority.Medium, ApplicationType.REPOSITORY, collectreq.VenueNo, collectreq.VenueBranchNo, 0);
            }
            return lst;
        }
        public resCollectDTS UpdateCollectionDetails(updateCollectDTS collectupd)

        {
            resCollectDTS obj = new resCollectDTS();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _collectionNo = new SqlParameter("CollectionNo", collectupd.CollectionNo);
                    var _openingBalance = new SqlParameter("OpeningBalance", collectupd.OpeningBalance);
                    var _closingBalance = new SqlParameter("ClosingBalance", collectupd.ClosingBalance);
                    var _collectionDate = new SqlParameter("CollectionDate", collectupd.CollectionDate);
                    var _venueNo = new SqlParameter("VenueNo", collectupd.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", collectupd.VenueBranchNo);
                    var _userNo = new SqlParameter("UserNo", collectupd.UserNo);
                    var lst = context.UpdateCollectionDetails.FromSqlRaw(
                    "Execute dbo.pro_UpdateCollectionDetails @collectionNo, @openingBalance, @closingBalance, @collectionDate, @venueNo, @venueBranchNo, @userNo",
                          _collectionNo, _openingBalance, _closingBalance, _collectionDate, _venueNo, _venueBranchNo, _userNo).ToList();

                    obj.CollectionNo = lst[0].CollectionNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CollectionDetailsRepository.UpdateCollectionDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, collectupd.VenueNo, collectupd.VenueBranchNo, 0);
            }
            return obj;

        }
    }
}
