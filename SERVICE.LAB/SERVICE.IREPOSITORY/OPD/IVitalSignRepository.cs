using Service.Model;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dev.IRepository
{
    public interface IVitalSignRepository
    {
        List<VitalSignDTO> GetVitalSignList(VitalSignDTORequest RequestItem);
        SaveVitalSignDTOResponse InsertVitalSign(SaveVitalSignDTORequest req);
        List<VitalSignMastersResponse> GetVitalSignMasters(VitalSignMastersRequest RequestItem);
        List<GetAllergyResponse> GetAllergyDetails(GetAllergyRequest RequestItem);
        SaveAllergyResponse SaveAllergyDetails(SaveAllergyRequest objDTO);
        List<GetDiseasesResponse> GetDiseasesDetails(GetDiseasesRequest RequestItem);
        SaveDiseasesResponse SaveDiseasesDetails(SaveDiseasesRequest objDTO);
        string GetVitalResultHistory(GetAllergyRequest RequestItem);
        List<lstVaccineSchedule> GetVaccineSchedule(GetVaccineScheduleRequest req);
        void SaveVaccineRecord(SaveVaccineRecordDTORequest req);
        lstPatientLatestVisit GetPatientLatestVisit(GetLatestPatientVisitRequest req);
        List<GetVitalResultResponse> GetVitalResultDateTime(GetAllergyRequest RequestItem);
    }
}
