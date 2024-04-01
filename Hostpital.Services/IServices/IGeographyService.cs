using Hospital.Domain.Models;

namespace Hostpital.Service.IServices
{
    public interface IGeographyService
    {
        List<Province> GetProvinces(int page, int rows);

        List<District> GetDistricts(int page, int rows);

        List<Ward> GetWards(int page, int rows);
    }
}
