using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterPlant.Models
{
    public class Plant
    {
        public int id { get; set; }
        public string plant_name { get; set; }
        public int status { get; set; }
        public string last_watered { get; set; }
    }
}
