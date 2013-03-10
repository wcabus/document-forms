using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// Provides data for the events that render controls.
    /// </summary>
    public class RenderEventArgs : EventArgs
    {
        private Color _customColor = Color.Empty;
        private bool _colorChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderEventArgs"/> class.
        /// </summary>
        /// <param name="g">The graphics used to paint.</param>
        /// <param name="target">The control on which to paint.</param>
        /// <param name="bounds">The bounding area.</param>
        /// <param name="color">The color.</param>
        public RenderEventArgs(Graphics g, Control target, Rectangle bounds, Color color)
        {
            DefaultColor = color;
            Target = target;
            Bounds = bounds;
            Graphics = g;
        }

        /// <summary>
        /// Gets or sets the color that will be used when drawing.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Color" /> that represents the color of the brush while drawing.</returns>
        public Color CustomColor
        {
            get
            {
                return _colorChanged ? _customColor : DefaultColor;
            }
            set { 
                _customColor = value;
                _colorChanged = true;
            }
        }

        internal Color DefaultColor { get; set; }

        /// <summary>
        /// Gets the graphics used to paint.
        /// </summary>
        /// <returns>The <see cref="System.Drawing.Graphics"/> used to paint.</returns>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets or sets the bounding area.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the bounding area.</returns>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets the control on which we will draw.
        /// </summary>
        /// <returns>The <see cref="System.Windows.Forms.Control"/> on which we will draw.</returns>
        public Control Target { get; private set; }
    }
}