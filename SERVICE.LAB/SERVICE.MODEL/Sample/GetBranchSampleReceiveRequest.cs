using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{
   public class GetBranchSampleReceiveRequest: GetSampleOutsourceRequest
    {
        public int TransferBranchNo { get; set; }
    }
}
