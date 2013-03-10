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
        /// Gets the <see cref="DocumentPanel"/> that currently contains this IDocumentView.
        /// </summary>
        /// <returns>A null reference if the current IDocumentView is not docked.</returns>
        DocumentPanel ParentDocumentPanel { get; set; }

        /// <summary>
        /// Gets the <see cref="FormBorderStyle"/> used when the view is shown as a window.
        /// </summary>
        FormBorderStyle WindowBorderStyle { get; }
    }
}