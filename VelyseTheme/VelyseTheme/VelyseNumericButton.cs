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
    public class VelyseNumericButton : Control
    {


        private int W, H;
        private MouseState State = MouseState.None;
        private int x, y;
        private long _Value, _Min, _Max;
        private bool Bool;
        public enum _ThemeChoose
        {
            Light,
            Dark,
            Custom
        }




        public long Value
        {
            get
            {
                return Convert.ToInt64(_Value);
            }
            set
            {
                if (value <= _Max & value >= _Min)
                    _Value = value;
                Invalidate();
            }
        }

        public long Maximum
        {
            get
            {
                return _Max;
            }
            set
            {
                if (value > _Min)
                    _Max = value;
                if (_Value > _Max)
                    _Value = _Max;
                Invalidate();
            }
        }

        public long Minimum
        {
            get
            {
                return _Min;
            }
            set
            {
                if (value < _Max)
                    _Min = value;
                if (_Value < _Min)
                    _Value = Minimum;
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            x = e.Location.X;
            y = e.Location.Y;
            Invalidate();
            if (e.X < Width - 23)
                Cursor = Cursors.IBeam;
            else
                Cursor = Cursors.Hand;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (x > Width - 21 && x < Width - 3)
            {
                if (y < 15)
                {
                    if ((Value + 1) <= _Max)
                        _Value += 1;
                }
                else if ((Value - 1) >= _Min)
                    _Value -= 1;
            }
            else
            {
                Bool = !Bool;
                Focus();
            }
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (Bool)
                    _Value = Convert.ToInt64(Convert.ToString(_Value) + e.KeyChar.ToString());
                if (_Value > _Max)
                    _Value = _Max;
                Invalidate();
            }
            catch
            {
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Back)
                Value = 0;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 30;
        }


        [Category("Velyse Appearance")]
        private Color _BackColor = Color.FromArgb(45, 47, 49);
        public Color BaseColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
            }
        }

        [Category("Velyse Appearance")]
        private Color _ButtonColor = Color.FromArgb(49, 79, 124);
        public Color ButtonColor
        {
            get
            {
                return _ButtonColor;
            }
            set
            {
                _ButtonColor = value;
            }
        }
        [Category("Velyse Appearance")]
        private Color _AddSubTextColor = Color.Gray;
        public Color AddSubTextColor
        {
            get
            {
                return _AddSubTextColor;
            }
            set
            {
                _AddSubTextColor = value;
            }
        }
        [Category("Velyse Appearance")]
        private Color _ValTextColor = Color.FromArgb(150, 150, 150);
        public Color ValueTextColor
        {
            get
            {
                return _ValTextColor;
            }
            set
            {
                _ValTextColor = value;
            }
        }

        [Category("Velyse Appearance")]
        private _ThemeChoose myTheme;
        public _ThemeChoose _Theme
        {
            get
            {
                return myTheme;
            }
            set
            {
                myTheme = value;
            }
        }



        public VelyseNumericButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 10);
            BackColor = _BackColor;
            ForeColor = _AddSubTextColor;
            _Min = 0;
            _Max = 9999999;
        }


        protected override void OnPaint(PaintEventArgs e)
        {



            var B = new Bitmap(Width, Height);
            var G = Graphics.FromImage(B);

            W = Width; H = Height;

            Rectangle Base = new Rectangle(0, 0, W, H);

            {
                var withBlock = G;
                withBlock.SmoothingMode = SmoothingMode.HighQuality;
                withBlock.PixelOffsetMode = PixelOffsetMode.HighQuality;
                withBlock.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                withBlock.Clear(BackColor);

                // -- Base
                withBlock.FillRectangle(new SolidBrush(_BackColor), Base);
                withBlock.FillRectangle(new SolidBrush(_ButtonColor), new Rectangle(Width - 24, 0, 24, H));

                // -- Add
                withBlock.DrawString("+", new Font("Segoe UI", 10), new SolidBrush(_AddSubTextColor), new Point(Width - 19, 0));
                // -- Subtract
                withBlock.DrawString("-", new Font("Segoe UI", 10, FontStyle.Bold), new SolidBrush(_AddSubTextColor), new Point(Width - 17, 12));

                // -- Text
                Color ColorColor = Color.FromArgb(150, 150, 150);
                withBlock.DrawString(Convert.ToString(_Value), Font, new SolidBrush(_ValTextColor), new Rectangle(5, 1, W, H), new StringFormat() { LineAlignment = StringAlignment.Center });
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

    }
}
