using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFML.UI
{
    public class MFMLExceptionShowBox : MFMLMessageBox
    {
        protected MFMLExceptionShowBox(Exception e) :
            base("错误", "MFML运行中出现未处理的异常。面向开发者的详细信息：\r\n" + e.ToString(), MessageBoxButtons.OK)
        {
            this.Size = new Size(1000, 700);
            this.textLabel.Font = new Font(FontFamily.GenericMonospace, 9);
        }

        public static DialogResult ShowExceptionBox(Exception e)
        {
            var form = new MFMLExceptionShowBox(e);
            DialogResult r = form.ShowDialog();
            form.Dispose();
            return r;
        }
    }
}
