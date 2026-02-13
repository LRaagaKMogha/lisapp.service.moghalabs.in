using Service.Model.Integration;
using System.Threading.Tasks;

namespace Service.Repository.Integration.externalservices
{
    public interface IPatientDetailsService
    {
        Task<ExternalPatientDetails> GetPatientDetails(string patientId);
    }
}
