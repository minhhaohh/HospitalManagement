using Hospital.Domain.DTO;
using Hospital.Domain.Objects;

namespace Hostpital.Service.IServices
{
    public interface IGeographyService
    {
        Task<List<ProvinceDto>> GetProvincesAsync();

        Task<List<DistrictDto>> GetDistrictsAsync();

        Task<List<WardDto>> GetWardsAsync();

        Task<IPagedList<ProvinceDto>> SearchProvincesAsync(PagingParams paging);

        Task<IPagedList<DistrictDto>> SearchDistrictsAsync(PagingParams paging);

        Task<IPagedList<WardDto>> SearchWardsAsync(PagingParams paging);
    }
}
