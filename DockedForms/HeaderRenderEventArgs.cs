using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// Provides data for the <see cref="DocumentPanelRenderer.RenderHeader"/> event.
    /// </summary>
    public class HeaderRenderEventArgs : RenderEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderRenderEventArgs"/> class.
        /// </summary>
        /// <param name="g">The graphics used to paint the header.</param>
        /// <param name="target">The control on which to paint the header.</param>
        /// <param name="bounds">The bounding area of the header.</param>
        /// <param name="backgroundColor">The background color of the header.</param>
        /// <param name="bottomLineColor">The color of the bottom part of the header.</param>
        public HeaderRenderEventArgs(Graphics g, Control target, Rectangle bounds, Color backgroundColor,
                                               Color bottomLineColor)
            : base(g, target, bounds, backgroundColor)
        {
            BottomLineColor = bottomLineColor;
        }

        /// <summary>
        /// Gets or sets the color used for drawing the bottom part of the header.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Color" />.</returns>
        public Color BottomLineColor { get; set; }
    }
}