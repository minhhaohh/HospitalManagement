namespace Hospital.Domain.Models
{
    public class Province
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public List<District> Districts { get; set; }

        public Province()
        {
            Code = string.Empty;
            Name = string.Empty;
            LevelName = string.Empty;
            Districts = new List<District>();
        }

        public Province(string code, string name, string levelName)
        {
            Code = code;
            Name = name;
            LevelName = levelName;
            Districts = new List<District>();
        }
    }
}
