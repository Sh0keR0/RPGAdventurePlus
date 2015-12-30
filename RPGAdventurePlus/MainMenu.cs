using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace RPGAdventurePlus
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            if (!File.Exists(GlobalSetting.OPTION_FILE_NAME))
                GlobalSetting.CreateOptionFile("English", "Normal", true);

            InitializeComponent();
            if (File.Exists(GlobalSetting.SAVE_FILES_NAME))
                btnLoadGame.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {

           /* this.Close();
            CharacterCreation creationForm = new CharacterCreation();
            creationForm.Show();*/
            this.Enabled = false;
            SaveFileName saveFileName = new SaveFileName(this);
            saveFileName.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LoadGame loadGame = new LoadGame(this);
            loadGame.Show();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Options options = new Options(this);
            options.Show();
            
        }

    }
}
