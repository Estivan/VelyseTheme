#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Globalization;
using static Helpers;

#endregion

enum MouseState : byte
{
    None = 0,
    Over = 1,
    Down = 2,
    Block = 3
}
internal sealed class Helpers
{
    public enum RoundingStyle : byte
    {
        All,
        Top,
        Bottom,
        Left,
        Right,
        TopRight,
        BottomRight
    }

    public void CenterString(Graphics G, string T, Font F, Color C, Rectangle R)
    {
        SizeF sizeF = G.MeasureString(T, F);
        using (SolidBrush B = new SolidBrush(C))
        {
            G.DrawString(T, F, B, checked(new Point((int)Math.Round(unchecked((double)R.Width / 2.0 - (double)(sizeF.Width / 2f))), (int)Math.Round(unchecked((double)R.Height / 2.0 - (double)(sizeF.Height / 2f))))));
        }
    }

    public static Color FromHex(string hex)
    {
        return ColorTranslator.FromHtml(hex);
    }



    public static Color ColorFromHex(string Hex)
    {
        return Color.FromArgb(checked((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), NumberStyles.HexNumber)));
    }


    public static Rectangle FullRectangle(Size S, bool Subtract)
    {
        Rectangle result;
        if (Subtract)
        {
            result = checked(new Rectangle(0, 0, S.Width - 1, S.Height - 1));
        }
        else
        {
            result = new Rectangle(0, 0, S.Width, S.Height);
        }
        return result;
    }

    public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, Helpers.RoundingStyle Style = Helpers.RoundingStyle.All)
    {
        GraphicsPath graphicsPath = new GraphicsPath();
        checked
        {
            int num = Rounding * 2;
            graphicsPath.StartFigure();
            bool flag = Rounding == 0;
            GraphicsPath result;
            if (flag)
            {
                graphicsPath.AddRectangle(Rect);
                graphicsPath.CloseAllFigures();
                result = graphicsPath;
            }
            else
            {
                switch (Style)
                {
                    case Helpers.RoundingStyle.All:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case Helpers.RoundingStyle.Top:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                    case Helpers.RoundingStyle.Bottom:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case Helpers.RoundingStyle.Left:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case Helpers.RoundingStyle.Right:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        break;
                    case Helpers.RoundingStyle.TopRight:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                        graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                    case Helpers.RoundingStyle.BottomRight:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                }
                graphicsPath.CloseAllFigures();
                result = graphicsPath;
            }
            return result;
        }
    }



}

namespace VelyseTheme
{
    #region  ThemeContainer

    public class VelyseForm : ContainerControl
    {

        #region  Enums

        public enum MouseState
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion
        #region  Variables

        private Rectangle HeaderRect;
        protected MouseState State;
        private int MoveHeight;
        private Point MouseP = new Point(0, 0);
        private bool Cap = false;
        private bool HasShown;

        #endregion
        #region  Properties

        private bool _Sizable = true;
        public bool Sizable
        {
            get
            {
                return _Sizable;
            }
            set
            {
                _Sizable = value;
            }
        }

        private bool _SmartBounds = true;
        public bool SmartBounds
        {
            get
            {
                return _SmartBounds;
            }
            set
            {
                _SmartBounds = value;
            }
        }

        public enum VLRoundCorner
        {
            Round,
            Square,

        }
        VLRoundCorner _RoundCorner = new VLRoundCorner();
        public VLRoundCorner RoundCorners
        {
            get { return _RoundCorner; }
            set { _RoundCorner = value; }
        }





        private bool _IsParentForm;
        protected bool IsParentForm
        {
            get
            {
                return _IsParentForm;
            }
        }

        protected bool IsParentMdi
        {
            get
            {
                if (Parent == null)
                {
                    return false;
                }
                return Parent.Parent != null;
            }
        }

        private bool _ControlMode;
        protected bool ControlMode
        {
            get
            {
                return _ControlMode;
            }
            set
            {
                _ControlMode = value;
                Invalidate();
            }
        }

