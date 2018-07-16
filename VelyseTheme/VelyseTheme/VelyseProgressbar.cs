using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelyseTheme;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Globalization;
using System.Drawing.Text;

namespace VelyseTheme
{
    public class VelyseProgressBar : Control
    {

        private Color _ProgressColor = Color.FromArgb(47, 156, 215);
        [Category("Velyse Appearance")]
        public Color ProgressColor
        {
            get { return _ProgressColor; }
            set
            {
                _ProgressColor = value;
                Invalidate();
            }
        }
        private Color _BackColor = Color.FromArgb(23, 26, 28);
        [Category("Velyse Appearance")]
        public Color BaseColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }
        private Color _TextColor = Color.White;
        [Category("Velyse Appearance")]
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }
        private Color _OutLineColor = Color.FromArgb(35, 35, 35);
        [Category("Velyse Appearance")]
        public Color OutLineColor
        {
            get { return _OutLineColor; }
            set { _OutLineColor = value; }
        }



        private int _val = 0;
        public int Value
        {
            get { return _val; }
            set
            {
                _val = value;
                ValChanged();
                Invalidate();
            }
        }

        private int _min = 0;
        public int Min
        {
            get { return _min; }
            set
            {
                _min = value;
                Invalidate();
            }
        }

        private int _max = 100;
        public int Max
        {
            get { return _max; }
            set
            {
                _max = value;
                Invalidate();
            }
        }

        private bool _showPercent = false;
        public bool ShowPercent
        {
            get { return _showPercent; }
            set
            {
                _showPercent = value;
                Invalidate();
            }
        }

        private void ValChanged()
        {
            if (_val > _max)
            {
                _val = _max;
            }
        }

        public VelyseProgressBar()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            dynamic G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            if (Parent.BackColor == Color.FromArgb(40, 40, 40))
            {
                G.Clear(_BackColor);
            }
            else
            {
                G.Clear(_BackColor);
            }
            int _Progress = Convert.ToInt32(_val / _max * Width);

            G.DrawRectangle(new Pen(_OutLineColor), 0, 0, Width, Height);
            G.FillRectangle(new SolidBrush(_ProgressColor), new Rectangle(0, 0, _val * (Width - 35) / (_max - _min), Height));
            G.DrawLine(new Pen(Color.FromArgb(30, Color.White)), new Point(0, 0), new Point(_Progress - 1, 0));
            G.FillRectangle(new SolidBrush(_OutLineColor), new Rectangle(0, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_OutLineColor), new Rectangle(Width - 1, 0, 1, 1));
            G.DrawLine(new Pen(Color.FromArgb(70, Color.Black)), new Point(0, Height), new Point(_Progress - 1, Height));
            G.InterpolationMode = (InterpolationMode)7;
        }

    }
}
