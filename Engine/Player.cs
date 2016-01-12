using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
namespace Engine
{
   public class Player : LivingCreature
    {
       private int _gold, _experincePoints;
       public int Gold { get { return _gold; }
           set
           {
               _gold = value;
               OnPropertyChanged("Gold");
           }

       }
       public int ExperiencePoints { get { return _experincePoints; } 
           set
           {
               _experincePoints = value;
               OnPropertyChanged("ExperiencePoints");
           }
       }
     //  public int Level { get; set; }
       public BindingList<InventoryItem> Inventory { get; set; }
       public BindingList<PlayerQuest> Quests { get; set; }
       public Location CurrentLocation { get; set; }
       public Weapon CurrentWeapon { get; set; }
       public HealingPotion CurrentPotion { get; set; }
       public List<Weapon> Weapons
       {
           get { return Inventory.Where(x => x.Details is Weapon).Select(x => x.Details as Weapon).ToList(); }
       }
       public List<HealingPotion> Potions
       {
           get { return Inventory.Where(x => x.Details is HealingPotion).Select(x => x.Details as HealingPotion).ToList(); }
       }


       public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int level, int strength, int dexterity, int intelligent, int currentMana, int maximumMana,Race race, Armour armourUsed = null)
            : base(currentHitPoints, maximumHitPoints, level,strength, dexterity, intelligent, currentMana, maximumMana, race, armourUsed)
       {
           Gold = gold;
           ExperiencePoints = experiencePoints;
           Inventory = new BindingList<InventoryItem>();
           Quests = new BindingList<PlayerQuest>();
       }

       public static Player LoadPlayerInformationFromXml(string xmlPlayerData)
       {
           Player player;
           try
           {
               XmlDocument playerData = new XmlDocument();
               playerData.LoadXml(xmlPlayerData);

               int currentHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentHitPoints").InnerText);
               int maximumHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/MaximumHitPoints").InnerText);
               int gold = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Gold").InnerText);
               int experiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ExperiencePoints").InnerText);
               int level = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Level").InnerText);
               int strength = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Strength").InnerText);
               int dexterity = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Dexterity").InnerText);
               int intelligent = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Intelligent").InnerText);
               int currentMana = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentMana").InnerText);
               int maximumMana = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/MaximumMana").InnerText);
               Race race = World.RaceByID(Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Race").InnerText));
               Armour armourUsed = null;
               if (Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ArmourUsed").InnerText) != 0)
               {
                  armourUsed = (Armour)World.ItemByID(Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ArmourUsed").InnerText));
               }
             player = new Player(currentHitPoints,maximumHitPoints,gold,experiencePoints,level,strength,dexterity,intelligent,currentMana,maximumMana,race,armourUsed);
             player.CurrentLocation = World.LocationByID(Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText));
             if (Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText) != 0)
                 player.CurrentWeapon = (Weapon)World.ItemByID(Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText));
             else
                 player.CurrentWeapon = null;

             if (Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentPotion").InnerText) != 0)
                 player.CurrentPotion = (HealingPotion)World.ItemByID(Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentPotion").InnerText));
             else
                 player.CurrentPotion = null;
               foreach(XmlNode node in playerData.SelectNodes("/Player/InventoryItems/InventoryItem"))
               {
                  
                       player.AddItem(World.ItemByID(Convert.ToInt32(node.Attributes["ID"].Value)),Convert.ToInt32(node.Attributes["Quantity"].Value));
                   
               }

               foreach(XmlNode node in playerData.SelectNodes("/Player/PlayerQuests/PlayerQuest"))
               {
                   PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(Convert.ToInt32(node.Attributes["ID"].Value)));
                   playerQuest.IsCompleted = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);
                   player.Quests.Add(playerQuest);
               }
           }
           catch
           {
               player = new Player(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, World.RaceByID(World.RACE_ID_HUNMAN));
              
           }
           return player;
       }

