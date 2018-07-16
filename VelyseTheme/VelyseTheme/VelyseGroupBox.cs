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
    public class VelyseGroupBox : ContainerControl
    {

        #region " Properties "

        private Color _TopColor = Color.FromArgb(49, 79, 124);
        [Category("Velyse Appearance")]
        public Color TopColor
        {
            get { return _TopColor; }
            set { _TopColor = value; }
        }

        private Color _BackColor = Color.FromArgb(31, 31, 31);
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
        [Category("Velyse Appearance")]
        public enum PanelDr
        {
            Top,
            Side,

        }

        [Category("Velyse Appearance")]
        PanelDr _PanelSide = new PanelDr();
        public PanelDr PanelSide
        {
            get { return _PanelSide; }
            set { _PanelSide = value; }
        }

        private Font _Font = new Font("Arial", 9);
        [Category("Velyse Appearance")]
        public Font GroupBoxFont
        {
            get { return _Font; }
            set { _Font = value; }
        }
        #endregion

        public VelyseGroupBox()
        {
            Size = new Size(200, 100);
            BackColor = _BackColor;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            dynamic G = e.Graphics;
            G.Clear(_BackColor);
            if (_PanelSide == PanelDr.Top)
            {

                G.FillRectangle(new SolidBrush(_TopColor), new Rectangle(0, 0, Width, 20));
                G.FillRectangle(new SolidBrush(_TopColor), new Rectangle(0, 0, Width, 20));

                G.DrawString(Text, _Font, new SolidBrush(_TextColor), new Point(10, 4));

            }
            else
            {

                G.FillRectangle(new SolidBrush(_TopColor), new Rectangle(0, 0, 7, Height));
                G.DrawString(Text, _Font, new SolidBrush(_TextColor), new Point(10, 4));

            }
        }
    }
}
