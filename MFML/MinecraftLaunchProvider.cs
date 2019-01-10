using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML
{
    public class MinecraftLaunchProvider
    {
        public readonly MinecraftVersion Version;

        public string MinecraftClientJar
        {
            get { return Version.JarPath; }
        }

        public MinecraftLaunchProvider(MinecraftVersion version)
        {
            Version = version;
        }

        public string GenerateLaunchCommandLine()
        {
            return null;
        }
    }
}
