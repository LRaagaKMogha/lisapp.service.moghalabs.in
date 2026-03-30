using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Service.Common.Core
{
    public class Logger
    {
        private readonly IConfiguration _config;

        public Logger(IConfiguration config)
        {
            _config = config;
        }

        public void LogWrite(string message)
        {
            try
            {
                var logPath = _config["Logging:ExceptionLogPath"];

                if (string.IsNullOrWhiteSpace(logPath))
                    return;

                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                var filePath = Path.Combine(
                    logPath,
                    $"WinLog-{DateTime.Now:yyyyMMdd}.txt"
                );

                File.AppendAllText(filePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\temp\fallback_log.txt", ex.ToString());
            }
        }

        public void LogWritebyVenue(int venueNo, int venueBranchNo, string message)
        {
            try
            {
                var basePath = _config["Logging:ExceptionLogPath"];

                if (string.IsNullOrWhiteSpace(basePath))
                    return;

                var logDir = Path.Combine(basePath, venueNo.ToString(), venueBranchNo.ToString());
                Directory.CreateDirectory(logDir);

                var filePath = Path.Combine(
                    logDir,
                    $"WinLog-{DateTime.Now:yyyyMMdd}.txt"
                );

                // Safe write
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(message);
                }

                var isSendLog = Convert.ToInt16(_config["Logging:IssendLog"] ?? "0");

                if (isSendLog == 1)
                {
                    SendNotifcationLog(
                        $"Failed Email Error VenueNo-{venueNo} | VenueBranchNo-{venueBranchNo}",
                        message
                    );
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
            }
        }

        public void SendNotifcationLog(string subject, string body)
        {
            try
            {
                var fromAddress = _config["Logging:FromAddress"];
                var password = _config["Logging:FromAddressPassword"];
                var host = _config["Logging:HostAddress"];
                var port = int.Parse(_config["Logging:Port"] ?? "25");
                var toAddress = _config["Logging:ToAddress"];
                var enableSsl = Convert.ToBoolean(_config["Logging:IsSSL"] ?? "false");

                if (string.IsNullOrWhiteSpace(fromAddress) ||
                    string.IsNullOrWhiteSpace(toAddress) ||
                    string.IsNullOrWhiteSpace(host))
                    return;

                using (var smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.Credentials = new NetworkCredential(fromAddress, password);
                    smtpClient.EnableSsl = enableSsl;

                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress(fromAddress);
                        mail.To.Add(toAddress);
                        mail.Subject = subject;
                        mail.Body = body;

                        smtpClient.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
            }
        }
    }
}