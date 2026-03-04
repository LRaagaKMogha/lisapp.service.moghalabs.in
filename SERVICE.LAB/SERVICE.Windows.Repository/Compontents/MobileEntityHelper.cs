using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Win.Repository
{
    public class MobileEntityHelper
    {
        public List<Pro_CsaAppTransaction_Result> GetCsaTransactionDetails(int venueNo, int VenueBranchNo, int Param, int userno)
        {
            List<Pro_CsaAppTransaction_Result> objresult = new List<Pro_CsaAppTransaction_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.Pro_CsaAppTransaction(venueNo, VenueBranchNo, Param, userno).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }
        public List<Pro_DrpCustomer_Result> GetcustomerDetails(int VenueNo,int VenueBranchNo)
        {
            List<Pro_DrpCustomer_Result> objresult = new List<Pro_DrpCustomer_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.Pro_DrpCustomer(VenueNo, VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }
        public void UpdateCSATransaction(int CsaTrasnactionid)
        {
            using (YMEntities context = new YMEntities())
            {
                var CSATransaction = context.tbl_CSATransaction.FirstOrDefault(a => a.CSATransactionNo == CsaTrasnactionid);
                CSATransaction.CollectedStatus = 2;
                context.Entry(CSATransaction).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public int InsertCSATransactionDetails(int Customerid, int Userid, int Samplecount, decimal Amount, int VenueNo, int VenueBranchNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.pro_InsertCsaTransaction(Customerid, Samplecount, Amount, Userid, VenueNo, VenueBranchNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public tbl_User validateLogin(string username, string password)
        {
            tbl_User objresult = new tbl_User();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.tbl_User.Where(a => a.LoginName == username && a.Status == true).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }
        public int PushMessage(string templateKey, string messageType, string address, string cCAddress, string bCCAddress, string messageXML, Nullable<bool> isAttachment, string messageAttachmentXML, Nullable<System.DateTime> scheduleTime, Nullable<int> venueNo, Nullable<int> venueBranchNo, Nullable<int> userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                   result = context.Pro_PushMessage(templateKey, messageType, address, cCAddress, bCCAddress, messageXML, isAttachment, messageAttachmentXML, scheduleTime, venueNo, venueBranchNo, userNo,0,0);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}