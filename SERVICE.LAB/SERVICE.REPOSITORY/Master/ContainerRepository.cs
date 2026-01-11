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
    public class ContainerRepository : IContainerRepository
    {
        private IConfiguration _config;
        public ContainerRepository(IConfiguration config) { _config = config; }

        public List<TblContainer> Getcontainermaster(ContainerMasterRequest containerRequest)
        {
            List<TblContainer> objresult = new List<TblContainer>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _containerNo = new SqlParameter("containerNo", containerRequest?.containerNo);
                    var _venueNo = new SqlParameter("venueNo", containerRequest?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", containerRequest?.venueBranchno);
                    var _pageIndex = new SqlParameter("pageIndex", containerRequest?.pageIndex);
                    objresult = context.Getcontainer.FromSqlRaw(
                        "Execute dbo.pro_GetContainermaster @containerNo, @venueNo, @venueBranchno,@pageIndex",
                         _containerNo, _venueNo, _venueBranchno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ContainerRepository.GetcontainerDetails" + containerRequest.containerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, containerRequest.venueNo, containerRequest.venueBranchno, 0);
            }
            return objresult;
        }

        public ContainerMasterResponse Insertcontainermaster(TblContainer tblContainer)
        {
            ContainerMasterResponse objresult = new ContainerMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _containerNo = new SqlParameter("containerNo", tblContainer?.containerNo);
                    var _containerCode = new SqlParameter("containerCode", tblContainer?.containerCode);
                    var _containerName = new SqlParameter("containerName", tblContainer?.containerName);
                    var _description = new SqlParameter("description", tblContainer?.description);
                    var _containerVolume = new SqlParameter("containerVolume", tblContainer?.containerVolume);
                    var _isContainerimage = new SqlParameter("isContainerimage", tblContainer?.isContainerimage);
                    var _containerImagename = new SqlParameter("containerImagename", tblContainer?.containerImagename);
                    var _status = new SqlParameter("status", tblContainer?.status);
                    //var _isActive = new SqlParameter("isActive", tblContainer.isActive);
                    var _venueno = new SqlParameter("venueno", tblContainer?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblContainer?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblContainer?.userNo);
                    var _imageColor = new SqlParameter("imageColor", tblContainer?.imageColor);

                    objresult = context.Insertcontainer.FromSqlRaw(
                        "Execute dbo.pro_InsertContainer @containerNo,@containerCode,@containerName,@description," +
                        "@containerVolume,@isContainerimage,@containerImagename,@status,@venueNo,@venueBranchno,@userNo,@imageColor",
                         _containerNo, _containerCode, _containerName, _description, _containerVolume,
                         _isContainerimage, _containerImagename, _status, _venueno, _venueBranchno, _userNo, _imageColor).AsEnumerable().FirstOrDefault();
                   

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ContainerRepository.Insertcontainermaster" + tblContainer.containerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblContainer.venueNo, tblContainer.venueBranchno, 0);
            }
            return objresult;
        }

    }
}