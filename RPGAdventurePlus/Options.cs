using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace RPGAdventurePlus
{
    public partial class Options : Form
    {
        MainMenu menu;
        string Language;
        string Difficulty;
        bool AutoSave;
        int count = 1;
        public Options(MainMenu mainMenu)
        {
            menu = mainMenu;
            InitializeComponent();
            try
            {
                XmlDocument optionsData = new XmlDocument();
                optionsData.LoadXml(File.ReadAllText(GlobalSetting.OPTION_FILE_NAME));
                //    btnAutoSave.Enabled = Convert.ToBoolean(OptionsData.SelectSingleNode("/Options/AutoSave"));
                Language = optionsData.SelectSingleNode("/Options/Language").InnerText;
                Difficulty = optionsData.SelectSingleNode("/Options/Difficulty").InnerText;
                AutoSave = Convert.ToBoolean(optionsData.SelectSingleNode("/Options/AutoSave").InnerText);


                if (AutoSave)
                {
                    btnAutoSave.Text = "ON";
                }
                else
                {
                    btnAutoSave.Text = "OFF";
                }
                btnDifficulty.Text = Difficulty;
                btnLanguage.Text = Language;
            }catch
            {
                MessageBox.Show("Failed to load options");
                this.Close();
                menu.Enabled = true;
            }
          
        }

        private void btnAutoSave_Click(object sender, EventArgs e)
        {
            if(AutoSave)
            {
                AutoSave = false;
                btnAutoSave.Text = "OFF";
            }else
            {
                AutoSave = true;
                btnAutoSave.Text = "ON";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalSetting.CreateOptionFile(Language, Difficulty, AutoSave);
            GlobalSetting.UpdateSettings();
           this.Close();
            menu.Enabled = true;
            menu.Focus();
        }

        private void btnDifficulty_Click(object sender, EventArgs e)
        {
           
            if(count == 1)
            {
                btnDifficulty.Text = "Normal";
                Difficulty = "Normal";
                count++;
                
            }else if (count == 2)
            {
                btnDifficulty.Text = "Hard";
                Difficulty = "Hard";
                count++;
            }else if(count == 3)
            {
                btnDifficulty.Text = "Insane";
                Difficulty = "Insane";
                count = 1;
            }
            
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }
    }
}
