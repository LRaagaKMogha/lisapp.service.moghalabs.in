using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net.Mail;

namespace DEV.Common
{
    /// <summary>
    /// Configuration Helper
    /// Date(16/MARCH/2021)
    /// createdby: senthilkumard
    /// </summary>
    public class ConfigurationHelper
    {
        #region Initialize Configuration
        /// <summary>
        /// Initialize Configuration
        /// </summary>
        /// <param name="_configuration"></param>
        public static void InitializeConfiguration(IConfiguration _configuration)
        {
            System.Configuration.ConfigurationManager.AppSettings["ConnectionString"] = _configuration.GetSection("connectionStrings:DefaultConnection").Value;
            System.Configuration.ConfigurationManager.AppSettings["EnableNotification"] = _configuration.GetSection("EnableNotification").Value;
            System.Configuration.ConfigurationManager.AppSettings["EnableFileLog"] = _configuration.GetSection("EnableFileLog").Value;
            System.Configuration.ConfigurationManager.AppSettings["FromAddress"] = _configuration.GetSection("FromAddress").Value;
            System.Configuration.ConfigurationManager.AppSettings["HostAddress"] = _configuration.GetSection("HostAddress").Value;
            System.Configuration.ConfigurationManager.AppSettings["Port"] = _configuration.GetSection("Port").Value;
            System.Configuration.ConfigurationManager.AppSettings["Password"] = _configuration.GetSection("Password").Value;
            System.Configuration.ConfigurationManager.AppSettings["Toaddress"] = _configuration.GetSection("Toaddress").Value;
            System.Configuration.ConfigurationManager.AppSettings["LogFilepath"] = _configuration.GetSection("LogFilepath").Value;
            System.Configuration.ConfigurationManager.AppSettings["IsSSL"] = _configuration.GetSection("IsSSL").Value;
            System.Configuration.ConfigurationManager.AppSettings["MKey"] = _configuration.GetSection("MKey").Value;
            System.Configuration.ConfigurationManager.AppSettings["Salt"] = _configuration.GetSection("Salt").Value;

        }
        public static void InitializeBBConfiguration(Dictionary<string, string> values)
        {
            foreach (KeyValuePair<string, string> kvp in values)
            {
                System.Configuration.ConfigurationManager.AppSettings[kvp.Key] = kvp.Value;
            }
        }
        #endregion
        public void Writesqllog(Dictionary<string, string> param)
        {
            try
            {
                using (SqlConnection oConnection = new SqlConnection(EncryptionHelper.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"] ?? string.Empty)))
                {
                    oConnection.Open();
                    using (SqlCommand oCommand = new SqlCommand())
                    {
                        oCommand.CommandText = "pro_writelog_exception";
                        oCommand.Connection = oConnection;
                        oCommand.CommandType = CommandType.StoredProcedure;
                        foreach (var item in param)
                        {
                            oCommand.Parameters.AddWithValue(item.Key, item.Value);
                        }
                        oCommand.ExecuteNonQuery();
                        oConnection.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                Writefilelog(ex.ToFormattedString());
            }
        }
        public void SendNotifcationLog(string subject, string body)
        {
            try
            {
                string FromAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"] ?? string.Empty;
                SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["HostAddress"] ?? string.Empty, int.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"] ?? string.Empty));
                smtpClient.Credentials = new System.Net.NetworkCredential(FromAddress, System.Configuration.ConfigurationManager.AppSettings["Password"] ?? string.Empty);
                smtpClient.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsSSL"]);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromAddress);
                mail.To.Add(System.Configuration.ConfigurationManager.AppSettings["Toaddress"] ?? string.Empty);
                mail.Body = body;
                mail.Subject = subject;
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Writefilelog(ex.ToFormattedString());
            }
        }
        public void Writefilelog(string message)
        {
            FileStream? fileStream = null;
            StreamWriter? streamWriter = null;

            try
            {
                string? logBasePath = System.Configuration.ConfigurationManager.AppSettings["LogFilepath"];
                if (string.IsNullOrWhiteSpace(logBasePath)) 
                    return;

                string logFilePath = Path.Combine(logBasePath, $"{DateTime.Today:yyyyMMdd}.txt");

                var logFileInfo = new FileInfo(logFilePath);
                var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName
                                   ?? throw new InvalidOperationException("Invalid log file path"));

                if (!logDirInfo.Exists) 
                    logDirInfo.Create();

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
                if (streamWriter != null) 
                    streamWriter.Close();

                if (fileStream != null) 
                    fileStream.Close();
            }
        }
    }

    public static class ExceptionHelper
    {
        public static string ToFormattedString(this Exception exception)
        {
            var messages = exception
            .FromHierarchy(ex => ex.InnerException, ex => ex != null)
            .Select(ex => ex!.Message ?? string.Empty);

            return string.Join(Environment.NewLine, messages);
        }
        public static IEnumerable<TSource> FromHierarchy<TSource>(
        this TSource source,
        Func<TSource, TSource> nextItem,
        Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }
    }
}
