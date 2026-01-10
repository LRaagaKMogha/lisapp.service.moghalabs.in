using DEV.Model;
using DEV.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEV.Common;

namespace Dev.Repository
{
    public class FavouriteMasterRepository : IFavouriteMasterRepository
    {
        private IConfiguration _config;
        public FavouriteMasterRepository(IConfiguration config) { _config = config; }

        public List<Tblfav> GetFavouriteMasterDetails(GetCommonMasterRequest getfav)
        {
            List<Tblfav> objresult = new List<Tblfav>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (getfav.masterNo > 0)
                    {
                        objresult = context.Tblfav.Where(x => x.VenueNo == getfav.venueno && x.Status == true).ToList();
                    }
                    else
                    {
                        objresult = context.Tblfav.Where(x => x.VenueNo == getfav.venueno).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FavouriteMasterRepository.GetFavouriteMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, getfav.venueno, getfav.venuebranchno, 0);
            }
            return objresult;
        }
       
        public List<Tblgroup> GetGroupDetails(int VenueNo, int VenueBranchNo)
        {
            List<Tblgroup> objresult = new List<Tblgroup>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.Tblgroup.Where(a => a.VenueNo == VenueNo && a.VenueBranchNo == VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FavouriteMasterRepository.GetGroupDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<Tblpack> GetPackDetails(int VenueNo, int VenueBranchNo)
        {
            List<Tblpack> objresult = new List<Tblpack>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.Tblpack.Where(a => a.VenueNo == VenueNo && a.VenueBranchNo == VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FavouriteMasterRepository.GetPackDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public int InsertfavDetails(Tblfav favitem)
        {
            int result = 0;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (favitem.FavoriteServiceNo > 0)
                    {
                        context.Entry(favitem).State = EntityState.Modified;
                    }
                    else
                    {
                        favitem.CreatedOn = DateTime.Now;
                        context.Tblfav.Add(favitem);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FavouriteMasterRepository.InsertfavDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, favitem.VenueNo,favitem.VenueBranchNo, 0);
            }
            return result;
        }
    }
}

