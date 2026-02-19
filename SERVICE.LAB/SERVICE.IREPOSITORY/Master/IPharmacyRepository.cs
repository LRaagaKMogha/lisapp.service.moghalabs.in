using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IPharmacyRepository
    { 
        List<TblGeneric> GetGeneric(Reqgeneric req);
        GenericMasterResponse InsertGeneric(TblGeneric tblGeneric);
        List<TblMedtype> GetMedicinetype(Reqmedtype medtype);
        MedtypeMasterResponse InsertMedtype(TblMedtype tblmedtype);
        List<TblMedstr> GetMedstr(Reqmedstr medstr);
        MedstrMasterResponse InsertMedstr(TblMedstr tblmedstr);
    }
}
