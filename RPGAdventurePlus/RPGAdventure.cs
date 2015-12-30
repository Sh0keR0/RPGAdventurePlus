using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Xml;
using System.IO;

namespace RPGAdventurePlus
{

    public partial class FormAdventurePlus : Form
    {
        public Player _player;
        private Monster _currentMonster;
        private bool InCombat = false;
        private string saveFileName;

        public FormAdventurePlus(Player newPlayer, string SaveFileName)
        {
            InitializeComponent();
            saveFileName = SaveFileName;
            lblCurrentMana.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            _player = newPlayer;

            UpdateUI();
            MoveTo(_player.CurrentLocation);


        }
        private void MoveTo(Location newLocation) // move the player to new location
        {
            bool AllowPlayerToMove = true;
            if (newLocation.HasRequirementToEnter() && !_player.HasItem(newLocation.ItemRequiredToEnter, 1))
            {
                ShowMessage("You need " + newLocation.ItemRequiredToEnter.Name + " in order to enter this area");
                AllowPlayerToMove = false;
            }
            if(newLocation.LevelRequirement >_player.Level)
            {
                ShowMessage("You need to be at least level " + newLocation.LevelRequirement.ToString() + " to enter this location.");
                AllowPlayerToMove = false;
            }
            if(AllowPlayerToMove == true)
            {
                _player.CurrentLocation = newLocation; 
               // _player.UpdateMaximumStats();
                _player.Heal(_player.MaximumHitPoints);
                _player.RestoreManaToFull();
               
                InCombat = false;
              

                if (newLocation.HasQuestAvailable()) // if there's a quest available here
                {
                    if (!_player.HasOngoingQuest(newLocation.QuestAvailableHere) && !_player.IsQuestCompleted(newLocation.QuestAvailableHere))
                    {
                        StartQuest(newLocation.QuestAvailableHere);
                    }
                    else if(!_player.IsQuestCompleted(newLocation.QuestAvailableHere))
                    {
                        bool HaveQuestItems = true;
                        foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems) // check if the player has all the required items
                        {
                            if (!_player.HasItem(qci.Details, qci.Quantity))
                            {
                                HaveQuestItems = false;
                                // _player.RemoveItem(GetInventoryItem(qci.Details), qci.Quantity);

                                // CompleteQuest(GetPlayerQuest(newLocation.QuestAvailableHere));
                                /* foreach (InventoryItem ii in _player.Inventory)
                                 {
                                     if (ii.Details.ID == qci.Details.ID)
                                     {
                                         _player.RemoveItem(ii, 1);
                                     }
                                 }*/
                            }

                        }
                        if (HaveQuestItems == true) // if the player has all the item, remove them and then complete the quest
                        {
                            foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                            {
                                _player.RemoveItem(GetInventoryItem(qci.Details), qci.Quantity);
                            }
                            CompleteQuest(GetPlayerQuest(newLocation.QuestAvailableHere));
                        }

                    }
                }

                if(newLocation.HasLivingMonster())
                {
                    BeginBattle(newLocation.MonsterLivingHere);
                }
                UpdateUI();
            }

        }
        private void ShowMessage(string message)
        {
            rtbMessages.Text += message + Environment.NewLine;
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
        public void UpdateUI(bool statsOnly = false)
        {
            if(statsOnly == false)
            { 
                btnNorth.Visible = (_player.CurrentLocation.LocationToNorth != null);
                btnEast.Visible = (_player.CurrentLocation.LocationToEast != null);
                btnSouth.Visible = (_player.CurrentLocation.LocationToSouth != null);
                btnWest.Visible = (_player.CurrentLocation.LocationToWest != null);
                rtbLocation.Text = _player.CurrentLocation.Name + Environment.NewLine;
                rtbLocation.Text += _player.CurrentLocation.Description + Environment.NewLine;
                RefreshPlayerInventoryList();
                RefreshPlayerPotionsList();
                RefreshPlayerQuestList();
                RefreshPlayerWeaponList();
                RefreshPlayerSpellList();
            }
            lblHitPoints.Font = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
            //lblHitPoints.ForeColor = Color.Red;
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblCurrentMana.Text = _player.CurrentMana.ToString();
            rtbLocation.Text = _player.CurrentLocation.Name + Environment.NewLine;
            rtbLocation.Text += _player.CurrentLocation.Description + Environment.NewLine;
          /*  if (InCombat == false)
                HideCombatUI();
            else
                ShowCombatUI();*/
        }
        private void RewardXP(int amount)
        {
            _player.ExperiencePoints += amount;
            if ((_player.ExperiencePoints / 10) >= _player.Level)
            {
                _player.ExperiencePoints -= _player.Level * 10;
                _player.LevelUp();
                ShowMessage("Congratulation! you leveled up!");
                ShowMessage("You are now level " + _player.Level.ToString());

            }
        }
        private void CompleteQuest(PlayerQuest quest)
        {
            ShowMessage("Quest completed! You receive:");
            ShowMessage(quest.Details.RewardExperiencePoints.ToString() + " EXP");
            ShowMessage(quest.Details.RewardGold.ToString() + " Gold");
            if (quest.Details.RewardItem != null)
            {
                ShowMessage(quest.Details.RewardItem.Name);
                _player.AddItem(quest.Details.RewardItem, 1);
            }
            RewardXP(quest.Details.RewardExperiencePoints);
            _player.RewardGold(quest.Details.RewardGold);
            quest.IsCompleted = true;
            UpdateUI();
        }
        private PlayerQuest GetPlayerQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in _player.Quests)
            {
                if (playerQuest.Details == quest)
                    return playerQuest;
            }
            return null;
        }
        private InventoryItem GetInventoryItem(Item item)
        {
            foreach(InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details == item)
                    return inventoryItem;
            }
            return null;
        }
        private QuestCompletionItem GetQuestCompletionItem (Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                if(qci.Details.ID == quest.ID)
                {
                    return qci;
                }
            }
            return null;
        }
        private void StartQuest(Quest quest)
        {
            ShowMessage("Quest started! " + quest.Name);
            ShowMessage(quest.Description);
            ShowMessage("Requirements: ");
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                if(qci.Quantity == 1)
                {
                    ShowMessage(qci.Quantity.ToString() + " " + qci.Details.Name);
                }else
                {
                    ShowMessage(qci.Quantity.ToString() + " " + qci.Details.NamePlural);
                }
            }
            ShowMessage("");
            _player.Quests.Add(new PlayerQuest(quest));
            RefreshPlayerQuestList();
        }
        private void ShowCombatUI()
        {
            if(cboWeapons.SelectedText != null)
            {

            }
            cboWeapons.Visible = true;
            cboPotions.Visible = true;
            btnUsePotion.Visible = true;
            btnUseWeapon.Visible = true;
        }
        private void HideCombatUI()
        {
            cboWeapons.Visible = false;
            cboPotions.Visible = false;
            btnUsePotion.Visible = false;
            btnUseWeapon.Visible = false;
        }
        private void BeginBattle(Monster monster, bool boss = false) // WIP
        {
            ShowMessage("You see " + monster.Name);
            Monster StandardMonster = World.MonsterByID(monster.ID);


                _currentMonster = new Monster(StandardMonster.ID, StandardMonster.Name, StandardMonster.MaximumDamage, StandardMonster.RewardExperiencePoints, StandardMonster.RewardGold, StandardMonster.CurrentHitPoints, StandardMonster.MaximumHitPoints, StandardMonster.Level,StandardMonster.Strength,StandardMonster.Dexterity,StandardMonster.Intelligent,StandardMonster.CurrentMana,StandardMonster.MaximumMana,StandardMonster.CreatureRace,StandardMonster.ArmourUsed);
                _currentMonster.Spells = StandardMonster.Spells;
            if (boss == true) // if this is a boss, double the hp
             {
                 _currentMonster.MaximumDamage *= 2;
                 _currentMonster.MaximumHitPoints *= 2;
                 _currentMonster.CurrentHitPoints = _currentMonster.MaximumHitPoints;
             }
            foreach (LootItem lootItem in StandardMonster.LootTable)
            {
                _currentMonster.LootTable.Add(lootItem);
            }

            InCombat = true;
            UpdateUI();

        }
        private void RefreshPlayerSpellList()
        {
            
            List<Spell> spells = new List<Spell>();
            foreach(SpellList sp in _player.Spells)
            {
                spells.Add(sp.Details);
            }

            if (spells.Count == 0)
            {
                cboSpells.Visible = false;
                btnUseSpell.Visible = false;
            }
            else
            {

                cboSpells.DataSource = spells;

                cboSpells.DisplayMember = "Name";
                cboSpells.ValueMember = "ID";
                cboSpells.Visible = true;
                btnUseSpell.Visible = true;
                cboSpells.SelectedIndex = 0;
               
            }
        }
        private void RefreshPlayerInventoryList()
        {
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();
            foreach(InventoryItem invetoryItem in _player.Inventory)
            {
                if (invetoryItem.Quantity > 0)
                    dgvInventory.Rows.Add(new[] { invetoryItem.Details.Name, invetoryItem.Quantity.ToString() });
            }
        }
        private void RefreshPlayerQuestList()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Completed?";
            dgvQuests.Rows.Clear();

            foreach(PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }
        private void RefreshPlayerWeaponList()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach(InventoryItem inventoryItem in _player.Inventory)
            {
                if(inventoryItem.Details is Weapon)
                {
                    if(inventoryItem.Quantity > 0)
                        weapons.Add((Weapon)inventoryItem.Details);
                }
            }
            if (weapons.Count == 0 || InCombat == false)
            {
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {

                cboWeapons.SelectedIndexChanged -= cboWeapons_SelectedIndexChanged;
                cboWeapons.DataSource = weapons;
                cboWeapons.SelectedIndexChanged += cboWeapons_SelectedIndexChanged;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";
                cboWeapons.Visible = true;
                btnUseWeapon.Visible = true;
                if (_player.CurrentWeapon != null)
                {
                    cboWeapons.SelectedItem = _player.CurrentWeapon;
                }
                else
                {
                    cboWeapons.SelectedIndex = 0;
                }
            }
               
            
        }
        private void RefreshPlayerPotionsList()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();
            foreach (InventoryItem invetoryItem in _player.Inventory)
            {
                if(invetoryItem.Details is HealingPotion && invetoryItem.Quantity > 0)
                {
                    healingPotions.Add((HealingPotion)invetoryItem.Details);
                }
            }
            if(healingPotions.Count == 0 || InCombat == false)
            {
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                btnUsePotion.Visible = true;
                cboPotions.Visible = true;
                cboPotions.SelectedIndexChanged -= cboPotions_SelectedIndexChanged;
                    cboPotions.DataSource = healingPotions;
                    cboPotions.SelectedIndexChanged += cboPotions_SelectedIndexChanged;
                    cboPotions.DisplayMember = "Name";
                    cboPotions.ValueMember = "ID";
                    if (_player.CurrentPotion != null)
                    {
                        cboPotions.SelectedItem = _player.CurrentPotion;
                    }
                    else
                    {
                        cboPotions.SelectedIndex = 0;
                    }
                
            }

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnCompleteQuest_Click(object sender, EventArgs e)
        {
            _player.AddItem(World.ItemByID(World.ITEM_ID_SNAKE_FANG), 10);
            _player.AddItem(World.ItemByID(World.ITEM_ID_HEALING_POTION), 3);
            _player.AddItem((Item)World.ItemByID(World.ITEM_ID_LEGENDARY_SWORD),1);
            _player.AddItem(World.ItemByID(World.ITEM_ID_DEATHCLAW_HAND), 1);
            _player.AddSpell(World.SpellByID(World.SPELL_ID_FIREBALL));
            _player.AddItem(World.ItemByID(World.ITEM_ID_ADVENTURER_PASS),1);
            _player.AddSpell(World.SpellByID(World.SPELL_ID_RESTORE_HEALTH));
            _player.AddSpell(World.SpellByID(World.SPELL_ID_RESTORE_MANA));
            _player.AddSpell(World.SpellByID(World.SPELL_ID_ENDURANCE));
            _player.MaximumMana = 100;
            _player.RestoreManaToFull();
            UpdateUI();
          //  InventoryScreen inventoryScreen = new InventoryScreen(_player);
           // inventoryScreen.Show();
        }
        private void EndCombat(Monster monster)
        {
            // End combat, reward XP, gold and items if any
            InCombat = false;
            
            ShowMessage("You received: " + monster.RewardExperiencePoints.ToString() + " EXP");
            _player.RewardGold(monster.RewardGold);
            ShowMessage("You received: " + monster.RewardGold.ToString() + " gold!");
            List<InventoryItem> lootedItems = new List<InventoryItem>();
            foreach(LootItem lootItem in _currentMonster.LootTable)
            {
                if(RandomNumber.Between(1,100) <= lootItem.DropPercentage)
                {
                    lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                }
            }
            if(lootedItems.Count != 0)
            {
                foreach(InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItem(inventoryItem.Details, inventoryItem.Quantity);
                    ShowMessage("You received: " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name);
                }
            }
            RewardXP(monster.RewardExperiencePoints);
            _player.RemoveAllBuffs();
        //    _player.UpdateMaximumStats();
            UpdateUI();
        }
        private void CombatPhase (int Action) // 1 - attack, 2 - healing potion
        {
            if (InCombat == true)
            {
                int damageDealt;
                if (Action == 1) // attack
                {
                    if (RandomNumber.Between(_player.Level, _player.Level + 5) - (_currentMonster.Level) <= 0)
                    {
                        ShowMessage("You missed your attack!");
                    }
                    else
                    {
                        Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;
                        damageDealt = RandomNumber.Between(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);
                        damageDealt += _player.Level - _currentMonster.Level;
                        if(_currentMonster.ArmourUsed != null)
                        {
                         //   damageDealt = (damageDealt + (_player.Level - _currentMonster.Level)) - (_currentMonster.ArmourUsed.Resistance / 2);
                            damageDealt -= _currentMonster.ArmourUsed.Resistance / 2;
                        }
                        _currentMonster.DealDamage(damageDealt);
                        ShowMessage("You dealt " + damageDealt.ToString() + " points of damage to " + _currentMonster.Name);
                    }
                }else if(Action == 2) // use potion
                {
                    HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;
                    _player.Heal(potion.AmountToHeal);
                    _player.RemoveItem(GetInventoryItem((Item)potion), 1);
                    ShowMessage("You used " + potion.Name + " to restore " + potion.AmountToHeal.ToString() + " points of health");
                    UpdateUI();
                }else if(Action == 3) // use magic spell
                {
                    Spell spell = (Spell)cboSpells.SelectedItem;
                    if(_player.CurrentMana >= spell.ManaCost)
                    {
                        if (spell.EffectID == World.SPELL_EFFECT_ID_DAMAGE)
                        {
                           
                            damageDealt = spell.EffectAmount;
                            damageDealt += _player.Level - _currentMonster.Level;
                            if (_currentMonster.ArmourUsed != null)
                            {
                                damageDealt -= _currentMonster.ArmourUsed.MagicResistance / 2;
                            }
                            _currentMonster.DealDamage(damageDealt);
                            ShowMessage("You dealt " + damageDealt.ToString() + " points of magic damage to the target");
                        }else if(spell.EffectID == World.SPELL_EFFECT_ID_HEAL)
                        {
                            _player.Heal(spell.EffectAmount);
                            ShowMessage("You healed yourself for " + spell.EffectAmount.ToString() + " hit points");
                        }else if(spell.EffectID == World.SPELL_EFFECT_ID_RESTORE_MANA)
                        {
                            _player.RestoreMana(spell.EffectAmount);
                            ShowMessage("Your restored " + spell.EffectAmount.ToString() + " mana!");
                        }
                        else
                        {
                            ShowMessage("Your spell didn't do anything!");
                        }
                        _player.DrainMana(spell.ManaCost);
                    }
                }

                if (_currentMonster.CurrentHitPoints <= 0)
                 {
                        ShowMessage("");
                        ShowMessage("You defeated " + _currentMonster.Name + ".");
                        EndCombat(_currentMonster);
                        _player.Heal(_player.MaximumHitPoints);
                        MoveTo(_player.CurrentLocation);
                }                
                else
                {
                    if (RandomNumber.Between(_currentMonster.Level, _currentMonster.Level + 5) -( _player.Level) <= 0)
                    {
                        ShowMessage("You managed to dodge the enemey attack!");
                    }
                    else
                    {

                        
                        bool magicAttack = false;
                        Spell spellToCast = null;
                      if(_currentMonster.Spells.Count != 0)
                       {
                          
                            int rndNumber = RandomNumber.Between(1, 3);
                            int highestDamageSpell = 0;
                            if(rndNumber > 1 || _currentMonster.Intelligent > _currentMonster.Strength) // 66% chance to use a spell attack instead of normal attack or if t
                                // if the monster int is higher, he will magic attack
                            {
                                foreach(SpellList sp in _currentMonster.Spells)
                                {
                                    if(sp.Details.CombatSpell = true && _currentMonster.CurrentMana >= sp.Details.ManaCost)
                                    {
                                        if (sp.Details.EffectAmount > highestDamageSpell) // if the spell is stronger, use it
                                        {
                                            spellToCast = sp.Details;
                                            highestDamageSpell = sp.Details.EffectAmount;
                                            magicAttack = true;
                                        }
                                        
                                    }
                                }


                         }
                        }
                        if (magicAttack == true)
                        {
                            damageDealt = spellToCast.EffectAmount;
                            damageDealt += _currentMonster.Level - _player.Level; // add one point of damage for each level difference 
                            _currentMonster.DrainMana(spellToCast.ManaCost);
                            if(_player.ArmourUsed != null)
                            {
                                damageDealt -= _player.ArmourUsed.MagicResistance / 2;
                            }
                            ShowMessage(_currentMonster.Name + " hit you for " + damageDealt.ToString() + " points of magic damage");

                        }
                        else
                        {
                            damageDealt = RandomNumber.Between(1, _currentMonster.MaximumDamage);
                            damageDealt += _currentMonster.Level - _player.Level;
                            ShowMessage(_currentMonster.Name + " hit you for " + damageDealt.ToString() + " points of damage");
                        }
                        _player.DealDamage(damageDealt);
                    }
                    if (_player.CurrentHitPoints <= 0)
                    {
                        InCombat = false;
                        _player.Heal(_player.MaximumHitPoints);
                        MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                        ShowMessage("You are DEAD");
                        ShowMessage(" ");
                        ShowMessage("Spawned back home");
                    }
                    
                }
                UpdateUI();
            }
        }
        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            CombatPhase(1);
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            CombatPhase(2);
        }

        private void cboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cboWeapons.SelectedItem;

        }

        private void cboPotions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentPotion = (HealingPotion)cboPotions.SelectedItem;
        }

        private void btnUseSpell_Click(object sender, EventArgs e)
        {
            Spell spell = (Spell)cboSpells.SelectedItem;
            if(InCombat == true)
            {
                if(spell.CombatSpell == false)
                {
                    ShowMessage("You can't use this spell in combat");
                }
                else
                {
                    if (_player.CurrentMana >= spell.ManaCost)
                    {
                        CombatPhase(3);
                    }
                    else
                    {
                        ShowMessage("You don't have enough mana to use this spell");
                    }
                }
            }
            else { 
                if(spell.CombatSpell == true)
                {
                    ShowMessage("You must be in combat to use this spell");
                }
                else
                {
                    // custom effect code
                    if(spell.ManaCost > _player.CurrentMana)
                    {
                        ShowMessage("You don't have enough mana to cast this");
                    }
                    else
                    {
                        if (spell.ID == World.SPELL_ID_TELEPORT)
                        {
                            ShowMessage("Teleportation spell used");
                            ShowMessage("");
                            ShowMessage("Teleported back home");
                            _player.DrainMana(spell.ManaCost);
                            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                        }
                     /*   }else if(spell.EffectID == World.SPELL_EFFECT_ID_RAISE_HP)
                        {
                            ShowMessage("Maximum hit points increased!");
                            ShowMessage("This effect will wear off when you defeat a monster");
                            _player.Buffs.Add(new BuffsList(World.BuffByID(World.BUFF_ID_INCREASE_MAX_HP),20));
                            _player.UpdateMaximumStats();
                            _player.Heal(spell.EffectAmount);
                          //  _player.Heal()

                        }*/
                    }
                }
            }
        }

        private void FormAdventurePlus_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FormAdventurePlus_Load(object sender, EventArgs e)
        {

        }

        private void FormAdventurePlus_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(saveFileName, _player.toXmlString());
        }

    }
}
