using Service.Model.Integration;
using System.Threading.Tasks;

namespace Service.Repository.Integration.externalservices
{
    public interface IPatientdetailsService
    {
        Task<ExternalPatientDetails> GetPatientdetails(string patientId);
    }
}
