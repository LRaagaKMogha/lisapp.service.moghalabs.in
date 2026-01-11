using Service.Model.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Integration.externalservices
{
    public interface IPatientDetailsService
    {
        Task<ExternalPatientDetails> GetPatientDetails(string patientId);
    }
}
