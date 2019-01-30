using MFML.Core;
using MFML.Game;
using MFML.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace MFML.Download
{
    public class ForgeDownloadItem : DownloadItem
    {
        public MinecraftVersion MCVersion { get; private set; }
        public string ForgeVersion { get; private set; }
        public string MCBranch { get; private set; }
        public string Url { get; private set; }
        public bool UseBMCL { get; private set; }

        public ForgeDownloadItem(MinecraftVersion MCVersion, string ForgeVersion, string MCBranch, string Url, bool UseBMCL)
        {
            this.MCVersion = MCVersion;
            this.ForgeVersion = ForgeVersion;
            this.MCBranch = MCBranch;
            this.Url = Url;
            this.UseBMCL = UseBMCL;
        }

        public override event DownloadProgress OnProgressChanged;

        public override string Description => "版本：" + ForgeVersion;

        public override void Download()
        {
            OnProgressChanged("正在检查launchwrapper安装情况并确保安装", 0);
            MCVersion.InstallLaunchWrapper();
            var jarfile = string.Format("forge-{0}-{1}-{2}-universal.jar", this.MCVersion.VersionName, this.ForgeVersion, this.MCBranch);
            var folder = string.Format("net/minecraftforge/forge/{0}-{1}-{2}/", this.MCVersion.VersionName, this.ForgeVersion, this.MCBranch);
            var location = folder + jarfile;
            var realLocation = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\" + location.Replace('/', '\\');
            var realFolder = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\" + folder.Replace('/', '\\');
            if (!Directory.Exists(realFolder))
            {
                Directory.CreateDirectory(realFolder);
            }
            using (var wc = new WebClient())
            {
                OnProgressChanged("开始下载"+jarfile, 0);
                bool downloaded = false;
                wc.DownloadFileCompleted += (o, e) =>
                {
                    downloaded = true;
                    wc.Dispose();
                };
                wc.DownloadProgressChanged += (o, e) => OnProgressChanged(null, e.ProgressPercentage);
                wc.DownloadFileAsync(new Uri(this.Url), realLocation);
                while (!downloaded)
                {
                    Thread.Sleep(100);
                }
            }
            var zip = ZipFile.Open(realLocation, ZipArchiveMode.Read);
            var versionlistentry = zip.GetEntry("version.json");
            var sr = new StreamReader(versionlistentry.Open());
            var json = sr.ReadToEnd();
            sr.Close();
            zip.Dispose();
            var matches = Regex.Matches(json, "\"name\": \"(.*?)\"");
            var manifest = MinecraftManifest.AnalyzeFromVersion(MCVersion);
            int needDownload = 0;
            int downloadedCount = 0;
            foreach (Match match in matches)
            {
                var groups = EnumeratorUtils.MakeListFromEnumerator(match.Groups.GetEnumerator());
                var libname = (string)groups[1];
                var names = new List<string>(libname.Split(':'));
                if (names[1] != "launchwrapper")
                {
                    var fnames = names[0].Split('.');
                    names.RemoveAt(0);
                    names.InsertRange(0, fnames);
                    string url;
                    if (this.UseBMCL)
                    {
                        url = "http://bmclapi2.bangbang93.com/maven/";
                    }
                    else
                    {
                        url = "http://files.minecraftforge.net/maven/";
                    }
                    url += string.Join("/", names);
                    var jarname = names[names.Count - 2] + "-" + names[names.Count - 1] + ".jar";
                    url += "/" + jarname;
                    var localDir = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\" + string.Join("\\", names) + "\\";
                    if (!Directory.Exists(localDir))
                    {
                        Directory.CreateDirectory(localDir);
                    }
                    var localLoc = localDir + jarname;
                    var wc = new WebClient();
                    wc.DownloadFileCompleted += (o, e) =>
                    {
                        downloadedCount++;
                        OnProgressChanged(jarname + "已下载", (int)(downloadedCount/(double)needDownload*100));
                        wc.Dispose();
                    };
                    wc.DownloadProgressChanged += (o, e) =>
                        Debug.WriteLine(string.Format("{0} Downloaded {1}", jarname, e.ProgressPercentage));
                    wc.DownloadFileAsync(new Uri(url), localLoc);
                    var library = new MinecraftLibrary
                    {
                        name = libname
                    };
                    manifest.libraries.Add(library);
                    needDownload++;
                }
            }
            while (needDownload != downloadedCount)
            {
                Thread.Sleep(100);
            }
            if (manifest.arguments == null)
            {
                manifest.minecraftArguments += " --tweakClass cpw.mods.fml.common.launcher.FMLTweaker";
            }
            else
            {
                manifest.arguments.game.Add("--tweakClass");
                manifest.arguments.game.Add("cpw.mods.fml.common.launcher.FMLTweaker");
            }
            MCVersion.SaveManifest(manifest);
        }
    }

    public class ForgeDownloader : Downloader
    {
        readonly MinecraftVersion MCVersion;
        readonly bool UseBMCL;

        public ForgeDownloader(bool UseBMCL, MinecraftVersion version)
        {
            this.UseBMCL = UseBMCL;
            this.MCVersion = version;
        }

        public override List<DownloadItem> GetAllItemsToDownload()
        {
            var list = new List<DownloadItem>();
            using (var wc = new WebClient())
            {
                if (UseBMCL)
                {
                    var manifestUrl = string.Format(
                        "https://bmclapi2.bangbang93.com/forge/minecraft/{0}",
                        MCVersion.VersionName
                        );
                    var jsonlist = wc.DownloadString(manifestUrl);
                    var matches = Regex.Matches(jsonlist, "\"version\": \"(.*?)\"");
                    foreach (Match match in matches)
                    {
                        var groups = EnumeratorUtils.MakeListFromEnumerator(match.Groups.GetEnumerator());
                        var version = ((Group)groups[1]).Value;
                        var mc = this.MCVersion.VersionName;
                        var branch = this.MCVersion.VersionName;
                        var url = string.Format(
                            "http://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{0}-{1}-{2}/forge-{0}-{1}-{2}-universal.jar",
                            mc,
                            version,
                            branch
                            );
                        list.Add(new ForgeDownloadItem(this.MCVersion, version, branch, url, this.UseBMCL));
                    }
                }
                else
                {
                    var manifestUrl = string.Format(
                        "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_{0}.html",
                        MCVersion.VersionName
                        );
                    var htmllist = wc.DownloadString(manifestUrl);
                    var matches = Regex.Matches(htmllist, "<tr>\\n\\s*?<td class=\"download-version\">\\n\\s*?(.*?)\\n");
                    foreach (Match match in matches)
                    {
                        var groups = EnumeratorUtils.MakeListFromEnumerator(match.Groups.GetEnumerator());
                        var version = ((Group)groups[1]).Value;
                        var mc = this.MCVersion.VersionName;
                        var branch = this.MCVersion.VersionName;
                        var url = string.Format(
                            "http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}-{2}/forge-{0}-{1}-{2}-universal.jar",
                            mc,
                            version,
                            branch
                            );
                        list.Add(new ForgeDownloadItem(this.MCVersion, version, branch, url, this.UseBMCL));
                    }
                }
            }
            return list;
        }
    }
}
