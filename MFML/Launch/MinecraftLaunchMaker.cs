using MFML.Core;
using MFML.Game;
using MFML.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MFML.Launch
{
    public abstract class MinecraftLaunchMaker
    {
        public readonly MinecraftVersion Version;
        public readonly MinecraftManifest manifest;
        public readonly string GameDir;
        public readonly string AssetsRoot;
        public readonly string UserType;
        public readonly string ClassPath;
        public readonly string LibRoot;
        public readonly string JavaExe;

        public Process MinecraftProcess { get; protected set; }
        public ConsoleWindow LogWindow { get; protected set; }

        public MinecraftLaunchMaker(MinecraftVersion version, string PlayerName, ConsoleWindow LogWindow)
        {
            Version = version;
            this.LogWindow = LogWindow;
            this.PlayerName = PlayerName;
            manifest = MinecraftManifest.AnalyzeFromVersion(Version);
            GameDir = LauncherMain.Instance.Settings.MinecraftFolderName;
            AssetsRoot = GameDir + "assets\\";
            AssetsRoot = AssetsRoot.Substring(AssetsRoot.IndexOf('\\')+1);
            LibRoot = GameDir + "libraries\\";
            UserType = "mojang";
            var jrepath = LauncherMain.Instance.Settings.JREPath;
            if (jrepath.Last() != '\\')
            {
                jrepath += '\\';
            }
            JavaExe = jrepath + "bin\\java.exe";
            ClassPath = this.GenerateClassPath();
        }

        public virtual bool IsOffline { get { return true; } }

        public string MinecraftClientJar
        {
            get { return Version.JarPath; }
        }

        public string PlayerName { get; set; }

        public abstract string UUID { get; }

        public abstract string AccessToken { get; }

        public void Launch()
        {
            var cd = Environment.CurrentDirectory;
            if (cd.Last() != '\\')
            {
                cd += '\\';
            }
            string[] args = GenerateLaunchCommandLine();
            for (int i = 1;i<args.Length;i++)
            {
                var arg = args[i];
                if (arg.Contains(' '))
                {
                    args[i] = '"' + arg + '"';
                }
            }
            var filename = args[0];
            var cmdArgs = string.Join(" ", args.Skip(1));
            MinecraftProcess = new Process();
            MinecraftProcess.StartInfo.FileName = filename;
            MinecraftProcess.StartInfo.Arguments = cmdArgs;
            MinecraftProcess.StartInfo.UseShellExecute = false;
            MinecraftProcess.StartInfo.RedirectStandardError = true;
            MinecraftProcess.StartInfo.RedirectStandardInput = true;
            MinecraftProcess.StartInfo.RedirectStandardOutput = true;
            MinecraftProcess.StartInfo.CreateNoWindow = true;
            MinecraftProcess.StartInfo.WorkingDirectory = cd + GameDir;
            MinecraftProcess.Start();
            Task.Factory.StartNew(() =>
            {
                while (!MinecraftProcess.HasExited)
                {
                    LogWindow.WriteLine(MinecraftProcess.StandardOutput.ReadLine());
                }
            });
            Task.Factory.StartNew(() =>
            {
                while (!MinecraftProcess.HasExited)
                {
                    LogWindow.WriteLine(MinecraftProcess.StandardError.ReadLine());
                }
            });
            MinecraftProcess.WaitForExit();
            if (MinecraftProcess.ExitCode != 0)
            {
                var result = MFMLMessageBox.ShowMessageBox("警告",
                    "检测到Minecraft运行发生错误！" + Environment.NewLine + "请选择是否查看调试窗口",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    LogWindow.ShowDialogSafely();
                }
            }
        }

        public virtual string[] GenerateLaunchCommandLine()
        {
            var seri = new JavaScriptSerializer();
            var args = new List<string>();
            args.Add(JavaExe);
            args.Add("-Dminecraft.client.jar="+Version.JarPath);
            args.Add("-XX:+UnlockExperimentalVMOptions");
            args.Add("-XX:+UseG1GC");
            args.Add(string.Format("-Xmx{0}m", LauncherMain.Instance.Settings.MaxMemory));
            if (IsOffline)
            {
                args.Add("-Dfml.ignoreInvalidMinecraftCertificates=true");
            }
            if (manifest.arguments != null)
            {
                var jvmargs = manifest.arguments.jvm;
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
            }
            else
            {
                args.Add(string.Format("-Djava.library.path={0}", Version.GameNativesPath));
                args.Add("-cp");
                args.Add(ClassPath);
                args.Add(manifest.mainClass);
                var argtext = manifest.minecraftArguments.Split(' ');
                foreach (var arg in argtext)
                {
                    args.Add(ProcessArgument(arg));
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
            var pathes = new StringBuilder();
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
                    if (lib != null)
                    {
                        var names = lib.name.Split(':');
                        var ids = names.Skip(1);
                        var packages = names[0].Split('.');
                        List<string> dirs = new List<string>();
                        dirs.AddRange(packages);
                        dirs.AddRange(ids);
                        dirs.Add(string.Join("-", ids) + ".jar");
                        var path = cd + LibRoot + string.Join("\\", dirs);
                        pathes.Append(path);
                        pathes.Append(';');
                    }
                }
            }
            pathes.Append(cd + Version.JarPath);
            return pathes.ToString();
        }

        private string ProcessArgument(string str)
        {
            return str
                .Replace("${auth_player_name}", PlayerName)
                .Replace("${version_name}", Version.VersionName)
                .Replace("${game_directory}", ".")
                .Replace("${assets_root}", AssetsRoot)
                .Replace("${assets_index_name}", manifest.assets)
                .Replace("${auth_uuid}", UUID)
                .Replace("${auth_access_token}", AccessToken)
                .Replace("${user_type}", UserType)
                .Replace("${user_properties}", "{}")
                .Replace("${version_type}", manifest.type)
                .Replace("${natives_directory}", Version.GameNativesPath)
                .Replace("${classpath}", ClassPath)
                .Replace("${launcher_name}", LauncherMain.LAUNCHER_NAME)
                .Replace("${launcher_version}", LauncherMain.LAUNCHER_VERSION);
        }
    }
}
