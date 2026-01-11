using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Service.Model
{    
    public class GetProductbatchdetailsbyPO
    {
        public decimal mrp { get; set; }
        public decimal sellingPrice { get; set; }
        public string batchNo { get; set; }
        public string expireDate { get; set; }
        public string MFGDate { get; set; }
        public int qty { get; set; }
        public int freeqty { get; set; }
        public int productNo { get; set; }
        public Int64 GRNProductNo { get; set; }
    }
}
