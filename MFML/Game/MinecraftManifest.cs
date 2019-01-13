using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MFML.Game
{
    public class MinecraftManifest
    {
        public string id;
        public MinecraftLaunchArguments arguments;
        public string minecraftArguments;
        public string mainClass;
        public AssetIndex assetIndex;
        public string assets;
        public List<MinecraftLibrary> libraries;
        public MinecraftJarDownloads downloads;
        public MinecraftLogging logging;
        public string type;
        public string time;
        public string releaseTime;
        public int minimumLauncherVersion;
        public bool hidden;

        public static MinecraftManifest AnalyzeFromVersion(MinecraftVersion version)
        {
            var filename = version.VersionManifestPath;
            var sr = new StreamReader(new FileStream(filename, FileMode.Open));
            var data = sr.ReadToEnd();
            sr.Close();
            var seri = new JavaScriptSerializer();
            return seri.Deserialize<MinecraftManifest>(data);
        }
    }
}
