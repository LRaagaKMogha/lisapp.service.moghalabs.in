using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class TblProductType
    {
        public int productTypeno { get; set; }
        public string? productTypename { get; set; }       
        public Int16 sequenceNo { get; set; }
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public Int16 userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int currentseqNo { get; set; }

    }

    public class ProductTypeMasterRequest
    {
        public int productTypeno { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int pageIndex { get; set; }
    }
    public class ProductTypeMasterResponse
    {
        public int productTypeno { get; set; }      
    }
    public class ProductcategoryRequest
    {
        public int categoryNo { get; set; }
        public int venueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public class TblProductCategory
    {
        public int categoryNo { get; set; }
        public string? categoryCode { get; set; }
        public string? categoryName { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public bool? categorystatus { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ProductcategoryResponse
    {
        public int categoryNo { get; set; }
       
    }

}
