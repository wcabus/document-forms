using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This interface defines what a document view needs to be able to dock and undock.
    /// </summary>
    public interface IDocumentView
    {
        /// <summary>
        /// Gets or sets if the Form is docked inside a <see cref="DocumentPanel"/> or not.
        /// </summary>
        bool IsDocked { get; set; }

        /// <summary>
        /// Gets or sets if this view is currently in the process of being docked or undocked.
        /// </summary>
        bool IsDockingOrUndocking { get; set; }

        /// <summary>
        /// Gets the <see cref="DocumentPanel"/> that currently contains this IDocumentView.
        /// </summary>
        /// <returns>A null reference if the current IDocumentView is not docked.</returns>
        DocumentPanel ParentDocumentPanel { get; set; }

        /// <summary>
        /// Gets the <see cref="FormBorderStyle"/> used when the view is shown as a window.
        /// </summary>
        FormBorderStyle WindowBorderStyle { get; }

        /// <summary>
        /// Gets the <see cref="Font"/> used when the view if shown as a window.
        /// </summary>
        Font WindowFont { get; }

        /// <summary>
        /// Gets the <see cref="FormWindowState"/> used when the view is shown as a window.
        /// </summary>
        FormWindowState OriginalWindowState { get; }

        /// <summary>
        /// Gets 
        /// </summary>
        bool WindowInTaskbar { get; }
    }
}