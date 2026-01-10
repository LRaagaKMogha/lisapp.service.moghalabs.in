using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(objgrppkg), "GRPMAS")]
    public class TblGroupMasterMapping : DtoToTableMapping<objgrppkg>
    {
        public override void SetUp()
        {
            IsAutoMap = false;
            TableName = "tbl_Group";
            EntityIdProperty = nameof(objgrppkg.serviceNo);
            AddProperty(x => x.serviceNo, "GroupNo");
            AddProperty(x => x.tcNo, "tcNo");
            AddProperty(x => x.shortName, "GroupShortName", value => new { MasterKey = "Group Short Name" });
            AddProperty(x => x.serviceName, "GroupName", value => new { MasterKey = "Group Name" });
            AddProperty(x => x.displayName, "GroupDisplayName", value => new { MasterKey = "Group Display Name" });
            AddProperty(x => x.testCode, "GroupCode", value => new { MasterKey = "Group Code" });
            AddProperty(x => x.deptNo, "DeptNo", value => new { MasterKey = "Department" });
            AddProperty(x => x.sampleNo, "sampleNo", value => new { MasterKey = "Specimen" });
            AddProperty(x => x.containerNo, "containerNo", value => new { MasterKey = "Container" });
            AddProperty(x => x.sequenceNo, "GSequenceNo", value => new { MasterKey = "Group Order Number" });
            AddProperty(x => x.rate, "rate");
            AddProperty(x => x.gender, "Gender", value => new { MasterKey = "GenderId" });
            AddProperty(x => x.cutoffTime, "CutoffTime", value => new { MasterKey = "Cut-Off Time" });
            AddProperty(x => x.processingMinutes, "processingMinutes", value => new { MasterKey = "Processing Time" });
            AddProperty(x => x.processingDays, "ProcessingDays", value => new { Category = "Weekdays" });
            AddProperty(x => x.ReptDeptHeaderNo, "ReptDeptHeaderNo", value => new { MasterKey = "Printing Report Department" });
            AddProperty(x => x.languagecode, "LanguageCode", value => new { MasterKey = "Language" });
            AddProperty(x => x.languageText, "LanguageText");
            AddProperty(x => x.BarShortName, "BarShortName", value => new { MasterKey = "Bar-code Short Name" });
            AddProperty(x => x.nehrInterpreditationnotes, "nehrinterpnotes", value => new { MasterKey = "NEHR Interpretation Notes" });
            AddProperty(x => x.isActive, "IsActive", value => new { Toggle = "Yes/No", Label = "Is Active" });
            AddProperty(x => x.isRateEditable, "IsRateEditable", value => new { Toggle = "Yes/No", Label = "Rate Editable" });
            AddProperty(x => x.isAllowDiscount, "IsAllowDiscount", value => new { Toggle = "Yes/No", Label = "Allow Discount" });
            AddProperty(x => x.isOutsource, "IsOutsource", value => new { Toggle = "Yes/No", Label = "Is outsource" });
            AddProperty(x => x.isInterNotes, "IsInterNotes", value => new { Toggle = "Yes/No", Label = "Inter Notes" });
            AddProperty(x => x.isBillDisclaimer, "IsBillDisclaimer", value => new { Toggle = "Yes/No", Label = "Bill Disclaimer" });
            AddProperty(x => x.isReportDisclaimer, "IsReportDisclaimer", value => new { Toggle = "Yes/No", Label = "Report Disclaimer" });
            AddProperty(x => x.isConsent, "IsConsent", value => new { Toggle = "Yes/No", Label = "Consent" });
            AddProperty(x => x.isComments, "IsComments", value => new { Toggle = "Yes/No", Label = "Comments" });
            AddProperty(x => x.isSecondReview, "IsSecondReview", value => new { Toggle = "Yes/No", Label = "Second Review" });
            AddProperty(x => x.IsInfectionCtrlRept, "IsInfectionControl", value => new { Toggle = "Yes/No", Label = "Infection Control Report" });
            AddProperty(x => x.isdisplayinreport, "isdisplayinreport", value => new { Toggle = "Yes/No", Label = "Display Name In Report" });
            AddProperty(x => x.FromDate, "FromDate", value => new { MasterKey = "From Date" });
            AddProperty(x => x.ToDate, "ToDate", value => new { MasterKey = "To Date" });
            AddProperty(x => x.loincNo, "loincNo", value => new { MasterKey = "LOINC" });
            AddProperty(x => x.lstgrppkgservice, "", null, dto =>
            {
                return dto.lstgrppkgservice.Select(test =>
                {
                    return new { serviceName = test.serviceName, childSequenceNo = test.childsequenceNo };
                }).ToList();
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<objgrppkg, object>>>
            {
                x => x.bufferDate, x => x.bufferDays, x => x.ChoiceCount, x => x.IsChoice, x => x.billDisclaimer, x => x.reportDisclaimer,
                x => x.consentNotes, x => x.samplequantity, x => x.isunacceptable, x => x.unacceptcondition, x =>x.isincludeinstruction, x => x.includeinstruction,
                x => x.IsApproval, x => x.IsReject, x => x.RejectReason, x => x.OldServiceNo, x => x.loincCode, x => x.comments, x => x.userNo
            };
        }
    }

    [DtoMapping(typeof(objgrppkg), "PKGMAS")]
    public class TblPackageMasterMapping : DtoToTableMapping<objgrppkg>
    {
        public override void SetUp()
        {
            IsAutoMap = false;
            TableName = "tbl_Package";
            EntityIdProperty = nameof(objgrppkg.serviceNo);
            AddProperty(x => x.serviceNo, "PackageNo");
            AddProperty(x => x.serviceName, "PackageName", value => new { MasterKey = "Package Name" });
            AddProperty(x => x.displayName, "PackageDisplayName", value => new { MasterKey = "Package Display Name" });
            AddProperty(x => x.shortName, "PackageShortName", value => new { MasterKey = "Package Short Name" });
            AddProperty(x => x.testCode, "PackageCode", value => new { MasterKey = "Package Code" });
            AddProperty(x => x.sequenceNo, "PSequenceNo", value => new { MasterKey = "Package Order Number" });
            AddProperty(x => x.rate, "rate");
            AddProperty(x => x.gender, "Gender", value => new { MasterKey = "GenderId" });
            AddProperty(x => x.FromDate, "FromDate", value => new { MasterKey = "From Date" });
            AddProperty(x => x.ToDate, "ToDate", value => new { MasterKey = "To Date" });
            AddProperty(x => x.languagecode, "LanguageCode", value => new { MasterKey = "Language" });
            AddProperty(x => x.languageText, "LanguageText");
            AddProperty(x => x.isActive, "IsActive", value => new { Toggle = "Yes/No", Label = "Is Active" });
            AddProperty(x => x.isRateEditable, "IsRateEditable", value => new { Toggle = "Yes/No", Label = "Rate Editable" });
            AddProperty(x => x.isAllowDiscount, "IsAllowDiscount", value => new { Toggle = "Yes/No", Label = "Allow Discount" });
            AddProperty(x => x.isOutsource, "IsOutsource", value => new { Toggle = "Yes/No", Label = "Is outsource" });
            AddProperty(x => x.isInterNotes, "IsInterNotes", value => new { Toggle = "Yes/No", Label = "Inter Notes" });
            AddProperty(x => x.isBillDisclaimer, "IsBillDisclaimer", value => new { Toggle = "Yes/No", Label = "Bill Disclaimer" });
            AddProperty(x => x.isReportDisclaimer, "IsReportDisclaimer", value => new { Toggle = "Yes/No", Label = "Report Disclaimer" });
            AddProperty(x => x.isConsent, "IsConsent", value => new { Toggle = "Yes/No", Label = "Consent" });
            AddProperty(x => x.isComments, "IsComments", value => new { Toggle = "Yes/No", Label = "Comments" });
            AddProperty(X => X.IsChoice, "IsChoice", value => new { Toggle = "Yes/No", Lable = "Choice" });
            AddProperty(x => x.ChoiceCount, "ChoiceCount", value => new { MasterKey = "Choice Count" });
            AddProperty(x => x.bufferDays, "bufferDays", value => new { MasterKey = "Buffer Days" });
            AddProperty(x => x.bufferDate, "bufferDate", value => new { MasterKey = "Buffer Date" });
            AddProperty(x => x.isdisplayinreport, "isdisplayinreport", value => new { Toggle = "Yes/No", Label = "Display Name In Report" });
            AddProperty(x => x.loincNo, "loincNo", value => new { MasterKey = "LOINC" });

            AddProperty(x => x.lstgrppkgservice, "", null, dto =>
            {
                return dto.lstgrppkgservice.Select(test =>
                {
                    return new { serviceName = test.serviceName, childSequenceNo = test.childsequenceNo };
                }).ToList();
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<objgrppkg, object>>>
            {
                x => x.deptNo, x => x.cutoffTime, x => x.processingMinutes, x => x.processingDays, x => x.ReptDeptHeaderNo, x => x.BarShortName,
                x => x.nehrInterpreditationnotes, x => x.isSecondReview, x => x.IsInfectionCtrlRept, x => x.consentNotes, x => x.comments,
                x => x.samplequantity, x => x.unacceptcondition, x => x.unacceptcondition, x =>x.isincludeinstruction, x => x.includeinstruction,
                x => x.IsApproval, x => x.IsReject, x => x.RejectReason, x => x.OldServiceNo, x => x.userNo
            };
        }
    }
}
