using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This class is the default renderer for a <see cref="DocumentPanel"/>.
    /// </summary>
    public class DocumentPanelDefaultRenderer : DocumentPanelRenderer
    {
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