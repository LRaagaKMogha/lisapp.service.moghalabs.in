using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory.Master
{
    public class InventoryAllMastersRequest
    {
    }

    public class SupplierMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int supplierNo { get; set; }
        public bool status { get; set; }
    }
    public class ManufacturerMasterRequest
    {
        
       // public int venueBranchNo { get; set; }
      //  public int userNo { get; set; }
        public int manufacturerNo { get; set; }
        public int venueNo { get; set; }
        public int pageIndex { get; set; }
        // public bool status { get; set; }
    }

    public class AssetManagementRequest
    {
       public int InstrumentsNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int branchNo { get; set; }
        public int DepartmentNo { get; set; }   
        public bool status { get; set; }
        public int pageIndex { get; set; }            

    }
    public class Uploadpdffreq
    {
        public string instrumentname { get; set; }
        public int instrumentno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public string Actualbinarydata { get; set; }
        public string Actualfilename { get; set; }

        public string filetype { get; set; }

    }
    public class Uploadpdffres
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
