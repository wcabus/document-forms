using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// The DocumentViewHelper class allows you to use the <see cref="IDocumentView"/> interface,
    /// while providing the necessary code to dock an <see cref="IDocumentView"/> into a <see cref="DocumentPanel"/>.
    /// </summary>
    public static class DocumentViewHelper
    {
        private const int WmNcLButtonDown = 0xA1;
        private const int HtCaption = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr ReleaseCapture();

        /// <summary>
        /// Docks the <paramref name="documentView"/> inside the <paramref name="documentPanel"/>.
        /// If the <paramref name="documentView"/> has already been docked, this method undocks it first.
        /// This allows you to easily move the <paramref name="documentView"/> between different <see cref="DocumentPanel"/> instances.
        /// </summary>
        /// <param name="documentPanel"></param>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="documentPanel"/> or <paramref name="documentView"/> is a null reference.</exception>
        public static void Dock(DocumentPanel documentPanel, IDocumentView documentView)
        {
            if (documentPanel == null)
                throw new ArgumentNullException("documentPanel");

            if (documentView == null)
                throw new ArgumentNullException("documentView");

            if (documentView.IsDocked)
                Undock(documentView); //Undock from the panel: this removes the button from the header

            //Update the status on the view
            documentView.IsDocked = true;

            //Set Form-specific properties to make it appear docked.
            Form docForm = documentView as Form;
            if (docForm != null)
            {
                docForm.TopLevel = false;
                docForm.FormBorderStyle = FormBorderStyle.None;
            }

            documentView.ParentDocumentPanel = documentPanel;
            documentPanel.DockDocument(documentView);
        }

        /// <summary>
        /// Undocks the <paramref name="documentView"/> from the <see cref="DocumentPanel"/> that is holding the view.
        /// If the <paramref name="documentView"/> isn't docked, this method does nothing.
        /// </summary>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="documentView"/> is a null reference.</exception>
        public static void Undock(IDocumentView documentView)
        {
            if (documentView == null)
                throw new ArgumentNullException("documentView");

            if (!documentView.IsDocked)
                return;

            //Undock from the panel: this removes the button from the header
            documentView.ParentDocumentPanel.UndockDocument(documentView);
            //Update the status on the view
            documentView.IsDocked = false;

            //Set Form-specific properties to let it become a window again.
            Form docForm = (documentView as Form);
            if (docForm != null)
            {
                docForm.FormBorderStyle = documentView.WindowBorderStyle;
                docForm.TopLevel = true;
                    //set this as last, Dock must be DockStyle.None when setting this property to true.
            }
        }

        /// <summary>
        /// Sets the <paramref name="documentView"/> in an undocked state.
        /// If the <paramref name="documentView"/> isn't docked, this method does nothing.
        /// </summary>
        /// <param name="documentView"></param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="documentView"/> is a null reference.</exception>
        public static void UndockView(IDocumentView documentView)
        {
            if (documentView == null)
                throw new ArgumentNullException("documentView");

            if (!documentView.IsDocked)
                return;

            //Update the status on the view
            documentView.IsDocked = false;

            //Set Form-specific properties to let it become a window again.
            Form docForm = (documentView as Form);
            if (docForm != null)
            {
                docForm.FormBorderStyle = documentView.WindowBorderStyle;
                docForm.TopLevel = true;
                    //set this as last, Dock must be DockStyle.None when setting this property to true.

                var position = Cursor.Position;
                position.Offset(-30, -10); //otherwhise, we'd be holding the top-left corner of the form

                docForm.Location = position;

                // "Reset" the mouse status...
                ReleaseCapture();
                // ...and trick Windows in thinking that the user has clicked on the caption bar.
                SendMessage(docForm.Handle, WmNcLButtonDown, HtCaption, 0);
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
        public static bool RegisterView(DocumentPanel documentPanel, IDocumentView view)
        {
            if (documentPanel == null)
                throw new ArgumentNullException("documentPanel");

            if (view == null)
                throw new ArgumentNullException("view");

            return documentPanel.RegisterViewForDocking(view);
        }
    }
}