using System.ServiceProcess;

namespace Service.Win.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {               
               new JobSchedulerManager()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
