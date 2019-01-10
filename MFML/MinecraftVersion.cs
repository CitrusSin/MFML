using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFML
{
    public class MinecraftVersion
    {
        public string VersionName { get; private set; }
        public string VersionDirectory { get; private set; }

        public string VersionJsonPath
        {
            get { return VersionDirectory + VersionName + ".json"; }
        }

        public string JarPath
        {
            get { return VersionDirectory + VersionName + ".jar"; }
        }

        public MinecraftVersion(string VersionName)
        {
            this.VersionName = VersionName;
            var mcdir = MFML.Instance.Settings.MinecraftFolderName;
            VersionDirectory = mcdir + "versions\\" + VersionName + "\\";
        }

        public override string ToString()
        {
            return VersionName;
        }
    }
}
