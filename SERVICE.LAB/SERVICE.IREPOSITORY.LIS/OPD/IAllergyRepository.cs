using DEV.Model;
using DEV.Model.Inventory.Master;
using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IAllergyRepository
    {
        List<lstAllergyType> GetAllergyTypes(reqAllergyType allType);
        AllergyTypeResponse InsertAllergyTypes(TblAllergyType req);
        List<lstAllergyMaster> GetAllergyMasters(reqAllergyMaster allyName);
        rtnAllergyMaster InsertAllergyMasters(TblAllergyMaster res);
        List<lstOPDReasonMaster> GetOPDReasonMaster(reqOPDReasonMaster resMas);
        rtnOPDReasonMaster InsertOPDReasonMaster(TblOPDReasonMaster reasonMas);
        rtnAllergyReaction InsertAllergyReaction(TblAllergyReaction res);
        List<rtnAllergyReactionres> GetAllergyReactionl(rtnAllergyReactionreq masterRequest);
    }
}
