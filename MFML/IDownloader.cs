using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFML
{
    public class DownloadItemInfo {}

    public delegate void DownloadProgress(string status, int percent);

    public interface IDownloader
    {
        List<DownloadItemInfo> GetAllItemsToDownload();
        void Download(DownloadItemInfo content, DownloadProgress SetProgress);
    }
}
