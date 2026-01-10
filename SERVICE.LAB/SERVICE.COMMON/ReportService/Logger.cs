using System;
using System.IO;

namespace DEV.Common
{
    public class LogWriter
    {
        public LogWriter(string? logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string? logMessage)
        {
            string m_exePath = string.Empty;
            m_exePath = @"C:\\log";
            
            if (!Directory.Exists(m_exePath))
            {
                Directory.CreateDirectory(m_exePath);
            }
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + (ApplicationConstants.DoubleSlash ?? "\\") + "log.txt"))
                {
                    Log(logMessage ?? string.Empty, w);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Logger.LogWrite", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
        }
        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Logger.Log", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
        }
    }
}