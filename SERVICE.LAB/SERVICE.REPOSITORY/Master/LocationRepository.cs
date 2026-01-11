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
using static System.Windows.Forms.AxHost;

namespace Dev.Repository
{
    public class locationMasterRepository : IlocationMasterRepository
    {
        private IConfiguration _config;
        public locationMasterRepository(IConfiguration config) { _config = config; }

        public List<TblCountry> GetCountrymaster(CountryMasterRequest countryMasterRequest)
        {
            List<TblCountry> objresult = new List<TblCountry>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _countryNo = new SqlParameter("countryNo", countryMasterRequest?.countryNo);

                    var _CurrencyNo = new SqlParameter("CurrencyNo", countryMasterRequest?.currencyNo);

                    var _pageIndex = new SqlParameter("pageIndex", countryMasterRequest?.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", countryMasterRequest?.VenueNo);

                    objresult = context.GetCountrymaster.FromSqlRaw(
                        "Execute dbo.pro_GetCountrymaster @countryNo,@CurrencyNo,@pageIndex,@VenueNo",
                         _countryNo, _CurrencyNo, _pageIndex, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.GetCountrymaster" + countryMasterRequest.countryNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
               
            }
            return objresult;
        }

        public CountryMasteResponse InsertCountrymaster(Countrytab Country)
        {
            CountryMasteResponse objresult = new CountryMasteResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _countryNo = new SqlParameter("countryNo", Country?.countryNo);
                    var _countryName = new SqlParameter("countryName", Country?.countryName);
                    var _status = new SqlParameter("status", Country?.status);
                    var _Capital = new SqlParameter("Capital", Country?.Capital);
                    var _isdCode = new SqlParameter("isdCode", Country?.isdCode);
                    var _sequenceNo = new SqlParameter("sequenceNo", Country?.sequenceNo);
                    var _currencyNo = new SqlParameter("currencyNo", Country?.currencyNo);
                    var _VenueNo = new SqlParameter("VenueNo", Country?.VenueNo);
                    var _userNo = new SqlParameter("userNo", Country?.userNo);
                    var _isoCode = new SqlParameter("isoCode", Country?.isoCode);

                    var obj = context.InsertCountrymaster.FromSqlRaw(
                           "Execute dbo.pro_InsertCountrymaster @countryNo,@countryName,@Capital,@ISDCode,@CurrencyNo," +
                           "@sequenceNo,@status,@VenueNo,@userNo,@isoCode",
                              _countryNo, _countryName, _Capital, _isdCode,
                              _currencyNo, _sequenceNo, _status, _VenueNo, _userNo, _isoCode).ToList();

                    objresult.countryNo = obj[0].countryNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.InsertCountrymaster" + Country.countryNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                
            }
            return objresult;

        }
        //state

