using Service.Model.Master;
using System.Collections.Generic;

namespace Service.IRepository.Master
{
    public interface IScrollTextMasterRepository
    {
        List<ScrollTextMasterResponse> GetScrollTextMaster(GetScrollTextMasterRequest scrollMaster);
        SaveScrollTextMasterResponse InsertScrollTextMaster(SaveScrollTextMasterRequest request);
    }
}
