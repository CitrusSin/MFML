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

        public MinecraftVersion(string VersionName)
        {
            this.VersionName = VersionName;
            var mcdir = MFML.Instance.Settings.MinecraftFolderName;
            VersionDirectory = mcdir + "versions\\" + VersionName + "\\";
        }

        public void AddUsedLibrary()
        {

        }

        public override string ToString()
        {
            return VersionName;
        }
    }
}
