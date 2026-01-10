using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
   public class GetBranchSampleReceiveRequest: GetSampleOutsourceRequest
    {
        public int TransferBranchNo { get; set; }
    }
}
