using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEV.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.Office.Interop.Word;

namespace DEV.API.SERVICE.Controllers
{

    [ApiController]
    public class LocationMasterController : ControllerBase
    {
        private readonly IlocationMasterRepository _locationMasterRepository;
        public LocationMasterController(IlocationMasterRepository noteRepository)
        {
            _locationMasterRepository = noteRepository;

        }
        [HttpPost]
        [Route("api/Locationmaster/GetCountrymaster")]
        public IEnumerable<TblCountry> GetCountrymaster(CountryMasterRequest countryMasterRequest)
        {
            List<TblCountry> result = new List<TblCountry>();
            try
            {
                result = _locationMasterRepository.GetCountrymaster(countryMasterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetCountrymaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);             
            }
            return result;
        }
            [HttpPost]
            [Route("api/Locationmaster/InsertCountrymaster")]
            public CountryMasteResponse InsertCountrymaster(Countrytab Country)
                {
                CountryMasteResponse objresult = new CountryMasteResponse();
                try
                {
                    objresult = _locationMasterRepository.InsertCountrymaster(Country);
                    string _CacheKey = CacheKeys.CommonMaster + "CountryName" + Country.VenueNo + Country.Venuebranchno;
                    string _CacheKey1 = CacheKeys.CommonMaster + "countrymaster" + Country.VenueNo + Country.Venuebranchno;
                    MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                    MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey1);
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                    MemoryCacheRepository.RemoveItem(_CacheKey1);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "InsertCountrymaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return objresult;
            }
            //State

            [HttpPost]
            [Route("api/Locationmaster/GetStatemaster")]
            public IEnumerable<lstState> GetStatemaster(StateRequest state)
            {
                List<lstState> result = new List<lstState>();
                try
                {
                    result = _locationMasterRepository.GetStatemaster(state);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "GetStatemaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return result;
            }
            [HttpPost]
            [Route("api/Locationmaster/InsertStatemaster")]
            public StateResponse InsertStatemaster(Statetab state)
            {
                StateResponse objresult = new StateResponse();
                try
                {
                    objresult = _locationMasterRepository.InsertStatemaster(state);
                    string _CacheKey = CacheKeys.CommonMaster + "StateName" + state.VenueNo + state.Venuebranchno;
                    MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                    MemoryCacheRepository.RemoveItem(_CacheKey);

                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "InsertStatemaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return objresult;
            }
            //City

            [HttpPost]
            [Route("api/Locationmaster/GetCitymaster")]
            public IEnumerable<CityLst> GetCitymaster(CityRequest city)
            {
                List<CityLst> result = new List<CityLst>();
                try
                {
                    result = _locationMasterRepository.GetCitymaster(city);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "GetCitymaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return result;
            }
            [HttpPost]
            [Route("api/Locationmaster/InsertCitymaster")]
            public CityResponse InsertCitymaster(Citytab city)
            {
                CityResponse objresult = new CityResponse();
                try
                {
                    objresult = _locationMasterRepository.InsertCitymaster(city);
                    string _CacheKey = CacheKeys.CommonMaster + "CityName" + city.VenueNo + city.Venuebranchno;
                    MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                    MemoryCacheRepository.RemoveItem(_CacheKey);

                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "InsertCitymaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return objresult;
            }
            //Place
            [HttpPost]
            [Route("api/Locationmaster/GetPlacemaster")]
            public IEnumerable<PlaceLst> GetPlacemaster(PlaceRequest place)
            {
                List<PlaceLst> result = new List<PlaceLst>();
                try
                {
                    result = _locationMasterRepository.GetPlacemaster(place);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "GetPlacemaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return result;
            }
        [HttpPost]
        [Route("api/Locationmaster/InsertPlacemaster")]
        public PlaceResponse InsertPlacemaster(PlaceLst place)
        {
            PlaceResponse objresult = new PlaceResponse();
            try
            {
                objresult = _locationMasterRepository.InsertPlacemaster(place);
                string _CacheKey = CacheKeys.CommonMaster + "PINCODE";
                MemoryCacheRepository.RemoveItem(_CacheKey);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertPlacemaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        //Nationality
        [HttpPost]
        [Route("api/Locationmaster/GetNationalityMaster")]
        public IEnumerable<NationalityLst> GetNationalityMaster(NationalityRequest Nationality)
        {
            List<NationalityLst> result = new List<NationalityLst>();
            try
            {
                result = _locationMasterRepository.GetNationalityMaster(Nationality);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetNationalityMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
            }

        [HttpPost]
        [Route("api/Locationmaster/InsertNationalitymaster")]
        public NationalityResponse InsertNationalitymaster(NationalityLst Nationality)
        {
            NationalityResponse objresult = new NationalityResponse();
            try
            {
                objresult = _locationMasterRepository.InsertNationalitymaster(Nationality);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertNationalitymaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
    }

}