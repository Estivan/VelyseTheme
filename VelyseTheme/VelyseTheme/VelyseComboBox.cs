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
    public class VelyseComboBox : ComboBox
    {

        #region " Properties"
        private Color _AccentColor;
        [Category("Velyse Appearance")]
        public Color AccentColor
        {
            get { return _AccentColor; }
            set
            {
                _AccentColor = value;
                Invalidate();
            }
        }
        private Color _TextColor = Color.FromArgb(255, 255, 255);
        [Category("Velyse Appearance")]
        public Color TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }
        private Color _BaseColor = Color.FromArgb(20, 23, 25);
        [Category("Velyse Appearance")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }
        private Color _BorderColor = Color.FromArgb(20, 20, 20);
        [Category("Velyse Appearance")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }
        private Color _ArrowColor = Color.FromArgb(49, 79, 124);
        [Category("Velyse Appearance")]
        public Color ArrowColor
        {
            get { return _ArrowColor; }
            set
            {
                _ArrowColor = value;
                Invalidate();
            }
        }


        private int _StartIndex = 0;
        private int StartIndex
        {
            get { return _StartIndex; }
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }
        public void ReplaceItem(Object sender, DrawItemEventArgs e)
        {
            //  Dim C As ComboBox
            e.DrawBackground();
            try
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(_AccentColor), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(_BorderColor), e.Bounds);
                }
                e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, new SolidBrush(_TextColor), e.Bounds);
            }
            catch
            {
            }
            // C.Size = New Size(Me.Width - 8, Me.Height - 40)
        }
        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new List<Point>();
            points.Add(FirstPoint);
            points.Add(SecondPoint);
            points.Add(ThirdPoint);
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }

        #endregion

        public VelyseComboBox() : base()
        {
            DrawItem += ReplaceItem;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            BackColor = _BaseColor;
            ForeColor = _TextColor;
            AccentColor = Color.FromArgb(40, 40, 40);
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = new Font("Century", 10f);
            StartIndex = 0;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;


            G.Clear(_BaseColor);
            G.DrawLine(new Pen(new SolidBrush(_ArrowColor), 3), new Point(Width - 18, 10), new Point(Width - 14, 14));
            G.DrawLine(new Pen(new SolidBrush(_ArrowColor), 3), new Point(Width - 14, 14), new Point(Width - 10, 10));
            G.DrawLine(new Pen(new SolidBrush(_ArrowColor)), new Point(Width - 14, 15), new Point(Width - 14, 14));

            G.DrawRectangle(new Pen(new SolidBrush(_BorderColor)), new Rectangle(0, 0, Width - 1, Height - 1));


            try
            {
                G.DrawString(Text, Font, new SolidBrush(_TextColor), new Rectangle(7, 0, Width - 1, Height - 1), new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                });

            }
            catch
            {
            }

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);

            G.Dispose();
            B.Dispose();
        }

    }
}
