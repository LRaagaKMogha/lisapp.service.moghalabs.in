using System;
using System.ServiceProcess;

namespace Service.WinSelfHosting
{
    public partial class WinSelfHostService : ServiceBase
    {
        public WinSelfHostService()
        {
            InitializeComponent();
        }
       
        #region Gets invoked when a service is started

        protected override void OnStart(string[] args)
        {
            try
            {
                
                ServiceStartup objstartup = new ServiceStartup();
                objstartup.StartupAPI();
            }
            catch (Exception ex)
            {
               
            }
        }

        #endregion

        #region Gets invoked when a service is stopped
        /// <summary>
        ///  Service OnStop   
        /// </summary>
        protected override void OnStop()
        {
            try
            {              
                System.GC.Collect();
            }
            catch (Exception ex)
            {
               
            }
        }

        #endregion

    }
}
