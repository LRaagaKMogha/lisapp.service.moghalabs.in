using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IspecializationRepository
    {
        List<Tblspecialization> Getspecializationmaster(SpecializationMasterRequest specializationitem);
        SpecializationMasterResponse Insertspecializatiomaster(Tblspecialization tblspecialization);
        int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest);
    }
}