using PatientManagement.Models;

namespace PatientManagement.IServices
{
    public interface IGeographyService
    {
        List<Province> GetProvinces(int page, int rows);

        List<District> GetDistricts(int page, int rows);

        List<Ward> GetWards(int page, int rows);
    }
}
