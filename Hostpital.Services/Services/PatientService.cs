using AutoMapper;
using Azure;
using Hospital.Domain.Consts;
using Hospital.Domain.DTO;
using Hospital.Domain.Extensions;
using Hospital.Domain.Models;
using Hospital.Domain.Objects;
using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            return await _mapper.ProjectTo<PatientDto>(_context.Set<Patient>()).ToListAsync();
        }

        public async Task<IPagedList<PatientDto>> SearchPatientsAsync(PatientQuery condition, PagingParams paging)
        {
            try
            {
                var patients = _context.Set<Patient>()
                    .WhereIf(condition.ChartNumber.HasValue(), x => x.ChartNumber == condition.ChartNumber)
                    .WhereIf(condition.FirstName.HasValue(), x => x.FirstName.Contains(condition.FirstName))
                    .WhereIf(condition.LastName.HasValue(), x => x.LastName.Contains(condition.LastName))
                    .WhereIf(condition.Gender.HasValue(), x => x.ChartNumber == condition.Gender)
                    .WhereIf(condition.Dob != null, x => x.Dob == condition.Dob)
                    .WhereIf(condition.Phone.HasValue(), x => x.Phone.Contains(condition.Phone))
                    .WhereIf(condition.Email.HasValue(), x => x.Email.Contains(condition.Email))
                    .WhereIf(condition.Address.HasValue(), x => x.Address.Contains(condition.Address))
                    .WhereIf(condition.WardCode.HasValue(), x => x.WardCode == condition.WardCode)
                    .WhereIf(condition.DistrictCode.HasValue(), x => x.DistrictCode == condition.DistrictCode)
                    .WhereIf(condition.ProvinceCode.HasValue(), x => x.ProvinceCode == condition.ProvinceCode)
                    .WhereIf(condition.ZipCode.HasValue(), x => x.ZipCode == condition.ZipCode);

                int totalRecords = patients.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)paging.PageSize);

                patients = patients.OrderBy(s => s.ChartNumber);
                patients = patients.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize);

                return new PagedList<PatientDto>(
                    _mapper.ProjectTo<PatientDto>(patients).ToList(),
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

        public async Task<PatientDto> GetPatientByChartNumberAsync(string chartNumber)
        {
            var result = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.ChartNumber == chartNumber);
            return _mapper.Map<PatientDto>(result);
        }

        public async Task<string> GetNewChartNumberAsync()
        {
            var lastPatient = _context.Set<Patient>()
                .OrderByDescending(x => x.ChartNumber)
                .FirstOrDefault();
            var maxNumber = lastPatient == null ? 0 : int.Parse(lastPatient.ChartNumber.Replace(ChartNumber.Prefix, ""));
            return ChartNumber.Prefix + (maxNumber + 1).ToString($"D{ChartNumber.Lenght - ChartNumber.Prefix.Length}");
        }

        public async Task CreateAsync(PatientCreateDto patient)
        {
            try
            {
                _context.Set<Patient>().Add(_mapper.Map<Patient>(patient));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(string chartNumber, PatientUpdateDto patient)
        {
            try
            {
                var existed = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.ChartNumber == chartNumber);

                if (existed == null)
                    return false;

                existed = _mapper.Map<Patient>(patient);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return false;
            }
            return true;
        }

        public async Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
