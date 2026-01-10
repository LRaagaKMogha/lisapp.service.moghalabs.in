using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class reqorgAntiRange
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismno { get; set; }
        public int OrganismGroupNo { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class reqorgGroupAntiRange
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismGrpno { get; set; }
        public int organismtypeno { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
    }
    
    public partial class orgAntiRange
    {
        public int userno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public List<lstorgAntiRange> lstorgAntiRange { get; set; }

    }

    public partial class lstorgAntiRange
    {
        public int antibioticno { get; set; }
        public int organismAntibioticRangeNo { get; set; }
        public int organismno { get; set; }
        public string antibioticname { get; set; }
        public int sequenceNo { get; set; }
        public int? sensitiveFrom { get; set; }
        public int? sensitiveTo { get; set; }
        public int? intermediateFrom { get; set; }
        public int? intermediateTo { get; set; }
        public int? resistantFrom { get; set; }
        public int? resistantTo { get; set; }
        public string interprange { get; set; }
    }
    public partial class orggetresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismno { get; set; }
        public int organismgroupno { get; set; }
        public string? OrganismGroupName { get; set; }
        public string? organismname { get; set; }
        public string? notes { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string? organismCode { get; set; }
    }

    public partial class orgGrpresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismgrpno { get; set; }
        public int organismtypeno { get; set; }
        public string? organismgrpname { get; set; }
        public string? organismTypeName { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class orgresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismno { get; set; }
        public int organismgroupno { get; set; }
        public string? organismname { get; set; }
        public string? notes { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public string? organismshortcode { get; set; }
    }
    public partial class orggrpresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismgrpno { get; set; }
        public int organismtypeno { get; set; }
        public string? organismgrpname { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
    }
    public partial class orginsertresponse
    {
        public int organismno { get; set; }

        public int LastPageIndex { get; set; }
    }
    public partial class orginsertGrpresponse
    {
        public int organismGrpno { get; set; }
    }
    public partial class orgtypereq
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismtypeno { get; set; }
        public int currentseqNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class orgtyperesponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismtypeno { get; set; }
        public int sequenceno { get; set; }
        public string? organismTypeName { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int currentseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class orgtypeinsertresponse
    {
        public int organismtypeno { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class antireq
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int antibioticno { get; set; }
        public int newseqno { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class antiresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int antibioticno { get; set; }
        public int sequenceno { get; set; }
        public string? antibioticName { get; set; }
        public string? antibioticMccode { get; set; }
        public string? antibioticcode { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int newseqno { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class antinsertresponse
    {
        public int antibioticno { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class orgAntiresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismAntibioticMapNo { get; set; }
        public int organismTypeNo { get; set; }
        public string OrganismTypeName { get; set; }
        public int antibioticno { get; set; }
        public int sequenceno { get; set; }
        public string antibioticName { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int seqnoNew { get; set; }
        public int organismNo { get; set; }
        public string organismName { get; set; }
    }
    public partial class orgAntinsertresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismAntibioticMapNo { get; set; }
        public int organismTypeNo { get; set; }
        public int antibioticno { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int organismNo { get; set; }
    }
    public partial class orgAntirequest
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismAntibioticMapNo { get; set; }
        public int organismTypeNo { get; set; }
        public int organismNo { get; set; }
        public int antibioticno { get; set; }
        public int pageIndex { get; set; }
        public int seqnoNew { get; set; }
    }
    public partial class organtinsertresponse
    {
        public int organismAntibioticMapNo { get; set; }

        public int LastPageIndex { get; set; }

    }
}