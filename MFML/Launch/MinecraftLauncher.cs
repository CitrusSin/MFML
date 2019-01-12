using MFML.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Game
{
    public class MinecraftLauncher
    {
        public readonly MinecraftVersion Version;
        public readonly MinecraftManifest manifest;

        public string MinecraftClientJar
        {
            get { return Version.JarPath; }
        }

        public MinecraftLauncher(MinecraftVersion version)
        {
            Version = version;
            manifest = MinecraftManifest.AnalyzeFromVersion(Version);
        }

        public virtual string GenerateLaunchCommandLine()
        {
            return null;
        }
    }
}
