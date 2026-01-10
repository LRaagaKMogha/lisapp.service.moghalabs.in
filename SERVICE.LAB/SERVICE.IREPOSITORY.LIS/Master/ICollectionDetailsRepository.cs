using DEV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository
{
    public interface ICollectionDetailsRepository
    {
        List<lstCollectDTS> GetCollectionDetails(reqCollectDTS collectreq);
        resCollectDTS UpdateCollectionDetails(updateCollectDTS collectupd);
    }
}
