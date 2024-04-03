using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllPatientsAsync();

        Task<IPagedList<PatientDto>> SearchPatientsAsync(PatientQuery condition, PagingParams paging);

        Task<PatientDto> GetPatientByChartNumberAsync(string chartNumber);

        Task<string> GetNewChartNumberAsync();

        Task<bool> CreateAsync(PatientCreateDto patient);

        Task<bool> UpdateAsync(string chartNumber, PatientUpdateDto patient);

        Task<bool> DeleteAsync();
    }
}
