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
namespace VelyseTheme
{
    public class VelyseButton : Control
    {
        #region " Properties "

        private Color _MouseOver = Color.FromArgb(15, 132, 165);
        [Category("Velyse Appearance")]
        public Color MouseOver
        {
            get { return _MouseOver; }
            set { _MouseOver = value; }
        }

        private Color _MouseDown = Color.FromArgb(5, 122, 155);
        [Category("Velyse Appearance")]
        public Color VMouseDown
        {
            get { return _MouseDown; }
            set { _MouseDown = value; }
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
        #endregion

        #region "Variables"
        #endregion
        private MouseState State = MouseState.None;

        #region "Properties"

        #region " Mouse States"

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        #endregion

        #endregion

        public VelyseButton()
        {
            Size = new Size(150, 50);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            dynamic G = e.Graphics;
            // 23, 26, 28
            // 20, 23, 25

            G.Clear(_BackColor);
            G.DrawRectangle(new Pen(_BackColor), new Rectangle(0, 0, Width - 1, Height - 1));

            switch (State)
            {
                case MouseState.Over:
                    G.FillRectangle(new SolidBrush(_MouseOver), new Rectangle(1, 1, Width - 2, Height - 2));
                    break;
                case MouseState.Down:
                    G.FillRectangle(new SolidBrush(_MouseDown), new Rectangle(1, 1, Width - 2, Height - 2));
                    break;
            }

            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(1, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, 2, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(2, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(1, 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 2, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, 2, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 3, 0, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 2, 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, Height - 2, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(1, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(0, Height - 3, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(2, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(1, Height - 2, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, Height - 2, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 2, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 1, Height - 3, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 3, Height - 1, 1, 1));
            G.FillRectangle(new SolidBrush(_BackColor), new Rectangle(Width - 2, Height - 2, 1, 1));

            G.DrawString(Text, Font, Brushes.White, new Point((Width / 2) - (TextRenderer.MeasureText(Text, Font).Width / 2), (Height / 2) - (TextRenderer.MeasureText(Text, Font).Height / 2)));

            base.OnPaint(e);


        }
    }
}
