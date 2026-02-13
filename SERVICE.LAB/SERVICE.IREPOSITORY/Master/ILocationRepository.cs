using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IlocationMasterRepository
    {
        List<TblCountry> GetCountrymaster(CountryMasterRequest countryMasterRequest);
        CountryMasteResponse InsertCountrymaster(Countrytab Country);
        //state
        List<lstState> GetStatemaster(StateRequest state);
        StateResponse InsertStatemaster(Statetab state);
        //City
        List<CityLst> GetCitymaster(CityRequest city);
        CityResponse InsertCitymaster(Citytab city);
        //Place
        List<PlaceLst> GetPlacemaster(PlaceRequest place);
        PlaceResponse InsertPlacemaster(PlaceLst place);
        //Nationality
        List<NationalityLst> GetNationalityMaster(NationalityRequest Nationality);
        NationalityResponse InsertNationalitymaster(NationalityLst Nationality);
    }
}
