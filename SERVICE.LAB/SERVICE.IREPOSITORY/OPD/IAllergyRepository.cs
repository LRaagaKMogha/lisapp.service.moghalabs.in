using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
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
