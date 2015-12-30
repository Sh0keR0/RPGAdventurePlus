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
namespace RPGAdventurePlus
{
    public partial class CharacterCreation : Form
    {
        private Race currentRace;
        private int Strength, Int, Dex,HitPoints,Mana, count = 1;
        private string Benefit;
        private Player player = null;
        private static string fileName;
        public CharacterCreation(string FileName)
        {
            InitializeComponent();
            ChangeRace(1);
            fileName = FileName;

        }

        private void btnNextRace_Click(object sender, EventArgs e)
        {
            count++;
            if (count > 4)
                count = 1;
            ChangeRace(count);
        }
        private void ChangeRace(int id)
        {
           if(id == 1) // human race
           {
               currentRace = World.RaceByID(World.RACE_ID_HUNMAN);
               Strength = 5;
               Int = 5;
               Dex = 3;
               HitPoints = 20;
               Mana = 10;
               Benefit = "Start with extra 10 gold";
               pcRaceImage.Image = Properties.Resources.human_race;
           }else if(id == 2) // Elf
           {
               currentRace = World.RaceByID(World.RACE_ID_ELF);
               Strength = 3;
               Int = 8;
               Dex = 3;
               HitPoints = 10;
               Mana = 20;
               pcRaceImage.Image = Properties.Resources.elf_race;
               Benefit = "Start with extra mana and a spell";
           }else if(id == 3) // Orc
           {
               currentRace = World.RaceByID(World.RACE_ID_ORC);
               Strength = 8;
               Int = 2;
               Dex = 3;
               HitPoints = 30;
               Mana = 10;
               pcRaceImage.Image = Properties.Resources._3D_Orc;
               Benefit = "Start with extra hit points and armour";
           }else if(id == 4) // Dwarf
           {
               currentRace = World.RaceByID(World.RACE_ID_DWARF);
               Strength = 3;
               Int = 6;
               Dex = 6;
               HitPoints = 10;
               pcRaceImage.Image = Properties.Resources.dwarf_race;
               Benefit = "Start with Iron sword";
           }
           UpdateUI();
        }

        private void btnPreviousRace_Click(object sender, EventArgs e)
        {
            count--;
            if (count < 1)
                count = 4;
            ChangeRace(count);
        }
        private void UpdateUI()
        {
            lblRaceName.Text = currentRace.Name;
            lblRaceBenefit.Text = Benefit;
            lblHitPoints.Text = HitPoints.ToString();
            lblDex.Text = Dex.ToString();
            lblInt.Text = Int.ToString();
            lblStrength.Text = Strength.ToString();
            lblMana.Text = Mana.ToString();           

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            player = new Player(HitPoints, HitPoints, 10, 0, 1, Strength, Dex, Int, Mana, Mana, currentRace);
            if(currentRace.ID == World.RACE_ID_HUNMAN)
            {
                player.Gold += 10;
            }else if(currentRace.ID == World.RACE_ID_ELF)
            {
                player.AddSpell(World.SpellByID(World.SPELL_ID_FIREBALL));
            }else if(currentRace.ID == World.RACE_ID_ORC)
            {
                player.AddItem((Armour)World.ItemByID(World.ITEM_ID_METAL_ARMOUR), 1);
                player.ArmourUsed = (Armour)World.ItemByID(World.ITEM_ID_METAL_ARMOUR);
            }else if(currentRace.ID == World.RACE_ID_DWARF)
            {
                player.AddItem(World.ItemByID(World.ITEM_ID_IRON_SWORD),1);
            }

            player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
            player.AddItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1);
            GlobalSetting.CreateNewSave(fileName);
            FormAdventurePlus formAdventure = new FormAdventurePlus(player,fileName+".xml");
            formAdventure.Show();
            this.Close();
           
           
        }

        private void CharacterCreation_FormClosed(object sender, FormClosedEventArgs e)
        {
           if (player == null)
                Application.Exit();
        }

       


    }
}
