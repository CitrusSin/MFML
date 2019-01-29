using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Download
{
    public abstract class DownloadItem
    {
        public virtual event DownloadProgress OnProgressChanged;
        public abstract string Description { get; }
        public abstract void Download();

        public override string ToString()
        {
            return this.Description;
        }
    }
}