        private FormStartPosition _StartPosition;
        public FormStartPosition StartPosition
        {
            get
            {
                if (_IsParentForm && !_ControlMode)
                {
                    return ParentForm.StartPosition;
                }
                else
                {
                    return _StartPosition;
                }
            }
            set
            {
                _StartPosition = value;

                if (_IsParentForm && !_ControlMode)
                {
                    ParentForm.StartPosition = value;
                }
            }
        }

        private Color _TopColor = Color.FromArgb(49, 79, 124);
        [Category("Velyse Appearance")]
        public Color TopColor
        {
            get { return _TopColor; }
            set { _TopColor = value; }
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

        private Font _Font = new Font("Segoe UI", 12);
        [Category("Velyse Appearance")]
        public Font Text_Font
        {
            get { return _Font; }
            set { _Font = value; }
        }


        #endregion
        #region  EventArgs


        protected sealed override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null)
            {
                return;
            }
            _IsParentForm = Parent is Form;

            if (!_ControlMode)
            {
                InitializeMessages();

                if (_IsParentForm)
                {
                    this.ParentForm.FormBorderStyle = FormBorderStyle.None;
                    this.ParentForm.TransparencyKey = Color.Fuchsia;

                    if (!DesignMode)
                    {
                        ParentForm.Shown += FormShown;
                    }
                }
                Parent.BackColor = BackColor;
                //   Parent.MinimumSize = New Size(261, 65)
            }
        }

