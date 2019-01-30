using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroFramework;
using MFML.Core;
using MFML.Game;

namespace MFML.UI
{
    public partial class MainWindow : Form
    {
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassLong(IntPtr hwnd, int nIndex);
        private bool DragMouse;
        private Point MouseDragPoint;
        private Color ThemeColor1 = Color.DeepSkyBlue;
        private readonly LauncherMain Instance;

        public Color ThemeColor
        {
            get { return ThemeColor1; }
            set
            {
                ThemeColor1 = value;
                CloseButton.BackColor = value;
                MinimizeButton.BackColor = value;
                settingsButton.BackColor = value;
                downloadGame.BackColor = value;
                startMCButton.BackColor = value;
                BackColor = value;
            }
        }

        public MainWindow(LauncherMain Instance)
        {
            this.Instance = Instance;
            InitializeComponent();
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
            Font UsedFont = MetroFonts.Label(MetroLabelSize.Medium, MetroLabelWeight.Regular);
            float h = UsedFont.GetHeight();
            RectangleF textRect = new RectangleF(10, 15 - (h / 2), Width - 60, 15 + (h / 2));
            Brush b = new SolidBrush(ThemeColor);
            g.FillRectangle(b, textRect);
            b.Dispose();
            g.DrawString(Text, UsedFont, Brushes.White, textRect);
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
            Instance.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void playerNameBox_Leave(object sender, EventArgs e)
        {
            if (playerNameBox.Text != "")
            {
                Instance.Settings.PlayerName = playerNameBox.Text;
            }
            else
            {
                MFMLMessageBox.ShowMessageBox(this, "错误", "游戏名不能为空！", MessageBoxButtons.OK);
                this.playerNameBox.Focus();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            Instance.FormInitalization(this);
            ThemeColor = Instance.Settings.ThemeColor;
            playerNameBox.Text = Instance.Settings.PlayerName;
            versionsBox.Items.AddRange(Instance.MinecraftVersions.ToArray());
            var mcversion = Instance.Settings.SelectedVersion;
            if (mcversion == "")
            {
                if (Instance.MinecraftVersions.FirstOrDefault() != default(MinecraftVersion))
                {
                    versionsBox.SelectedItem = Instance.MinecraftVersions.FirstOrDefault();
                    Instance.Settings.SelectedVersion = Instance.MinecraftVersions.FirstOrDefault().VersionName;
                }
            }
            else
            {
                versionsBox.SelectedItem = Instance.MinecraftVersions.Find(v => v.VersionName == mcversion);
            }
        }

        private void versionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Instance.Settings.SelectedVersion = versionsBox.SelectedItem.ToString();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsWindow ui = new SettingsWindow(Instance);
            ui.ShowDialog(this);
            if (!ui.IsDisposed)
            {
                ui.Dispose();
            }
        }

        private void downloadGame_Click(object sender, EventArgs e)
        {
            Instance.ShowDownloadMinecraftList();
        }

        public void AddVersion(MinecraftVersion ver)
        {
            Invoke(new Func<object, int>(versionsBox.Items.Add), ver);
        }

        private void startMCButton_Click(object sender, EventArgs e)
        {
            if (playerNameBox.Text.Length != 0)
            {
                this.startMCButton.Text = "启动中。。。";
                this.startMCButton.Enabled = false;
                Instance.RunMinecraft((MinecraftVersion)versionsBox.SelectedItem);
                this.startMCButton.Text = "启动Minecraft";
                this.startMCButton.Enabled = true;
            }
            else
            {
                MFMLMessageBox.ShowMessageBox(this, "错误", "请先设置账户或游戏名！", MessageBoxButtons.OK);
            }
        }

        private void optifineButton_Click(object sender, EventArgs e)
        {
            if (versionsBox.SelectedItem != null)
            {
                Instance.ShowDownloadOptifineList(versionsBox.SelectedItem as MinecraftVersion);
            }
            else
            {
                MFMLMessageBox.ShowMessageBox(this, "错误", "安装Optifine需要选定一个游戏版本！\r\n" +
                    "如果你暂时没有任何游戏版本可供下载，请点击\"添加版本\"来下载一个。", MessageBoxButtons.OK);
            }
        }

        private void forgeButton_Click(object sender, EventArgs e)
        {
            if (versionsBox.SelectedItem != null)
            {
                Instance.ShowDownloadForgeList(versionsBox.SelectedItem as MinecraftVersion);
            }
            else
            {
                MFMLMessageBox.ShowMessageBox(this, "错误", "安装Forge需要选定一个游戏版本！\r\n" +
                    "如果你暂时没有任何游戏版本可供下载，请点击\"添加版本\"来下载一个。", MessageBoxButtons.OK);
            }
        }
    }
}
