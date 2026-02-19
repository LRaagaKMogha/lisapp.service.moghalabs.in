using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IVitalSignRepository
    {
        List<VitalSignDTO> GetVitalSignList(VitalSignDTORequest RequestItem);
        SaveVitalSignDTOResponse InsertVitalSign(SaveVitalSignDTORequest req);
        List<VitalSignMastersResponse> GetVitalSignMasters(VitalSignMastersRequest RequestItem);
        List<GetAllergyResponse> GetAllergydetails(GetAllergyRequest RequestItem);
        SaveAllergyResponse SaveAllergydetails(SaveAllergyRequest objDTO);
        List<GetDiseasesResponse> GetDiseasesDetails(GetDiseasesRequest RequestItem);
        SaveDiseasesResponse SaveDiseasesDetails(SaveDiseasesRequest objDTO);
        string GetVitalResultHistory(GetAllergyRequest RequestItem);
        List<lstVaccineSchedule> GetVaccineSchedule(GetVaccineScheduleRequest req);
        void SaveVaccineRecord(SaveVaccineRecordDTORequest req);
        lstPatientLatestVisit GetPatientLatestVisit(GetLatestPatientVisitRequest req);
        List<GetVitalResultResponse> GetVitalResultDateTime(GetAllergyRequest RequestItem);
    }
}