        protected sealed override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_ControlMode)
            {
                HeaderRect = new Rectangle(0, 0, Width - 14, MoveHeight - 7);
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                SetState(MouseState.Down);
            }
            if (!(_IsParentForm && ParentForm.WindowState == FormWindowState.Maximized || _ControlMode))
            {
                if (HeaderRect.Contains(e.Location))
                {
                    Capture = false;
                    WM_LMBUTTONDOWN = true;
                    DefWndProc(ref Messages[0]);
                }
                else if (_Sizable && !(Previous == 0))
                {
                    Capture = false;
                    WM_LMBUTTONDOWN = true;
                    DefWndProc(ref Messages[Previous]);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!(_IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (_Sizable && !_ControlMode)
                {
                    InvalidateMouse();
                }
            }
            if (Cap)
            {
                Parent.Location = (Point)((object)(Convert.ToDouble(MousePosition) - Convert.ToDouble(MouseP)));
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            ParentForm.Text = Text;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        private void FormShown(object sender, EventArgs e)
        {
            if (_ControlMode || HasShown)
            {
                return;
            }

            if (_StartPosition == FormStartPosition.CenterParent || _StartPosition == FormStartPosition.CenterScreen)
            {
                Rectangle SB = Screen.PrimaryScreen.Bounds;
                Rectangle CB = ParentForm.Bounds;
                ParentForm.Location = new Point(SB.Width / 2 - CB.Width / 2, SB.Height / 2 - CB.Width / 2);
            }
            HasShown = true;
        }

        #endregion
        #region  Mouse & Size

        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        private Point GetIndexPoint;
        private bool B1x;
        private bool B2x;
        private bool B3;
        private bool B4;
        private int GetIndex()
        {
            GetIndexPoint = PointToClient(MousePosition);
            B1x = GetIndexPoint.X < 7;
            B2x = GetIndexPoint.X > Width - 7;
            B3 = GetIndexPoint.Y < 7;
            B4 = GetIndexPoint.Y > Height - 7;

            if (B1x && B3)
            {
                return 4;
            }
            if (B1x && B4)
            {
                return 7;
            }
            if (B2x && B3)
            {
                return 5;
            }
            if (B2x && B4)
            {
                return 8;
            }
            if (B1x)
            {
                return 1;
            }
            if (B2x)
            {
                return 2;
            }
            if (B3)
            {
                return 3;
            }
            if (B4)
            {
                return 6;
            }
            return 0;
        }

        private int Current;
        private int Previous;
        private void InvalidateMouse()
        {
            Current = GetIndex();
            if (Current == Previous)
            {
                return;
            }

            Previous = Current;
            switch (Previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private Message[] Messages = new Message[9];
        private void InitializeMessages()
        {
            Messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (int I = 1; I <= 8; I++)
            {
                Messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
            }
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent.Width > bounds.Width)
            {
                Parent.Width = bounds.Width;
            }
            if (Parent.Height > bounds.Height)
            {
                Parent.Height = bounds.Height;
            }

            int X = Parent.Location.X;
            int Y = Parent.Location.Y;

            if (X < bounds.X)
            {
                X = bounds.X;
            }
            if (Y < bounds.Y)
            {
                Y = bounds.Y;
            }

            int Width = bounds.X + bounds.Width;
            int Height = bounds.Y + bounds.Height;

            if (X + Parent.Width > Width)
            {
                X = Width - Parent.Width;
            }
            if (Y + Parent.Height > Height)
            {
                Y = Height - Parent.Height;
            }

            Parent.Location = new Point(X, Y);
        }

        private bool WM_LMBUTTONDOWN;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (WM_LMBUTTONDOWN && m.Msg == 513)
            {
                WM_LMBUTTONDOWN = false;

                SetState(MouseState.Over);
                if (!_SmartBounds)
                {
                    return;
                }

                if (IsParentMdi)
                {
                    CorrectBounds(new Rectangle(Point.Empty, Parent.Parent.Size));
                }
                else
                {
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea);
                }
            }
        }

        #endregion

        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        public VelyseForm()
        {
            SetStyle((ControlStyles)(139270), true);
            BackColor = _BackColor;
            Padding = new Padding(10, 70, 10, 9);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            MoveHeight = 66;
            Font = _Font;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.Clear(_BackColor);
            G.FillRectangle(new SolidBrush(_TopColor), new Rectangle(0, 0, Width, 60));

            if (_RoundCorner == VLRoundCorner.Round)
            {
                // Draw Left upper corner
                G.FillRectangle(Brushes.Fuchsia, 0, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 1, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 2, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 3, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, 2, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, 3, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 1, 1, 1, 1);

                G.FillRectangle(new SolidBrush(_TopColor), 1, 3, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), 1, 2, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), 2, 1, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), 3, 1, 1, 1);

                // Draw right upper corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 3, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 4, 0, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 2, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 3, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 1, 1, 1);

                G.FillRectangle(new SolidBrush(_TopColor), Width - 1, 3, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), Width - 1, 2, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), Width - 2, 1, 1, 1);
                G.FillRectangle(new SolidBrush(_TopColor), Width - 3, 1, 1, 1);

                // Draw Left bottom corner
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 2, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 3, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 4, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 2, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 3, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 2, 1, 1);

                G.FillRectangle(new SolidBrush(_BackColor), 1, Height - 3, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), 1, Height - 4, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), 3, Height - 2, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), 2, Height - 2, 1, 1);

                // Draw right bottom corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 2, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 3, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height - 1, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 4, 1, 1);
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 2, 1, 1);

                G.FillRectangle(new SolidBrush(_BackColor), Width - 2, Height - 3, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 2, Height - 4, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 4, Height - 2, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 3, Height - 2, 1, 1);
            }
            else if (_RoundCorner == VLRoundCorner.Square)
            {
                G.FillRectangle(new SolidBrush(_BackColor), Width - 2, Height - 3, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 2, Height - 4, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 4, Height - 2, 1, 1);
                G.FillRectangle(new SolidBrush(_BackColor), Width - 3, Height - 2, 1, 1);

            }


            G.DrawString(Text, _Font, new SolidBrush(_TextColor), new Rectangle(20, 20, Width - 1, Height), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
        }
    }

    #endregion
}
