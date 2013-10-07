using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This class implements the basic behavior of a document view Form.
    /// </summary>
    public class DocumentView : Form, IDocumentView
    {
        private FormBorderStyle _originalBorderStyle;
        private Font _originalFont;
        private FormWindowState _originalState;
        private bool _originalInTaskbar;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DocumentView()
        {
            _originalBorderStyle = base.FormBorderStyle;
            _originalFont = base.Font;
            _originalInTaskbar = base.ShowInTaskbar;
            _originalState = base.WindowState;
        }

        public bool IsDocked { get; set; }
        public DocumentPanel DocumentPanel { get; set; }

        public bool IsDockingOrUndocking { get; set; }

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

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        public new Font Font
        {
            get { return _originalFont; }
            set
            {
                if (Equals(_originalFont, value))
                    return;

                _originalFont = value;

                if (IsDocked)
                    return; //do not overwrite the font if this Form is currently in a docked state.

                base.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the form is minimized, maximized, or normal.
        /// </summary>
        public new FormWindowState WindowState
        {
            get { return _originalState; }
            set
            {
                if (_originalState == value)
                    return;

                _originalState = value;
                if (!IsDocked)
                    base.WindowState = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the form is displayed in the Windows taskbar.
        /// </summary>
        public new bool ShowInTaskbar
        {
            get { return _originalInTaskbar; }
            set
            {
                if (_originalInTaskbar == value)
                    return;

                _originalInTaskbar = value;
                if (!IsDocked)
                    base.ShowInTaskbar = value;
            }
        }

        public FormBorderStyle WindowBorderStyle
        {
            get { return _originalBorderStyle; }
        }

        public Font WindowFont
        {
            get { return _originalFont; }
        }

        public FormWindowState OriginalWindowState
        {
            get { return _originalState; }
        }

        public bool WindowInTaskbar
        {
            get { return _originalInTaskbar; }
        }

        public virtual bool AllowClosing { get; protected set; }

        public void Show(bool startDocked, IDocumentView currentView)
        {
            //This form will be docked if startDocked is true or either currentView is null or currentView exists and is docked.
            bool docked = startDocked || currentView == null || currentView.IsDocked;

            if (docked)
                DockView();
            else
                Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            AllowClosing = !e.Cancel; 
        }

        private void DockView()
        {
            DocumentViewHelper.Dock(this);
        }
    }
}