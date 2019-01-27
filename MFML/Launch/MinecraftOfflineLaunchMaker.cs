using System;
using MFML.Core;
using MFML.Game;
using MFML.UI;

namespace MFML.Launch
{
    public class MinecraftOfflineLaunchMaker : MinecraftLaunchMaker
    {
        public MinecraftOfflineLaunchMaker(MinecraftVersion version, string PlayerName, ConsoleWindow LogWindow) : base(version, PlayerName, LogWindow)
        {
        }

        public override string UUID => LauncherMain.Instance.Settings.OfflineUUID;

        public override string AccessToken => LauncherMain.Instance.Settings.OfflineUUID;
    }
}
