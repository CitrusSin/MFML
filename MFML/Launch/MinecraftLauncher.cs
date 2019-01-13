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
        public readonly string LibRoot;
        public readonly string JavaExe;

        public MinecraftLauncher(MinecraftVersion version)
        {
            Version = version;
            manifest = MinecraftManifest.AnalyzeFromVersion(Version);
            GameDir = LauncherMain.Instance.Settings.MinecraftFolderName;
            AssetsRoot = GameDir + "assets\\";
            LibRoot = GameDir + "libraries\\";
            UserType = "mojang";
            var jrepath = LauncherMain.Instance.Settings.JREPath;
            if (jrepath.Last() != '\\')
            {
                jrepath += '\\';
            }
            JavaExe = jrepath + "bin\\javaw.exe";
            ClassPath = this.GenerateClassPath();
        }

        public virtual bool IsOffline { get { return true; } }

        public string MinecraftClientJar
        {
            get { return Version.JarPath; }
        }

        public string PlayerName { get; set; }

        public virtual string UUID
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        public virtual string AccessToken
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        public virtual string[] GenerateLaunchCommandLine()
        {
            var seri = new JavaScriptSerializer();
            var args = new List<string>();
            args.Add(JavaExe);
            var jvmargs = manifest.arguments.jvm;
            args.Add("-Dminecraft.client.jar="+Version.JarPath);
            args.Add("-XX:+UnlockExperimentalVMOptions");
            args.Add("-XX:+UseG1GC");
            args.Add(string.Format("-Xmx{0}m", LauncherMain.Instance.Settings.MaxMemory));
            if (IsOffline)
            {
                args.Add("-Dfml.ignoreInvalidMinecraftCertificates=true");
            }
            foreach (var argo in jvmargs)
            {
                if (argo is string)
                {
                    args.Add(ProcessArgument(argo as string));
                }
                else
                {
                    var arg = seri.ConvertToType<Argument>(argo);
                    bool ShouldAddThisArgument = true;
                    var rules = arg.rules;
                    foreach (var rule in rules)
                    {
                        if (!rule.Allowed)
                        {
                            ShouldAddThisArgument = false;
                            break;
                        }
                    }
                    if (ShouldAddThisArgument)
                    {
                        if (arg.value is List<string>)
                        {
                            foreach (var str in (List<string>)arg.value)
                            {
                                args.Add(ProcessArgument(str));
                            }
                        }
                        else if (arg.value is string)
                        {
                            args.Add(ProcessArgument((string)arg.value));
                        }
                    }
                }
            }
            args.Add(manifest.mainClass);
            var mcargs = manifest.arguments.game;
            foreach (var argo in mcargs)
            {
                if (argo is string)
                {
                    args.Add(ProcessArgument(argo as string));
                }
                else
                {
                    var arg = seri.ConvertToType<Argument>(argo);
                    bool ShouldAddThisArgument = true;
                    var rules = arg.rules;
                    foreach (var rule in rules)
                    {
                        if (!rule.Allowed)
                        {
                            ShouldAddThisArgument = false;
                            break;
                        }
                    }
                    if (ShouldAddThisArgument)
                    {
                        if (arg.value is List<string>)
                        {
                            foreach (var str in (List<string>)arg.value)
                            {
                                args.Add(ProcessArgument(str));
                            }
                        }
                        else if (arg.value is string)
                        {
                            args.Add(ProcessArgument((string)arg.value));
                        }
                    }
                }
            }
            return args.ToArray();
        }

        private string GenerateClassPath()
        {
            var cd = Environment.CurrentDirectory;
            if (cd.Last() != '\\')
            {
                cd += "\\";
            }
            var pathes = new List<string>();
            var libraries = manifest.libraries;
            foreach (var lib in libraries)
            {
                bool NeedThisLib = true;
                if (lib.rules != null)
                {
                    var rules = lib.rules;
                    foreach (var rule in rules)
                    {
                        if (!rule.Allowed)
                        {
                            NeedThisLib = false;
                            break;
                        }
                    }
                }
                if (NeedThisLib)
                {
                    if (lib.downloads.artifact != null)
                    {
                        var artifact = lib.downloads.artifact;
                        var path = cd + LibRoot + artifact.path.Replace('/', '\\');
                        pathes.Add(path);
                    }
                }
            }
            pathes.Add(cd + Version.JarPath);
            return string.Join(";", pathes);
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
                .Replace("${classpath}", ClassPath)
                .Replace("${launcher_name}", LauncherMain.LAUNCHER_NAME)
                .Replace("${launcher_version}", LauncherMain.LAUNCHER_VERSION);
        }
    }
}
