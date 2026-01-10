using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{

    //Login Request & Response
    public class ExternalPatientLoginRequest
    {
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalPatientLoginoutput
    {
        public string? result { get; set; }
    }
    public class ExternalPatientLoginResponse
    {
        public int status { get; set; }
        public int userNo { get; set; }
        public string? otp { get; set; }
    }

    //OTP Verifiy
    public class ExternalPatientOTPRequest
    {
        public string? otp { get; set; }
        public int userNo { get; set; }
    }
    public class ExternalPatientOTPResponse
    {
        public int status { get; set; }
        public string? username { get; set; }
    }

    //Signup
    public class ExternalPatientSignupRequest
    {
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? DOB { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }

    }
    public class ExternalPatientSignupResponse
    {
        public int status { get; set; }
        public string? message { get; set; }
        public int userNo { get; set; }
        public string? otp { get; set; }
    }
    public class ExternalPatientEditRequest
    {
        public int UserNo { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? DOB { get; set; }
        public string? EmailId { get; set; }
    }
    public class ExternalPatientEditResult
    {
        public string? result { get; set; }
    }
    public class ExternalPatientEditResponse
    {
        public int status { get; set; }
        public string? message { get; set; }
    }
    public class ExternalPatientMasterData
    {
        public List<ExternalPatientMasterDataResponse>? lstTitle { get; set; }
        public List<ExternalPatientMasterDataResponse>? lstRelationship { get; set; }
        public List<ExternalPatientMasterDataResponse>? lstGender { get; set; }

    }
    public class ExternalPatientMasterDataResponse
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
    public class ExternalPatientCommonRequest
    {
        public int userNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalPatientUserDetail
    {
        public int PatientNo { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? DOB { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Pincode { get; set; }
    }
    public class ExternalPatientfamilyResponse
    {
        public int userNo { get; set; }
        public string? PatientId { get; set; }
        public string? Name { get; set; }
        public string? Relationship { get; set; }
    }
    public class ExternalPatientServiceResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public List<ExternalPatientService>? lstTest { get; set; }
    }
    public class ExternalPatientService
    {
        public int ServiceNo { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public int RateListNo { get; set; }
        public decimal Amount { get; set; }
        public bool IsFasting { get; set; }
    }
    public class ExternalPatientAddmember
    {
        public int SignupNo { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Relationship { get; set; }
    }
    public class ExternalPatientAppResponse
    {
        public int status { get; set; }
        public string? message { get; set; }
    }
    public class ExternalPatientPayment
    {
        public string? BookingID { get; set; }
        public int GrossAmount { get; set; }
        public int DiscountAmount { get; set; }
        public int NetAmount { get; set; }
        public int PaidAmount { get; set; }
        public int CollectedAmount { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<ExternalLstPaymentDetail>? lstPaymentDetails { get; set; }
    }

    public class ExternalPatientAppServiceResponse
    {
        public int status { get; set; }
        public string? message { get; set; }
        public List<ServiceSearchDTO>? lstService { get; set; }
    }
    public class ExternalLstPaymentDetail
    {
        public int result { get; set; }
        public string? PaymentType { get; set; }
    }
    public class ExternalPatientCommonResult
    {
        public string? result { get; set; }
    }
    public class ExternalPatientAddMemberResult
    {
        public int result { get; set; }
    }
}

