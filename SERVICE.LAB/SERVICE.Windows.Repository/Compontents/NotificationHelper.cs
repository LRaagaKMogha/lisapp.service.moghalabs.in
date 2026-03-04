using Service.Win.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.Win.Repository
{
    public class NotificationHelper
    {
        #region Call Message Service
        /// <summary>
        /// CallMessageService
        /// </summary>
        public void CallMessageService()
        {
            try
            {
                Logger.LogWrite("CallMessageService method of NotificationHelper class is called.");

                List<MessageHostDTO> MessageList = GetMessageQueueList_Email();
                foreach (var item in MessageList)
                {
                    if (item.CommunicationType.ToUpper() == "EMAIL")
                    {
                        Logger.LogWrite("Sending email for message queue no: " + item.MessageQueueNo);
                        Task<int> result = SendEmail(item);
                        if (result.Result > 0)
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 2);
                        }
                        else
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 3);
                        }
                    }
                    else if (item.CommunicationType.ToUpper().Trim() == "SMS")
                    {
                        int result = SendSMS(item);
                        if (result > 0)
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 2);
                        }
                        else
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 3);
                        }
                    }
                    else if (item.CommunicationType.ToUpper().Trim() == "WSMS")
                    {
                        int result = 0;
                        if (item.VenueNo == 40)
                        {
                            result = SendPostWSMS(item);
                        }
                        else
                        {
                            result = SendWSMS(item);
                        }

                        if (result > 0)
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 2);
                        }
                        else
                        {
                            UpdateEmailStatus(item.MessageQueueNo, 3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }
        #endregion

        #region Get Message QueueList
        /// <summary>
        /// GetMessageQueueList - Email
        /// </summary>
        public List<MessageHostDTO> GetMessageQueueList_Email()
        {
            List<MessageHostDTO> objresult = new List<MessageHostDTO>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    int oldMessageQueueNo = 0;
                    int newMessageQueueNo = 0;
                    int OldAttachmentNo = 0;
                    int newAttachmentNo = 0;

                    var Messageitem = context.Pro_GetMessageQueueList_Email().ToList();

                    foreach (var MessageList in Messageitem)
                    {
                        MessageHostDTO item = new MessageHostDTO();
                        List<MessageAttachment> lstAttachment = new List<MessageAttachment>();
                        newMessageQueueNo = MessageList.MessageQueueNo;
                        var AttachmentItem = Messageitem.Where(x => x.MessageQueueNo == newMessageQueueNo).Select(x => new { x.MessageQueueAttachmentNo, x.AttachmentName, x.AttachmentURL, x.IsEmbed }).ToList();
                        
                        if (newMessageQueueNo != oldMessageQueueNo)
                        {
                            item.MessageQueueNo = MessageList.MessageQueueNo;
                            item.HostAddress = MessageList.Host;
                            item.ToAddress = MessageList.Address;
                            item.CCAddress = MessageList.CCAddress;
                            item.BCCAddress = MessageList.BCCAddress;
                            item.Username = MessageList.UserName;
                            item.Password = MessageList.Password;
                            item.Port = MessageList.Port;
                            item.SSLStatus = (bool)MessageList.IsSSL;
                            item.Param1 = MessageList.Param1;
                            item.Param2 = MessageList.Param2;
                            item.Param3 = MessageList.Param3;
                            item.CommunicationType = MessageList.CommunicationType;
                            item.MessageContent = new MessageContent();
                            item.MessageContent.Subject = MessageList.Subject;
                            item.MessageContent.Body = MessageList.Body;
                            item.VenueNo = (int)MessageList.VenueNo;
                            item.VenueBranchNo = (int)MessageList.VenueBranchNo;
                            oldMessageQueueNo = MessageList.MessageQueueNo;
                            OldAttachmentNo = 0;
                            
                            foreach (var Item in AttachmentItem)
                            {
                                newAttachmentNo = (int)Item.MessageQueueAttachmentNo;
                                if (OldAttachmentNo != newAttachmentNo)
                                {
                                    MessageAttachment objAttachment = new MessageAttachment()
                                    {
                                        AttachmentName = Item.AttachmentName,
                                        AttachmentURL = Item.AttachmentURL,
                                        IsEmbed = (bool)Item.IsEmbed
                                    };
                                    OldAttachmentNo = newAttachmentNo;
                                    lstAttachment.Add(objAttachment);

                                }
                                item.MessageContent.Attachment = lstAttachment;
                            }
                            objresult.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return objresult;
        }
        #endregion

        #region Get Message QueueList
        /// <summary>
        /// GetMessageQueueList - Whatsapp 
        /// </summary>
        public List<MessageHostDTO> GetMessageQueueList_WMS()
        {
            List<MessageHostDTO> objresult = new List<MessageHostDTO>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    int oldMessageQueueNo = 0;
                    int newMessageQueueNo = 0;
                    int OldAttachmentNo = 0;
                    int newAttachmentNo = 0;

                    var Messageitem = context.Pro_GetMessageQueueList_WMS().ToList();

                    foreach (var MessageList in Messageitem)
                    {
                        MessageHostDTO item = new MessageHostDTO();
                        List<MessageAttachment> lstAttachment = new List<MessageAttachment>();
                        newMessageQueueNo = MessageList.MessageQueueNo;
                        var AttachmentItem = Messageitem.Where(x => x.MessageQueueNo == newMessageQueueNo).Select(x => new { x.MessageQueueAttachmentNo, x.AttachmentName, x.AttachmentURL, x.IsEmbed }).ToList();
                        if (newMessageQueueNo != oldMessageQueueNo)
                        {
                            item.MessageQueueNo = MessageList.MessageQueueNo;
                            item.HostAddress = MessageList.Host;
                            item.ToAddress = MessageList.Address;
                            item.CCAddress = MessageList.CCAddress;
                            item.BCCAddress = MessageList.BCCAddress;
                            item.Username = MessageList.UserName;
                            item.Password = MessageList.Password;
                            item.Port = MessageList.Port;
                            item.SSLStatus = (bool)MessageList.IsSSL;
                            item.Param1 = MessageList.Param1;
                            item.Param2 = MessageList.Param2;
                            item.Param3 = MessageList.Param3;
                            item.CommunicationType = MessageList.CommunicationType;
                            item.MessageContent = new MessageContent();
                            item.MessageContent.Subject = MessageList.Subject;
                            item.MessageContent.Body = MessageList.Body;
                            item.VenueNo = (int)MessageList.VenueNo;
                            item.VenueBranchNo = (int)MessageList.VenueBranchNo;
                            oldMessageQueueNo = MessageList.MessageQueueNo;
                            OldAttachmentNo = 0;
                            foreach (var Item in AttachmentItem)
                            {
                                newAttachmentNo = (int)Item.MessageQueueAttachmentNo;
                                if (OldAttachmentNo != newAttachmentNo)
                                {
                                    MessageAttachment objAttachment = new MessageAttachment()
                                    {
                                        AttachmentName = Item.AttachmentName,
                                        AttachmentURL = Item.AttachmentURL,
                                        IsEmbed = (bool)Item.IsEmbed
                                    };
                                    OldAttachmentNo = newAttachmentNo;
                                    lstAttachment.Add(objAttachment);

                                }
                                item.MessageContent.Attachment = lstAttachment;
                            }
                            objresult.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return objresult;
        }
        #endregion

        #region Update Email Status
        /// <summary>
        /// UpdateEmailStatus
        /// </summary>
        /// <param name="MessageQueueNo"></param>
        /// <param name="Status"></param>
        public void UpdateEmailStatus(int MessageQueueNo, int Status)
        {
            using (YMEntities context = new YMEntities())
            {
                var MessageQueue = context.tbl_MessageQueue.FirstOrDefault(a => a.MessageQueueNo == MessageQueueNo);
                MessageQueue.IsSend = Status;
                context.Entry(MessageQueue).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion

        #region SendEmail
        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="Messageitem"></param>
        /// <returns></returns>
        internal async Task<int> SendEmail(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                StringBuilder strEmbedcontent = new StringBuilder();
                MailMessage mail = new MailMessage();
                using (SmtpClient SmtpServer = new SmtpClient(Messageitem.HostAddress))
                {
                    mail.From = new MailAddress(Messageitem.Username);
                    mail.To.Add(Messageitem.ToAddress);
                    if (!string.IsNullOrEmpty(Messageitem.CCAddress))
                    {
                        mail.CC.Add(Messageitem.CCAddress);
                    }
                    if (!string.IsNullOrEmpty(Messageitem.BCCAddress))
                    {
                        mail.Bcc.Add(Messageitem.BCCAddress);
                    }
                    mail.Subject = Messageitem.MessageContent.Subject;
                    System.Net.Mail.Attachment attachment;
                    foreach (var item in Messageitem.MessageContent.Attachment)
                    {
                        if (item.IsEmbed)
                        {
                            strEmbedcontent.Append("</br>");
                            strEmbedcontent.Append(item.AttachmentURL);
                        }
                        else
                        {
                            attachment = new System.Net.Mail.Attachment(item.AttachmentURL);
                            attachment.Name = item.AttachmentName;
                            mail.Attachments.Add(attachment);
                        }
                    }
                    mail.Body = Messageitem.MessageContent.Body + " </br>" + strEmbedcontent.ToString();
                    mail.IsBodyHtml = true;
                    SmtpServer.Port = Messageitem.Port;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Messageitem.Username, Messageitem.Password);
                    SmtpServer.EnableSsl = Messageitem.SSLStatus;
                    SmtpServer.Timeout = 2000;
                    await SmtpServer.SendMailAsync(mail);
                    result = 1;
                }
            }
            catch (SmtpFailedRecipientException RE)
            {
                Logger.LogWritebyVenue(Messageitem.VenueNo, Messageitem.VenueBranchNo, RE.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                Logger.LogWritebyVenue(Messageitem.VenueNo, Messageitem.VenueBranchNo, ex.ToString());
                return 0;
            }
            return result;
        }
        #endregion

        #region SendSMS
        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="ApiServiceURL"></param>
        /// <param name="SSLStatus"></param>
        /// <returns></returns>
        internal int SendSMS(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                string PostURL = Messageitem.HostAddress + "&" + Messageitem.Param1 + "=" + Messageitem.ToAddress + "&" + Messageitem.Param2 + "=" + Messageitem.MessageContent.Body;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(PostURL);
                if (Messageitem.SSLStatus)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                result = 1;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
                return 0;
            }
            return result;
        }
        #endregion

        #region SendWSMS
        /// <summary>
        /// WhatsApp Send SMS
        /// </summary>
        /// <param name="ApiServiceURL"></param>
        /// <param name="SSLStatus"></param>
        /// <returns></returns>
        internal int SendWSMS(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                StringBuilder PostURL = new StringBuilder();
                PostURL.Append(Messageitem.HostAddress + "&" + Messageitem.Param1 + "=91" + Messageitem.ToAddress + "&" + Messageitem.Param2 + "=" + Messageitem.MessageContent.Body);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(PostURL.ToString());
                if (Messageitem.SSLStatus)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                myReq.Proxy = null;
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                if (Messageitem.MessageContent.Attachment.Count > 0)
                {
                    SendWSMSFiles(Messageitem);
                }
                result = 1;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
                return 0;
            }
            return result;
        }

        /// <summary>
        ///  WhatsApp file SMS
        /// </summary>
        /// <param name="Messageitem"></param>
        /// <returns></returns>
        internal int SendWSMSFiles(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                StringBuilder PostURL = new StringBuilder();
                foreach (var item in Messageitem.MessageContent.Attachment)
                {
                    Messageitem.HostAddress = Messageitem.HostAddress.Replace("sendText?", "sendFiles?");
                    PostURL.Append(Messageitem.HostAddress + "&" + Messageitem.Param1 + "=91" + Messageitem.ToAddress + "&" + Messageitem.Param3 + "=" + item.AttachmentURL);
                }
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(PostURL.ToString());
                if (Messageitem.SSLStatus)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                myReq.Proxy = null;
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                result = 1;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
                return 0;
            }
            return result;
        }      
        internal int SendPostWSMS(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                WMSPOSTRequest objrequest = new WMSPOSTRequest();
                objrequest.countryCode = "+91";
                objrequest.phoneNumber = Messageitem.ToAddress;
                objrequest.callbackData = "some text here";
                objrequest.type = "Template";
                objrequest.template = new WMSPOSTTemplate();
                objrequest.template.name = "send_reports";
                objrequest.template.languageCode = "en";
                objrequest.template.headerValues = new List<string>();
                //foreach (var item in Messageitem.MessageContent.Attachment)
                //{
                //    objrequest.template.headerValues.Add(item.AttachmentURL);
                //    objrequest.template.fileName = item.AttachmentName;
                //}
                objrequest.template.bodyValues = new List<string>();
                foreach (string item in Messageitem.MessageContent.Body.Split('|'))
                {
                    if (item.Contains(".com"))
                    {
                        objrequest.template.headerValues.Add(item);
                        int pos = item.LastIndexOf("/") + 1;
                        objrequest.template.fileName = item.Substring(pos, item.Length - pos);
                    }
                    else {
                        objrequest.template.bodyValues.Add(item);
                    }
                }
                string ServiceMethod = Messageitem.HostAddress;
                var ServiceWebRequest = WebRequest.CreateHttp(ServiceMethod);
                ServiceWebRequest.ContentType = "application/json; charset=utf-8";
                ServiceWebRequest.Headers["Authorization"] = "Bearer " + Messageitem.Password;
                ServiceWebRequest.Method = "POST";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(objrequest);
                    Logger.LogWrite(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                //ServiceWebRequest.Proxy = null;
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var data = JsonConvert.DeserializeObject<WMSPOSTResponse>(streamReader.ReadToEnd());
                    result = Convert.ToInt16(data.result);
                }
                result = 1;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
                return 0;
            }
            return result;
        }
        internal int SendPostWSMS_14(MessageHostDTO Messageitem)
        {
            int result = 0;
            try
            {
                WMSPostRequst_14 objrequest = new WMSPostRequst_14();
                objrequest.apiKey = Messageitem.Password;
                objrequest.campaignName = "e_report";
                objrequest.destination = Messageitem.ToAddress;
                objrequest.userName = "DML";
                objrequest.source = "new-landing-page form";
                objrequest.media = new Media();
                //foreach (string item in Messageitem.MessageContent.Body.Split('|'))
                //{
                //    if (item.Contains(".com"))
                //    {
                //        objrequest.media.url = item;
                //        int pos = item.LastIndexOf("/") + 1;
                //        objrequest.media.filename = item.Substring(pos, item.Length - pos);
                //    }
                //}
                foreach (var item in Messageitem.MessageContent.Attachment)
                {
                    objrequest.media.url = item.AttachmentURL;
                    objrequest.media.filename = item.AttachmentName;
                }
                var forms = objrequest.templateParams = new List<string>();
                forms.Add("$FirstName");
                forms.Add("$FirstName");
                string ServiceMethod = Messageitem.HostAddress;
                var ServiceWebRequest = WebRequest.CreateHttp(ServiceMethod);
                ServiceWebRequest.ContentType = "application/json; charset=utf-8";
                ServiceWebRequest.Method = "POST";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(objrequest);
                    Logger.LogWrite(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                //ServiceWebRequest.Proxy = null;
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(streamReader.ReadToEnd());
                    Logger.LogWrite(data.ToString());
                }
                result = 1;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
                return 0;
            }
            return result;
        }
        #endregion
    }
}
