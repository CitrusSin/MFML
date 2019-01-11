using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MFML.Download;
using MFML.UI;
using MFML.Game;

namespace MFML.Core
{
    public class LauncherMain
    {
        const string SETTINGS_FILE_NAME = "settings.xml";
        public static LauncherMain Instance { get; private set; }

        public Configuration Settings { get; private set; }
        public List<MinecraftVersion> MinecraftVersions { get; private set; }

        MainWindow MainForm;

        public static LauncherMain CreateInstance()
        {
            if (Instance != null)
                return Instance;
            else
            {
                new LauncherMain();
                return Instance;
            }
        }

        private LauncherMain()
        {
            Instance = this;
            Settings = new Configuration(SETTINGS_FILE_NAME);
            checkMinecraft();
        }

        public void FormInitalization(MainWindow launcherWindow)
        {
            MainForm = launcherWindow;
        }

        private void checkMinecraft()
        {
            // Check if minecraft's folder is existing
            var mcfolder = Settings.MinecraftFolderName;
            if (!Directory.Exists(mcfolder))
                Directory.CreateDirectory(mcfolder);
            // Search all versions of Minecraft
            var versionsfolder = mcfolder + "\\versions";
            if (!Directory.Exists(versionsfolder))
                Directory.CreateDirectory(versionsfolder);
            var versionfolders = Directory.GetDirectories(versionsfolder);
            MinecraftVersions = new List<MinecraftVersion>();
            foreach (string versionfolder in versionfolders)
            {
                var versionName = versionfolder.Split('\\').Last();
                MinecraftVersions.Add(new MinecraftVersion(versionName));
            }
        }

        public void LaunchMinecraft(MinecraftVersion ver)
        {
            var launchProvider = new MinecraftLauncher(ver);
        }

        public void AddMinecraftVersion(MinecraftVersion ver)
        {
            MinecraftVersions.Add(ver);
            MainForm.AddVersion(ver);
        }

        public void ShowDownloadMinecraftList()
        {
            var downloader = new MinecraftDownloader();
            var downloadDialog = new DownloadWindow(downloader);
            downloadDialog.ShowDialog(MainForm);
        }

        public void Exit()
        {
            Settings.Save();
            MainForm.Close();
        }

    }
}
