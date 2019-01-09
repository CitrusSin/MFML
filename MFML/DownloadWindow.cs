using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MFML
{
    public partial class DownloadWindow : Form
    {
        IDownloadProvider Provider;
        List<DownloadItemInfo> Items;

        public DownloadWindow(IDownloadProvider Provider)
        {
            this.Provider = Provider;
            InitializeComponent();
        }

        private bool DragMouse = false;
        private Point MouseDragPoint;
        private Color ThemeColor1 = Color.DeepSkyBlue;

        public Color ThemeColor
        {
            get { return ThemeColor1; }
            set
            {
                ThemeColor1 = value;
                CloseButton.BackColor = value;
                MinimizeButton.BackColor = value;
                BackColor = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Y <= 30 && e.Button == MouseButtons.Left)
            {
                DragMouse = true;
                MouseDragPoint = e.Location;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (DragMouse)
            {
                SetDesktopLocation
                    (
                    MousePosition.X - MouseDragPoint.X,
                    MousePosition.Y - MouseDragPoint.Y
                    );
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            DragMouse = false;
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = CreateGraphics();
            float h = SystemFonts.CaptionFont.GetHeight();
            RectangleF textRect = new RectangleF(10, 15 - (h / 2), Width - 60, 15 + (h / 2));
            Brush b = new SolidBrush(ThemeColor);
            g.FillRectangle(b, textRect);
            b.Dispose();
            g.DrawString(Text, SystemFonts.CaptionFont, Brushes.White, textRect);
            g.DrawLine(Pens.Black, 0, 0, Width - 1, 0);                  // Draw black border
            g.DrawLine(Pens.Black, Width - 1, 0, Width - 1, Height - 1);
            g.DrawLine(Pens.Black, Width - 1, Height - 1, 0, Height - 1);
            g.DrawLine(Pens.Black, 0, Height - 1, 0, 0);
            g.Dispose();
        }

        private void TopBarButton_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Red;
        }

        private void TopBarButton_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = ThemeColor;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void DownloadWindow_Load(object sender, EventArgs e)
        {
            listBox1.Enabled = false;
            CloseButton.Enabled = false;
            SetProgress("加载所有可下载版本中。。。", 0);
            downloader.RunWorkerAsync("init");
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            CloseButton.Enabled = false;
            listBox1.Enabled = false;
            downloader.RunWorkerAsync(listBox1.SelectedItem);
        }

        private void downloader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is string && (string)e.Argument == "init")
            {
                Items = Provider.GetAllItemsToDownload();
                e.Result = 0;
            }
            else
            {
                Provider.Download((DownloadItemInfo)e.Argument,
                    (a, b) => Invoke(new Action<string, int>(SetProgress), a, b));
                e.Result = 1;
            }
        }

        public void SetProgress(string status, int progress)
        {
            if (status != null)
                textBox1.Text += status + "\r\n";
            progressBar1.Value = progress;
        }

        private void downloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 0)
                listBox1.Items.AddRange(Items.ToArray());
            listBox1.Enabled = true;
            CloseButton.Enabled = true;
            SetProgress("已完成！", 100);
        }
    }
}
