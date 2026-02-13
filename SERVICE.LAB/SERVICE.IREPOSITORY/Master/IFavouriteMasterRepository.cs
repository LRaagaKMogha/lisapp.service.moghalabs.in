using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IFavouriteMasterRepository
    {
        List<Tblfav> GetFavouriteMasterDetails(GetCommonMasterRequest getfav);
        List<Tblgroup> GetGroupDetails(int VenueNo, int VenueBranchNo);
        List<Tblpack> GetPackDetails(int VenueNo, int VenueBranchNo);
        int InsertfavDetails(Tblfav favitem);
    }
}

