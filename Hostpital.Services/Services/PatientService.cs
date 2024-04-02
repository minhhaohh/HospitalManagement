using AutoMapper;
using FluentValidation;
using Hospital.Domain.Consts;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Domain.Validations;
using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
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

        public List<PatientDto> GetPatients()
        {
            return _mapper.Map<List<PatientDto>>(_context.Set<Patient>().ToList());
        }

        public PatientDto GetPatientByChartNumber(string chartNumber)
        {
            var result = _context.Set<Patient>()
                .AsQueryable()
                .FirstOrDefault(x => x.ChartNumber == chartNumber);
            return _mapper.Map<PatientDto>(result);
        }

        public string GetNewChartNumber()
        {
            var lastPatient = _context.Set<Patient>()
                .AsQueryable()
                .OrderBy(x => x.ChartNumber)
                .Take(1)
                .FirstOrDefault();
            var maxNumber = lastPatient == null ? 0 : int.Parse(lastPatient.ChartNumber.Replace(ChartNumber.Prefix, ""));
            return ChartNumber.Prefix + (maxNumber + 1).ToString("D8");
        }

        public int GetMaxChartNumberPatient()
        {
            var a = _context.Set<Patient>();
            var result = _context.Set<Patient>()
                .AsQueryable()
                .OrderBy(x => x.ChartNumber)
                .Take(1)
                .FirstOrDefault();
            return result == null ? 0 : int.Parse(result.ChartNumber.Replace(ChartNumber.Prefix, ""));
        }

        public void Create(PatientCreateDto patient)
        {
            PatientValidator validator = new PatientValidator();

            validator.ValidateAndThrow(patient);
            _context.Set<Patient>().Add(
                new Patient(patient.ChartNumber, patient.FirstName, patient.LastName, patient.Gender,
                    patient.Dob, patient.Phone, patient.Email, patient.Address, patient.WardCode, 
                    patient.DistrictCode, patient.ProvinceCode, patient.ZipCode));
            _context.SaveChanges();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
