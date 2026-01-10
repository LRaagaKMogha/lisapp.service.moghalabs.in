using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class PackRepository : IPackRepository
    {
        private IConfiguration _config;
        public PackRepository(IConfiguration config) { _config = config; }

        public List<TblPack> Getpackmaster(PackMasterRequest packRequest)
        {
            List<TblPack> objresult = new List<TblPack>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _packNo = new SqlParameter("packNo", packRequest?.packNo);
                    var _venueNo = new SqlParameter("venueNo", packRequest?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", packRequest?.pageIndex);

                    objresult = context.Getpack.FromSqlRaw(
                    "Execute dbo.pro_Getpackmaster @packNo, @venueNo,@pageIndex",
                    _packNo, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PackRepository.Getpackmaster" + packRequest.packNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, packRequest.venueNo, packRequest.venueBranchno, 0);
            }
            return objresult;
        }

        public PackMasterResponse Insertpackmaster(TblPack tblPack)
        {
            PackMasterResponse objresult = new PackMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _packNo = new SqlParameter("packNo", tblPack?.packNo);
                    var _description = new SqlParameter("description", tblPack?.description);
                    var _quantity = new SqlParameter("quantity", tblPack?.quantity);            
                    var _sequenceNo = new SqlParameter("sequenceNo", tblPack?.sequenceNo);
                    var _status = new SqlParameter("status", tblPack?.status);
                    var _venueNo = new SqlParameter("venueNo", tblPack?.venueNo);
                    var _userNo = new SqlParameter("userNo", tblPack?.userNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblPack?.venueBranchno);

                    var obj = context.Insertpack.FromSqlRaw(
                    "Execute dbo.pro_InsertPackmaster @packNo,@description,@quantity,@sequenceNo," +
                    "@status,@venueNo,@userNo,@VenueBranchNo",
                    _packNo, _description, _quantity, _sequenceNo, _status,_venueNo, _userNo, _venueBranchno).ToList();
                    
                    objresult.packNo = obj[0].packNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PackRepository.Insertpackmaster" + tblPack.packNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblPack.venueNo, tblPack.venueBranchno, tblPack.userNo);
            }
            return objresult;
        }
    }
}