       public string toXmlString()
       {
           XmlDocument playerData = new XmlDocument();
           XmlNode player = playerData.CreateElement("Player");
           playerData.AppendChild(player);
           XmlNode stats = playerData.CreateElement("Stats");
           player.AppendChild(stats);
           // current hitpoints
           XmlNode currentHitPoints = playerData.CreateElement("CurrentHitPoints");
           currentHitPoints.AppendChild(playerData.CreateTextNode(this.CurrentHitPoints.ToString()));
           stats.AppendChild(currentHitPoints);
           // max hitpoints
           XmlNode maximumHitPoints = playerData.CreateElement("MaximumHitPoints");
           maximumHitPoints.AppendChild(playerData.CreateTextNode(this.MaximumHitPoints.ToString()));
           stats.AppendChild(maximumHitPoints);
            // gold
           XmlNode gold = playerData.CreateElement("Gold");
           gold.AppendChild(playerData.CreateTextNode(this.Gold.ToString()));
           stats.AppendChild(gold);
           // Experince points
           XmlNode experincePoints = playerData.CreateElement("ExperiencePoints");
           experincePoints.AppendChild(playerData.CreateTextNode(this.ExperiencePoints.ToString()));
           stats.AppendChild(experincePoints);
          // current Location
           XmlNode currentLocation = playerData.CreateElement("CurrentLocation");
           currentLocation.AppendChild(playerData.CreateTextNode(this.CurrentLocation.ID.ToString()));
           stats.AppendChild(currentLocation);
           // level
           XmlNode level = playerData.CreateElement("Level");
           level.AppendChild(playerData.CreateTextNode(this.Level.ToString()));
           stats.AppendChild(level);
           //strength
           XmlNode strength = playerData.CreateElement("Strength");
           strength.AppendChild(playerData.CreateTextNode(this.Strength.ToString()));
           stats.AppendChild(strength);
           // dexterity 
           XmlNode dexterity = playerData.CreateElement("Dexterity");
           dexterity.AppendChild(playerData.CreateTextNode(this.Dexterity.ToString()));
           stats.AppendChild(dexterity);
           // Intelligent
           XmlNode intelligent = playerData.CreateElement("Intelligent");
           intelligent.AppendChild(playerData.CreateTextNode(this.Intelligent.ToString()));
           stats.AppendChild(intelligent);
           // current Mana
           XmlNode currentMana = playerData.CreateElement("CurrentMana");
           currentMana.AppendChild(playerData.CreateTextNode(this.CurrentMana.ToString()));
           stats.AppendChild(currentMana);
           // maximum mana
           XmlNode maximumMana = playerData.CreateElement("MaximumMana");
           maximumMana.AppendChild(playerData.CreateTextNode(this.MaximumMana.ToString()));
           stats.AppendChild(maximumMana);
           //race
           XmlNode race = playerData.CreateElement("Race");
           race.AppendChild(playerData.CreateTextNode(this.CreatureRace.ID.ToString()));
           stats.AppendChild(race);
           // Armourused
           XmlNode armourUsed = playerData.CreateElement("ArmourUsed");
           if (this.ArmourUsed != null)
               armourUsed.AppendChild(playerData.CreateTextNode(this.ArmourUsed.ID.ToString()));
           else
               armourUsed.AppendChild(playerData.CreateTextNode("0")); // 0 = null
           stats.AppendChild(armourUsed);
           
           // current weapon
           XmlNode currentWeapon = playerData.CreateElement("CurrentWeapon");
           if(this.CurrentWeapon != null)
                currentWeapon.AppendChild(playerData.CreateTextNode(this.CurrentWeapon.ID.ToString()));
           else
               currentWeapon.AppendChild(playerData.CreateTextNode("0"));
           stats.AppendChild(currentWeapon);
           // current Potion
           XmlNode currentPotion = playerData.CreateElement("CurrentPotion");
           if(this.CurrentPotion != null)
                 currentPotion.AppendChild(playerData.CreateTextNode(this.CurrentPotion.ID.ToString()));
           else
               currentPotion.AppendChild(playerData.CreateTextNode("0"));
           stats.AppendChild(currentPotion);
           //Inventory items
           XmlNode inventroyItems = playerData.CreateElement("InventoryItems");
           player.AppendChild(inventroyItems);

           foreach(InventoryItem item in this.Inventory)
           {
               XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

               XmlAttribute idAttribute = playerData.CreateAttribute("ID");
               idAttribute.Value = item.Details.ID.ToString();
               inventoryItem.Attributes.Append(idAttribute);

               XmlAttribute quantityAttribute = playerData.CreateAttribute("Quantity");
               quantityAttribute.Value = item.Quantity.ToString();
               inventoryItem.Attributes.Append(quantityAttribute);

               inventroyItems.AppendChild(inventoryItem);
           }

           // Quests
           XmlNode playerQuests = playerData.CreateElement("PlayerQuests");
           player.AppendChild(playerQuests);

           foreach(PlayerQuest quest in this.Quests)
           {
               XmlNode playerQuest = playerData.CreateElement("PlayerQuest");

               XmlAttribute idAttribute = playerData.CreateAttribute("ID");
               idAttribute.Value = quest.Details.ID.ToString();
               playerQuest.Attributes.Append(idAttribute);

               XmlAttribute isCompletedAttribute = playerData.CreateAttribute("IsCompleted");
               isCompletedAttribute.Value = quest.IsCompleted.ToString();
               playerQuest.Attributes.Append(isCompletedAttribute);

               playerQuests.AppendChild(playerQuest);
           }

           return playerData.InnerXml;
           
       }

