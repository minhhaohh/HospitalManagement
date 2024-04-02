using Hospital.Domain.DTO;

namespace Hospital.Web.Models
{
    public class PatientViewModel
    {
        public List<WardDto> Wards {  get; set; }
        
        public List<DistrictDto> Districts { get; set; }

        public List<ProvinceDto> Provinces { get; set; }

        public PatientViewModel() 
        {
            Wards = new List<WardDto>();
            Districts = new List<DistrictDto>();
            Provinces = new List<ProvinceDto>();
        }
    }
}
