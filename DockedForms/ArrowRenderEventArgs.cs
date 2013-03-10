using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// Provides data for the <see cref="DocumentPanelRenderer.RenderArrow"/> event.
    /// </summary>
    public class ArrowRenderEventArgs : RenderEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrowRenderEventArgs"/> class.
        /// </summary>
        /// <param name="g">The graphics used to paint the arrow.</param>
        /// <param name="target">The control on which to paint the arrow.</param>
        /// <param name="arrowRectangle">The bounding area of the arrow.</param>
        /// <param name="arrowColor">The color of the arrow.</param>
        /// <param name="arrowDirection">The direction in which the arrow points.</param>
        public ArrowRenderEventArgs(Graphics g, Control target, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
            : base(g, target, arrowRectangle, arrowColor)
        {
            Direction = arrowDirection;
        }

        /// <summary>
        /// Gets or sets the direction in which the arrow points.
        /// </summary>
        /// <returns>One of the <see cref="System.Windows.Forms.ArrowDirection" /> values.</returns>
        public ArrowDirection Direction { get; set; }
    }
}