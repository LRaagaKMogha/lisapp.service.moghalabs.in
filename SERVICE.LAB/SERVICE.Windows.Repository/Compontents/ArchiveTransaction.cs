using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using Dev.Win.Common;
using DevExpress.Data.Browsing;

namespace DEV.Windows.Repository
{
    public class ArchiveTransaction
    {
        public void PushArchiveData()
        {
            try
            {
                string LiveConnection = ConfigurationManager.ConnectionStrings["LiveConnection"].ConnectionString;
                string ArchiveConnection = ConfigurationManager.ConnectionStrings["ArchiveConnection"].ConnectionString;
                DataContext objConnection = new DataContext(ArchiveConnection);
                if (objConnection.check_connection())
                {
                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    DataContext objDataContext = new DataContext(LiveConnection);
                    DataTable datable = objDataContext.getdatatable(objdictionary, "pro_ArchiveTransaction");
                    ArchiveTransactionDTO objDTO = new ArchiveTransactionDTO();
                    if (datable.Rows.Count > 0)
                    {
                        objDTO.PatientNo = (int)datable.Rows[0]["PatientNo"];
                        objDTO.PatientVisitNo = (int)datable.Rows[0]["PatientVisitNo"];
                        objDTO.PatientTransaction = (string)datable.Rows[0]["PatientTransaction"];
                        objDTO.PatientTransactionStatus = (string)datable.Rows[0]["PatientTransactionStatus"];
                        objDTO.PatientBill = (string)datable.Rows[0]["PatientBill"];
                        if (datable.Rows[0]["PatientBillCancel"] != System.DBNull.Value)
                        {
                            objDTO.PatientBillCancel = (string)datable.Rows[0]["PatientBillCancel"];
                        }
                        objDTO.PatientBillDetails = (string)datable.Rows[0]["PatientBillDetails"];
                        if (datable.Rows[0]["PatientBillDues"] != System.DBNull.Value)
                        {
                            objDTO.PatientBillDues = (string)datable.Rows[0]["PatientBillDues"];
                        }
                        if (datable.Rows[0]["PatientBillPayments"] != System.DBNull.Value)
                        {
                            objDTO.PatientBillPayments = (string)datable.Rows[0]["PatientBillPayments"];
                        }
                        objDTO.PatientBillTransaction = (string)datable.Rows[0]["PatientBillTransaction"];
                        if (datable.Rows[0]["PatientDiscount"] != System.DBNull.Value)
                        {
                            objDTO.PatientDiscount = (string)datable.Rows[0]["PatientDiscount"];
                        }
                        objDTO.Orders = (string)datable.Rows[0]["Orders"];
                        objDTO.OrderDetails = (string)datable.Rows[0]["OrderDetails"];
                        objDTO.OrderTransaction = (string)datable.Rows[0]["OrderTransaction"];
                        objDTO.OrderTransactionStatus = (string)datable.Rows[0]["OrderTransactionStatus"];
                        if (datable.Rows[0]["PatientResult"] != System.DBNull.Value)
                        {
                            objDTO.PatientResult = (string)datable.Rows[0]["PatientResult"];
                        }
                        if (datable.Rows[0]["PatientFinalResult"] != System.DBNull.Value)
                        {
                            objDTO.PatientFinalResult = (string)datable.Rows[0]["PatientFinalResult"];
                        }
                        if (datable.Rows[0]["PatientResultMB"] != System.DBNull.Value)
                        {
                            objDTO.PatientResultMB = (string)datable.Rows[0]["PatientResultMB"];
                        }
                        if (datable.Rows[0]["PatientResultMBDrug"] != System.DBNull.Value)
                        {
                            objDTO.PatientResultMBDrug = (string)datable.Rows[0]["PatientResultMBDrug"];
                        }
                        if (datable.Rows[0]["PatientResultTemplate"] != System.DBNull.Value)
                        {
                            objDTO.PatientResultTemplate = (string)datable.Rows[0]["PatientResultTemplate"];
                        }
                        objDTO.VenueNo = (int)datable.Rows[0]["VenueNo"];
                        objDTO.VenueBranchNo = (int)datable.Rows[0]["VenueBranchNo"];
                        objDTO.IsTemplate = (bool)datable.Rows[0]["IsTemplate"];
                        if (datable.Rows[0]["TemplateTextResult"] != System.DBNull.Value)
                        {
                            objDTO.TemplateTextResult = (string)datable.Rows[0]["TemplateTextResult"];
                        }

                        Dictionary<string, string> objArchivedictionary = new Dictionary<string, string>();
                        objArchivedictionary.Add("PatientNo", objDTO.PatientNo.ToString());
                        objArchivedictionary.Add("PatientVisitNo", objDTO.PatientVisitNo.ToString());
                        objArchivedictionary.Add("PatientTransaction", objDTO.PatientTransaction.ToString());
                        objArchivedictionary.Add("PatientTransactionStatus", objDTO.PatientTransactionStatus.ToString());
                        objArchivedictionary.Add("PatientBill", objDTO.PatientBill.ToString());
                        objArchivedictionary.Add("PatientBillCancel", objDTO.PatientBillCancel.ValidateEmpty());
                        objArchivedictionary.Add("PatientBillDetails", objDTO.PatientBillDetails.ToString());
                        objArchivedictionary.Add("PatientBillDues", objDTO.PatientBillDues.ValidateEmpty());
                        objArchivedictionary.Add("PatientBillPayments", objDTO.PatientBillPayments.ValidateEmpty());
                        objArchivedictionary.Add("PatientBillTransaction", objDTO.PatientBillTransaction.ToString());
                        objArchivedictionary.Add("PatientDiscount", objDTO.PatientDiscount.ValidateEmpty());
                        objArchivedictionary.Add("Orders", objDTO.Orders.ToString());
                        objArchivedictionary.Add("OrderDetails", objDTO.OrderDetails.ToString());
                        objArchivedictionary.Add("OrderTransaction", objDTO.OrderTransaction.ToString());
                        objArchivedictionary.Add("OrderTransactionStatus", objDTO.OrderTransactionStatus.ToString());
                        objArchivedictionary.Add("PatientResult", objDTO.PatientResult.ValidateEmpty());
                        objArchivedictionary.Add("PatientFinalResult", objDTO.PatientFinalResult.ValidateEmpty());
                        objArchivedictionary.Add("PatientResultMB", objDTO.PatientResultMB.ValidateEmpty());
                        objArchivedictionary.Add("PatientResultMBDrug", objDTO.PatientResultMBDrug.ValidateEmpty());
                        objArchivedictionary.Add("PatientResultTemplate", objDTO.PatientResultTemplate.ValidateEmpty());
                        if (objDTO.IsTemplate)
                        {
                            var templatelist = JsonConvert.DeserializeObject<List<ArchiveTemplateDTO>>(objDTO.TemplateTextResult);
                            foreach (var item in templatelist)
                            {
                                string path = ConfigurationManager.AppSettings["TransTemplateFilePath"];
                                path = path + objDTO.VenueNo.ToString() + "/" + item.OrderListNo + "/" + item.TestNo + ".ym";

                                if (File.Exists(path))
                                {
                                    string content = File.ReadAllText(path);
                                    item.Results = content;
                                }
                            }
                            objArchivedictionary.Add("TemplateTextResult", JsonConvert.SerializeObject(templatelist));
                        }
                        else
                        {
                            objArchivedictionary.Add("TemplateTextResult", "");
                        }
                        objArchivedictionary.Add("VenueNo", objDTO.VenueNo.ToString());
                        objArchivedictionary.Add("VenueBranchNo", objDTO.VenueBranchNo.ToString());
                        DataContext objArchiveContext = new DataContext(ArchiveConnection);
                        DataTable result = objArchiveContext.getdatatable(objArchivedictionary, "pro_PushArchiveTransaction");
                        if (result.Rows.Count > 0)
                        {
                            int output = (int)result.Rows[0]["Result"];
                            if (output == 1)
                            {
                                Dictionary<string, string> objupdictionary = new Dictionary<string, string>();
                                objupdictionary.Add("PatientVisitNo", objDTO.PatientVisitNo.ToString());
                                objupdictionary.Add("VenueNo", objDTO.VenueNo.ToString());
                                objupdictionary.Add("VenueBranchNo", objDTO.VenueBranchNo.ToString());
                                DataTable updatable = objDataContext.getdatatable(objupdictionary, "pro_UpdateArchiveTransaction");
                                int upoutput = (int)updatable.Rows[0]["Result"];
                                if (upoutput == 1)
                                {
                                    Logger.LogWrite("Record Sucessfully pushed to Archive DB -" + objDTO.PatientVisitNo.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.LogWrite("No Records found");
                    }
                }
                else
                {
                    Logger.LogWrite("Unable to connect Archive Database");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }
    }

}