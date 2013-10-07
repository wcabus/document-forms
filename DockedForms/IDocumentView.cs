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
        /// Gets or sets if the Form is docked inside a <see cref="DocumentForms.DocumentPanel"/> or not.
        /// </summary>
        bool IsDocked { get; set; }

        /// <summary>
        /// Gets or sets if this view is currently in the process of being docked or undocked.
        /// </summary>
        bool IsDockingOrUndocking { get; set; }

        /// <summary>
        /// Gets the <see cref="DocumentForms.DocumentPanel"/> that is associated with this <see cref="IDocumentView"/>.
        /// </summary>
        /// <returns></returns>
        DocumentPanel DocumentPanel { get; set; }

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
        /// Gets if the window should show a button in the Windows Taskbar when the view is shown as a window.
        /// </summary>
        bool WindowInTaskbar { get; }

        /// <summary>
        /// Gets if this window can be closed or not. 
        /// </summary>
        bool AllowClosing { get; }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="startDocked">
        /// When this parameter is true, the view will be shown docked inside the <see cref="DocumentForms.DocumentPanel"/>.
        /// Else, it will be docked if <paramref name="currentView"/> is docked, or undocked if <paramref name="currentView"/> is not docked.
        /// </param>
        /// <param name="currentView">
        /// If this parameter is null, the view will be docked by default.
        /// </param>
        void Show(bool startDocked, IDocumentView currentView);
    }
}