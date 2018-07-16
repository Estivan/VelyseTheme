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
    public class VelyseCircularProgressBar : Control
    {

        #region Enums

        public enum _ProgressShape
        {
            Round,
            Flat
        }

        #endregion

        #region Variables

        private long _Value;
        private long _Maximum = 100;
        private _ProgressShape ProgressShapeVal;

        #endregion

        #region Custom Properties

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
        private Color _ProgressColo2r = Color.FromArgb(47, 156, 215);
        [Category("Velyse Appearance")]
        public Color ProgressColor2
        {
            get { return _ProgressColo2r; }
            set
            {
                _ProgressColo2r = value;
                Invalidate();
            }
        }
        private Color _BackColor = Color.FromArgb(30, 33, 35);
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

        public long Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum)
                    value = _Maximum;
                _Value = value;
                Invalidate();
            }
        }

        public long Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < 1)
                    value = 1;
                _Maximum = value;
                Invalidate();
            }
        }

        public _ProgressShape ProgressShape
        {
            get { return ProgressShapeVal; }
            set
            {
                ProgressShapeVal = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetStandardSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
        }

        protected override void OnPaintBackground(PaintEventArgs p)
        {
            base.OnPaintBackground(p);
        }

        #endregion

        #region Methods
        public VelyseCircularProgressBar()
        {
            Size = new Size(130, 130);
            Font = new Font("Segoe UI", 15);
            MinimumSize = new Size(100, 100);
            DoubleBuffered = true;
            Value = 57;
            ProgressShape = _ProgressShape.Flat;
            this.ForeColor = _TextColor;
            this.BackColor = _BackColor;
        }

        private void SetStandardSize()
        {
            int _Size = Math.Max(Width, Height);
            Size = new Size(_Size, _Size);
        }

        public void Increment(int Val)
        {
            this._Value += Val;
            Invalidate();
        }

        public void Decrement(int Val)
        {
            this._Value -= Val;
            Invalidate();
        }
        #endregion

        #region Events
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Bitmap bitmap = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.Clear(_BackColor);


                    using (Pen pen2 = new Pen(_OutLineColor))
                    {
                        graphics.DrawEllipse(pen2, 0x18 - 6, 0x18 - 6, (this.Width - 0x30) + 12, (this.Height - 0x30) + 12);
                    }


                    using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this._ProgressColor, this._ProgressColo2r, LinearGradientMode.ForwardDiagonal))
                    {
                        using (Pen pen = new Pen(brush, 14f))
                        {
                            switch (this.ProgressShapeVal)
                            {
                                case _ProgressShape.Round:
                                    pen.StartCap = LineCap.Round;
                                    pen.EndCap = LineCap.Round;
                                    break;

                                case _ProgressShape.Flat:
                                    pen.StartCap = LineCap.Flat;
                                    pen.EndCap = LineCap.Flat;
                                    break;
                            }

                            graphics.DrawArc(pen, 0x12, 0x12, (this.Width - 0x23) - 2, (this.Height - 0x23) - 2, -90, (int)Math.Round((double)((360.0 / ((double)this._Maximum)) * this._Value)));
                        }
                    }


                    Brush FontColor = new SolidBrush(_TextColor);
                    SizeF MS = graphics.MeasureString(Convert.ToString(Convert.ToInt32((100 / _Maximum) * _Value)), Font);
                    graphics.DrawString(Convert.ToString(Convert.ToInt32((100 / _Maximum) * _Value)), Font, new SolidBrush(_TextColor), Convert.ToInt32(Width / 2 - MS.Width / 2), Convert.ToInt32(Height / 2 - MS.Height / 2));
                    e.Graphics.DrawImage(bitmap, 0, 0);
                    graphics.Dispose();
                    bitmap.Dispose();
                }
            }
        }
        #endregion
    }
}
