using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IspecializationRepository
    {

        List<Tblspecialization> Getspecializationmaster(SpecializationMasterRequest specializationitem);
        SpecializationMasterResponse Insertspecializatiomaster(Tblspecialization tblspecialization);
        int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest);
    }

}
