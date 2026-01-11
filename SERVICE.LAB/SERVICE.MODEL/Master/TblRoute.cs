using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblRoute
    {
        public int RouteNo { get; set; }
        public string? RouteCode { get; set; }
        public string? RouteName { get; set; }
        public string? Description { get; set; }
        public int? SequenceNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int UserNo { get; set; }

    }
    public partial class Routelst
    {
        public int RouteNo { get; set; }
        public string? RouteCode { get; set; }
        public string? RouteName { get; set; }
        public string? Description { get; set; }
        public int? SequenceNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int Routecount { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int UserNo { get; set; }

    }
    public class RouteMasterRequest
    {
        public int RouteNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public int Routecount { get; set; }


    }
    public class RouteMasterResponse
    {
        public int RouteNo { get; set; }
    }
}