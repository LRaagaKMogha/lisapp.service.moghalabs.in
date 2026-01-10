using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{

    [DtoMapping(typeof(InsertCommentSubCategoryReqest))]
    public class TblCanTextSubCatyMapping : DtoToTableMapping<InsertCommentSubCategoryReqest>
    {
        public override void SetUp()
        {
            TableName = "tbl_CommentsSubCategory";
            EntityIdProperty = nameof(InsertCommentSubCategoryReqest.SubCatyNo);
            SubMenuCode = "Cantext - Sub-Category";
            AddProperty(x => x.CategoryNo, "CatyNo");
            AddProperty(x => x.SubCatyDesc, "Description");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<InsertCommentSubCategoryReqest, object>>>
            {
                x => x.userNo
            };
        }
    }

    [DtoMapping(typeof(CommentInsReq))]
    public class TblCanTextMasterMapping : DtoToTableMapping<CommentInsReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_Common_CommentMaster";
            EntityIdProperty = nameof(CommentInsReq.CommentsMastNo);
            SubMenuCode = "Cantext Master";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<CommentInsReq, object>>>
            {
                x => x.userNo
            };
        }
    }
}
