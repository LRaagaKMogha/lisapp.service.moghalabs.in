using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(TblAnalyzerresponse))]
    public class TblAnalyzerMasterMapping : DtoToTableMapping<TblAnalyzerresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_AnalyzerMaster";
            EntityIdProperty = nameof(TblAnalyzerresponse.analyzerMasterNo);
            SubMenuCode = "Analyzer";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblAnalyzerresponse, object>>>
            {
                x => x.userNo
            };
        }
    }

    [DtoMapping(typeof(AnaParamDto))]
    public class TblAnalyzerParamMasterMapping : DtoToTableMapping<AnaParamDto>
    {
        public override void SetUp()
        {
            TableName = "tbl_AnalyzerVsParameters";
            EntityIdProperty = nameof(AnaParamDto.AnalyzerParamNo);
            SubMenuCode = "Analyzer - Parameter";
        }
    }

    [DtoMapping(typeof(responseTest))]
    public class TblAnalyzerParamTestMasterMapping : DtoToTableMapping<responseTest>
    {
        public override void SetUp()
        {
            TableName = "tbl_AnalyzerVsParametersVsTests";
            EntityIdProperty = nameof(responseTest.analyzerparamTestNo);
            SubMenuCode = "Analyzer - Parameter - Test";
            AddProperty(x => x.tstatus, "Status");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<responseTest, object>>>
            {
                x => x.userNo
            };
        }
    }
}
