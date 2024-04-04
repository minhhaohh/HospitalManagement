using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllPatientsAsync();

        Task<IPagedList<PatientDto>> SearchPatientsAsync(PatientQuery condition, PagingParams paging);

        Task<PatientDto> GetPatientByIdAsync(int id);

        Task<string> CreateAsync(PatientCreateDto patient);

        Task<string> UpdateAsync(int id, PatientUpdateDto patient);

        Task<string> DeleteAsync(int id);
    }
}
