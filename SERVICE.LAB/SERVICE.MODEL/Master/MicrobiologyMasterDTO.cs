using System.Collections.Generic;

namespace Service.Model
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
        public List<LstorgAntiRange> LstorgAntiRange { get; set; }
    }
    public partial class LstorgAntiRange
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
    public partial class Orggetresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismno { get; set; }
        public int organismgroupno { get; set; }
        public string OrganismGroupName { get; set; }
        public string organismname { get; set; }
        public string notes { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string organismCode { get; set; }
    }
    public partial class Orggrpresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismgrpno { get; set; }
        public int organismtypeno { get; set; }
        public string organismgrpname { get; set; }
        public string organismTypeName { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class Orgresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismno { get; set; }
        public int organismgroupno { get; set; }
        public string organismname { get; set; }
        public string notes { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public string organismshortcode { get; set; }
    }
    public partial class Orggrpresponse
    {
        public int Venueno { get; set; }
        public int Venuebranchno { get; set; }
        public int Organismgrpno { get; set; }
        public int Organismtypeno { get; set; }
        public string Organismgrpname { get; set; }
        public int Sequenceno { get; set; }
        public bool Status { get; set; }
        public int userno { get; set; }
    }
    public partial class Orginsertresponse
    {
        public int organismno { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class OrginsertGrpresponse
    {
        public int organismGrpno { get; set; }
    }
    public partial class Orgtypereq
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismtypeno { get; set; }
        public int currentseqNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class Orgtyperesponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismtypeno { get; set; }
        public int sequenceno { get; set; }
        public string organismTypeName { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int currentseqNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class Orgtypeinsertresponse
    {
        public int organismtypeno { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class Antireq
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int antibioticno { get; set; }
        public int newseqno { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class Antiresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int antibioticno { get; set; }
        public int sequenceno { get; set; }
        public string antibioticName { get; set; }
        public string antibioticMccode { get; set; }
        public string antibioticcode { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int newseqno { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class Antinsertresponse
    {
        public int antibioticno { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class OrgAntiresponse
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
    public partial class OrgAntinsertresponse
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int organismTypeNo { get; set; }
        public int antibioticno { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int organismNo { get; set; }
        public int OrganismAntibioticMapNo { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class OrgAntiInsertResponse
    {
        public int OrganismAntibioticMapNo { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class OrgAntirequest
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
}