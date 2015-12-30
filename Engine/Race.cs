using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Race
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Race(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
    }
}
