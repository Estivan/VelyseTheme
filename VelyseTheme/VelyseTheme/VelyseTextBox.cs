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
using VelyseTheme;
using System.Drawing.Text;

namespace VelyseTheme
{
    public class VelyseTextBox : Control
    {

        #region  Variables



        public TextBox VelyseTB = new TextBox();
        private int _maxchars = 32767;
        private bool _ReadOnly;
        private bool _Multiline;
        private Image _Image;
        private Size _ImageSize;
        private HorizontalAlignment ALNType;
        private bool isPasswordMasked = false;
        private Pen P1;
        private SolidBrush B1;
        private GraphicsPath Shape;

        #endregion
        #region  Properties



        public HorizontalAlignment TextAlignment
        {
            get
            {
                return ALNType;
            }
            set
            {
                ALNType = value;
                Invalidate();
            }
        }
        public int MaxLength
        {
            get
            {
                return _maxchars;
            }
            set
            {
                _maxchars = value;
                VelyseTB.MaxLength = MaxLength;
                Invalidate();
            }
        }

        public bool UseSystemPasswordChar
        {
            get
            {
                return isPasswordMasked;
            }
            set
            {
                VelyseTB.UseSystemPasswordChar = UseSystemPasswordChar;
                isPasswordMasked = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
                if (VelyseTB != null)
                {
                    VelyseTB.ReadOnly = value;
                }
            }
        }
        public bool Multiline
        {
            get
            {
                return _Multiline;
            }
            set
            {
                _Multiline = value;
                if (VelyseTB != null)
                {
                    VelyseTB.Multiline = value;

                    if (value)
                    {
                        VelyseTB.Height = Height - 23;
                    }
                    else
                    {
                        Height = VelyseTB.Height + 23;
                    }
                }
            }
        }

        [Category("Velyse Appearance")]
        private Color _TextColor = Color.FromArgb(255, 255, 255);
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }
        [Category("Velyse Appearance")]
        private Color _BaseColor = Color.FromArgb(21, 21, 21);

        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        [Category("Velyse Appearance")]
        private Color _BorderColor = Color.FromArgb(49, 79, 124);
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }



        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                _Image = value;

                if (Image == null)
                {
                    VelyseTB.Location = new Point(8, 10);
                }
                else
                {
                    VelyseTB.Location = new Point(35, 11);
                }
                Invalidate();
            }
        }

        protected Size ImageSize
        {
            get
            {
                return _ImageSize;
            }
        }



        #endregion

        #region  EventArgs


        private void _Enter(object Obj, EventArgs e)
        {
            P1 = new Pen(_BaseColor);
            Refresh();
        }

        private void _Leave(object Obj, EventArgs e)
        {
            P1 = new Pen(_BaseColor);
            Refresh();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = VelyseTB.Text;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            VelyseTB.Text = Text;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            VelyseTB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            VelyseTB.Font = Font;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void _OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                VelyseTB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                VelyseTB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_Multiline)
            {
                VelyseTB.Height = Height - 23;
            }
            else
            {
                Height = VelyseTB.Height + 23;
            }

            Shape = new GraphicsPath();
            Shape.AddArc(0, 0, 10, 10, 180, 90);
            Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            Shape.CloseAllFigures();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            VelyseTB.Focus();
        }

        public void _TextChanged(Object sender, EventArgs e)
        {
            Text = VelyseTB.Text;
        }

        public void _BaseTextChanged(Object sender, EventArgs e)
        {
            VelyseTB.Text = Text;
        }

        #endregion

        public void AddTextBox()
        {
            VelyseTB.Location = new Point(8, 10);
            VelyseTB.Text = String.Empty;
            VelyseTB.BorderStyle = BorderStyle.None;
            VelyseTB.TextAlign = HorizontalAlignment.Left;
            VelyseTB.Font = new Font("Tahoma", 11);
            VelyseTB.UseSystemPasswordChar = UseSystemPasswordChar;
            VelyseTB.Multiline = false;
            VelyseTB.BackColor = _BaseColor;
            VelyseTB.ScrollBars = ScrollBars.None;
            VelyseTB.KeyDown += _OnKeyDown;
            VelyseTB.Enter += _Enter;
            VelyseTB.Leave += _Leave;
            VelyseTB.TextChanged += OnBaseTextChanged;
        }

        public VelyseTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddTextBox();
            Controls.Add(VelyseTB);

            P1 = new Pen(_BaseColor);
            B1 = new SolidBrush(_BaseColor);
            BackColor = Color.Transparent;
            ForeColor = _TextColor;

            Text = null;
            Font = new Font("Segoe UI", 10);
            Size = new Size(97, 29);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            var W = Width - 1;
            var H = Height - 1;

            Rectangle Base = new Rectangle(0, 0, W, H);

            {
                var withBlock = G;
                withBlock.SmoothingMode = SmoothingMode.HighQuality;
                withBlock.PixelOffsetMode = PixelOffsetMode.HighQuality;
                withBlock.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                withBlock.Clear(BackColor);

                //-- Colors
                VelyseTB.BackColor = _BaseColor;
                VelyseTB.ForeColor = _TextColor;

                //-- Base
                withBlock.FillRectangle(new SolidBrush(_BaseColor), Base);
            }
            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }
}
