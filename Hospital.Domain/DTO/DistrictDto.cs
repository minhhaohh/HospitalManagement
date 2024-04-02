using Hospital.Domain.Models;

namespace Hospital.Domain.DTO
{
    public class DistrictDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public string ProvinceCode { get; set; }

        public Province Province { get; set; }

        public List<Ward> Wards { get; set; }
    }
}
