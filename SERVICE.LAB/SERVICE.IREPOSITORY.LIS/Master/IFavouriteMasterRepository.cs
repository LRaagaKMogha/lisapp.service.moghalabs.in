using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model.Sample;


namespace Dev.IRepository
{
    public interface IFavouriteMasterRepository
    {
        // List<GetAnalyzerMasterResponse> GetAnalyzerMasterDetails(GetAnalyzerMasterRequest getRequest);

        List<Tblfav> GetFavouriteMasterDetails(GetCommonMasterRequest getfav);

        List<Tblgroup> GetGroupDetails(int VenueNo, int VenueBranchNo);

        List<Tblpack> GetPackDetails(int VenueNo, int VenueBranchNo);
        //List<Favoritemaster> GetCustomservice(CommonSearchRequest searchRequest);
        int InsertfavDetails(Tblfav favitem);
        // InsertTariffMasterResponse InsertTariffMasterDetails(InsertTariffMasterRequest tariffMasteritem);
    }
}

