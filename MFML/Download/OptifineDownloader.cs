using MFML.Core;
using MFML.Game;
using MFML.Game.BMCLAPI;
using MFML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MFML.Download
{
    public class OptifineDownloader : Downloader
    {
        private class OptifineDownloadVersionInfo
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public override string ToString()
            {
                return string.Format("版本：{0}", Name);
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
            throw new NotImplementedException();
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
                        listItem.Name = item.type;
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
                        info.Name = name;
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
