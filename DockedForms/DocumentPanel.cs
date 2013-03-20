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

            btnScrollLeft.Click += (s, e) => ScrollToButton(-1);
            btnScrollRight.Click += (s, e) => ScrollToButton(1);
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

            button.ContextMenuStrip = buttonContextMenu;
            var mnuActivateButton = new ToolStripMenuItem(button.Text);
            button.ToolStripMenuItem = mnuActivateButton;
            viewMenuStrip.Items.Add(mnuActivateButton);

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
            viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
            button.ParentDocumentPanel = null;
            button.DocumentView = null;
            if (_previousDocument == button)
                _previousDocument = null;

            button.ToolStripMenuItem.Dispose();
            button.Dispose();

            //Make another button active.
            SetActiveButton(_previousDocument);
        }

        internal void UndockButton(DocumentHeaderButton button)
        {
            //Note: this method does not unregister the view: it can still be docked!

            //Remove the button to the button panel
            _documentButtons.Remove(button);
            viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
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

            //Dispose the button
            button.ParentDocumentPanel = null;
            button.DocumentView = null;

            button.ToolStripMenuItem.Dispose();
            button.Dispose();
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

            if (button == null || button.IsDisposed)
            {
                //Get the last document button
                if (_documentButtons.Any())
                    button = _documentButtons.Last();
            }

            if (button == null || button.IsDisposed || !(button.DocumentView as IDocumentView).IsDocked)
                return;

            button.ParentDocumentPanel = this;
            button.IsActive = true;

            //Ensure that this button is visible in the button panel
            var loc = button.Location;
            loc.Offset(DocumentButtonPanel.Location);

            if (loc.X < 0)
                DocumentButtonPanel.Location = new Point(-button.Location.X, 0);
            else if (loc.X + button.Width > pnlFlowHolder.Width)
                DocumentButtonPanel.Location = new Point(-button.Location.X + (pnlFlowHolder.Width - button.Width), 0);

            //Set the form in the bottom control
            DocumentHolderPanel.SuspendLayout();
            try
            {
                DocumentHolderPanel.Controls.Clear();
                DocumentHolderPanel.Controls.Add(button.DocumentView);
                button.DocumentView.Show(); //Makes sure that the document is visible
            }
            finally
            {
                DocumentHolderPanel.ResumeLayout(true);
            }
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

        /// <summary>
        /// Handles when the user clicks the DocumentPanel's close button.
        /// This button closes the currently active document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Scrolls to the next button and brings it into the view.
        /// </summary>
        /// <param name="relativePosition">-1 to scroll left, 1 to scroll right.</param>
        private void ScrollToButton(int relativePosition)
        {
            if (!_documentButtons.Any())
                return;

            var activeButton = _documentButtons.First(b => b.IsActive);
            var newPos = _documentButtons.IndexOf(activeButton) + relativePosition;
            if (newPos < 0 || newPos >= _documentButtons.Count)
                return;

            SetActiveButton(_documentButtons[newPos]);
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

        /// <summary>
        /// User clicked the "Close Document" menu item of the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhenCloseContextClicked(object sender, EventArgs e)
        {
            WhenCloseClicked(sender, e);
        }

        private void WhenCloseAllButThisClicked(object sender, EventArgs e)
        {
            CloseAllDocumentsWhere(b => !b.IsActive);
            SuspendLayout();
            try
            {
                //Reposition the panel and the remaining button.
                _documentButtons.First().Location = Point.Empty;
                DocumentButtonPanel.Location = Point.Empty;
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        private void WhenCloseAllClicked(object sender, EventArgs e)
        {
            CloseAllDocumentsWhere(b => true);
        }

        private void CloseAllDocumentsWhere(Predicate<DocumentHeaderButton> whereClause)
        {
            SuspendLayout();
            try
            {
                foreach (var button in _documentButtons.Where(b => whereClause(b)))
                {
                    button.DocumentView.FormClosed -= WhenFormClosed;
                    button.DocumentView.Close();
                    button.DocumentView.Dispose();

                    viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
                    button.ParentDocumentPanel = null;
                    button.DocumentView = null;

                    button.ToolStripMenuItem.Dispose();
                    button.Dispose();
                }
                _documentButtons.RemoveAll(whereClause);
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        private void OpenViewMenu(object sender, EventArgs e)
        {
            Point position = btnShowViews.Location;
            position.Offset(0, btnShowViews.Height);
            viewMenuStrip.Show(btnShowViews, position);
        }
    }
}
