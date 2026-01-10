using System;
using System.IO;

namespace DEV.Common
{
    public static class Logger
    {
        public static void LogFileWrite(string message)
        {
            FileStream? fileStream = null;
            StreamWriter? streamWriter = null;
            try
            {
                string logFilePath = ApplicationConstants.LogFilePath;

                // Ensure base path is valid
                if (string.IsNullOrEmpty(logFilePath))
                    return;

                logFilePath = Path.Combine(logFilePath, $"ProgramLog-{DateTime.Today:yyyyMMdd}.txt");

                #region Create the Log file directory if it does not exist
                FileInfo logFileInfo = new FileInfo(logFilePath);

                if (logFileInfo.DirectoryName != null)
                {
                    DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

                    if (!logDirInfo.Exists)
                        logDirInfo.Create();
                }
                #endregion

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
                streamWriter?.Close();
                fileStream?.Close();
            }
        }
    }
}