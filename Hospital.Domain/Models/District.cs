namespace Hospital.Domain.Models
{
    public class District
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public string ProvinceCode { get; set; }

        public Province Province { get; set; }

        public List<Ward> Wards { get; set; }

        public District()
        {
            Code = string.Empty;
            Name = string.Empty;
            LevelName = string.Empty;
            ProvinceCode = string.Empty;
        }

        public District(string code, string name, string levelName, string provinceCode)
        {
            Code = code;
            Name = name;
            LevelName = levelName;
            ProvinceCode = provinceCode;
        }
    }
}
