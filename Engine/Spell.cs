using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Spell
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int EffectID { get; set; }
        public int EffectAmount { get; set; }
        public bool CombatSpell { get; set; }
//        public int CustomEffect { get; set; }
        public Spell (int id, string name, int manaCost, int effectID, int effectAmount,bool combatSpell)
        {
            ID = id;
            Name = name;
            ManaCost = manaCost;
            EffectID = effectID;
            EffectAmount = effectAmount;
            CombatSpell = combatSpell;
//            CustomEffect = customEffect;
        }
    }
}
