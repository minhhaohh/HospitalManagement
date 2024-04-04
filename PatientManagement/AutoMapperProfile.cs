using AutoMapper;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Web.Models;

namespace Hospital.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Province, ProvinceDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Ward, WardDto>();

            CreateMap<Patient, PatientDto>();
            CreateMap<PatientCreateDto, Patient>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ChartNumber, opt => opt.Ignore());
            CreateMap<PatientUpdateDto, Patient>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ChartNumber, opt => opt.Ignore());
            CreateMap<PatientDto, PatientViewModel>();
        }
    }
}
