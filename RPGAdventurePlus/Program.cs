using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Engine;
namespace RPGAdventurePlus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          /*  if (!File.Exists(GlobalSetting.PLAYER_DATA_FILE_NAME))
            {
                CharacterCreation creationForm = new CharacterCreation();
                creationForm.Show();
            }
            else
            {
                Player player = Player.LoadPlayerInformationFromXml(File.ReadAllText(GlobalSetting.PLAYER_DATA_FILE_NAME));
                FormAdventurePlus formAdventurePlus = new FormAdventurePlus(player);
                formAdventurePlus.Show();
            }
   */
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Application.Run();
        }
    }
}
