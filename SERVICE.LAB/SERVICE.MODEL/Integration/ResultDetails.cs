using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Integration
{
    public class NotifyResponse
    {
        public string visitNumber { get; set; }
        public string customerId { get; set; }
        public string orderId { get; set; }
        public int totalInhouseOrdered { get; set; }
        public int totalInhouseCompleted { get; set; }
        public int totalSendOutOrdered { get;set; }
        public int totalSendOutCompleted { get; set; }
        public string sourceSystem { get; set; }
        public string sourceRequestID { get; set; }
        public List<labResultItems> labResultItems { get; set; }
    }
    public class labResultItems
    {
        public string testCode { get; set;}
        public string testName { get; set; }
        public string testStatus { get; set;}
    }
}
