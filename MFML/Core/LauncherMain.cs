using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MFML.Download;
using MFML.UI;
using MFML.Game;
using MFML.Launch;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MFML.Core
{
    public class LauncherMain
    {
        const string SETTINGS_FILE_NAME = "settings.xml";

        public static LauncherMain Instance { get; private set; }
        public const string LAUNCHER_NAME = "Micrafast's Minecraft Launcher";
        public const string LAUNCHER_VERSION = "b0.1.0001";

        public Configuration Settings { get; private set; }
        public List<MinecraftVersion> MinecraftVersions { get; private set; }

        MainWindow MainForm;

        public static LauncherMain CreateInstance()
        {
            if (Instance != null)
            {
                return Instance;
            }
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
            {
                Directory.CreateDirectory(mcfolder);
            }
            // Search all versions of Minecraft
            var versionsfolder = mcfolder + "\\versions";
            if (!Directory.Exists(versionsfolder))
            {
                Directory.CreateDirectory(versionsfolder);
            }
            var versionfolders = Directory.GetDirectories(versionsfolder);
            MinecraftVersions = new List<MinecraftVersion>();
            foreach (string versionfolder in versionfolders)
            {
                var versionName = versionfolder.Split('\\').Last();
                MinecraftVersions.Add(new MinecraftVersion(versionName));
            }
        }

        public void RunMinecraft(MinecraftVersion ver)
        {
            Settings.Save();
            var PlayerName = this.Settings.PlayerName;
            var LogWindow = new ConsoleWindow();
            var LaunchMaker = new MinecraftOfflineLaunchMaker(ver, PlayerName, LogWindow);
            MainForm.Hide();
            Task MCWatcherTask = Task.Factory.StartNew(LaunchMaker.Launch);
            if (Settings.NeedDebug)
            {
                Task.Factory.StartNew(LogWindow.ShowDialogSafely);
            }
            while (!MCWatcherTask.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            if (!Settings.NeedDebug)
            {
                LogWindow.Close();
            }
            MainForm.Show();
        }

        public void AddMinecraftVersion(MinecraftVersion ver)
        {
            MinecraftVersions.Add(ver);
            MainForm.AddVersion(ver);
        }

        public void ShowDownloadMinecraftList()
        {
            Settings.Save();
            var downloader = new MinecraftDownloader(Settings.UseBMCL, Settings.MinecraftFolderName);
            var downloadDialog = new DownloadWindow(downloader);
            downloadDialog.ShowDialog(MainForm);
            downloadDialog.Dispose();
        }

        public void Exit()
        {
            Settings.Save();
            Application.Exit();
        }

    }
}
