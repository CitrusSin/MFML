using MFML.Core;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace MFML.Game
{
    public class MinecraftVersion
    {
        public string VersionName { get; private set; }
        public string VersionDirectory { get; private set; }

        public string VersionManifestPath
        {
            get { return VersionDirectory + VersionName + ".json"; }
        }

        public string JarPath
        {
            get { return VersionDirectory + VersionName + ".jar"; }
        }

        public string NativesPath
        {
            get { return VersionDirectory + VersionName + "-natives\\"; }
        }

        public string GameNativesPath
        {
            get { return NativesPath.Substring(NativesPath.IndexOf('\\') + 1); }
        }

        public bool LaunchWithLaunchWrapper
        {
            get
            {
                var manifest = MinecraftManifest.AnalyzeFromVersion(this);
                return manifest.mainClass == "net.minecraft.launchwrapper.Launch";
            }
        }

        public MinecraftVersion(string VersionName)
        {
            this.VersionName = VersionName;
            var mcdir = LauncherMain.Instance.Settings.MinecraftFolderName;
            VersionDirectory = mcdir + "versions\\" + VersionName + "\\";
        }

        public void SaveManifest(MinecraftManifest manifest)
        {
            var seri = new JavaScriptSerializer();
            var text = seri.Serialize(manifest);
            var sw = new StreamWriter(new FileStream(VersionManifestPath, FileMode.OpenOrCreate));
            sw.Write(text);
            sw.Close();
        }

        public void InstallLaunchWrapper()
        {
            const string LAUNCHWRAPPER_VERSION = "1.12";
            if (!this.LaunchWithLaunchWrapper)
            {
                var mcdir = LauncherMain.Instance.Settings.MinecraftFolderName;
                var libraryloc = mcdir + string.Format(
                    "libraries\\net\\minecraft\\launchwrapper\\{0}\\", LAUNCHWRAPPER_VERSION);
                if (!Directory.Exists(libraryloc))
                {
                    Directory.CreateDirectory(libraryloc);
                }
                libraryloc += string.Format("launchwrapper-{0}.jar", LAUNCHWRAPPER_VERSION);
                if (!File.Exists(libraryloc))
                {
                    using (var wc = new WebClient())
                    {
                        string url = string.Format(
                                "https://libraries.minecraft.net/net/minecraft/launchwrapper/{0}/launchwrapper-{0}.jar",
                                LAUNCHWRAPPER_VERSION
                                );
                        if (LauncherMain.Instance.Settings.UseBMCL)
                        {
                            url = url.Replace("libraries.minecraft.net",
                                "bmclapi2.bangbang93.com/libraries");
                        }
                        wc.DownloadFile(url, libraryloc);
                    }
                }
                var manifest = MinecraftManifest.AnalyzeFromVersion(this);
                manifest.mainClass = "net.minecraft.launchwrapper.Launch";
                var wrapperLibrary = new MinecraftLibrary();
                wrapperLibrary.name = string.Format("net.minecraft:launchwrapper:{0}", LAUNCHWRAPPER_VERSION);
                manifest.libraries.Add(wrapperLibrary);
                this.SaveManifest(manifest);
            }
        }

        public override string ToString()
        {
            return VersionName;
        }
    }
}
