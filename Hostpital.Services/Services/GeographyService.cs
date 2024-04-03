using AutoMapper;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Domain.Objects;
using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hostpital.Service.Services
{
    public class GeographyService : IGeographyService
    {
        private readonly ILogger<GeographyService> _logger;

        private readonly HospitalContext _context;

        private readonly IMapper _mapper;

        public GeographyService(ILogger<GeographyService> logger, 
            HospitalContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProvinceDto>> GetProvincesAsync()
        {
            return await _mapper.ProjectTo<ProvinceDto>(_context.Set<Province>()).ToListAsync();
        }

        public async Task<List<DistrictDto>> GetDistrictsAsync()
        {
            return await _mapper.ProjectTo<DistrictDto>(_context.Set<District>()).ToListAsync();
        }

        public async Task<List<WardDto>> GetWardsAsync()
        {
            return await _mapper.ProjectTo<WardDto>(_context.Set<Ward>()).ToListAsync();
        }

        public async Task<IPagedList<ProvinceDto>> SearchProvincesAsync(PagingParams paging)
        {
            try
            {
                var data = _context.Set<Province>().AsQueryable();

                int totalRecords = data.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)paging.PageSize);

                data = data.OrderBy(s => s.Code);
                data = data.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize);

                return new PagedList<ProvinceDto>(
                    _mapper.ProjectTo<ProvinceDto>(data).ToList(),
                    paging.PageIndex,
                    paging.PageSize,
                    totalRecords
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<IPagedList<DistrictDto>> SearchDistrictsAsync(PagingParams paging)
        {
            try
            {
                var data = _context.Set<District>().AsQueryable();

                int totalRecords = data.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)paging.PageSize);

                data = data.OrderBy(s => s.Code);
                data = data.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize);

                return new PagedList<DistrictDto>(
                    _mapper.ProjectTo<DistrictDto>(data).ToList(),
                    paging.PageIndex,
                    paging.PageSize,
                    totalRecords
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<IPagedList<WardDto>> SearchWardsAsync(PagingParams paging)
        {
            try
            {
                var data = _context.Set<Ward>().AsQueryable();

                int totalRecords = data.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)paging.PageSize);

                data = data.OrderBy(s => s.Code);
                data = data.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize);

                return new PagedList<WardDto>(
                    _mapper.ProjectTo<WardDto>(data).ToList(),
                    paging.PageIndex,
                    paging.PageSize,
                    totalRecords
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
