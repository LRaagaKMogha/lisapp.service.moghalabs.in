using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Win.Common.Model
{
    public class BloodBankBarCode
    {
        public string PRNFile { get; set;  }
        public string ExportPRNFile { get; set; }
        public string PrinterName { get; set;  }
        public List<Dictionary<string, string>> barcodeItems { get; set; }
    }
}
