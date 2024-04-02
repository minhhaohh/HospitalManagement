using AutoMapper;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;

namespace Hospital.Domain
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Province, ProvinceDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Ward, WardDto>();

            CreateMap<Patient, PatientDto>();
            CreateMap<Patient, PatientCreateDto>();
        }
    }
}
