using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IPharmacyRepository
    { 
        List<TblGeneric> GetGeneric(reqgeneric req);
        GenericMasterResponse InsertGeneric(TblGeneric tblGeneric);
        List<TblMedtype> GetMedicinetype(reqmedtype medtype);
        MedtypeMasterResponse InsertMedtype(TblMedtype tblmedtype);
        List<TblMedstr> GetMedstr(reqmedstr medstr);
        MedstrMasterResponse InsertMedstr(TblMedstr tblmedstr);
    }
}
