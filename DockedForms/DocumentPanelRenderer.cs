using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// Handles the painting functionality for <see cref="DocumentPanel"/> objects.
    /// </summary>
    public abstract class DocumentPanelRenderer
    {
        /// <summary>
        /// Gets the <see cref="IColorTable"/> used to draw the different elements.
        /// </summary>
        public IColorTable ColorTable { get; protected set; }

        /// <summary>Occurs when an arrow is rendered.</summary>
        public event EventHandler<ArrowRenderEventArgs> RenderArrow;

        /// <summary>Occurs when a close glyph is rendered.</summary>
        public event EventHandler<RenderEventArgs> RenderCloseGlyph;

        /// <summary>Occurs when the background is rendered.</summary>
        public event EventHandler<RenderEventArgs> RenderBackground;

        /// <summary>Occurs when the header is rendered.</summary>
        public event EventHandler<HeaderRenderEventArgs> RenderHeader;

        internal virtual DocumentPanelRenderer RendererOverride { get { return null; } }

        /// <summary>Draws an arrow.</summary>
        /// <param name="e">A <see cref="ArrowRenderEventArgs" /> that contains data to draw the arrow.</param>
        public void DrawArrow(ArrowRenderEventArgs e)
        {
            OnRenderArrow(e);
            var localEventHandler = RenderArrow;
            if (localEventHandler != null)
                localEventHandler(this, e);
        }

        /// <summary>Draws a close glyph.</summary>
        /// <param name="e">A <see cref="RenderEventArgs" /> that contains data to draw the close glyph.</param>
        public void DrawCloseGlyph(RenderEventArgs e)
        {
            OnRenderCloseGlyph(e);
            var localEventHandler = RenderCloseGlyph;
            if (localEventHandler != null)
                localEventHandler(this, e);
        }

        /// <summary>Draws the background.</summary>
        /// <param name="e">A <see cref="RenderEventArgs" /> that contains data to draw the background.</param>
        public void DrawBackground(RenderEventArgs e)
        {
            OnRenderBackground(e);
            var localEventHandler = RenderBackground;
            if (localEventHandler != null)
                localEventHandler(this, e);
        }

        /// <summary>Draws the header.</summary>
        /// <param name="e">A <see cref="HeaderRenderEventArgs" /> that contains data to draw the header.</param>
        public void DrawHeader(HeaderRenderEventArgs e)
        {
            OnRenderHeader(e);
            var localEventHandler = RenderHeader;
            if (localEventHandler != null)
                localEventHandler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="RenderArrow"/> event.
        /// </summary>
        /// <param name="e">A <see cref="ArrowRenderEventArgs"/> that contains the event data.</param>
        protected virtual void OnRenderArrow(ArrowRenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                RendererOverride.OnRenderArrow(e);
                return;
            }

            Graphics graphics = e.Graphics;
            Rectangle arrowRectangle = e.Bounds;
            using (Brush brush = new SolidBrush(e.CustomColor))
            {
                Point point = new Point(arrowRectangle.Left + arrowRectangle.Width / 2, arrowRectangle.Top + arrowRectangle.Height / 2);
                ArrowDirection direction = e.Direction;
                Point[] points;
                switch (direction)
                {
                    case ArrowDirection.Left:
                        points = new Point[]
                            {
                                new Point(point.X + 2, point.Y - 4),
                                new Point(point.X + 2, point.Y + 4),
                                new Point(point.X - 2, point.Y)
                            };
                        break;
                    case ArrowDirection.Up:
                        points = new Point[]
                            {
                                new Point(point.X - 2, point.Y + 1),
                                new Point(point.X + 3, point.Y + 1),
                                new Point(point.X, point.Y - 2)
                            };
                        break;
                    case ArrowDirection.Right:
                        points = new Point[]
                            {
                                new Point(point.X - 2, point.Y - 4),
                                new Point(point.X - 2, point.Y + 4),
                                new Point(point.X + 2, point.Y)
                            };
                        break;
                    default: //Down
                        points = new Point[]
                            {
                                new Point(point.X - 2, point.Y - 1),
                                new Point(point.X + 3, point.Y - 1),
                                new Point(point.X, point.Y + 2)
                            };
                        break;
                }

                graphics.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// Raises the <see cref="RenderCloseGlyph"/> event.
        /// </summary>
        /// <param name="e">A <see cref="RenderEventArgs"/> that contains the event data.</param>
        protected virtual void OnRenderCloseGlyph(RenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                RendererOverride.OnRenderCloseGlyph(e);
                return;
            }

            Graphics graphics = e.Graphics;
            using (Pen pen = new Pen(e.CustomColor))
            {
                Point point = new Point(e.Bounds.Left + e.Bounds.Width / 2, e.Bounds.Top + e.Bounds.Height / 2);

                graphics.DrawLine(pen, point, new Point(point.X - 3, point.Y - 3));
                graphics.DrawLine(pen, point, new Point(point.X - 3, point.Y + 3));
                graphics.DrawLine(pen, point, new Point(point.X + 3, point.Y - 3));
                graphics.DrawLine(pen, point, new Point(point.X + 3, point.Y + 3));

                point.Offset(1, 0);
                graphics.DrawLine(pen, point, new Point(point.X - 3, point.Y - 3));
                graphics.DrawLine(pen, point, new Point(point.X - 3, point.Y + 3));
                graphics.DrawLine(pen, point, new Point(point.X + 3, point.Y - 3));
                graphics.DrawLine(pen, point, new Point(point.X + 3, point.Y + 3));
            }
        }

        /// <summary>
        /// Raises the <see cref="RenderBackground"/> event.
        /// </summary>
        /// <param name="e">A <see cref="RenderEventArgs"/> that contains the event data.</param>
        protected virtual void OnRenderBackground(RenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                RendererOverride.OnRenderBackground(e);
                return;
            }
        }

        /// <summary>
        /// Raises the <see cref="RenderHeader"/> event.
        /// </summary>
        /// <param name="e">A <see cref="HeaderRenderEventArgs"/> that contains the event data.</param>
        protected virtual void OnRenderHeader(HeaderRenderEventArgs e)
        {
            if (RendererOverride != null)
            {
                RendererOverride.OnRenderHeader(e);
                return;
            }
        }
    }
}