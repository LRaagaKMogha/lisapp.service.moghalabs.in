using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Integration.externalservices
{
    public class PatientDetailsServiceFactory
    {
        public static IPatientDetailsService Create(string serviceType, IConfiguration config)
        {
            switch(serviceType)
            {
                case "RCMS":
                    return new RCMSPatientDetailsService(config);
                case "SAP":
                    return new SAPPatientDetailsService(config);
                case "EMR":
                    return new SAPPatientDetailsService(config);
                default:
                    return new RCMSPatientDetailsService(config);

            }
        }
    }
}
