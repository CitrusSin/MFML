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
using System.Web.Script.Serialization;

namespace MFML.Download
{
    public class MinecraftDownloadItem : DownloadItem
    {
        public override event DownloadProgress OnProgressChanged;

        public string Id { get; private set; }
        public string Type { get; private set; }
        public string Url { get; private set; }
        public string ReleaseTime { get; private set; }

        private readonly bool UseBMCLAPI;
        private readonly string MinecraftFolderName;
        private readonly string MinecraftAssetsURL;

        public MinecraftDownloadItem(bool UseBMCLAPI, string MinecraftFolderName, string Id, string Type, string Url, string ReleaseTime)
        {
            this.UseBMCLAPI = UseBMCLAPI;
            this.MinecraftFolderName = MinecraftFolderName;
            this.Id = Id;
            this.Type = Type;
            this.Url = Url;
            this.ReleaseTime = ReleaseTime;
            this.MinecraftAssetsURL =
                UseBMCLAPI ?
                "http://bmclapi2.bangbang93.com/assets/" :
                "https://resources.download.minecraft.net/";
        }

        public override string Description => string.Format("版本：{0} 状态：{1} 发布时间：{2}", Id, Type, ReleaseTime);

        public override void Download()
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            var jsonURL = this.Url;
            var id = this.Id;
            var MCVersion = new MinecraftVersion(id);
            Directory.CreateDirectory(MCVersion.VersionDirectory);
            MinecraftManifest info;
            // Solve json and download jar
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.DownloadFile(jsonURL, MCVersion.VersionManifestPath);
                info = MinecraftManifest.AnalyzeFromVersion(MCVersion);
                OnProgressChanged("已获取版本下载信息", 0);
                string jarloc = info.downloads.client.url;
                if (UseBMCLAPI)
                {
                    jarloc = string.Format("https://bmclapi2.bangbang93.com/version/{0}/client", id);
                }
                var DownloadHasDone = false;
                OnProgressChanged(string.Format("正在从{0}下载{1}.jar", jarloc, id), 0);
                wc.DownloadProgressChanged += (sender, e) => OnProgressChanged(null, e.ProgressPercentage);
                wc.DownloadFileCompleted += (sender, e) => DownloadHasDone = true;
                wc.DownloadFileAsync(new Uri(jarloc), MCVersion.JarPath);
                while (!DownloadHasDone)
                {
                    Thread.Sleep(500);
                }
                OnProgressChanged("minecraft.jar已下载", 0);
            }
            // Analyze libraries and download
            OnProgressChanged("开始分析需要下载的前置库和本地化前置", 0);
            DownloadLibraries(info, MCVersion);
            OnProgressChanged("开始下载资源文件", 0);
            DownloadAssets(info);
            LauncherMain.Instance.AddMinecraftVersion(MCVersion);
        }

        private void DownloadAssets(MinecraftManifest info)
        {
            var assetsFolder = MinecraftFolderName + "assets\\";
            if (!Directory.Exists(assetsFolder))
            {
                Directory.CreateDirectory(assetsFolder);
            }
            var indexesFolder = assetsFolder + "indexes\\";
            if (!Directory.Exists(indexesFolder))
            {
                Directory.CreateDirectory(indexesFolder);
            }
            var objectsFolder = assetsFolder + "objects\\";
            if (!Directory.Exists(objectsFolder))
            {
                Directory.CreateDirectory(objectsFolder);
            }
            var id = info.assetIndex.id;
            var url = info.assetIndex.url;
            var indexPath = indexesFolder + id + ".json";
            if (!File.Exists(indexPath))
            {
                OnProgressChanged("正在下载并解析资源目录", 0);
                var seri = new JavaScriptSerializer();
                Dictionary<string, Dictionary<string, Dictionary<string, object>>> dict;
                using (var wc = new WebClient())
                {
                    dict = seri.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(wc.DownloadString(url));
                }
                var sw = new StreamWriter(new FileStream(indexPath, FileMode.Create));
                sw.Write(seri.Serialize(dict));
                sw.Close();
                OnProgressChanged("正在下载资源文件", 0);
                var objects = dict["objects"];
                const int MAX_DOWNLOADING = 100;
                int downloading = 0;
                int downloaded = 0;
                int needDownload = 0;
                foreach (var kvpair in objects)
                {
                    var resName = kvpair.Key;
                    var resInfo = kvpair.Value;
                    var hash = (string)resInfo["hash"];
                    var hashPath = hash.Substring(0, 2) + '\\' + hash;
                    var hashurl = MinecraftAssetsURL + hashPath;
                    var localPath = objectsFolder + hashPath;
                    if (!Directory.Exists(objectsFolder + hash.Substring(0, 2)))
                    {
                        Directory.CreateDirectory(objectsFolder + hash.Substring(0, 2));
                    }
                    while (downloading >= MAX_DOWNLOADING)
                    {
                        Thread.Sleep(10);
                    }
                    var wc = new WebClient();
                    wc.DownloadFileCompleted += (a, b) =>
                    {
                        downloaded++;
                        OnProgressChanged(string.Format("已下载{0}。。。", resName), (int)((downloaded / (float)objects.Count) * 100));
                        wc.Dispose();
                        downloading--;
                    };
                    wc.DownloadProgressChanged += (o, e) => Debug.WriteLine(string.Format("{0} Downloaded {1}", resName, e.ProgressPercentage));
                    wc.DownloadFileAsync(new Uri(hashurl), localPath);
                    needDownload++;
                    downloading++;
                }
                while (downloaded < needDownload)
                {
                    Thread.Sleep(500);
                }
            }
        }

        private void DownloadLibraries(MinecraftManifest info, MinecraftVersion MCVersion)
        {
            List<MinecraftLibrary> libraries = info.libraries;
            var libFolder = MinecraftFolderName + "libraries\\";
            int AllDownloadCount = 0;
            int DownloadedCount = 0;
            var nativesAccessLock = new object();
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
                    var downloads = lib.downloads;
                    if (lib.natives != null)
                    {
                        var natives = lib.natives;
                        var classifier = natives["windows"].Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "86");
                        var classifiers = downloads.classifiers;
                        var item = classifiers[classifier].Clone() as DownloadInfo;
                        var path = (libFolder + item.path).Replace('/', '\\');
                        if (!File.Exists(path))
                        {
                            if (UseBMCLAPI)
                            {
                                item.url = "https://bmclapi2.bangbang93.com/libraries/" + item.path;
                            }
                            AllDownloadCount++;
                            item.DownloadAsync(path, (sender, arg) =>
                            {
                                var NativesPath = MCVersion.NativesPath;
                                Directory.CreateDirectory(NativesPath);
                                lock (nativesAccessLock)
                                {
                                    var zip = ZipFile.Open(path, ZipArchiveMode.Read);
                                    zip.ExtractToDirectory(NativesPath);
                                    if (Directory.Exists(NativesPath + "META-INF\\"))
                                    {
                                        Directory.Delete(NativesPath + "META-INF\\", true);
                                    }
                                }
                                DownloadedCount++;
                            });
                            OnProgressChanged(string.Format("开始从{0}下载文件。。。", item.url), 0);
                        }
                        else
                        {
                            var NativesPath = MCVersion.NativesPath;
                            Directory.CreateDirectory(NativesPath);
                            lock (nativesAccessLock)
                            {
                                var zip = ZipFile.Open(path, ZipArchiveMode.Read);
                                zip.ExtractToDirectory(NativesPath);
                                if (Directory.Exists(NativesPath + "META-INF\\"))
                                {
                                    Directory.Delete(NativesPath + "META-INF\\", true);
                                }
                            }
                        }
                    }
                    if (downloads.artifact != null)
                    {
                        var artifact = downloads.artifact.Clone() as DownloadInfo;
                        var path = (libFolder + artifact.path).Replace('/', '\\');
                        if (!File.Exists(path))
                        {
                            if (UseBMCLAPI)
                            {
                                artifact.url = "https://bmclapi2.bangbang93.com/libraries/" + artifact.path;
                            }
                            AllDownloadCount++;
                            artifact.DownloadAsync(path, (sender, arg) =>
                            {
                                DownloadedCount++;
                            });
                            OnProgressChanged(string.Format("开始从{0}下载文件。。。", artifact.url), 0);
                        }
                    }
                }
            }
            while (DownloadedCount != AllDownloadCount)
            {
                OnProgressChanged(null, (int)((DownloadedCount / (double)AllDownloadCount) * 100));
                Thread.Sleep(500);
            }
        }
    }

    public class MinecraftDownloader : Downloader
    {
        readonly string MinecraftVersionManifestURL = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

        public readonly bool UseBMCLAPI;
        public readonly string MinecraftFolderName;

        public MinecraftDownloader(bool UseBMCLAPI, string MinecraftFolderName)
        {
            this.UseBMCLAPI = UseBMCLAPI;
            this.MinecraftFolderName = MinecraftFolderName;
        }

        public override List<DownloadItem> GetAllItemsToDownload()
        {
            var l = new List<DownloadItem>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(MinecraftVersionManifestURL);
                var matches = Regex.Matches(json,
                    "{\"id\": \"(.*?)\", \"type\": \"(.*?)\"," +
                    " \"url\": \"(.*?)\", \"time\": \"(.*?)\"," +
                    " \"releaseTime\": \"(.*?)\"}");
                foreach (Match m in matches)
                {
                    var e = m.Groups.GetEnumerator();
                    var glist = EnumeratorUtils.MakeListFromEnumerator(e);
                    var id = ((Group)glist[1]).Value;
                    var type = ((Group)glist[2]).Value;
                    var url = ((Group)glist[3]).Value;
                    var releaseTime = ((Group)glist[5]).Value;
                    var vi = new MinecraftDownloadItem(UseBMCLAPI, MinecraftFolderName, id, type, url, releaseTime);
                    l.Add(vi);
                }
            }
            return l;
        }
    }
}
