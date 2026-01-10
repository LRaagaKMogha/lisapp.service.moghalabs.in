using DEV.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Master
{
    public interface IScrollTextMasterRepository
    {
        List<ScrollTextMasterResponse> GetScrollTextMaster(GetScrollTextMasterRequest scrollMaster);
        SaveScrollTextMasterResponse InsertScrollTextMaster(SaveScrollTextMasterRequest request);
    }
}
