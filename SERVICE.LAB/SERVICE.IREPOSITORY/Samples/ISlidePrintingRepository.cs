using Service.Model;
using Service.Model.Sample;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface ISlidePrintingRepository
    {
        List<GetSlidePrintingResponse> GetSlidePrintingDetails(SlidePrintingRequest RequestItem);
        SlidePrintPatientdetailsResponse GetSlidePrintingPatientdetails(CommonFilterRequestDTO RequestItem);
        CommonTokenResponse SaveSlidePrintingDetails(SlidePrintPatientdetailsResponse RequestItem);
        CommonTokenResponse GenerateSlideNumber(CommonFilterRequestDTO RequestItem);
        List<ExistingRCHNoResponse> GetExistingRCHNoDetails(CommonFilterRequestDTO RequestItem);
        List<GetBulkSlidePrintingDetails> GetBulkSlidePrintDetails(GetBulkSlidePrintingRequest RequestItem);
    }
}
