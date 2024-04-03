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

        public async Task<JqGridResult<ProvinceDto>> GetProvincesForJqGridAsync(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<Province>().AsQueryable();

            int totalRecords = result.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return new JqGridResult<ProvinceDto>(totalPages, page, totalRecords, _mapper.ProjectTo<ProvinceDto>(result).ToList());
        }

        public async Task<JqGridResult<DistrictDto>> GetDistrictsForJqGridAsync(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<District>().AsQueryable();

            int totalRecords = result.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return new JqGridResult<DistrictDto>(totalPages, page, totalRecords, _mapper.ProjectTo<DistrictDto>(result).ToList());
        }

        public async Task<JqGridResult<WardDto>> GetWardsForJqGridAsync(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<Ward>().AsQueryable();

            int totalRecords = result.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return new JqGridResult<WardDto>(totalPages, page, totalRecords, _mapper.ProjectTo<WardDto>(result).ToList());
        }
    }
}
