using MFML.Core;
using MFML.Game;
using MFML.Game.BMCLAPI;
using MFML.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MFML.Download
{
    public class OptifineDownloader : Downloader
    {
        private class OptifineDownloadVersionInfo
        {
            public string MCVersion { get; set; }
            public string Type { get; set; }
            public string Patch { get; set; }
            public string Url { get; set; }
            public override string ToString()
            {
                return string.Format("Optifine {0} {1} {2}", MCVersion, Type.Replace('_', ' '), Patch);
            }
        }

        const string OPTIFINE_OFFICAL_DOWNLOADS = "https://www.optifine.net/downloads";
        const string BMCLAPI_OPTIFINE_LIST_FORMAT = "https://bmclapi2.bangbang93.com/optifine/{0}";

        readonly MinecraftVersion Version;

        List<OptifineDownloadVersionInfo> downloadVersionInfos;

        public OptifineDownloader(MinecraftVersion Version)
        {
            this.Version = Version;
        }

        public override event DownloadProgress OnProgressChanged;

        public override void Download()
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            Version.InstallLaunchWrapper();
            var item = downloadVersionInfos.Find((i) => i.ToString() == this.SelectedItem);
            Debug.Assert(item != null);
            var url = item.Url;
            var versionText = string.Format("{0}_{1}_{2}", item.MCVersion, item.Type, item.Patch);
            var jarname = "Optifine-" + versionText + ".jar";
            var dirpath = "optifine\\Optifine\\" + versionText + "\\";
            var downloadDirPath = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\" + dirpath;
            if (!Directory.Exists(downloadDirPath))
            {
                Directory.CreateDirectory(downloadDirPath);
            }
            var path = dirpath + jarname;
            var downloadPath = downloadDirPath + jarname;
            var manifest = MinecraftManifest.AnalyzeFromVersion(this.Version);
            using (var wc = new WebClient())
            {
                OnProgressChanged("开始下载。。", 0);
                wc.DownloadProgressChanged +=
                    (sender, args) => OnProgressChanged(null, (int)(args.ProgressPercentage * 0.8));
                Task downloadTask = wc.DownloadFileTaskAsync(url, downloadPath);
                while (!downloadTask.IsCompleted)
                {
                    Thread.Sleep(500);
                }
            }
            OnProgressChanged("正在添加依赖项。。。", 80);
            var library = new MinecraftLibrary();
            library.name = dirpath.Substring(0, dirpath.Length - 1).Replace('\\', ':');
            library.downloads = new LibraryDownloads();
            library.downloads.artifact = new DownloadInfo();
            library.downloads.artifact.path = path.Replace('\\', '/');
            library.downloads.artifact.url = url;
            manifest.libraries.Add(library);
            if (manifest.minecraftArguments == null)
            {
                manifest.arguments.game.Add("--tweakClass");
                manifest.arguments.game.Add("optifine.OptiFineTweaker");
            }
            else
            {
                manifest.minecraftArguments += " --tweakClass optifine.OptiFineTweaker";
            }
            
            OnProgressChanged(null, 100);
            Version.SaveManifest(manifest);
        }

        public override List<string> GetAllItemsToDownload()
        {
            var list = new List<string>();
            var manifest = MinecraftManifest.AnalyzeFromVersion(this.Version);
            var id = manifest.id;
            var UseBMCL = LauncherMain.Instance.Settings.UseBMCL;
            downloadVersionInfos = new List<OptifineDownloadVersionInfo>();
            using (var wc = new WebClient())
            {
                if (UseBMCL)
                {
                    var jsonurl = string.Format(BMCLAPI_OPTIFINE_LIST_FORMAT, id);
                    var seri = new JavaScriptSerializer();
                    var itemList = seri.Deserialize<List<BMCLOptifineDownloadItem>>(
                        wc.DownloadString(jsonurl)
                        );
                    foreach (var item in itemList)
                    {
                        var listItem = new OptifineDownloadVersionInfo();
                        listItem.MCVersion = item.mcversion;
                        listItem.Type = item.type;
                        listItem.Patch = item.patch;
                        listItem.Url = string.Format(
                            "https://bmclapi2.bangbang93.com/optifine/{0}/{1}/{2}",
                            item.mcversion,
                            item.type,
                            item.patch
                            );
                        downloadVersionInfos.Add(listItem);
                        list.Add(listItem.ToString());
                    }
                }
                else
                {
                    var downloads = wc.DownloadString(OPTIFINE_OFFICAL_DOWNLOADS);
                    Match main = Regex.Match(
                        downloads,
                        string.Format("<h2>Minecraft {0}</h2>((.|\n)*?)</table>", id.Replace(".", "\\."))
                        );
                    MatchCollection matches = Regex.Matches(
                        main.Value,
                        "<tr class='downloadLine.*?'>\n" +
                        "<td class='downloadLineFile.*?'>(.*?)</td>\n" +
                        "<td class='downloadLineDownload.*?'><a href =.*?>.*?</a></td>\n" +
                        "<td class='downloadLineMirror'><a href =\"(.*?)\">.*?</a></td>\n" +
                        "<td class='downloadLineChangelog'><a href=.*?>.*?</a></td>\n" +
                        "<td class='downloadLineDate'>(.*?)</td>"
                        );
                    foreach (Match match in matches)
                    {
                        var groups = EnumeratorUtils.MakeListFromEnumerator(
                            (IEnumerator<string>)match.Groups.GetEnumerator()
                            );
                        var name = groups[1];
                        var mirror = groups[2];
                        var jar = EnumeratorUtils.MakeListFromEnumerator(
                            (IEnumerator<string>)Regex.Match(
                                mirror,
                                "^http://optifine.net/adloadx?f=(.*)$"
                                ).Groups.GetEnumerator()
                            )[1];
                        var adsite = wc.DownloadString(mirror);
                        var uri = Regex.Match(adsite, string.Format("downloadx?f={0}&x=.*?'", jar)).Value;
                        uri = uri.Substring(0, uri.Length - 1);
                        var url = "https://optifine.net/" + uri;
                        var info = new OptifineDownloadVersionInfo();
                        var args = name.Split(' ');
                        var mcversion = args[1];
                        var type = args[2] + "_" + args[3];
                        var patch = args[4];
                        info.Url = url;
                        downloadVersionInfos.Add(info);
                        list.Add(info.ToString());
                    }
                }
            }
            return list;
        }
    }
}
