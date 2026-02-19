using System;
using System.Collections.Generic;
using System.Linq;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class Favouritemaster : ControllerBase
    {
        private readonly IFavouriteMasterRepository _FavouriteMasterRepository;
        public Favouritemaster(IFavouriteMasterRepository favouriteMasterRepository)
        {
            _FavouriteMasterRepository = favouriteMasterRepository;
        }

        [HttpPost]
        [Route("api/Favouritemaster/GetFavouriteMasterDetails")]
        public IEnumerable<Tblfav> GetFavouriteMasterDetails(GetCommonMasterRequest getfav)
       {
            List<Tblfav> Objresult = new List<Tblfav>();
            try
            {                
                Objresult = _FavouriteMasterRepository.GetFavouriteMasterDetails(getfav);               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetFavouriteMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getfav.venueno, getfav.venuebranchno, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/Favmaster/Getgroupdetails")]
        public List<Tblgroup> GetGroupDetails(int VenueNo, int VenueBranchNo)
        {
            List<Tblgroup> Objresult = new List<Tblgroup>();
            try
            {
                Objresult = _FavouriteMasterRepository.GetGroupDetails(VenueNo, VenueBranchNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetGroupDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/Favmaster/Getpackdetails")]
        public List<Tblpack> GetPackDetails(int VenueNo, int VenueBranchNo)
        {
            List<Tblpack> Objresult = new List<Tblpack>();
            try
            {
                Objresult = _FavouriteMasterRepository.GetPackDetails(VenueNo, VenueBranchNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPackDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/FavMaster/InsertfavDetails")]
        public int InsertfavDetails([FromBody] Tblfav favitem)
        {
            int result = 0;
            try
            {
                string _CacheKey = CacheKeys.CommonMaster + "FAVORITE" + favitem.VenueNo + favitem.VenueBranchNo ;
                _FavouriteMasterRepository.InsertfavDetails(favitem);
                MemoryCacheRepository.RemoveItem(_CacheKey);
                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertfavDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, favitem.VenueNo,favitem.VenueBranchNo , 0);
            }
            return result;
        }
    }
}




