using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetPatientsAsync();

        Task<JqGridResult<PatientDto>> GetPatientsForJqGridAsync(int page, int rows);

        Task<PatientDto> GetPatientByChartNumberAsync(string chartNumber);

        Task<string> GetNewChartNumberAsync();

        Task CreateAsync(PatientCreateDto patient);

        Task UpdateAsync(string chartNumber, PatientUpdateDto patient);

        Task DeleteAsync();
    }
}
