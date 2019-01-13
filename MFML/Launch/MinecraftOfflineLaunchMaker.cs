using System;
using MFML.Game;
using MFML.UI;

namespace MFML.Launch
{
    public class MinecraftOfflineLaunchMaker : MinecraftLaunchMaker
    {
        public MinecraftOfflineLaunchMaker(MinecraftVersion version, string PlayerName, ConsoleWindow LogWindow) : base(version, PlayerName, LogWindow)
        {
        }

        public override string UUID => Guid.NewGuid().ToString("N");

        public override string AccessToken => Guid.NewGuid().ToString("N");
    }
}
