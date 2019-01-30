using MFML.Core;
using MFML.Game;
using MFML.Game.BMCLAPI;
using MFML.Utils;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MFML.Download
{
    public class OptifineDownloadItem : DownloadItem
    {
        public OptifineDownloadItem(MinecraftVersion MCVersion, string Type, string Patch, string Url)
        {
            this.MCVersion = MCVersion;
            this.Type = Type;
            this.Patch = Patch;
            this.Url = Url;
        }

        public MinecraftVersion MCVersion { get; private set; }
        public string Type { get; private set; }
        public string Patch { get; private set; }
        public string Url { get; private set; }

        public override string Description => string.Format("Optifine {0} {1} {2}", MCVersion, Type.Replace('_', ' '), Patch);

        public override event DownloadProgress OnProgressChanged;

        public override void Download()
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            MCVersion.InstallLaunchWrapper();
            var url = this.Url;
            var versionText = string.Format("{0}_{1}_{2}", this.MCVersion, this.Type, this.Patch);
            var jarname = "Optifine-" + versionText + ".jar";
            var dirpath = "optifine\\Optifine\\" + versionText + "\\";
            var downloadDirPath = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\" + dirpath;
            if (!Directory.Exists(downloadDirPath))
            {
                Directory.CreateDirectory(downloadDirPath);
            }
            var path = dirpath + jarname;
            var downloadPath = downloadDirPath + jarname;
            var manifest = MinecraftManifest.AnalyzeFromVersion(this.MCVersion);
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
            MCVersion.SaveManifest(manifest);
        }
    }

    public class OptifineDownloader : Downloader
    {
        const string OPTIFINE_OFFICAL_DOWNLOADS = "https://www.optifine.net/downloads";
        const string BMCLAPI_OPTIFINE_LIST_FORMAT = "https://bmclapi2.bangbang93.com/optifine/{0}";

        readonly MinecraftVersion Version;
        readonly bool UseBMCL;

        List<OptifineDownloadItem> downloadVersionInfos;

        public OptifineDownloader(bool UseBMCL, MinecraftVersion Version)
        {
            this.Version = Version;
            this.UseBMCL = UseBMCL;
        }

        public override List<DownloadItem> GetAllItemsToDownload()
        {
            var list = new List<DownloadItem>();
            var manifest = MinecraftManifest.AnalyzeFromVersion(this.Version);
            var id = manifest.id;
            downloadVersionInfos = new List<OptifineDownloadItem>();
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
                        var MCVersion = this.Version;
                        var Type = item.type;
                        var Patch = item.patch;
                        var Url = string.Format(
                            "https://bmclapi2.bangbang93.com/optifine/{0}/{1}/{2}",
                            item.mcversion,
                            item.type,
                            item.patch
                            );
                        var listItem = new OptifineDownloadItem(MCVersion, Type, Patch, Url);
                        downloadVersionInfos.Add(listItem);
                        list.Add(listItem);
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
                        "<tr class='downloadLine.*?'>\\n<td class='downloadLineFile.*?'>(.*?)</td>\\n<td class='downloadLineDownload.*?'><a href=.*?>Download</a></td>\\n<td class='downloadLineMirror'><a href=\"(.*?)\">&nbsp; \\(mirror\\)</a></td>"
                        );
                    foreach (Match match in matches)
                    {
                        var groups = EnumeratorUtils.MakeListFromEnumerator(
                            match.Groups.GetEnumerator()
                            );
                        var name = ((Group)groups[1]).Value;
                        var mirror = ((Group)groups[2]).Value;
                        var t = EnumeratorUtils.MakeListFromEnumerator(
                            Regex.Match(
                                mirror,
                                "http://optifine.net/adloadx\\?f=(.*)"
                                ).Groups.GetEnumerator()
                            );
                        var jar = ((Group)t[1]).Value;
                        var adsite = wc.DownloadString(mirror);
                        var uri = Regex.Match(adsite, string.Format("downloadx\\?f={0}&x=.*?'", jar.Replace(".", "\\."))).Value;
                        uri = uri.Substring(0, uri.Length - 1);
                        var url = "https://optifine.net/" + uri;
                        var args = name.Split(' ');
                        var type = args[2] + "_" + args[3];
                        var patch = args[4];
                        var info = new OptifineDownloadItem(Version, type, patch, url);
                        downloadVersionInfos.Add(info);
                        list.Add(info);
                    }
                }
            }
            return list;
        }
    }
}
