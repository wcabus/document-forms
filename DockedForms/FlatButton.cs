using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    internal class FlatButton : Control
    {
        [Flags]
        private enum MouseState
        {
            None = 0,
            MouseOver = 1,
            MouseDown = 2
        }

        private MouseState _mouseState = MouseState.None;

        public FlatButton() {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.StandardClick, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseState |= MouseState.MouseOver;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState &= ~MouseState.MouseOver;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseState |= MouseState.MouseDown;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseState &= ~MouseState.MouseDown;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ParentPanel == null)
                return;

            Color backgroundColor = Color.Empty;
            if (IsMouseHover && IsPressed)
                backgroundColor = SystemColors.Highlight;
            else if (IsMouseHover)
                backgroundColor = SystemColors.ButtonHighlight;

            if (backgroundColor != Color.Empty)
                ParentPanel.Renderer.DrawBackground(new RenderEventArgs(e.Graphics, this, ClientRectangle,
                                                                        backgroundColor));

            if (IsCloseButton)
            {
                Color glyphColor = SystemColors.ActiveCaptionText;

                if (IsMouseHover && IsPressed)
                    glyphColor = SystemColors.HighlightText;
                else if (IsMouseHover)
                    glyphColor = SystemColors.Highlight;
                
                ParentPanel.Renderer.DrawCloseGlyph(new RenderEventArgs(e.Graphics, this, ClientRectangle, glyphColor));
                
                return;
            }

            if (IsArrowButton)
            {
                Color arrowColor = SystemColors.ActiveCaptionText;
                
                if (IsMouseHover && IsPressed)
                    arrowColor = SystemColors.HighlightText;
                else if (IsMouseHover)
                    arrowColor = SystemColors.Highlight;

                ParentPanel.Renderer.DrawArrow(new ArrowRenderEventArgs(e.Graphics, this, ClientRectangle, arrowColor,
                                                                        ArrowDirection));
            }
        }

        public bool IsMouseHover { get { return _mouseState.HasFlag(MouseState.MouseOver); } }
        
        public bool IsPressed { get { return _mouseState.HasFlag(MouseState.MouseDown); } }

        public bool IsArrowButton { get; set; }
        public ArrowDirection ArrowDirection { get; set; }

        public bool IsCloseButton { get; set; }

        private DocumentPanel _documentPanel;

        public DocumentPanel ParentPanel
        {
            get { return _documentPanel; }
            set
            {
                if (_documentPanel == value)
                    return;

                _documentPanel = value;
                Invalidate();
            }
        }
    }
}