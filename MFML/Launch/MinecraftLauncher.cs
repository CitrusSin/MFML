using MFML.Core;
using MFML.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MFML.Game
{
    public class MinecraftLauncher
    {
        public readonly MinecraftVersion Version;
        public readonly MinecraftManifest manifest;
        public readonly string GameDir;
        public readonly string AssetsRoot;
        public readonly string UserType;
        public readonly string ClassPath;

        public MinecraftLauncher(MinecraftVersion version)
        {
            Version = version;
            manifest = MinecraftManifest.AnalyzeFromVersion(Version);
            GameDir = LauncherMain.Instance.Settings.MinecraftFolderName;
            AssetsRoot = GameDir + "\\assets";
            UserType = "mojang";
            ClassPath = this.GenerateClassPath();
        }

        public string MinecraftClientJar
        {
            get { return Version.JarPath; }
        }

        public string PlayerName { get; set; }

        public virtual string UUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public virtual string AccessToken
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public virtual string[] GenerateLaunchCommandLine()
        {
            var seri = new JavaScriptSerializer();
            var args = new List<string>();
            var jvmargs = manifest.arguments.jvm;
            foreach (var argo in jvmargs)
            {
                if (argo is string)
                {
                    args.Add(this.ProcessArgument(argo as string));
                }
                else
                {
                    var arg = seri.ConvertToType<Argument>(argo);

                }
            }
            return null;
        }

        private string GenerateClassPath()
        {
            return null;
        }

        private string ProcessArgument(string str)
        {
            return str
                .Replace("${auth_player_name}", PlayerName)
                .Replace("${version_name}", Version.VersionName)
                .Replace("${game_directory}", GameDir)
                .Replace("${assets_root}", AssetsRoot)
                .Replace("${assets_index_name}", manifest.assets)
                .Replace("${auth_uuid}", UUID)
                .Replace("${auth_access_token}", AccessToken)
                .Replace("${user_type}", UserType)
                .Replace("${version_type}", manifest.type)
                .Replace("${natives_directory}", Version.NativesPath)
                .Replace("${launcher_name}", LauncherMain.LAUNCHER_NAME)
                .Replace("${launcher_version}", LauncherMain.LAUNCHER_VERSION);
        }
    }
}
