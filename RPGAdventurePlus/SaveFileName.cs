using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGAdventurePlus
{
    public partial class SaveFileName : Form
    {
        MainMenu mainMenu;
        public SaveFileName(MainMenu mainMenuRef)
        {
            InitializeComponent();
            mainMenu = mainMenuRef;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtboxFileName.Text == "")
            {
                MessageBox.Show("The name can not be empty");
            }
            else
            {
                mainMenu.Close();
                string filename = txtboxFileName.Text;
                CharacterCreation characterCreation = new CharacterCreation(filename);
                this.Close();
                characterCreation.Show();
            }
        }

    }
}
