using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblContainer
    {
        public int containerNo { get; set; }
        public string containerCode { get; set; }
        public string containerName { get; set; }
        public string description { get; set; }
        public string containerVolume { get; set; }
        public bool? isContainerimage { get; set; }
        public string imageColor { get; set; }
        public string containerImagename { get; set; }
        public bool? isActive { get; set; }
        public bool? status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }

    public class ContainerMasterRequest
    {
        public int containerNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ContainerMasterResponse
    {
        public int containerNo { get; set; }
        public int LastPageIndex { get; set; }
    }

    public class FileuplaoadRequest
    {
        public string binaryData { get; set; }
        public string imageType { get; set; }
        public string imageName { get; set; }
        public int containerNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
    }
    public class FileuplaoadResponse
    {
        public int containerNo { get; set; }
    }
}
