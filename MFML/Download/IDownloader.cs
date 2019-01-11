using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFML.Download
{
    public interface IDownloader
    {
        List<DownloadItemInfo> GetAllItemsToDownload();
        void Download(DownloadItemInfo content, DownloadProgress SetProgress);
    }
}
