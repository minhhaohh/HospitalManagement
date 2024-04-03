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
        }
    }
}
