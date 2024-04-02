using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.DTO
{
    public class ProvinceDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }

        public List<District> Districts { get; set; }
    }
}
