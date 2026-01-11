using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IEditBillingRepository
    {
        //List<TblCountry> GetCountry();
        //List<TblState> GetState();
        //List<TblCity> GetCity();
        //List<TblPhysician> GetPhysicianDetails(int VenueNo, int VenueBranchNo);
        //List<TblDiscount> GetDiscountMaster(int VenueNo, int VenueBranchNo);
        //List<CustomerList> GetCustomers(int VenueNo, int VenueBranchNo);
        //CustomerList GetCustomerDetails(long Customerno, int VenueNo, int VenueBranchNo);
        //List<GroupTestDTO> GetGrouptest(int ServiceNo, int VenueNo, int VenueBranchNo);
        //List<TblCurrency> GetCurrency(int VenueNo, int VenueBranchNo);
        //List<ServiceSearchDTO> GetService(int VenueNo, int VenueBranchNo);
        //ServiceRateList GetServiceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo);
        FrontOffficeResponse InsertEditBilling(FrontOffficeDTO objDTO);
       // ReportOutput PrintBill(ReportRequestDTO req);
        GetEditPatientDetailsFinalResponse GetEditPatientDetails(long visitNo,int VenueNo, int VenueBranchNo);
        dynamic ValidatePTTTest(int ServiceNo, string ServiceType, int VisitNo, int VenueNo, int VenueBranchNo);
       // List<rescheckExists> checkExists(reqcheckExists req);
    }
}

