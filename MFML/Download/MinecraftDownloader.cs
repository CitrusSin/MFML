using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.IO.Compression;
using MFML.Core;
using MFML.Game;

namespace MFML.Download
{
    public class MinecraftDownloader : IDownloader
    {
        public class MinecraftVerItemInfo : DownloadItemInfo
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Url { get; set; }
            public string ReleaseTime { get; set; }
            public override string ToString()
            {
                return string.Format("版本：{0} 状态：{1} 发布时间：{2}", Id, Type, Url);
            }
        }

        const string VERSION_MANIFEST = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
        const string ASSETS_ROOT = "https://resources.download.minecraft.net/";

        public void Download(DownloadItemInfo content, DownloadProgress SetProgress)
        {
            var UseBmcl = LauncherMain.Instance.Settings.UseBMCL;
            var jsonURL = ((MinecraftVerItemInfo)content).Url;
            var id = ((MinecraftVerItemInfo)content).Id;
            var MCVersion = new MinecraftVersion(id);
            Directory.CreateDirectory(MCVersion.VersionDirectory);
            Dictionary<string, object> dict;
            // Solve json
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(jsonURL);
                SetProgress("已获取版本下载信息", 0);
                var seri = new JavaScriptSerializer();
                dict = seri.Deserialize<Dictionary<string, object>>(json);
                var sw = new StreamWriter(new FileStream(MCVersion.VersionJsonPath, FileMode.Create));
                sw.Write(seri.Serialize(dict));
                sw.Close();
            }
            // Download MC jar
            using (WebClient wc = new WebClient())
            {
                string jarloc;
                if (UseBmcl)
                    jarloc = "https://bmclapi2.bangbang93.com/version/" + id + "/client";
                else
                    jarloc =
                        (string)((Dictionary<string, object>)((Dictionary<string, object>)dict["downloads"])["client"])["url"];
                var DownloadHasDone = false;
                SetProgress(string.Format("正在从{0}下载{1}.jar", jarloc, id), 0);
                wc.DownloadProgressChanged += (sender, e) => SetProgress(null, e.ProgressPercentage);
                wc.DownloadFileCompleted += (sender, e) => DownloadHasDone = true;
                wc.DownloadFileAsync(new Uri(jarloc), MCVersion.JarPath);
                while (!DownloadHasDone)
                    Thread.Sleep(500);
                SetProgress("minecraft.jar已下载", 0);
            }
            // Analyze libraries and download
            SetProgress("开始分析需要下载的前置库和本地化前置", 0);
            DownloadLibraries(dict, MCVersion, SetProgress);
            SetProgress("开始下载资源文件", 0);
            DownloadAssets(dict, MCVersion, SetProgress);
            LauncherMain.Instance.AddMinecraftVersion(MCVersion);
        }

        private void DownloadAssets(Dictionary<string, object> JsonDict, MinecraftVersion MCVersion, DownloadProgress SetProgress)
        {
            var assetsFolder = LauncherMain.Instance.Settings.MinecraftFolderName + "assets\\";
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
            var assetIndex = (Dictionary<string, object>)JsonDict["assetIndex"];
            var id = (string)assetIndex["id"];
            var url = (string)assetIndex["url"];
            var indexPath = indexesFolder + id + ".json";
            SetProgress("正在下载并解析资源目录", 0);
            var seri = new JavaScriptSerializer();
            Dictionary<string, Dictionary<string, Dictionary<string, object>>> dict;
            using (var wc = new WebClient())
            {
                dict = seri.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(wc.DownloadString(url));
            }
            var sw = new StreamWriter(new FileStream(indexPath, FileMode.Create));
            sw.Write(seri.Serialize(dict));
            sw.Close();
            SetProgress("正在下载资源文件", 0);
            var objects = dict["objects"];
            int AllDownloadCount = 0;
            int DownloadedCount = 0;
            foreach (var kvpair in objects)
            {
                var resName = kvpair.Key;
                var resInfo = kvpair.Value;
                var hash = (string)resInfo["hash"];
                var hashPath = hash.Substring(0, 2) + '\\' + hash;
                var hashurl = ASSETS_ROOT + hashPath;
                var localPath = objectsFolder + hashPath;
                if (!Directory.Exists(objectsFolder + hash.Substring(0, 2)))
                {
                    Directory.CreateDirectory(objectsFolder + hash.Substring(0, 2));
                }
                AllDownloadCount++;
                var wc = new WebClient();
                wc.DownloadFileCompleted += (sender, args) =>
                {
                    DownloadedCount++;
                    wc.Dispose();
                };
                wc.DownloadFileAsync(new Uri(hashurl), localPath);
                SetProgress(string.Format("开始下载{0}。。。", resName), 0);
            }
            while (DownloadedCount != AllDownloadCount)
            {
                SetProgress(null, (int)((DownloadedCount / (double)AllDownloadCount) * 100));
                Thread.Sleep(500);
            }
        }

        private void DownloadLibraries(Dictionary<string, object> JsonDict, MinecraftVersion MCVersion, DownloadProgress SetProgress)
        {
            ArrayList libraries = (ArrayList)JsonDict["libraries"];
            var libFolder = LauncherMain.Instance.Settings.MinecraftFolderName + "libraries\\";
            int AllDownloadCount = 0;
            int DownloadedCount = 0;
            var nativesAccessLock = new object();
            foreach (var libO in libraries)
            {
                var lib = (Dictionary<string, object>)libO;
                bool NeedThisLib = true;
                bool hasRules = lib.TryGetValue("rules", out object rulesO);
                if (hasRules)
                {
                    var needstate = new Dictionary<string, bool>
                    {
                        ["windows"] = false,
                        ["osx"] = false,
                        ["linux"] = false
                    };
                    var rules = (ArrayList)rulesO;
                    foreach (var ruleO in rules)
                    {
                        var rule = (Dictionary<string, object>)ruleO;
                        var action = (string)rule["action"];
                        bool hasOSDistricts = rule.TryGetValue("os", out object osDistrictsO);
                        if (hasOSDistricts)
                        {
                            var osDistricts = (Dictionary<string, object>)osDistrictsO;
                            var osName = (string)osDistricts["name"];
                            needstate[osName] = (action == "allow");
                        }
                        else
                        {
                            needstate["windows"] = (action == "allow");
                            needstate["osx"] = (action == "allow");
                            needstate["linux"] = (action == "allow");
                        }
                    }
                    NeedThisLib = needstate["windows"];
                }
                if (NeedThisLib)
                {
                    var downloads = (Dictionary<string, object>)lib["downloads"];
                    bool IsNatives = lib.TryGetValue("natives", out object nativesO);
                    if (IsNatives)
                    {
                        var classifier = (string)((Dictionary<string, object>)nativesO)["windows"];
                        var classifiers = (Dictionary<string, object>)downloads["classifiers"];
                        var item = (Dictionary<string, object>)classifiers[classifier];
                        var path = (libFolder + (string)item["path"]).Replace('/', '\\');
                        var url = (string)item["url"];
                        AllDownloadCount++;
                        var wc = new WebClient();
                        wc.DownloadFileCompleted += (sender, arg) =>
                        {
                            DownloadedCount++;
                            var nativesLoc = MCVersion.VersionDirectory + MCVersion.VersionName + "-natives\\";
                            Directory.CreateDirectory(nativesLoc);
                            lock (nativesAccessLock)
                            {
                                var zip = ZipFile.Open(path, ZipArchiveMode.Read);
                                zip.ExtractToDirectory(nativesLoc);
                                if (Directory.Exists(nativesLoc + "META-INF\\"))
                                {
                                    Directory.Delete(nativesLoc + "META-INF\\", true);
                                }
                            }
                            wc.Dispose();
                        };
                        var downdir = path.Substring(0, path.LastIndexOf('\\') + 1);
                        if (!Directory.Exists(downdir))
                        {
                            Directory.CreateDirectory(downdir);
                        }
                        wc.DownloadFileAsync(new Uri(url), path);

                        SetProgress(string.Format("开始从{0}下载文件。。。", url), 0);
                    }
                    bool HasArtifact = downloads.TryGetValue("artifact", out object artifactO);
                    if (HasArtifact)
                    {
                        var Artifact = (Dictionary<string, object>)artifactO;
                        var path = (libFolder + (string)Artifact["path"]).Replace('/', '\\');
                        var url = (string)Artifact["url"];
                        AllDownloadCount++;
                        var wc = new WebClient();
                        wc.DownloadFileCompleted += (sender, arg) =>
                        {
                            DownloadedCount++;
                            wc.Dispose();
                        };
                        var downdir = path.Substring(0, path.LastIndexOf('\\') + 1);
                        if (!Directory.Exists(downdir))
                        {
                            Directory.CreateDirectory(downdir);
                        }
                        wc.DownloadFileAsync(new Uri(url), path);
                        SetProgress(string.Format("开始从{0}下载文件。。。", url), 0);
                    }
                }
            }
            while (DownloadedCount != AllDownloadCount)
            {
                SetProgress(null, (int)((DownloadedCount / (double)AllDownloadCount) * 100));
                Thread.Sleep(500);
            }
        }

        public List<DownloadItemInfo> GetAllItemsToDownload()
        {
            List<DownloadItemInfo> l = new List<DownloadItemInfo>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(VERSION_MANIFEST);
                var matches = Regex.Matches(json,
                    "{\"id\": \"(.*?)\", \"type\": \"(.*?)\"," +
                    " \"url\": \"(.*?)\", \"time\": \"(.*?)\"," +
                    " \"releaseTime\": \"(.*?)\"}");
                foreach (Match m in matches)
                {
                    var e = m.Groups.GetEnumerator();
                    e.MoveNext();
                    e.MoveNext();
                    var id = ((Group)e.Current).Value;
                    e.MoveNext();
                    var type = ((Group)e.Current).Value;
                    e.MoveNext();
                    var url = ((Group)e.Current).Value;
                    e.MoveNext();
                    e.MoveNext();
                    var releaseTime = ((Group)e.Current).Value;
                    var vi = new MinecraftVerItemInfo
                    {
                        Id = id,
                        Type = type,
                        Url = url,
                        ReleaseTime = releaseTime
                    };
                    l.Add(vi);
                }
            }
            return l;
        }
    }
}
