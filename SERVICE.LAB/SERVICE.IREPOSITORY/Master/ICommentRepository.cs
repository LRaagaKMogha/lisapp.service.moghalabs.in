using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface ICommentRepository
    {
        List<CommentGetRes> Getcommentmaster(CommentGetReq getReq);
        CommentInsRes Insertcommentmaster(CommentInsReq insReq);
        List<GetNationRaceRes>  GetNationRace(GetNationRaceReq getReq);
        InsNationRaceRes InsertNationRace(InsNationRaceReq insReq);
        List<TemplateCommentRes> TemplateInsertcomment(TemplateComment Req);
        CommentSubCatyInsResponse InsertCommentSubCategory(InsertCommentSubCategoryReqest Req);
        List<FetchCommentSubCategoryResponse> GetCommentSubCategory(FetchCommentSubCategoryReqest getReq);

        BankMasterResponse InsertBankMaster(InsertBankMastereq getReq);
        List<BankMasterResponse> GetBankMaster(InsertBankMasterr getReq);
        BankBranchResponse InsertBankBranch(InsertBankbranchreq getReq);
        List<BankBranchResponse> GetBankBranch(GetBankbranchreq getReq);


    }

}
