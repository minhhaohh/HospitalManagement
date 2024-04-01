namespace PatientManagement.Models
{
    public class ModelGeography
    {
        public string Type { get; set; }

        public List<Province> Provinces { get; set; }

        public List<District> Districts { get; set; }

        public List<Ward> Wards { get; set; }
    }
}
