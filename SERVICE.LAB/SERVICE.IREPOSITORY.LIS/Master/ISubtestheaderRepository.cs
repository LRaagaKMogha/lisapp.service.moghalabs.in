using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface ISubtestheaderRepository
    {

        List<TblSubtestheader> GetSubtestheadermaster(SubtestheaderMasterRequest subtestheaderMasterRequest);
        SubtestheaderMasterResponse InsertSubtestheadermaster(TblSubtestheader testheader);

        //SpecializationMasterResponse Insertspecializatiomaster(Tblspecialization tblspecialization);
        //int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest);
    }

}
