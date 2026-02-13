using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IQcmasterRepository
    {
        List<GetTblqcmaster> GetqcmasterDetails(qcmasterRequest qcmaster);
        QcMasterResponse InsertqcmasterDetails(saveqcDTO req);
        saveqcDTO editqcmasterDetails(EditqcDTO req);
        List<Qclotresponse> Getqclot(Qclotreq req);
        List<Qclevelresponse> Getqclevel(Qclevelreq req);
        List<Qclowhighresponse> Getqclowhighvalue(Qclowhighreq req);
    }
}