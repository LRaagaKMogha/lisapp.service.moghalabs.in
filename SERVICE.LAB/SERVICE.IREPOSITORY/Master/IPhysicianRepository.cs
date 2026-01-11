using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IPhysicianRepository
    {
        List<PhysicianDetailsResponse> GetPhysicianDetails(GetCommonMasterRequest masterRequest);
        int InsertPhysicianDetails(TblPhysician tblPhysician);
        int PhysicianMerging(List<TblPhysician> mergingPhysicians, int VenueNo, int VenueBranchNo, int UserID, int physicianNo);
        int PhysicianHaveVisits(TblPhysician tblPhysician);
        int SavePhysicianDetaile(TblPhysician tblPhysician);
        int DocumentUploadDetails(List<DocumentUploadlst> documentUploadlst, int VenueNo, int UserID, int physicianNo);
        List<PhysicianDocUploadDetailRes> GetPhysicianDocumentDetails(PhysicianDocUploadReq Req);
        List<OPDMachineRes> GetMachineTimeDetails(OPDMachineReq Req);
        List<OPDPhysicianRes> GetPhysicianOPDDetails(OPDPhysicianReq Req);
        int OPDPatientDetails(List<OPDPhysicianDetail> opdPhysiciandetail, TblPhysician tblPhysician);
        List<PhysicianOrClientCodeResponse> GetLastPhysicianCode(int VenueNo, int VenueBranchNo,string CodeType, string CodeToCheck = null);
        List<consultantdetails> GetConsultant(getconsultant getconsultant);
        int SaveConsultant(saveConsultant saveConsultant);
    }
}



