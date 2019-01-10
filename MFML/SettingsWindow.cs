using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MFML
{
    public partial class SettingsWindow : Form
    {

        private bool DragMouse = false;
        private Point MouseDragPoint;
        private Color ThemeColor1 = Color.DeepSkyBlue;
        private MFML Instance;

        public Color ThemeColor
        {
            get { return ThemeColor1; }
            set
            {
                ThemeColor1 = value;
                CloseButton.BackColor = value;
                MinimizeButton.BackColor = value;
                colorLabel1.ForeColor = value;
                colorLabel2.ForeColor = value;
                colorLabel3.ForeColor = value;
                BackColor = value;
            }
        }

        public SettingsWindow(MFML Instance)
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
            Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            ThemeColor = Instance.Settings.ThemeColor;
            javaPathBox.Text = Instance.Settings.JREPath;
            memoryBox.Text = Instance.Settings.MaxMemory.ToString();
            mcFolderBox.Text = Instance.Settings.MinecraftFolderName;
            BMCLAPIBox.Checked = Instance.Settings.UseBMCL;
        }

        private void javaPathBox_Leave(object sender, EventArgs e)
        {
            Instance.Settings.JREPath = javaPathBox.Text;
        }

        private void memoryBox_Leave(object sender, EventArgs e)
        {
            Instance.Settings.MaxMemory = int.Parse(memoryBox.Text);
        }

        private void mcFolderBox_Leave(object sender, EventArgs e)
        {
            Instance.Settings.MinecraftFolderName = mcFolderBox.Text;
        }

        private void BMCLAPIBox_CheckedChanged(object sender, EventArgs e)
        {
            Instance.Settings.UseBMCL = BMCLAPIBox.Checked;
        }
    }
}
