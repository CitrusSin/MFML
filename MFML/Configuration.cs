﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace MFML
{
    public class Configuration
    {

        string filename;
        XmlDocument configDoc = new XmlDocument();

        public Color ThemeColor
        {
            get
            {
                var colorStr = GetSettingByName("themecolor");
                return Color.FromName(colorStr);
            }
            set
            {
                if (value.IsNamedColor)
                    SetSetting("themecolor", value.Name);
                else
                    throw new Exception("Color is not named");
            }
        }

        public string MinecraftFolderName
        {
            get { return GetSettingByName("mcfolder"); }
            set { SetSetting("mcfolder", value); }
        }

        public string PlayerName
        {
            get { return GetSettingByName("playername"); }
            set { SetSetting("playername", value); }
        }

        public string SelectedVersion
        {
            get { return GetSettingByName("selectedversion"); }
            set { SetSetting("selectedversion", value); }
        }

        public string JREPath
        {
            get { return GetSettingByName("javapath"); }
            set { SetSetting("javapath", value); }
        }

        public int MaxMemory
        {
            get { return int.Parse(GetSettingByName("maxmemory")); }
            set { SetSetting("maxmemory", value.ToString()); }
        }

        public bool UseBMCL
        {
            get
            {
                var bstr = GetSettingByName("usebmcl");
                if (bstr == "true")
                    return true;
                else
                    return false;
            }

            set { SetSetting("usebmcl", value ? "true" : "false"); }
        }

        public Configuration(string filename)
        {
            this.filename = filename;
            if (!File.Exists(filename))
                NewFileInitalization();
            else
                configDoc.Load(filename);
        }

        private void NewFileInitalization()
        {
            configDoc.RemoveAll();
            configDoc.AppendChild(configDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            // Root node
            var settings = configDoc.CreateElement("settings");
            configDoc.AppendChild(settings);
            // Theme color setting
            var color = configDoc.CreateElement("themecolor");
            color.InnerText = "DeepSkyBlue";
            settings.AppendChild(color);
            // Minecraft folder setting
            var mcfolder = configDoc.CreateElement("mcfolder");
            mcfolder.InnerText = ".minecraft";
            settings.AppendChild(mcfolder);
            // Minecraft player name
            var playername = configDoc.CreateElement("playername");
            playername.InnerText = "";
            settings.AppendChild(playername);
            // Selected version
            var selectedversion = configDoc.CreateElement("selectedversion");
            selectedversion.InnerText = "";
            settings.AppendChild(selectedversion);
            // Java path
            var javapath = configDoc.CreateElement("javapath");
            string javahome;
            try
            {
                var javakey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");
                string currentjavaver = (string)javakey.GetValue("CurrentVersion");
                javakey.Close();
                var javaverkey = Registry.LocalMachine.OpenSubKey(
                    "SOFTWARE\\JavaSoft\\Java Runtime Environment\\" +
                    currentjavaver);
                javahome = (string)javaverkey.GetValue("JavaHome");
                javaverkey.Close();
            }
            catch (Exception)
            {
                javahome = "未找到JRE。请手动指定JRE安装目录。";
            }
            javapath.InnerText = javahome;
            settings.AppendChild(javapath);
            // Max memory for JVM
            var maxmemory = configDoc.CreateElement("maxmemory");
            maxmemory.InnerText = "1024";
            settings.AppendChild(maxmemory);
            // BMCLAPI
            var usebmcl = configDoc.CreateElement("usebmcl");
            usebmcl.InnerText = "true";
            settings.AppendChild(usebmcl);
            // Save all settings initalized
            configDoc.Save(this.filename);
        }

        private string GetSettingByName(string name)
        {
            return configDoc.SelectSingleNode("settings/" + name).InnerText;
        }

        private void SetSetting(string name, string context)
        {
            var node = configDoc.SelectSingleNode("settings/" + name);
            node.InnerText = context;
        }

        public void Save()
        {
            configDoc.Save(this.filename);
        }
    }
}