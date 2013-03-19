using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// A DocumentPanel can contain Form document objects.
    /// To be able to dock a Form, it either needs to implement <see cref="IDocumentView"/> or be derived from <see cref="DocumentView"/>.
    /// </summary>
    public partial class DocumentPanel : UserControl
    {
        private readonly List<DocumentHeaderButton> _documentButtons = new List<DocumentHeaderButton>();
        private readonly List<IDocumentView> _registeredViews = new List<IDocumentView>();

        private DocumentPanelRenderer _renderer;

        private DocumentHeaderButton _previousDocument = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DocumentPanel()
        {
            InitializeComponent();

            btnScrollLeft.ParentPanel = this;
            btnScrollRight.ParentPanel = this;
            btnCloseActiveView.ParentPanel = this;
            btnShowViews.ParentPanel = this;

            Renderer = new DocumentPanelDefaultRenderer();
        }

        private int GetNextButtonPosition()
        {
            return _documentButtons.Count == 0 ? 0 : _documentButtons.Sum(b => b.Width);
        }

        private void RecalculateHeaderWidth()
        {
            //Reposition the buttons
            int totalWidth = 0;
            foreach (var button in _documentButtons)
            {
                button.Location = new Point(totalWidth + 1, 0);
                totalWidth += button.Width + 1;
            }
            DocumentButtonPanel.Width = totalWidth;
        }

        /// <summary>
        /// Docks the given <paramref name="documentView"/> into this DocumentPanel.
        /// </summary>
        /// <param name="documentView"></param>
        public void DockDocument(IDocumentView documentView)
        {
            Form docForm = documentView as Form;
            docForm.Font = Font;

            //Create a header button
            DocumentHeaderButton button = new DocumentHeaderButton
                {
                    Location = new Point(GetNextButtonPosition(), 0),
                    DocumentView = docForm
                };

            //Add the button to the button panel
            _documentButtons.Add(button);
            DocumentButtonPanel.Controls.Add(button);
            RecalculateHeaderWidth();
            button.ParentDocumentPanel = this;

            button.SizeChanged += (s, e) => RecalculateHeaderWidth();
            docForm.FormClosed += WhenFormClosed;

            //Make the button active, this will select the document.
            SetActiveButton(button);
        }
        
        /// <summary>
        /// Undocks the given <paramref name="documentView"/> from this DocumentPanel.
        /// </summary>
        /// <param name="documentView"></param>
        public void UndockDocument(IDocumentView documentView)
        {
            //Note: this method does not unregister the view: it can still be docked!.

            //Find the button for this view
            DocumentHeaderButton button = _documentButtons.FirstOrDefault(b => b.DocumentView == documentView);
            if (button == null)
                return;

            Form docForm = documentView as Form;
            docForm.FormClosed -= WhenFormClosed;

            //Remove the button from the header
            _documentButtons.Remove(button);
            DocumentButtonPanel.Controls.Remove(button);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();

            //Dispose the button
            button.ParentDocumentPanel = null;
            button.DocumentView = null;
            if (_previousDocument == button)
                _previousDocument = null;

            button.Dispose();

            //Make another button active.
            SetActiveButton(_previousDocument);
        }

        internal void UndockButton(DocumentHeaderButton button)
        {
            //Note: this method does not unregister the view: it can still be docked!

            //Remove the button to the button panel
            _documentButtons.Remove(button);
            DocumentButtonPanel.Controls.Remove(button);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();

            if (_previousDocument == button)
                _previousDocument = null;

            //Make another button active.
            SetActiveButton(_previousDocument);

            Form docForm = button.DocumentView as Form;
            docForm.FormClosed -= WhenFormClosed;

            DocumentViewHelper.UndockView(button.DocumentView as IDocumentView);
        }

        internal void SetActiveButton(DocumentHeaderButton button)
        {
            //deactivate all other buttons
            var otherButton = _documentButtons.FirstOrDefault(b => b.IsActive);
            if (otherButton == button && otherButton != null)
                return;

            _previousDocument = otherButton;
            if (otherButton != null)
                otherButton.IsActive = false;

            if (button == null)
            {
                //Get the last document button
                if (_documentButtons.Any())
                    button = _documentButtons.Last();
            }

            if (button == null || !(button.DocumentView as IDocumentView).IsDocked)
                return;

            button.ParentDocumentPanel = this;
            button.IsActive = true;

            //set the form in the bottom control
            DocumentHolderPanel.Controls.Clear();
            DocumentHolderPanel.Controls.Add(button.DocumentView);
            button.DocumentView.Show(); //makes sure that it's visible
        }

        /// <summary>
        /// Gets or sets whether the close button at the top right corner is enabled or not.
        /// </summary>
        [Category("Behavior"), Description("When enabled, a close button is available at the top right of the DocumentPanel."), DefaultValue(false)]
        public bool CloseEnabled
        {
            get { return pnlCloseActiveView.Visible; }
            set { pnlCloseActiveView.Visible = value; }
        }

        /// <summary>
        /// Gets or sets the renderer used to draw the <see cref="DocumentPanel"/> and related controls.
        /// </summary>
        [Browsable(false)]
        public DocumentPanelRenderer Renderer
        {
            get { return _renderer; } 
            set
            {
                if (value == null)
                    value = new DocumentPanelDefaultRenderer();

                if (_renderer == value)
                    return;

                _renderer = value;
                DocumentHolderPanel.BackColor = _renderer.ColorTable.DocumentPanelBackground;
                Invalidate();
            }
        }

        /// <summary>
        /// Returns the currently active <see cref="IDocumentView"/>.
        /// </summary>
        public IDocumentView ActiveView
        {
            get
            {
                if (!_documentButtons.Any())
                    return null;

                return _documentButtons.First(b => b.IsActive).DocumentView as IDocumentView;
            }
        }

        private void WhenHeaderPaintFired(object sender, PaintEventArgs e)
        {
            Renderer.DrawHeader(new HeaderRenderEventArgs(e.Graphics, HeaderPanel, e.ClipRectangle, Renderer.ColorTable.DocumentTabBackground, Renderer.ColorTable.DocumentTabBottomBackground));
        }

        private void WhenCloseClicked(object sender, EventArgs e)
        {
            if (ActiveView == null) 
                return;

            var local = ActiveView as Form;
            if (local == null)
                return;

            local.FormClosed -= WhenFormClosed;
            local.Close();
            DocumentViewHelper.Undock(ActiveView);

            local.Dispose();
        }

        private void WhenFormClosed(object sender, EventArgs e)
        {
            Form docForm = sender as Form;
            if (docForm == null)
                return;

            UndockDocument(docForm as IDocumentView);
        }

        /// <summary>
        /// Registers the given <paramref name="view"/> in the <see cref="DocumentPanel"/>.
        /// When the user drags the view over the panel, it can be docked.
        /// </summary>
        /// <param name="view"></param>
        /// <returns>True if registration is successfull, false otherwhise (typically: it has already been registered).</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="view"/> is a null reference.</exception>
        public bool RegisterViewForDocking(IDocumentView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            if (_registeredViews.Contains(view))
                return false;

            Form docForm = view as Form;
            if (docForm == null)
                return false;

            _registeredViews.Add(view);
            docForm.Move += (s, e) =>
                {
                    IDocumentView document = docForm as IDocumentView;
                    if (document.IsDockingOrUndocking)
                        return;

                    if (pnlFlowHolder.ClientRectangle.Contains(PointToClient(docForm.Location)))
                    {
                        DocumentHolderPanel.SuspendLayout();
                        try
                        {
                            WindowsApi.ReleaseCapture();
                            DocumentViewHelper.Dock(this, document);
                        }
                        finally
                        {
                            DocumentHolderPanel.ResumeLayout(true);
                        }
                    }
                };
            docForm.Disposed += (s, e) => _registeredViews.Remove(docForm as IDocumentView);

            return true;
        }

        // On 64-bit systems, SetWindowPos fails silently if the controls are nested too deeply.
        // To fix this, we need to fire SizeChanged ourselves.
        // More info: http://support.microsoft.com/kb/953934
        protected override void OnSizeChanged(EventArgs e)
        {
            if (IsHandleCreated)
            {
                BeginInvoke((MethodInvoker)(() => base.OnSizeChanged(e)));
            }
            else
                base.OnSizeChanged(e);
        }
    }
}
