using System;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace DEV.WinSelfHosting
{
 
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            this.Installers.Add(GetServiceInstaller());
            this.Installers.Add(GetServiceProcessInstaller());

            this.AfterInstall += ProjectInstaller_AfterInstall;
        }

        private ServiceInstaller GetServiceInstaller()
        {
            ServiceInstaller installer = new ServiceInstaller();
            installer.ServiceName = GetConfigurationValue("WinServiceName");
            installer.StartType = ServiceStartMode.Automatic;
            return installer;
        }

        private ServiceProcessInstaller GetServiceProcessInstaller()
        {
            ServiceProcessInstaller installer = new ServiceProcessInstaller();
            installer.Account = ServiceAccount.LocalSystem;
            return installer;
        }

        private string GetConfigurationValue(string key)
        {
            Assembly service = Assembly.GetAssembly(typeof(ProjectInstaller));
            Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
            if (config.AppSettings.Settings[key] != null)
            {
                return config.AppSettings.Settings[key].Value;
            }
            else
            {
                // LoggingMgr.LogInformation("GetConfigurationValue - Settings collection does not contain the requested key:" + key);
                throw new IndexOutOfRangeException("Settings collection does not contain the requested key:" + key);
            }
        }

        void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            using (ServiceController sc = new ServiceController(this.GetServiceInstaller().ServiceName))
            {
                sc.Start();
            }
        }
    }
}
