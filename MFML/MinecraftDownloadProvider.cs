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
        public class VersionInfo : DownloadItemInfo
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

        const string VERSION_MANIFEST = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

        public void Download(DownloadItemInfo content, DownloadProgress SetProgress)
        {
            var UseBmcl = MFML.Instance.Settings.UseBMCL;
            var url = ((VersionInfo)content).url;
            var id = ((VersionInfo)content).id;
            var mc = MFML.Instance.Settings.MinecraftFolderName;
            var sb = new StringBuilder(mc);
            if (mc.Last() != '\\') sb.Append('\\');
            sb.Append("versions\\");
            sb.Append(id);
            sb.Append("\\");
            var dir = sb.ToString();
            Directory.CreateDirectory(dir);
            Dictionary<string, object> dict;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
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
            
            MFML.Instance.AddMinecraftVersion(new MinecraftVersion(id));
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
