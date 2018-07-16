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
    public class VelyseRadioButton : Control
    {
        private MouseState _State = MouseState.None;
        private int W;

        [Category("Velyse Appearance")]
        private Color _TextColor = Color.FromArgb(255, 255, 255);
        public Color TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }
        [Category("Velyse Appearance")]
        private Color _BackColor = Color.FromArgb(30, 33, 35);
        public Color BaseColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                Invalidate();
            }
        }
        [Category("Velyse Appearance")]
        private Color _BorderColor = Color.FromArgb(25, 25, 25);
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }
        [Category("Velyse Appearance")]
        private Color _CheckedColor = Color.FromArgb(35, 104, 160);
        public Color CheckedColor
        {
            get { return _CheckedColor; }
            set
            {
                _CheckedColor = value;
                Invalidate();
            }
        }

        [Category("Velyse Appearance")]
        private Color _ElipseColor = Color.FromArgb(25, 25, 25);
        public Color ElipseColor
        {
            get { return _ElipseColor; }
            set
            {
                _ElipseColor = value;
                Invalidate();
            }
        }


        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                InvalidateControls();
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnClick(EventArgs e)
        {
            if (!_Checked)
                Checked = true;
            base.OnClick(e);
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;
            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is VelyseRadioButton)
                {
                    ((VelyseRadioButton)C).Checked = false;
                    Invalidate();
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _State = MouseState.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _State = MouseState.None;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(_BackColor);
            if (Parent.BackColor == _BackColor)
                G.FillEllipse(new SolidBrush(_ElipseColor), new Rectangle(0, 0, 13, 13));
            else
                G.FillEllipse(new SolidBrush(_ElipseColor), new Rectangle(0, 0, 13, 13));
            G.DrawEllipse(new Pen(_BorderColor), new Rectangle(0, 0, 13, 13));
            if (Checked)
                G.FillEllipse(new SolidBrush(_CheckedColor), new Rectangle(3, 3, 7, 7));
            G.DrawString(Text, new Font("Arial", 9), new SolidBrush(_TextColor), new Point(18, 0));
        }
    }
}
