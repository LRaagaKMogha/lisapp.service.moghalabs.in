using Microsoft.Extensions.Configuration;

namespace Service.Repository.Integration.externalservices
{
    public class PatientdetailsServiceFactory
    {
        public static IPatientdetailsService Create(string serviceType, IConfiguration config)
        {
            switch(serviceType)
            {
                case "RCMS":
                    return new RCMSPatientdetailsService(config);
                case "SAP":
                    return new SAPPatientdetailsService(config);
                case "EMR":
                    return new SAPPatientdetailsService(config);
                default:
                    return new RCMSPatientdetailsService(config);
            }
        }
    }
}
