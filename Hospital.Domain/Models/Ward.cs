namespace Hospital.Domain.Models
{
    public class Ward
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public string DistrictCode { get; set; }

        public District District { get; set; }

        public Ward()
        {
        }
    }
}
