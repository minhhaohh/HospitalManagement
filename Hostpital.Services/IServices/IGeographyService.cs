using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IGeographyService
    {
        JqGridResult<ProvinceDto> GetProvinces(int page, int rows);

        JqGridResult<DistrictDto> GetDistricts(int page, int rows);

        JqGridResult<WardDto> GetWards(int page, int rows);
    }
}
