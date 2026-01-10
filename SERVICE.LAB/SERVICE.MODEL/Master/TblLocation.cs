using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEV.Model
{
    public partial class TblCountry
    {

        public int countryNo { get; set; }
        public string? countryName { get; set; }
        public string? Capital { get; set; }
        public string? isdCode { get; set; }
        public Byte? sequenceNo { get; set; }
        public Int16? currencyNo { get; set; }

        public int userNo { get; set; }
        public bool? status { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string isoCode { get; set; }
    }
    public partial class Countrytab
    {

        public int countryNo { get; set; }
        public string? countryName { get; set; }
        public string? Capital { get; set; }
        public string? isdCode { get; set; }
        public Byte? sequenceNo { get; set; }
        public Int16? currencyNo { get; set; }

        public int userNo { get; set; }
        public bool? status { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int VenueNo { get; set; }
        public int Venuebranchno { get; set; }
        public string isoCode { get; set; }
    }

    public class CountryMasterRequest
    {
        public int countryNo { get; set; }

        public Int16? currencyNo { get; set; }
        public int pageIndex { get; set; }
        public int VenueNo { get; set; }
    }
    public class CountryMasteResponse
    {
        public int countryNo { get; set; }
    }

    //State

    public partial class lstState
    {

        public int stateNo { get; set; }
        public string? stateName { get; set; }
        public int statecount { get; set; }
        public int? CountryNo { get; set; }
        public string? countryName { get; set; }
        public bool? status { get; set; }
        public bool? isunionTerritory { get; set; }
        public Int16? sequenceNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }

    }
    public partial class Statetab
    {

        public int stateNo { get; set; }
        public string? stateName { get; set; }
        public int statecount { get; set; }
        public int? CountryNo { get; set; }
        public string? countryName { get; set; }
        public bool? status { get; set; }
        public bool? isunionTerritory { get; set; }
        public Int16? sequenceNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int VenueNo { get; set; }
        public int Venuebranchno { get; set; }


    }

    public class StateRequest
    {
        public int stateNo { get; set; }
        public int? countryNo { get; set; }
        public int pageIndex { get; set; }
        public int venueNo { get; set; }

    }
    public class StateResponse
    {
        public int stateNo { get; set; }
    }

    //City
    public partial class CityLst
    {

        public int cityNo { get; set; }
        public string? cityName { get; set; }
        public int stateNo { get; set; }
        public string? stateName { get; set; }
        public int CountryNo { get; set; }
        public string? countryName { get; set; }

        public bool? status { get; set; }
        public Int16 sequenceNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class Citytab
    {

        public int cityNo { get; set; }
        public string? cityName { get; set; }
        public int stateNo { get; set; }
        public string? stateName { get; set; }
        public int CountryNo { get; set; }
        public string? countryName { get; set; }

        public bool? status { get; set; }
        public  Int16 sequenceNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int VenueNo { get; set; }
        public int Venuebranchno { get; set; }

    }
    public class CityRequest
    {
        public int cityNo { get; set; }
        public int countryNo { get; set; }
        public int stateNo { get; set; }
        public int pageIndex { get; set; }
        public int VenueNo { get; set; }
    }
    public class CityResponse
    {
        public int cityNo { get; set; }
    }

    //Place

    public partial class PlaceLst
    {

        public int placeMasterNo { get; set; }
        public string? placeName { get; set; }
        public int cityNo { get; set; }
        public string? cityName { get; set; }
        public int stateNo { get; set; }
        public string? stateName { get; set; }
        public int CountryNo { get; set; }
        public string? countryName { get; set; }
        public string? pinCode { get; set; }
        public string? stdCode { get; set; }
        public bool? status { get; set; }
        public int userNo { get; set; }
        public int VenueNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    
    }
    public class PlaceRequest
    {
        public int placeMasterNo { get; set; }
        public int cityNo { get; set; }
        public int countryNo { get; set; }
        public int stateNo { get; set; }
        public int pageIndex { get; set; }
        public int VenueNo { get; set; }

    }
    public class PlaceResponse
    {
        public int placeMasterNo { get; set; }
    }

    //Nationality
    public partial class NationalityLst
    {

        public short nationalityMasterNo { get; set; } 
        public string? description { get; set; }
        public int sequenceNo { get; set; }
        public bool? status { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        [NotMapped]
        public short VenueNo { get; set; }
    }
    public class NationalityRequest
    {
        public Int16 nationalityMasterNo { get; set; }
        public int pageIndex { get; set; }
        public int VenueNo { get; set; }

    }
    public class NationalityResponse
    {
        public Int16 nationalityMasterNo { get; set; }

    }
}

