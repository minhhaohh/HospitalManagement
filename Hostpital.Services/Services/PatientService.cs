using AutoMapper;
using Hospital.Domain.DTO;
using Hospital.Domain.Extensions;
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

        private readonly IMapper _mapper;

        private readonly HospitalContext _context;

        public PatientService(ILogger<PatientService> logger, 
            IMapper mapper,
            HospitalContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
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

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var result = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PatientDto>(result);
        }

        public async Task<string> CreateAsync(PatientCreateDto patient)
        {
            try
            {
                _context.Set<Patient>().Add(_mapper.Map<Patient>(patient));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
            return string.Empty;
        }

        public async Task<string> UpdateAsync(int id, PatientUpdateDto patient)
        {
            try
            {
                var existed = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.Id == id);

                if (existed == null)
                    return $"Not found Patient with Id {id}!!!";

                // Map the properties from patient to existed
                _mapper.Map(patient, existed);

                // Mark the entity as modified
                _context.Entry(existed).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return ex.Message;
            }
            return string.Empty;
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var existed = await _context.Set<Patient>()
                .FirstOrDefaultAsync(x => x.Id == id);

                if (existed == null)
                    return $"Not found Patient with Id {id}!!!";

                // Remove the patient entity from the context
                _context.Set<Patient>().Remove(existed);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return ex.Message;
            }

            return string.Empty;
        }
    }
}
