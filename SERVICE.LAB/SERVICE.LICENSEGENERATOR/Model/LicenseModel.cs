using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.LICENSEGENERATOR.Model
{
    public class LicenseModel
    {
        public string HardwareId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class LicenseWrapper
    {
        public string Data { get; set; }
        public string Signature { get; set; }
    }
}
