using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

namespace MFML
{
    public class MinecraftDownloadProvider : IDownloadProvider
    {
        class VersionInfo : DownloadItemInfo
        {
            public string id;
            public string type;
            public string url;
            public string releaseTime;
            public override string ToString()
            {
                return "版本：" + id + " 类型：" + type + " 发布时间："+releaseTime;
            }
        }

        struct DownloadTask
        {
            public string localeDir;
            public string url;
        }

        const string VERSION_MANIFEST = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

        public void Download(DownloadItemInfo content, DownloadProgress SetProgress)
        {
            
            var UseBmcl = MFML.Instance.Settings.UseBMCL;
            var jsonURL = ((VersionInfo)content).url;
            var id = ((VersionInfo)content).id;
            var MCVersion = new MinecraftVersion(id);
            var dir = MCVersion.VersionDirectory;
            Directory.CreateDirectory(dir);
            Dictionary<string, object> dict;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(jsonURL);
                var sw = new StreamWriter(new FileStream(dir+id+".json", FileMode.Create));
                sw.Write(json);
                sw.Close();
                SetProgress("已获取版本下载信息", 0);
                var seri = new JavaScriptSerializer();
                dict = seri.Deserialize<Dictionary<string, object>>(json);
            }
            using (WebClient wc = new WebClient())
            {
                // Download MC jar
                string jarloc;
                if (UseBmcl)
                    jarloc = "https://bmclapi2.bangbang93.com/version/" + id + "/client";
                else
                    jarloc =
                        (string)((Dictionary<string, object>)((Dictionary<string, object>)dict["downloads"])["client"])["url"];
                var DownloadHasDone = false;
                SetProgress(string.Format("正在从{0}下载minecraft.jar", jarloc), 0);
                wc.DownloadProgressChanged += (sender, e) => SetProgress(null, e.ProgressPercentage);
                wc.DownloadFileCompleted += (sender, e) => DownloadHasDone = true;
                wc.DownloadFileAsync(new Uri(jarloc), dir + id + ".jar");
                while (!DownloadHasDone)
                    Thread.Sleep(500);
                SetProgress("minecraft.jar已下载", 0);
            }
            List<Dictionary<string, object>> libraries = (List<Dictionary<string, object>>)dict["libraries"];
            SetProgress("开始分析需要下载的前置库和本地化前置", 0);
            var libFolder = MFML.Instance.Settings.MinecraftFolderName + "libraries\\";
            var tasks = new List<DownloadTask>();
            foreach (var lib in libraries)
            {
                bool NeedThisLib = true;
                object rulesO;
                bool hasRules = lib.TryGetValue("rules", out rulesO);
                if (hasRules)
                {
                    var needstate = new Dictionary<string, bool>();
                    needstate["windows"] = false;
                    needstate["osx"] = false;
                    needstate["linux"] = false;
                    var rules = (List<Dictionary<string, object>>)rulesO;
                    foreach (var rule in rules)
                    {
                        var action = (string)rule["action"];
                        object osDistrictsO;
                        bool hasOSDistricts = rule.TryGetValue("os", out osDistrictsO);
                        if (hasOSDistricts)
                        {
                            var osDistricts = (Dictionary<string, string>)osDistrictsO;
                            var osName = osDistricts["name"];
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
                    object nativesO;
                    bool IsNatives = lib.TryGetValue("natives", out nativesO);
                    if (IsNatives)
                    {
                        var classifier = ((Dictionary<string, string>)nativesO)["windows"];
                        var classifiers = (Dictionary<string, object>)downloads["classifiers"];
                        var item = (Dictionary<string, object>)classifiers[classifier];
                        DownloadTask task = new DownloadTask();
                        task.localeDir = libFolder + (string)item["path"];
                        task.url = (string)item["url"];
                        tasks.Add(task);
                    }
                }
            }
            MFML.Instance.AddMinecraftVersion(MCVersion);
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
                    //var time = ((Group)e.Current).Value;
                    e.MoveNext();
                    var releaseTime = ((Group)e.Current).Value;
                    var vi = new VersionInfo();
                    vi.id = id;
                    vi.type = type;
                    vi.url = url;
                    vi.releaseTime = releaseTime;
                    l.Add(vi);
                }
            }
            return l;
        }
    }
}
