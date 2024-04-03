using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IGeographyService
    {
        Task<JqGridResult<ProvinceDto>> GetProvincesForJqGridAsync(int page, int rows);

        Task<JqGridResult<DistrictDto>> GetDistrictsForJqGridAsync(int page, int rows);

        Task<JqGridResult<WardDto>> GetWardsForJqGridAsync(int page, int rows);
    }
}
