using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface ICollectionDetailsRepository
    {
        List<lstCollectDTS> GetCollectionDetails(reqCollectDTS collectreq);
        resCollectDTS UpdateCollectionDetails(updateCollectDTS collectupd);
    }
}
