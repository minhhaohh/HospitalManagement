using Hospital.Domain.Models;
using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
using Microsoft.Extensions.Logging;

namespace Hostpital.Service.Services
{
    public class GeographyService : IGeographyService
    {
        private readonly ILogger<GeographyService> _logger;

        private readonly PatientManagementContext _context;

        public GeographyService(ILogger<GeographyService> logger, PatientManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Province> GetProvinces(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<Province>().AsQueryable();
            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return result.ToList();
        }

        public List<District> GetDistricts(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<District>().AsQueryable();
            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return result.ToList();
        }

        public List<Ward> GetWards(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<Ward>().AsQueryable();
            result = result.OrderBy(s => s.Code);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return result.ToList();
        }
    }
}
