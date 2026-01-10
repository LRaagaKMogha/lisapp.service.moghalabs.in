using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;

namespace Dev.Win.Common
{
    public class Logger
    {
        public static void LogWrite(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = ConfigurationManager.AppSettings["ExceptionLogPath"].ToString();
                logFilePath = logFilePath + "WinLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static void LogWritebyVenue(int VenueNo, int VenueBranchNo, string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = ConfigurationManager.AppSettings["ExceptionLogPath"].ToString() + "/" + VenueNo + "/" + VenueBranchNo;
                logFilePath = logFilePath + "WinLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(message);

                int IssendLog = Convert.ToInt16(ConfigurationManager.AppSettings["IssendLog"]);
                if (IssendLog == 1)
                {
                    SendNotifcationLog("Failed Email Error VenueNo-" + VenueNo.ToString()+ "|VenueBranchNo-" + VenueBranchNo.ToString(), message);
                }
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }
        public static void SendNotifcationLog(string subject, string body)
        {
            try
            {
                string FromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();
                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["HostAddress"].ToString(), int.Parse(ConfigurationManager.AppSettings["Port"]));
                smtpClient.Credentials = new System.Net.NetworkCredential(FromAddress, ConfigurationManager.AppSettings["FromAddressPassword"].ToString());
                smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromAddress);
                mail.To.Add(ConfigurationManager.AppSettings["Toaddress"].ToString());
                mail.Body = body;
                mail.Subject = subject;
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
            }
        }
    }
}


