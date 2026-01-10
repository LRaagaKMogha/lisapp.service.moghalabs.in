using System;
using System.ServiceProcess;
using System.Configuration;

namespace Dev.WinService
{

    partial class SyncServiceManager : ServiceBase
    {
        public SyncServiceManager()
        {
            InitializeComponent();
        }
        #region class variables

        private bool isReady;
        private System.Threading.Timer MailTimer;

        #endregion

        #region Gets invoked when a service is started
        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                // value in seconds 
                int interval = int.Parse(ConfigurationManager.AppSettings["Timeinterval"].ToString());
                // Convert the seconds to milliseconds
                interval = interval * 1000;

                // Bind the timer with the method that needs to be called in regular interval
                this.MailTimer = new System.Threading.Timer(new System.Threading.TimerCallback(this.TimerWakeUp), null, 0, interval);

                // Set the state of the Service to true
                this.isReady = true;
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Gets invoked when a service is stopped
        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                // Set the state of the Service to false
                this.isReady = false;

                // Change the start time and interval time to infinite
                this.MailTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                // Set the timer method to null so that it stops calling the TimerWakeUp method 
                this.MailTimer = null;

                // Call GC to release the memory used by the resources 
                System.GC.Collect();

            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region This method will be called by the Timer class in regular intervals
        /// <summary>
        /// TimerWakeUp
        /// </summary>
        /// <param name="sender"></param>
        protected void TimerWakeUp(object sender)
        {
            try
            {
                // NotificationHelper objNotificationHelper = new NotificationHelper();
                // objNotificationHelper.CallMessageService();

                // ICMRExternalService objICMRExternalService = new ICMRExternalService();
                //objICMRExternalService.PushService();

                // FileDeleteHelper fileDeleteHelper = new FileDeleteHelper();
                //fileDeleteHelper.FileDeleteService();

                //ArchiveTransaction archiveTransaction = new ArchiveTransaction();
                //archiveTransaction.PushArchiveData();

                //ScheduleReport obj = new ScheduleReport();
                //obj.GetAutoApproval();

                //BBTReports obj = new BBTReports();
                //obj.SendEmailReports();
            }
            catch (Exception ex)
            {

            }
        }
        # endregion
    }
}
