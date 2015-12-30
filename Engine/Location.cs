using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{

   public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToWest { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToEast { get; set; }
        public int LevelRequirement { get; set; }

       public Location(int id, string name, string description, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null, int levelRequirement = 1)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
            LevelRequirement = levelRequirement;
        }
       public bool HasRequirementToEnter() // if the location require an item to enter, return true
       {
           if (this.ItemRequiredToEnter != null)
               return true;
           else
               return false;
       }
       public bool HasQuestAvailable() // if the location has an available quest, return true
       {
           if (this.QuestAvailableHere != null)
               return true;
           else
               return false;
       }
       public bool HasLivingMonster()  // check if a monster is in the location
       {
           if (this.MonsterLivingHere != null)
               return true;
           else
               return false;
       }
       
    }
   
}
