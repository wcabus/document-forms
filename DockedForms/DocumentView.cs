using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This class implements the basic behavior of a document view Form.
    /// </summary>
    public abstract class DocumentView : Form, IDocumentView
    {
        private FormBorderStyle _originalBorderStyle;
        private DocumentPanel _documentPanel;

        protected DocumentView()
        {
            _originalBorderStyle = base.FormBorderStyle;
        }

        public bool IsDocked { get; set; }

        public DocumentPanel ParentDocumentPanel
        {
            get { return _documentPanel; }
            set { _documentPanel = value; }
        }

        private void SetDocked(bool bDocked)
        {
            if (bDocked)
            {
                base.FormBorderStyle = FormBorderStyle.None;
                Dock = DockStyle.Fill;
            }
            else
            {
                base.FormBorderStyle = _originalBorderStyle;
                Dock = DockStyle.None;
            }
            TopLevel = !bDocked;
            IsDocked = bDocked;
        }

        /// <summary>
        /// Gets or sets the border style of the Form.
        /// </summary>
        ///
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.FormBorderStyle"/> that represents the style of border to display for the form. The default is FormBorderStyle.Sizable.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
        public new FormBorderStyle FormBorderStyle
        {
            get { return _originalBorderStyle; }
            set
            {
                if (_originalBorderStyle == value)
                    return;

                _originalBorderStyle = value;

                if (IsDocked)
                    return; //do not overwrite the style if this Form is currently in a docked state.

                base.FormBorderStyle = value;
            }
        }

        public FormBorderStyle WindowBorderStyle
        {
            get { return _originalBorderStyle; }
        }
    }
}