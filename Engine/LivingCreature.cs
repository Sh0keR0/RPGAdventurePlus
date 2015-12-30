using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
   public class LivingCreature
    {
       public int CurrentHitPoints { get; set; }
       public int MaximumHitPoints { get; set; }
       public int Level { get; set; }
       public int Strength { get; set; }
       public int Dexterity { get; set; }
       public int Intelligent { get; set; }
       public int CurrentMana { get; set; }
       public int MaximumMana { get; set; }
       public Armour ArmourUsed { get; set; }
       public List<SpellList> Spells { get; set; }
       public List<BuffsList> Buffs { get; set; }
       public int TemporaryMaximumMana { get; set; }
       public Race CreatureRace { get; set; }
       public LivingCreature(int currentHitPoints, int maximumHitPoints, int level, int strength, int dexterity, int intelligent, int currentMana, int maximumMana, Race race, Armour armourUsed = null)
       {
           CurrentHitPoints = currentHitPoints;
           MaximumHitPoints = maximumHitPoints;
           Level = level;
           Strength = strength;
           Dexterity = dexterity;
           Intelligent = intelligent;
           CurrentMana = currentMana;
           MaximumMana = maximumMana;
           ArmourUsed = armourUsed;
           Spells = new List<SpellList>();
           Buffs = new List<BuffsList>();
           TemporaryMaximumMana = 0;
           CreatureRace = race;
       }
       public void DealDamage(int amount)
       {
           this.CurrentHitPoints -= amount;
       }
       public bool HasSpell(Spell spell)
       {
           foreach(SpellList sp in Spells)
           {
               if (sp.Details.ID == spell.ID)
                   return true;
           }
           return false;
       }
       public void AddSpell(Spell spell)
       {
          if(!this.HasSpell(spell))
           this.Spells.Add(new SpellList(spell));
       }
       public void DrainMana(int amount)
       {
           this.CurrentMana -= amount;

       }
       public void RestoreManaToFull()
       {
           this.CurrentMana = this.MaximumMana;
       }
       public void RestoreMana(int amount)
       {
           if ((this.CurrentMana + amount) > this.MaximumMana)
               this.CurrentMana = this.MaximumMana;
           else
               this.CurrentMana += amount;
       }
       public void RemoveBuff(Buff buff)
       {
           if(this.Buffs.Count != 0)
           foreach(BuffsList bl in this.Buffs)
           {
               if (buff.ID == bl.Details.ID)
               {
                   this.Buffs.Remove(bl);
                   break;
               }
           }
       }
       public void RemoveAllBuffs()
       {
           if (this.Buffs.Count != 0)
           {
               foreach (BuffsList bl in this.Buffs)
               {
                   this.RemoveBuff(bl.Details);
               }
           }
       }
       
    }
}
