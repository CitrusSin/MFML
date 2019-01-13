﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MFML.Core;
using MFML.Game;

namespace MFML.UI
{
    public partial class MainWindow : Form
    {

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
            if (playerNameBox.TextLength != 0)
            {
                this.startMCButton.Text = "启动中。。。";
                this.startMCButton.Enabled = false;
                Instance.LaunchMinecraft((MinecraftVersion)versionsBox.SelectedItem);
            }
            else
            {
                MFMLMessageBox.ShowMessageBox(this, "错误", "请先设置账户或游戏名！", MessageBoxButtons.OK);
            }
        }
    }
}