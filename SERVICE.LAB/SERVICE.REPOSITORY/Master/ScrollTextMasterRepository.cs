using Dev.IRepository.Master;
using DEV.Common;
using Service.Model.EF;
using Service.Model.Master;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Dev.Repository.Master
{
    public class ScrollTextMasterRepository : IScrollTextMasterRepository
    {
        private IConfiguration _config;
        public ScrollTextMasterRepository(IConfiguration config) { _config = config; }
        public List<ScrollTextMasterResponse> GetScrollTextMaster(GetScrollTextMasterRequest scrollMaster)
        {
            List<ScrollTextMasterResponse> result = new List<ScrollTextMasterResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _UserNo = new SqlParameter("UserNo", scrollMaster?.userNo);
                    var _venueNo = new SqlParameter("venueNo", scrollMaster?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", scrollMaster?.venueBranchno);
                    var _PageIndex=new SqlParameter("PageIndex",scrollMaster?.PageIndex);
                    result = context.GetScrollTextMasterResponseDTO.FromSqlRaw(
                       "Execute dbo.Pro_GetScrollTextMessage @UserNo,@venueNo,@venueBranchno,@PageIndex",
                        _UserNo, _venueNo, _venueBranchno, _PageIndex).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ScrollTextMasterRepository. GetScrollTextMaster" + scrollMaster.userNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, scrollMaster.venueNo, scrollMaster.venueBranchno, 0);
            }
            return result;
        }


        public SaveScrollTextMasterResponse InsertScrollTextMaster(SaveScrollTextMasterRequest request)
        {
            SaveScrollTextMasterResponse objresult = new SaveScrollTextMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    string xmlData = commonUtility.ToXML(request);

                    var _xmlData = new SqlParameter("XmlData", xmlData);

                    var obj = context.SaveScrollTextMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertScrollTextMaster @XmlData", _xmlData).ToList();

                    objresult.Result = obj[0].Result;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, "ScrollTextMasterRepository.InsertScrollTextMastermaster" + request.CreatedBy.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, request.VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}
