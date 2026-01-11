using Service.Model;
using Service.Model.Sample;
using System.Collections.Generic;

namespace Dev.IRepository
{
    public interface ISlidePrintingRepository
    {
        List<GetSlidePrintingResponse> GetSlidePrintingDetails(SlidePrintingRequest RequestItem);
        SlidePrintPatientDetailsResponse GetSlidePrintingPatientDetails(CommonFilterRequestDTO RequestItem);
        CommonTokenResponse SaveSlidePrintingDetails(SlidePrintPatientDetailsResponse RequestItem);
        CommonTokenResponse GenerateSlideNumber(CommonFilterRequestDTO RequestItem);
        List<ExistingRCHNoResponse> GetExistingRCHNoDetails(CommonFilterRequestDTO RequestItem);
        List<GetBulkSlidePrintingDetails> GetBulkSlidePrintDetails(GetBulkSlidePrintingRequest RequestItem);
    }
}
