using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
   public class Armour : Item
    {
       public int Resistance { get; set; }
       public int MagicResistance { get; set; }

       public Armour(int id, string name, string namePlural,int resistance, int magicResistance) :base(id, name, namePlural)
       {
           Resistance = resistance;
           MagicResistance = magicResistance;
       }
    }
    
}