       private void CallInventoryChangedEvent(Item item)
       {
           if (item != null)
           {
               if (item is Weapon)
                   OnPropertyChanged("Weapons");
               if (item is HealingPotion)
                   OnPropertyChanged("Potions");
           }
       }

       public void AddItem (Item item, int quantity) // add item to the player inventory
       {
           InventoryItem theitem = Inventory.SingleOrDefault(ii => ii.Details.ID == item.ID);
           if (theitem != null)
               theitem.Quantity += quantity;
           else
               this.Inventory.Add(new InventoryItem(item, quantity));
           CallInventoryChangedEvent(item);
       }




       public bool HasItem(Item item, int quantity) // check if the player has an item(s)
       {
           return Inventory.Any(ii => ii.Details.ID == item.ID);
       }
       public void Heal(int health) 
       {
           this.CurrentHitPoints += health;
           if (this.CurrentHitPoints > this.MaximumHitPoints)
               this.CurrentHitPoints = this.MaximumHitPoints;
           
       }
       public bool HasOngoingQuest(Quest quest)
       {
           return this.Quests.Any(ii => ii.Details.ID == quest.ID);
       }
       public bool IsQuestCompleted(Quest quest)
       {
           foreach(PlayerQuest playerQuest in this.Quests)
           {
               if (playerQuest.IsCompleted && playerQuest.Details.ID == quest.ID)
                   return true;
           }
           return false;
       }
       public void RemoveItem(InventoryItem item, int quantity)
       {
           item.Quantity -= quantity;
           if(item.Quantity <= 0)
           {
               Inventory.Remove(item);
               CallInventoryChangedEvent(item.Details);
           }
       }
  /*     public void RewardXP(int amount)
       {
           this.ExperiencePoints += amount;
           if((this.ExperiencePoints/10)>=this.Level)
           {
               this.ExperiencePoints -= Level * 10;
               this.Level++;
           }
       }*/
       public void LevelUp()
       {
           this.Level++;
          this.MaximumHitPoints += 10; 
           this.Heal(this.MaximumHitPoints);

       }
       public void RewardGold(int amount)
       {
           this.Gold += amount;
       }
    }
}
