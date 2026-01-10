using DevExpress.Web.ASPxRichEdit.Localization;
using DevExpress.Web.ASPxRichEdit;
using DevExpress.Web;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Localization;
using System; 
using System.Configuration;
using System.IO; 

namespace DEV.ReportService.Editor
{
    public partial class Editor : System.Web.UI.Page
    {
        public string VenueNo = null;
        public string VenueBranchNo = null;
        public string PatientVisitNo = null;
        public string OrderListNo = null;
        public string ServiceNo = null;
        public string Type = null;
        public string TempNo = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            //RichEditDemoUtils.HideFileTab(ASPxRichEdit1);
            //richedit.CreateDefaultRibbonTabs(true);
            //RibbonTab fileTab = richedit.RibbonTabs[0];
            //richedit.RibbonTabs.Remove(fileTab);

            CreateRibbonReviewTab(ASPxRichEdit1);
            NewDocument();

            //DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["data"] != null)
                {
                    string decodedata = CommonSecurity.base64Decode(Request.QueryString["data"].ToString());
                    string[] data = decodedata.Split('&');
                    VenueNo = data[0];
                    VenueBranchNo = data[1];
                    PatientVisitNo = data[2];
                    OrderListNo = data[3];
                    ServiceNo = data[4];
                    Type = data[5];
                    if (Type == "O")
                    {
                        string FolderPath = ConfigurationManager.AppSettings["TransTemplateFilePath"] + VenueNo + "\\" + OrderListNo;
                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        string file = FolderPath + "\\" + ServiceNo + ".rtf";
                        if (File.Exists(file))
                        {
                            ASPxRichEdit1.Open(FolderPath + "\\" + ServiceNo + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                        }
                    }
                    else if (Type == "T")
                    {
                        string templateNo = OrderListNo;
                        string FolderPath = ConfigurationManager.AppSettings["MasterFilePath"] + VenueNo + "\\Template";
                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        string file = FolderPath + "\\" + templateNo + ".rtf";
                        if (File.Exists(file))
                        {
                            ASPxRichEdit1.Open(FolderPath + "\\" + templateNo + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                        }
                    }
                    else if (Type == "M")
                    {
                        string templateNo = OrderListNo;
                        string FolderPath = ConfigurationManager.AppSettings["MasterTemplateFilePath"] + VenueNo + "\\" + ServiceNo;
                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        string file = FolderPath + "\\" + templateNo + ".rtf";
                        if (File.Exists(file))
                        {
                            ASPxRichEdit1.Open(FolderPath + "\\" + templateNo + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                        }
                    }
                }
            }
        }
        protected void RichEdit_Saving(object source, DevExpress.Web.Office.DocumentSavingEventArgs e)
        {
            if (Request.QueryString["data"] != null)
            {
                string decodedata = CommonSecurity.base64Decode(Request.QueryString["data"].ToString());
                string[] data = decodedata.Split('&');
                VenueNo = data[0];
                VenueBranchNo = data[1];
                PatientVisitNo = data[2];
                OrderListNo = data[3];
                ServiceNo = data[4];
                Type = data[5];

                string FolderPath = ConfigurationManager.AppSettings["TransTemplateFilePath"] + VenueNo + "\\" + OrderListNo;
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                if (!File.Exists(FolderPath + "\\" + ServiceNo + ".rtf"))
                {
                    File.Create(FolderPath + "\\" + ServiceNo + ".rtf").Dispose();
                }
                var fileBytes = ASPxRichEdit1.SaveCopy(DocumentFormat.Rtf);
                using (Stream file = File.OpenWrite(FolderPath + "\\" + ServiceNo + ".rtf"))
                {
                    file.Write(fileBytes, 0, fileBytes.Length);
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["data"] != null)
            {
                string decodedata = CommonSecurity.base64Decode(Request.QueryString["data"].ToString());
                string[] data = decodedata.Split('&');
                VenueNo = data[0];
                VenueBranchNo = data[1];
                PatientVisitNo = data[2];
                OrderListNo = data[3];
                ServiceNo = data[4];
                Type = data[5];
                if (Type != null && Type.ToUpper() == "M")
                {
                    TempNo = hdntemplateno.Value; //data[6];
                    string FolderPath = ConfigurationManager.AppSettings["MasterTemplateFilePath"] + VenueNo + "\\" + ServiceNo;
                    if (!Directory.Exists(FolderPath))
                    {
                        Directory.CreateDirectory(FolderPath);
                    }
                    if (!File.Exists(FolderPath + "\\" + TempNo + ".rtf"))
                    {
                        File.Create(FolderPath + "\\" + TempNo + ".rtf").Dispose();
                    }
                    var fileBytes = ASPxRichEdit1.SaveCopy(DocumentFormat.Rtf);
                    using (Stream file = File.OpenWrite(FolderPath + "\\" + TempNo + ".rtf"))
                    {
                        file.Write(fileBytes, 0, fileBytes.Length);
                    }
                }
                else
                {
                    string issave = hdnIsSave.Value;
                    if (issave == "1")
                    {
                        string FolderPath = ConfigurationManager.AppSettings["TransTemplateFilePath"] + VenueNo + "\\" + OrderListNo;
                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        if (!File.Exists(FolderPath + "\\" + ServiceNo + ".rtf"))
                        {
                            File.Create(FolderPath + "\\" + ServiceNo + ".rtf").Dispose();
                        }
                        var fileBytes = ASPxRichEdit1.SaveCopy(DocumentFormat.Rtf);
                        using (Stream file = File.OpenWrite(FolderPath + "\\" + ServiceNo + ".rtf"))
                        {
                            file.Write(fileBytes, 0, fileBytes.Length);
                        }
                    }
                    else
                    {
                        string templateNo = hdntemplateno.Value;
                        string FolderPath = ConfigurationManager.AppSettings["MasterTemplateFilePath"] + VenueNo + "\\" + ServiceNo;
                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        string file = FolderPath + "\\" + templateNo + ".rtf";
                        if (File.Exists(file))
                        {
                            ASPxRichEdit1.Open(FolderPath + "\\" + templateNo + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                        }

                    }


                }
            }
        }

        private void CreateRibbonReviewTab(ASPxRichEdit demoRichEdit)
        {
            demoRichEdit.CreateDefaultRibbonTabs(true);
            RERReviewTab reviewTab = new RERReviewTab();

            // Spell check 
            RibbonGroup proofingGroup = new RibbonGroup();
            proofingGroup.Text = ASPxRichEditLocalizer.GetString(ASPxRichEditStringId.GroupDocumentProofing);

            var spellingButtonItem = new RibbonButtonItem
            {
                Name = "showSpellingDialog",
                Size = RibbonItemSize.Large,
                Text = XtraRichEditLocalizer.GetString(XtraRichEditStringId.MenuCmd_CheckSpelling),
            };

            spellingButtonItem.LargeImage.Url = "../Icons/SpellCheck.svg";

            proofingGroup.Items.Add(spellingButtonItem);

            // AugnitoVoice
            RibbonGroup augnitoVoiceGroup = new RibbonGroup();
            augnitoVoiceGroup.Text = "Voice";

            var augnitoVoicItem = new RibbonButtonItem
            {
                Name = "voiceToText",
                Size = RibbonItemSize.Large,
                Text = "Voice To Text", //XtraRichEditLocalizer.GetString(XtraRichEditStringId. MenuCmd_CheckSpelling),
            };

            augnitoVoicItem.LargeImage.Url = "../Icons/voice.png";
            augnitoVoiceGroup.Items.Add(augnitoVoicItem);

            reviewTab.Groups.Add(proofingGroup);
            reviewTab.Groups.Add(augnitoVoiceGroup);
            demoRichEdit.RibbonTabs.Add(reviewTab);
             
        }

        protected void ASPxRichEdit1_SpellCheckerWordAdded(object sender, DevExpress.XtraSpellChecker.WordAddedEventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Dictionaries", "custom.txt");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            // Append a new line to the file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(e.Word);
            }
        }

        private void NewDocument()
        {
            RichEditDocumentServer server = new RichEditDocumentServer();
            //server.Document.Sections[0].Page.Landscape = false;
            //server.Document.Unit = DevExpress.Office.DocumentUnit.Millimeter;
            //server.Document.Sections[0].Margins.Left = 0.5f;
            //server.Document.Sections[0].Margins.Right = 0.5f;
            //server.Document.Sections[0].Margins.Top = 0.5f;
            //server.Document.Sections[0].Margins.Bottom = 0.5f;

            //server.Document.DefaultCharacterProperties.FontName = "Arial";
            //server.Document.DefaultCharacterProperties.FontSize = 12f;
            //server.Document.DefaultCharacterProperties.ForeColor = Color.Red;

            server.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            //server.Document.Sections[0].Page.Width = 105f;
            server.Document.Sections[0].Page.Height = 1250f; 

            MemoryStream memoryStream = new MemoryStream();
            server.SaveDocument(memoryStream, DocumentFormat.Rtf);
            ASPxRichEdit1.Open("document" + Guid.NewGuid().ToString(), DocumentFormat.Rtf, () => { return memoryStream.ToArray(); });
        }

        //protected void UploadControl_FileUploadComplete1(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        //{

        //    if (e.UploadedFile.IsValid)
        //    {
        //        string fileName = e.UploadedFile.FileName;
        //        e.UploadedFile.SaveAs(MapPath(ASPxRichEdit1.WorkDirectory) + "\\" + fileName, true);
        //        e.CallbackData = fileName;
        //    }
        //}
    }
}
