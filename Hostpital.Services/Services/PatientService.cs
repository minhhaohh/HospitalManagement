using AutoMapper;
using Hospital.Domain.Consts;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Domain.Objects;
using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hostpital.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;

        private readonly HospitalContext _context;

        private readonly IMapper _mapper;

        public PatientService(ILogger<PatientService> logger, 
            HospitalContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> GetPatientsAsync()
        {
            return await _mapper.ProjectTo<PatientDto>(_context.Set<Patient>()).ToListAsync();
        }

        public async Task<JqGridResult<PatientDto>> GetPatientsForJqGridAsync(int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var result = _context.Set<Patient>().AsQueryable();

            int totalRecords = result.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            result = result.OrderBy(s => s.ChartNumber);
            result = result.Skip(pageIndex * pageSize).Take(pageSize);
            return new JqGridResult<PatientDto>(totalPages, page, totalRecords, _mapper.ProjectTo<PatientDto>(result).ToList());
        }

        public async Task<PatientDto> GetPatientByChartNumberAsync(string chartNumber)
        {
            var result = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.ChartNumber == chartNumber);
            return _mapper.Map<PatientDto>(result);
        }

        public async Task<string> GetNewChartNumberAsync()
        {
            var lastPatient = _context.Set<Patient>()
                .OrderBy(x => x.ChartNumber)
                .Take(1)
                .FirstOrDefault();
            var maxNumber = lastPatient == null ? 0 : int.Parse(lastPatient.ChartNumber.Replace(ChartNumber.Prefix, ""));
            return ChartNumber.Prefix + (maxNumber + 1).ToString($"D{ChartNumber.Lenght - ChartNumber.Prefix.Length}");
        }

        public async Task CreateAsync(PatientCreateDto patient)
        {
            _context.Set<Patient>().Add(
                new Patient()
                {
                    ChartNumber = patient.ChartNumber, 
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Gender = patient.Gender,
                    Dob = patient.Dob, 
                    Phone = patient.Phone,
                    Email = patient.Email, 
                    Address = patient.Address, 
                    WardCode = patient.WardCode,
                    DistrictCode = patient.DistrictCode,
                    ProvinceCode = patient.ProvinceCode, 
                    ZipCode = patient.ZipCode
                });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string chartNumber, PatientUpdateDto patient)
        {
            var existed = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.ChartNumber == chartNumber);

            if (existed == null)
                return;

            existed.FirstName = patient.FirstName;
            existed.LastName = patient.LastName;
            existed.Gender = patient.Gender;
            existed.Dob = patient.Dob;
            existed.Phone = patient.Phone;
            existed.Email = patient.Email;
            existed.Address = patient.Address;
            existed.WardCode = patient.WardCode;
            existed.DistrictCode = patient.DistrictCode;
            existed.ProvinceCode = patient.ProvinceCode;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
