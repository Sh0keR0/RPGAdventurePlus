using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace RPGAdventurePlus
{
  public static class GlobalSetting
    {
      public const string OPTION_FILE_NAME = "Options.xml";
      public const string SAVE_FILES_NAME = "Saves.xml";
      public static bool AutoSave = true;
      public static string Difficulty = "Normal";    // normal /hard / insane 
      public static string Language = "English";                   
      public static void CreateNewSave(string FileName) 
      {
          if (File.Exists(SAVE_FILES_NAME))
          {
              XmlDocument saveFilesData = new XmlDocument();
              saveFilesData.LoadXml(File.ReadAllText(SAVE_FILES_NAME));
              XmlNode saveFileNode = saveFilesData.CreateElement("Save");
              saveFileNode.AppendChild(saveFilesData.CreateTextNode(FileName + ".xml"));
              saveFilesData.SelectSingleNode("/Saves").AppendChild(saveFileNode);
              File.WriteAllText(SAVE_FILES_NAME, saveFilesData.InnerXml);
          }
          else
          {
              XmlDocument saveFilesData = new XmlDocument();
              XmlNode savesNode = saveFilesData.CreateElement("Saves");
              saveFilesData.AppendChild(savesNode);
              XmlNode saveFileNode = saveFilesData.CreateElement("Save");
              saveFileNode.AppendChild(saveFilesData.CreateTextNode(FileName + ".xml"));
              savesNode.AppendChild(saveFileNode);
              File.WriteAllText(SAVE_FILES_NAME, saveFilesData.InnerXml);
          }
      }
      public static void UpdateSettings()
      {
          XmlDocument optionData = new XmlDocument();
          optionData.LoadXml(File.ReadAllText(OPTION_FILE_NAME));
          Difficulty = optionData.SelectSingleNode("/Options/Difficulty").InnerText;
          AutoSave = Convert.ToBoolean(optionData.SelectSingleNode("/Options/AutoSave").InnerText);
          Language = optionData.SelectSingleNode("/Options/Language").InnerText;

      }
      public static void CreateOptionFile(string Language, string Difficulty, bool AutoSave)
      {
          XmlDocument optionData = new XmlDocument();
          XmlNode optionNode = optionData.CreateElement("Options");
          optionData.AppendChild(optionNode);
          XmlNode languageNode = optionData.CreateElement("Language");
          languageNode.AppendChild(optionData.CreateTextNode(Language));
          optionNode.AppendChild(languageNode);
          XmlNode autoSaveNode = optionData.CreateElement("AutoSave");
          autoSaveNode.AppendChild(optionData.CreateTextNode(AutoSave.ToString()));
          optionNode.AppendChild(autoSaveNode);
          XmlNode difficultyNode = optionData.CreateElement("Difficulty");
          difficultyNode.AppendChild(optionData.CreateTextNode(Difficulty));
          optionNode.AppendChild(difficultyNode);
          File.WriteAllText(OPTION_FILE_NAME, optionData.InnerXml);
      }
    }
}
