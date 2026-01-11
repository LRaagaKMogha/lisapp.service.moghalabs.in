using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblAllergyType
    {
        public Int16 AllergyTypeNo { get; set; }
        public string AllergyDescription { get; set; }
        public Byte SequenceNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class AllergyTypeResponse
    {
        public Int16 AllergyTypeNo { get; set; }
    }
    public partial class reqAllergyType
    {
        public Int16 AllergyTypeNo { get; set; }
        public bool? Status { get; set; }
        public string AllergyDescription { get; set; }
        public Int16 VenueNo { get; set; }
        public int updSeqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstAllergyType
    {
        public Int16 AllergyTypeNo { get; set; }
        public string AllergyDescription { get; set; }
        public byte SequenceNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int updSeqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class TblAllergyMaster
    {
        public Int16 AllergyMasterNo { get; set; }
        public Int16 AllergyTypeNo { get; set; }
        public string? Description { get; set; }
        public byte AlgySequenceNo { get; set; }
        public bool? AlgyStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class rtnAllergyMaster
    {
        public Int16 AllergyMasterNo { get; set; }
    }
    public partial class reqAllergyMaster
    {
        public Int16 AllergyMasterNo { get; set; }
        public Int16 AllergyTypeNo { get; set; }
        public bool? AlgyStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int updateseqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstAllergyMaster
    {
        public Int16 AllergyMasterNo { get; set; }
        public Int16 AllergyTypeNo { get; set; }
        public string? AllergyDescription { get; set; }
        public string? Description { get; set; }
        public byte AlgySequenceNo { get; set; }
        public bool? AlgyStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int updateseqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class reqOPDReasonMaster
    {
        public Int16 OPDReasonMastNo { get; set; }
        public Int16 TypeNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public int updateseqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstOPDReasonMaster
    {
        public Int16 OPDReasonMastNo { get; set; }
        public Int16 TypeNo { get; set; }
        public string? Description { get; set; }
        public string? ShortDesc { get; set; }
        public Int16 SeqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public string CommonValue { get; set; }
        public int updateseqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class TblOPDReasonMaster
    {
        public Int16 OPDReasonMastNo { get; set; }
        public Int16 TypeNo { get; set; }
        public string? Description { get; set; }
        public string? ShortDesc { get; set; }
        public Int16 SeqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class rtnOPDReasonMaster
    {
        public Int16 OPDReasonMastNo { get; set; }
    }
    public class TblAllergyReaction
    {
        public int AllergyReactionNo { get; set; }
        public int VenueNo { get; set; }
        public string Description { get; set; }
        public int? SequenceNo { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class rtnAllergyReaction
    {
        public int AllergyReactionNo { get; set; }
    }
    public class rtnAllergyReactionreq
    {
        public int AllergyReactionNo { get; set; }
        public int VenueNo { get; set; }
        public int PageIndex { get; set; }
    }
    public class rtnAllergyReactionres
    {
        public int RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int AllergyReactionNo { get; set; }
        public int VenueNo { get; set; }
        public string Description { get; set; }
        public int? SequenceNo { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }

}