        public List<lstState> GetStatemaster(StateRequest state)
        {
            List<lstState> objresult = new List<lstState>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _stateNo = new SqlParameter("stateNo", state?.stateNo);
                    var _CountryNo = new SqlParameter("CountryNo", state?.countryNo);
                    var _pageIndex = new SqlParameter("pageIndex", state?.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", state?.venueNo);

                    objresult = context.GetStatemaster.FromSqlRaw(
                        "Execute dbo.pro_GetStatemaster @stateNo,@CountryNo,@pageIndex,@VenueNo",
                         _stateNo, _CountryNo, _pageIndex, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.GetStatemaster" + state.stateNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);

            }
            return objresult;
        }
        public StateResponse InsertStatemaster(Statetab state)
        {
            StateResponse objresult = new StateResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _stateNo = new SqlParameter("stateNo", state?.stateNo);
                    var _stateName = new SqlParameter("stateName", state?.stateName);
                    var _CountryNo = new SqlParameter("CountryNo", state?.CountryNo);
                    var _status = new SqlParameter("status", state?.status);
                    var _isunionTerritory = new SqlParameter("isunionTerritory", state?.isunionTerritory);
                    var _sequenceNo = new SqlParameter("sequenceNo", state?.sequenceNo);
                    var _userNo = new SqlParameter("userNo", state?.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", state?.VenueNo);
                    var obj = context.InsertStatemaster.FromSqlRaw(
                        "EXEC dbo.pro_InsertStatemaster @stateNo,@stateName,@CountryNo,@status,@IsunionTerritory,@sequenceNo,@userNo,@VenueNo",
                        _stateNo, _stateName, _CountryNo, _status, _isunionTerritory, _sequenceNo, _userNo, _VenueNo
                    ).ToList();
                    objresult.stateNo = obj[0].stateNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.InsertStatemaster" + state.stateNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                
            }
            return objresult;

        }
        //City
        public List<CityLst> GetCitymaster(CityRequest city)
        {
            List<CityLst> objresult = new List<CityLst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _cityNo = new SqlParameter("cityNo", city?.cityNo);
                    var _stateNo = new SqlParameter("stateNo", city?.stateNo);
                    var _CountryNo = new SqlParameter("CountryNo", city?.countryNo);
                    var _pageIndex = new SqlParameter("pageIndex", city?.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", city?.VenueNo);

                    objresult = context.GetCitymaster.FromSqlRaw(
                      "Execute dbo.pro_GetCitymaster @cityNo,@stateNo,@CountryNo,@pageIndex,@VenueNo",
                       _cityNo, _stateNo, _CountryNo, _pageIndex, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.GetCitymaster" + city.cityNo.ToString(),  ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            ;
            }
            return objresult;
        }
        public CityResponse InsertCitymaster(Citytab city)
        {
            CityResponse objresult = new CityResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _cityNo = new SqlParameter("cityNo", city?.cityNo);
                    var _cityName = new SqlParameter("cityName", city?.cityName);
                    var _stateNo = new SqlParameter("stateNo", city?.stateNo);
                    var _CountryNo = new SqlParameter("CountryNo", city?.CountryNo);
                    var _status = new SqlParameter("status", city?.status);
                    var _sequenceNo = new SqlParameter("sequenceNo", city?.sequenceNo);
                    var _userNo = new SqlParameter("userNo", city?.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", city?.VenueNo);


                    var obj = context.InsertCitymaster.FromSqlRaw(
                           "Execute dbo.pro_InsertCitymaster @cityNo,@CityName,@stateNo,@CountryNo,@status,@sequenceNo,@userNo,@VenueNo",
                           
                            _cityNo, _cityName, _stateNo, _CountryNo, _status,
                                _sequenceNo, _userNo, _VenueNo).ToList();

                    objresult.cityNo = obj[0].cityNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.InsertCitymaster" + city.cityNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);

            }
            return objresult;

        }

        //Place
        public List<PlaceLst> GetPlacemaster(PlaceRequest place)
        {
            List<PlaceLst> objresult = new List<PlaceLst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _placeMasterNo = new SqlParameter("placeMasterNo", place?.placeMasterNo);
                    var _cityNo = new SqlParameter("cityNo", place?.cityNo);
                    var _stateNo = new SqlParameter("stateNo", place?.stateNo);
                    var _CountryNo = new SqlParameter("CountryNo", place?.countryNo);
                    var _pageIndex = new SqlParameter("pageIndex", place?.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", place?.VenueNo);

                    objresult = context.GetPlacemaster.FromSqlRaw(
                      "Execute dbo.pro_GetPlacemaster @placeMasterNo,@cityNo,@stateNo,@CountryNo,@pageIndex,@VenueNo",
                       _placeMasterNo, _cityNo, _stateNo, _CountryNo, _pageIndex, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.GetPlacemaster" + place.placeMasterNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
               
            }
            return objresult;
        }
        public PlaceResponse InsertPlacemaster(PlaceLst place)
        {
            PlaceResponse objresult = new PlaceResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _placeMasterNo = new SqlParameter("placeMasterNo", place?.placeMasterNo);
                    var _placeName = new SqlParameter("placeName", place?.placeName);
                    var _cityNo = new SqlParameter("cityNo", place?.cityNo);
                    var _pinCode = new SqlParameter("pinCode", place?.pinCode);
                    var _stdCode = new SqlParameter("stdCode", place?.stdCode);
                    var _stateNo = new SqlParameter("stateNo", place?.stateNo);
                    var _CountryNo = new SqlParameter("CountryNo", place?.CountryNo);
                    var _status = new SqlParameter("status", place?.status);
                    var _userNo = new SqlParameter("userNo", place?.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", place?.VenueNo);
                    var obj = context.InsertPlacemaster.FromSqlRaw(
                           "Execute dbo.pro_InsertPlacemaster @placeMasterNo,@placeName,@cityNo,@pinCode,@stdCode,@stateNo,@CountryNo,@status,@userNo,@VenueNo",
                            
                          _placeMasterNo, _placeName, _cityNo, _pinCode, _stdCode, _stateNo, _CountryNo, _status, _userNo, _VenueNo).ToList();

                    objresult.placeMasterNo = obj[0].placeMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.InsertPlacemaster"+place.placeMasterNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
               
            }
            return objresult;

        }

        //Nationality
        public List<NationalityLst> GetNationalityMaster(NationalityRequest Nationality)
        {
            List<NationalityLst> objresult = new List<NationalityLst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _nationalityMasterNo = new SqlParameter("nationalityMasterNo", Nationality?.nationalityMasterNo);
                    var _pageIndex = new SqlParameter("pageIndex", Nationality?.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", Nationality?.VenueNo);
                    objresult = context.GetNationalityMaster.FromSqlRaw(
                      "Execute dbo.pro_GetNationalityMaster @nationalityMasterNo,@pageIndex,@VenueNo",
                       _nationalityMasterNo, _pageIndex, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.GetNationalityMaster" + Nationality.nationalityMasterNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);

               
            }
            return objresult;
        }
        public NationalityResponse InsertNationalitymaster(NationalityLst Nationality)
        {
            NationalityResponse objresult = new NationalityResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _nationalityMasterNo = new SqlParameter("nationalityMasterNo", Nationality?.nationalityMasterNo);
                    var _description = new SqlParameter("description", Nationality?.description);
                    var _status = new SqlParameter("status", Nationality?.status);
                    var _userNo = new SqlParameter("userNo", Nationality?.userNo);
                    var _sequenceNo = new SqlParameter("sequenceNo", Nationality?.sequenceNo);
                    var _VenueNo = new SqlParameter("VenueNo", Nationality?.VenueNo);
                    var obj = context.InsertNationalitymaster.FromSqlRaw(
                           "Execute dbo.pro_InsertNationalitymaster @nationalityMasterNo,@description,@status,@userNo,@sequenceNo,@VenueNo",
                            
                          _nationalityMasterNo, _description, _status,_userNo,_sequenceNo, _VenueNo).ToList();

                    objresult.nationalityMasterNo = obj[0].nationalityMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "LocationRepository.InsertNationalitymaster" + Nationality.nationalityMasterNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
             
            
            }
            return objresult;

        }

    }
}