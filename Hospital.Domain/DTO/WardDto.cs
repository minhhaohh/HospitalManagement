using Hospital.Domain.Models;

namespace Hospital.Domain.DTO
{
    public class WardDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public string DistrictCode { get; set; }

        public District District { get; set; }
    }
}
