using System;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// The DocumentViewHelper class allows you to use the <see cref="IDocumentView"/> interface,
    /// while providing the necessary code to dock an <see cref="IDocumentView"/> into a <see cref="DocumentPanel"/>.
    /// </summary>
    public static class DocumentViewHelper
    {
        /// <summary>
        /// Docks the <paramref name="documentView"/> inside a <see cref="DocumentPanel"/>.
        /// If the <paramref name="documentView"/> has already been docked, this method undocks it first.
        /// This allows you to easily move the <paramref name="documentView"/> between different <see cref="DocumentPanel"/> instances.
        /// </summary>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="documentView"/> is a null reference.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the view is already docked.</exception>
        public static void Dock<TView>(TView documentView) where TView : Form, IDocumentView
        {
            if (documentView == null)
                throw new ArgumentNullException("documentView");

            if (documentView.IsDocked)
                throw new InvalidOperationException("Cannot dock a view that is already docked.");

            documentView.IsDockingOrUndocking = true;

            try
            {
                //Update the status on the view
                documentView.IsDocked = true;

                //Set Form-specific properties to make it appear docked.
                documentView.TopLevel = false;
                documentView.FormBorderStyle = FormBorderStyle.None;
                documentView.ShowInTaskbar = false;
                documentView.Dock = DockStyle.Fill;
                documentView.WindowState = FormWindowState.Normal;

                WindowsApi.SetWindowPos(documentView.Handle, IntPtr.Zero, 0, 0, 0, 0,
                                        SetWindowPosFlags.SwpNoActivate | SetWindowPosFlags.SwpNoMove |
                                        SetWindowPosFlags.SwpNoSize |
                                        SetWindowPosFlags.SwpNoZOrder | SetWindowPosFlags.SwpNoOwnerZOrder |
                                        SetWindowPosFlags.SwpFrameChanged);


                documentView.DocumentPanel.DockDocument(documentView);
            }
            finally
            {
                documentView.IsDockingOrUndocking = false;
            }
        }

        /// <summary>
        /// Undocks the <paramref name="documentView"/> from the <see cref="DocumentPanel"/> that is holding the view.
        /// If the <paramref name="documentView"/> isn't docked, this method does nothing.
        /// </summary>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="documentView"/> is a null reference.</exception>
        public static void Undock(IDocumentView documentView)
        {
            UndockInternal(documentView, false);
        }

        /// <summary>
        /// Sets the <paramref name="documentView"/> in an undocked state.
        /// If the <paramref name="documentView"/> isn't docked, this method does nothing.
        /// </summary>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="documentView"/> is a null reference.</exception>
        public static void UndockView(IDocumentView documentView)
        {
            UndockInternal(documentView, true);
        }

        private static void UndockInternal(IDocumentView documentView, bool isDragging)
        {
            if (documentView == null)
                throw new ArgumentNullException("documentView");

            if (!documentView.IsDocked)
                return;

            documentView.IsDockingOrUndocking = true;

            try
            {
                if (!isDragging)
                {
                    //Undock from the panel: this removes the button from the header
                    documentView.DocumentPanel.UndockDocument(documentView as Form);
                }

                //Update the status on the view
                documentView.IsDocked = false;

                //Set Form-specific properties to let it become a window again.
                Form docForm = (documentView as Form);
                if (docForm != null)
                {
                    docForm.FormBorderStyle = documentView.WindowBorderStyle;
                    docForm.Font = documentView.WindowFont;
                    docForm.Dock = DockStyle.None;
                    docForm.ShowInTaskbar = documentView.WindowInTaskbar;
                    docForm.WindowState = documentView.OriginalWindowState;

                    docForm.Parent = null;
                    docForm.TopLevel = true;
                    //set this as last, Dock must be DockStyle.None when setting this property to true.

                    if (!docForm.IsDisposed)
                    {
                        WindowsApi.SetWindowPos(docForm.Handle, IntPtr.Zero, docForm.Left, docForm.Top, docForm.Width,
                                                docForm.Height, SetWindowPosFlags.SwpFrameChanged);
                    }

                    if (isDragging)
                    {
                        var position = Cursor.Position;
                        position.Offset(-30, -10); //otherwhise, we'd be holding the top-left corner of the form

                        docForm.Location = position;

                        // "Reset" the mouse status...
                        WindowsApi.ReleaseCapture();
                        // ...and trick Windows in thinking that the user has clicked on the caption bar.
                        WindowsApi.SendMessage(docForm.Handle, WindowsApi.WmNcLButtonDown, WindowsApi.HtCaption, 0);
                    }
                }
            }
            finally
            {
                documentView.IsDockingOrUndocking = false;
            }
        }

        /// <summary>
        /// Registers the given <paramref name="view"/> in the <paramref name="documentPanel"/>.
        /// When the user drags the view over the <see cref="DocumentPanel"/>, it can be docked.
        /// </summary>
        /// <param name="documentPanel"></param>
        /// <param name="view"></param>
        /// <returns>True if registration is successfull, false otherwhise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="documentPanel"/> or <paramref name="view"/> is a null reference.</exception>
        public static bool RegisterView<TView>(DocumentPanel documentPanel, TView view) where TView : Form, IDocumentView
        {
            if (documentPanel == null)
                throw new ArgumentNullException("documentPanel");

            if (view == null)
                throw new ArgumentNullException("view");

            return documentPanel.RegisterViewForDocking(view);
        }
    }
}