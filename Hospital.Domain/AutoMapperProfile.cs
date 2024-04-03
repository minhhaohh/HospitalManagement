using AutoMapper;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;

namespace Hospital.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Province, ProvinceDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Ward, WardDto>();

            CreateMap<Patient, PatientDto>();
            CreateMap<Patient, PatientCreateDto>();
        }
    }
}
