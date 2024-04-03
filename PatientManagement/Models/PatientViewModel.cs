using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Models
{
    public class PatientViewModel
    {
        public string ChartNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; } = DateTime.Now;

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string WardCode { get; set; }

        public string DistrictCode { get; set; }

        public string ProvinceCode { get; set; }

        public string ZipCode { get; set; }

        public List<SelectListItem> Wards { get; set; }

        public List<SelectListItem> Districts { get; set; }

        public List<SelectListItem> Provinces { get; set; }

        public PatientViewModel()
        {
        }
    }
}
