using MFML.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFML.UI
{
    public partial class MFMLMessageBox : Form
    {
        private List<Button> buttons = new List<Button>();

        private readonly string title;
        private readonly string text;
        private readonly MessageBoxButtons types;

        protected MFMLMessageBox(string title, string text, MessageBoxButtons types)
        {
            InitializeComponent();
            this.title = title;
            this.text = text;
            this.types = types;
        }

        private bool DragMouse;
        private Point MouseDragPoint;
        private Color ThemeColor1 = Color.DeepSkyBlue;

        public Color ThemeColor
        {
            get { return ThemeColor1; }
            set
            {
                ThemeColor1 = value;
                BackColor = value;
                foreach (var button in buttons)
                {
                    button.BackColor = value;
                }
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

        private void MFMLMessageBox_Resize(object sender, EventArgs e)
        {
            panel1.Width = Width - 2;
            panel1.Height = Height - 31;
        }

        protected virtual void Initialization(string title, string text, MessageBoxButtons types)
        {
            ThemeColor = LauncherMain.Instance.Settings.ThemeColor;
            Text = title;
            textLabel.Text = text;
            switch (types)
            {
                case MessageBoxButtons.OKCancel:
                    AddCancelButton();
                    AddOKButton();
                    break;
                case MessageBoxButtons.OK:
                    AddOKButton();
                    break;
                case MessageBoxButtons.RetryCancel:
                    AddCancelButton();
                    AddRetryButton();
                    break;
                case MessageBoxButtons.YesNo:
                    AddNoButton();
                    AddYesButton();
                    break;
                case MessageBoxButtons.YesNoCancel:
                    AddCancelButton();
                    AddNoButton();
                    AddYesButton();
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    AddIgnoreButton();
                    AddRetryButton();
                    AddAbortButton();
                    break;
                default:
                    throw new InvalidOperationException("Not a value from MessageBoxButtons");
            }
        }

        private void AddOKButton()
        {
            this.AddButton("okButton", "确定", (a, b) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            });
        }

        private void AddCancelButton()
        {
            this.AddButton("cancelButton", "取消", (a, b) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            });
        }

        private void AddRetryButton()
        {
            this.AddButton("retryButton", "重试", (a, b) =>
            {
                DialogResult = DialogResult.Retry;
                Close();
            });
        }

        private void AddYesButton()
        {
            this.AddButton("yesButton", "是", (a, b) =>
            {
                DialogResult = DialogResult.Yes;
                Close();
            });
        }

        private void AddNoButton()
        {
            this.AddButton("noButton", "否", (a, b) =>
            {
                DialogResult = DialogResult.No;
                Close();
            });
        }

        private void AddIgnoreButton()
        {
            this.AddButton("ignoreButton", "忽略", (a, b) =>
            {
                DialogResult = DialogResult.Ignore;
                Close();
            });
        }

        private void AddAbortButton()
        {
            this.AddButton("abortButton", "中止", (a, b) =>
            {
                DialogResult = DialogResult.Abort;
                Close();
            });
        }

        public virtual void AddButton(string name, string text, EventHandler OnClick)
        {
            var button = new Button();
            button.BackColor = ThemeColor;
            button.FlatStyle = FlatStyle.Flat;
            button.Name = name;
            button.Size = new Size(75, 28);
            button.TabIndex = 0;
            button.Text = text;
            button.Click += OnClick;
            button.UseVisualStyleBackColor = false;
            this.buttonsPanel.Controls.Add(button);
            this.buttons.Add(button);
        }

        public static DialogResult ShowMessageBox(IWin32Window owner, string title, string text, MessageBoxButtons types)
        {
            var form = new MFMLMessageBox(title, text, types);
            DialogResult r = form.ShowDialog(owner);
            form.Dispose();
            return r;
        }

        private void MFMLMessageBox_Load(object sender, EventArgs e)
        {
            Initialization(this.title, this.text, this.types);
        }
    }
}
