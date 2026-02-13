using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface ISampleRepository
    {
        int InsertSampleDetails1 (TblSample Sampleitem);
        List<TblSample> GetSampleDetails(GetCommonMasterRequest sampleMasterRequest );
        List<TblSample> SearchSampleDetails(string SampleName);
        List<TblSample> GetSampleDetails1 (GetCommonMasterRequest sampleMasterRequest);
        List<sampleMasterResponse> InsertSampleDetails(TblSample Sampleitem);
    }
}
