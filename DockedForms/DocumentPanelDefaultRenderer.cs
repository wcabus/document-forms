using System.Drawing;

namespace DocumentForms
{
    /// <summary>
    /// This class is the default renderer for a <see cref="DocumentPanel"/>.
    /// </summary>
    public class DocumentPanelDefaultRenderer : DocumentPanelRenderer
    {
        private class DefaultColorTable : IColorTable
        {
            private readonly static Font DefaultFont = new Font("Segoe UI", 8.25f);
            private readonly static Font DefaultBoldFont = new Font(DefaultFont, FontStyle.Bold);

            public Color DocumentTabBackground { get { return SystemColors.Control; } }
            public Color DocumentTabBottomBackground { get { return SystemColors.ControlDarkDark; } }
            public Color DocumentPanelBackground { get { return SystemColors.InactiveCaption; } }

            public Color ActiveDocumentTabBackground { get { return SystemColors.ActiveCaption; } }
            public Color ActiveDocumentTabBottomBackground { get { return ActiveDocumentTabBackground; } }
            public Color ActiveDocumentTabForeground { get { return SystemColors.ActiveCaptionText; } }
            public Font ActiveDocumentTabFont { get { return DefaultBoldFont; } }

            public Color InactiveDocumentTabBackground { get { return SystemColors.InactiveCaption; } }
            public Color InactiveDocumentTabBottomBackground { get { return Color.Transparent; } }
            public Color InactiveDocumentTabForeground { get { return SystemColors.InactiveCaptionText; } }
            public Font InactiveDocumentTabFont { get { return DefaultFont; } }

            public Color ButtonBackground { get { return Color.Empty; } }
            public Color ButtonForeground { get { return SystemColors.ControlText; } }

            public Color ButtonHighlightBackground { get { return SystemColors.ButtonHighlight; } }
            public Color ButtonHighlightForeground { get { return ButtonForeground; } }

            public Color ButtonPressedBackground { get { return SystemColors.ButtonShadow; } }
            public Color ButtonPressedForeground { get { return Color.White; } }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DocumentPanelDefaultRenderer()
        {
            ColorTable = new DefaultColorTable();
        }

        protected override void OnRenderArrow(ArrowRenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                base.OnRenderArrow(e);
                return;
            }

            base.OnRenderArrow(e);
        }

        protected override void OnRenderCloseGlyph(RenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                base.OnRenderCloseGlyph(e);
                return;
            }

            base.OnRenderCloseGlyph(e);
        }

        protected override void OnRenderBackground(RenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                base.OnRenderBackground(e);
                return;
            }

            using (Brush brush = new SolidBrush(e.CustomColor))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        protected override void OnRenderHeader(HeaderRenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                base.OnRenderHeader(e);
                return;
            }

            using (Brush brush = new SolidBrush(e.CustomColor))
                e.Graphics.FillRectangle(brush, e.Bounds);

            using (Brush brush = new SolidBrush(e.BottomLineColor))
                e.Graphics.FillRectangle(brush, new Rectangle(new Point(e.Bounds.Left, e.Bounds.Bottom - 3), new Size(e.Bounds.Width, 3)));
        }
    }
}