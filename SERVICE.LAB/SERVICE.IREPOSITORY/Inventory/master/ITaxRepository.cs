using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface ITaxRepository
    {

        List<TblTax> Gettaxmaster(TaxMasterRequest taxRequest);
        TaxMasterResponse Inserttaxmaster(TblTax tbltax);
        List<TblHSN> GetHSNMaster(HSNMasterRequest HSNRequest);
        HSNMasterResponse InsertHSNmaster(TblHSN tblhsn);
        List<TblHSNRange> GetHSNRangeMaster(HSNRangeRequest HSNrangeRequest);
        HSNInsertResponse InsertHSNRangeMaster(TblInsertHSNRange tblhsnrange);
    }

}
