using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class UpdateStandardPatinetReport
    {
        public Int64? GenderId { get; set; }
        public DateTime? LastModifiedDateTime { get; set; } = DateTime.Now;
        public string? NRICNo { get; set; }
        public DateTime? PatientDOB { get; set; }
        public string? PatientName { get; set; }              
        public Int64 RegistrationID { get; set; }                
        public Int64? UserNo { get; set; }
        public int? VenueBranchNo { get; set; }
        public int? VenueNo { get; set; }
        public Int64? NationalityId { get; set; }
        public Int64? RaceId { get; set; }
        public Int64? ResidenceStatusId { get; set; }
        public string? Result { get; set; }
        public string? Comments { get; set; }
        public UpdateStandardPatinetReport()
        {

        }

        public UpdateStandardPatinetReport(
            Int64? genderId,
            DateTime? lastModifiedDateTime,            
            string? nRICNo,
            DateTime? patientDOB,
            string? patientName,
            Int64 registrationID,
            Int64? userNo,
            int? venueBranchNo,
            int? venueNo,
            Int64? nationalityId,
            Int64? raceId,
            Int64? residenceStatusId,
            string? result,
            string? comments
        )
        {
            GenderId = genderId;
            LastModifiedDateTime = lastModifiedDateTime;            
            NRICNo = nRICNo;
            PatientDOB = patientDOB;
            PatientName = patientName;
            RegistrationID = registrationID;
            UserNo = userNo;
            VenueBranchNo = venueBranchNo;
            VenueNo= venueNo;
            NationalityId = nationalityId;
            RaceId = raceId;
            ResidenceStatusId = residenceStatusId;
            Result = result;
            Comments = comments;
        }

        public static ErrorOr<UpdateStandardPatinetReport> Create(
             Int64? genderId,
            DateTime? lastModifiedDateTime,
            string? nRICNo,
            DateTime? patientDOB,
            string? patientName,
            Int64 registrationID,
            Int64? userNo,
            int? venueBranchNo,
            int? venueNo,
            Int64? nationalityId,
            Int64? raceId,
            Int64? residenceStatusId,string? result,string? comments)
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new UpdateStandardPatinetReport(genderId,lastModifiedDateTime,nRICNo,patientDOB,patientName,registrationID,userNo,venueBranchNo,venueNo,nationalityId,raceId,residenceStatusId,result,comments);
        }

        public static ErrorOr<UpdateStandardPatinetReport> From(UpdateStandardPatientRequest request)
        {            
            return Create(request.GenderId,request.LastModifiedDateTime,request.NRICNo,request.PatientDOB,request.PatientName,request.RegistrationID,request.UserNo,request.VenueBranchNo,request.VenueNo,request.NationalityId,request.RaceId,request.ResidenceStatusId,request.Result,request.Comments);
        }
    }

}
