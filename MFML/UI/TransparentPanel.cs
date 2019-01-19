using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MFML.UI
{
    public class TransparentPanel : Panel
    {
        private int _opacity = 125;

        #region Property
        [Bindable(true), Category("Custom"), DefaultValue(125), Description("背景的透明度. 有效值0-255")]
        public int Opacity
        {
            get { return _opacity; }
            set
            {
                if (value > 255) value = 255;
                else if (value < 0) value = 0;
                _opacity = value;
                this.Invalidate();
            }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do not allow the background to be painted
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this._opacity > 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(this._opacity, this.BackColor)),
                                         this.ClientRectangle);
            }
        }
    }
}
