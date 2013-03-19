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

            if (e.Button == MouseButtons.Right)
                OnClick(e); //Raise click event also when right-clicking

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ParentPanel == null)
                return;

            Color backgroundColor = Color.Empty;
            if (IsMouseHover && IsPressed)
                backgroundColor = ParentPanel.Renderer.ColorTable.ButtonPressedBackground;
            else if (IsMouseHover)
                backgroundColor = ParentPanel.Renderer.ColorTable.ButtonHighlightBackground;
            else
                backgroundColor = ParentPanel.Renderer.ColorTable.ButtonBackground;

            if (backgroundColor != Color.Empty)
                ParentPanel.Renderer.DrawBackground(new RenderEventArgs(e.Graphics, this, ClientRectangle,
                                                                        backgroundColor));

            if (IsCloseButton)
            {
                Color glyphColor = ParentPanel.Renderer.ColorTable.ButtonForeground;

                if (IsMouseHover && IsPressed)
                    glyphColor = ParentPanel.Renderer.ColorTable.ButtonPressedForeground;
                else if (IsMouseHover)
                    glyphColor = ParentPanel.Renderer.ColorTable.ButtonHighlightForeground;
                
                ParentPanel.Renderer.DrawCloseGlyph(new RenderEventArgs(e.Graphics, this, ClientRectangle, glyphColor));
                
                return;
            }

            if (IsArrowButton)
            {
                Color arrowColor = ParentPanel.Renderer.ColorTable.ButtonForeground;

                if (IsMouseHover && IsPressed)
                    arrowColor = ParentPanel.Renderer.ColorTable.ButtonPressedForeground;
                else if (IsMouseHover)
                    arrowColor = ParentPanel.Renderer.ColorTable.ButtonHighlightForeground;
                
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