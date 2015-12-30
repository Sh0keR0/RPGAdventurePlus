using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SpellList
    {
        public Spell Details { get; set; }
  
        public SpellList (Spell details)
        {
            Details = details;
        }
    }
}
