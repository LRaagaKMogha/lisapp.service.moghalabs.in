using System.Collections.Generic;

namespace Service.Common.Core.Model
{
    public class BloodBankBarCode
    {
        public string PRNFile { get; set;  }
        public string ExportPRNFile { get; set; }
        public string PrinterName { get; set;  }
        public List<Dictionary<string, string>> barcodeItems { get; set; }
    }
}
