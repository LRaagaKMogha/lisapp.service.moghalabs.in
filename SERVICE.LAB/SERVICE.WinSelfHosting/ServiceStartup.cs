using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Web.Http.SelfHost.Channels;
using Service.Win.Common;

namespace Service.WinSelfHosting
{
    public class ServiceStartup
    {
        public void StartupAPI()
        {
            try
            {
                Logger.LogWrite("StartupAPI - " + DateTime.Now.ToString());
                string PortNumber = ConfigurationManager.AppSettings["PortNo"].ToString();
                var config = new HttpSelfHostConfiguration("https://localhost:" + PortNumber + "");
                config.MaxReceivedMessageSize = 2147483647; // use config for this value
                config.EnableCors();

                config.Routes.MapHttpRoute(
                   name: "API",
                   routeTemplate: "{controller}/{action}/{id}",
                   defaults: new { id = RouteParameter.Optional }
               );
                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();

            }
            catch (Exception ex)
            {
                Logger.LogWrite("StartupAPI - " + ex.ToString());
                throw;
            }
        }
    }
    internal class MyHttpsSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public MyHttpsSelfHostConfiguration(string baseAddress) : base(baseAddress) { }
        public MyHttpsSelfHostConfiguration(Uri baseAddress) : base(baseAddress) { }
        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            //httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;
            return base.OnConfigureBinding(httpBinding);
        }
    }
}
