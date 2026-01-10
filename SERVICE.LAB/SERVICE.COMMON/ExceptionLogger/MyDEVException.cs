using System;
using System.Collections.Generic;
using System.Configuration;

namespace DEV.Common
{
    public static class MyDevException
    {
        public static void Error(Exception exception, string functionname, ExceptionPriority Priority, ApplicationType applicationType, int? veneueid, int? venuebranchid, int? userid)
        {
            string exceptioncontent = exception.ToFormattedString();
            ConfigurationHelper objconfig = new ConfigurationHelper();
            if (Priority == ExceptionPriority.High)
            {
                if (ConfigurationManager.AppSettings["EnableNotification"] == "1")
                {
                    objconfig.SendNotifcationLog("Exception Alert - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "/ VeneueBranchNo - " + venuebranchid, exceptioncontent);
                }
            }
            WriteDevException(exceptioncontent, functionname, Priority, applicationType, veneueid, venuebranchid, userid);

        }
        public static void WriteDevException(string message, string functionname, ExceptionPriority Priority, ApplicationType applicationType, int? veneueid, int? venuebranchid, int? userid)
        {
            ConfigurationHelper objconfig = new ConfigurationHelper();
            if (ConfigurationManager.AppSettings["EnableFileLog"] == "1")
            {
                objconfig.Writefilelog(message);
            }
            else
            {
                Dictionary<string, string> objparam = new Dictionary<string, string>();
                objparam.Add("Title", functionname);
                objparam.Add("Description", message);
                objparam.Add("Priority", Priority.ToString());
                objparam.Add("ApplicationType", applicationType.ToString());
                objparam.Add("ExceptionType", "1");
                objparam.Add("VenueNo", veneueid?.ToString() ?? string.Empty);
                objparam.Add("VenueBranchNo", venuebranchid?.ToString() ?? string.Empty);
                objparam.Add("userID", userid?.ToString() ?? string.Empty);
                objconfig.Writesqllog(objparam);
            }
        }

        public static void Error(Exception ex, string v1, ExceptionPriority low, ApplicationType rEPOSITORY, short venueNo, int v2)
        {
            throw new NotImplementedException();
        }
    }

}
