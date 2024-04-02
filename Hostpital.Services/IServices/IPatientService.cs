using Hospital.Domain.DTO;

namespace Hostpital.Service.IServices
{
    public interface IPatientService
    {
        List<PatientDto> GetPatients();

        PatientDto GetPatientByChartNumber(string chartNumber);

        string GetNewChartNumber();

        int GetMaxChartNumberPatient();

        void Create(PatientCreateDto patient);

        void Update();

        void Delete();
    }
}
