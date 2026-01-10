using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using DEV.Common;
using System.Web.Http;
using System.Web.Http.Cors;
using Dev.Win.Common.Model;

namespace DEV.WinSelfHosting
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    //[EnableCors("MyPolicy")]
    public class DeviceController : ApiController
    {
        [HttpGet]
        public string test()
        {
            Logger.LogFileWrite("Connected");
            return "Connected";
        }

        [HttpPost]
        public string PrintBloodBankBarCode(BloodBankBarCode input)
        {
            string result = string.Empty;
            var barCodeItems = input.barcodeItems;
            try
            {
                Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = string.IsNullOrEmpty(input.PRNFile) ? ConfigurationManager.AppSettings["PRNFile"].ToString() : input.PRNFile;
                string ExportPRNFile = string.IsNullOrEmpty(input.ExportPRNFile) ? ConfigurationManager.AppSettings["ExportPRNFile"].ToString() : input.ExportPRNFile;
                string PrinterName = string.IsNullOrEmpty(input.PrinterName) ? ConfigurationManager.AppSettings["PrinterName"].ToString() : input.PrinterName;
                string BarcodeNo = string.Empty;
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                foreach (var lst in barCodeItems)
                {
                    List<string> textarray = new List<string>();
                    if (lst.TryGetValue("#PRNFile#", out string value))
                    {
                        var relativePath = @"Prnfiles\" + value;
                        PRNFile = Path.Combine(currentDirectory, relativePath);
                    }
                    Logger.LogFileWrite("File Name - " + PRNFile);

                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#BarcodeNo#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    Logger.LogFileWrite("Log - " + result);
                    Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }

        [HttpPost]
        public string PrintBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["PrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFile"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFile_" + item.Value].ToString(); 
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#BarcodeNo#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    Logger.LogFileWrite("Log - " + result);
                    Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }
        [HttpPost]
        public string PrintSampleAccessionBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                Logger.LogFileWrite("PrintSampleAccessionBarcode Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["SampleAccessionPrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportSampleAccessionPRNFile"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["SampleAccessionPrinterName"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                           // if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#BarcodeNo#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    Logger.LogFileWrite("PrintSampleAccessionBarcode Log - " + result);
                    Logger.LogFileWrite("PrintSampleAccessionBarcode End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                Logger.LogFileWrite("PrintSampleAccessionBarcode Log - " + result);
            }
            return result;
        }
        [HttpPost]
        public string PrintPatientBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                Logger.LogFileWrite("PrintPatientBarcode Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["PatientPrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPatientPRNFile"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PatientPrinterName"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            Content = Content.Replace(item.Key, item.Value);
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    Logger.LogFileWrite("PrintPatientBarcode Log - " + result);
                    Logger.LogFileWrite("PrintPatientBarcode End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                Logger.LogFileWrite("PrintPatientBarcode Log - " + result);
            }
            return result;
        }
        [HttpPost]
        public string PrintRCHNoBarcode(List<Dictionary<string, string>> barcodeItem)
        {

            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["RCHNoPrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFileRCHNo"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameRCHNo"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["RCHNoPRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }
        [HttpPost]
        public string PrintSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["SlidePrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFileSlide"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameSlide"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["SlidePRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }
        [HttpPost]
        public string PrintSpecimenBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["SpecimenPrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPRNFileSpecimen"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameSpecimen"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["SpecimenPRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //sLogger.LogFileWrite("Log - " + result);
            }
            return result;
        }

        [HttpPost]
        public string HistoSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["HistoSlidePrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportHistoPRNFileSlide"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameSlide"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["HistoSlidePRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }

        [HttpPost]
        public string CytoSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["CytoSlidePrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportCytoPRNFileSlide"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameSlide"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["CytoSlidePRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }

        [HttpPost]
        public string PapSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            string result = string.Empty;
            try
            {
                //Logger.LogFileWrite("Start DateTime - " + DateTime.Now.ToString());
                string PRNFile = ConfigurationManager.AppSettings["PapSlidePrnFile"].ToString();
                string ExportPRNFile = ConfigurationManager.AppSettings["ExportPapPRNFileSlide"].ToString();
                string PrinterName = ConfigurationManager.AppSettings["PrinterNameSlide"].ToString();
                string BarcodeNo = string.Empty;
                foreach (var lst in barcodeItem)
                {
                    List<string> textarray = new List<string>();
                    if (File.Exists(PRNFile))
                    {
                        string Content = File.ReadAllText(PRNFile);
                        foreach (var item in lst)
                        {
                            if (item.Key == "#PRNType#") ExportPRNFile = ConfigurationManager.AppSettings["PapSlidePRNFile_" + item.Value].ToString();
                            Content = Content.Replace(item.Key, item.Value);
                            if (item.Key == "#RCHNumber#") BarcodeNo = item.Value;
                        }
                        File.WriteAllText(ExportPRNFile, Content.ToString());
                        RawPrinterHelper.SendFileToPrinter(PrinterName, ExportPRNFile, BarcodeNo);
                        //Logger.LogFileWrite("Log - " + BarcodeNo);
                    }
                    result = "Printed Suceessfully";
                    //Logger.LogFileWrite("Log - " + result);
                    //Logger.LogFileWrite("End DateTime - " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                //Logger.LogFileWrite("Log - " + result);
            }
            return result;
        }
    }
}

