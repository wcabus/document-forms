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
        private readonly List<IDocumentHeaderButton> _documentButtons = new List<IDocumentHeaderButton>();
        private readonly List<IDocumentView> _registeredViews = new List<IDocumentView>();

        private DocumentPanelRenderer _renderer;
        private IDocumentHeaderButton _previousDocument = null;

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

            AllowCloseUsingMiddleMouse = true;
            
            Renderer = new DocumentPanelDefaultRenderer();

            btnScrollLeft.Click += (s, e) => ScrollToButton(-1);
            btnScrollRight.Click += (s, e) => ScrollToButton(1);
        }

        /// <summary>
        /// Allows closing docked documents by clicking on their button using the middle mouse button.
        /// </summary>
        [Browsable(true), 
        Description("Gets or sets if its allowed to close docked documents when the user clicks the header button with the middle mouse button."),
        Category("Behavior"),
        DefaultValue(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowCloseUsingMiddleMouse { get; set; }

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
        public void DockDocument<TView>(TView documentView) where TView : Form, IDocumentView
        {
            documentView.Font = Font;

            //Create a header button
            var button = new DocumentHeaderButton<TView>
                {
                    Location = new Point(GetNextButtonPosition(), 0),
                    DocumentView = documentView,
                    ContextMenuStrip = buttonContextMenu
                };

            var mnuActivateButton = new ToolStripMenuItem(button.Text);
            button.ToolStripMenuItem = mnuActivateButton;
            viewMenuStrip.Items.Add(mnuActivateButton);

            //Add the button to the button panel
            _documentButtons.Add(button);
            DocumentButtonPanel.Controls.Add(button);
            RecalculateHeaderWidth();
            button.ParentDocumentPanel = this;

            button.SizeChanged += (s, e) => RecalculateHeaderWidth();
            documentView.FormClosed += WhenFormClosed;

            //Make the button active, this will select the document.
            SetActiveButton(button);
        }
        
        /// <summary>
        /// Undocks the given <paramref name="documentView"/> from this DocumentPanel.
        /// </summary>
        /// <param name="documentView"></param>
        public void UndockDocument(Form documentView)
        {
            //Note: this method does not unregister the view: it can still be docked!

            //Find the button for this view
            var button = _documentButtons.FirstOrDefault(b => b.OwnedForm == documentView);
            if (button == null)
                return;

            documentView.FormClosed -= WhenFormClosed;

            //Remove the button from the header
            _documentButtons.Remove(button);
            DocumentButtonPanel.Controls.Remove(button as Control);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();

            //Dispose the button
            viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
            button.ParentDocumentPanel = null;
            button.SetViewNull();
            if (_previousDocument == button)
                _previousDocument = null;

            button.ToolStripMenuItem.Dispose();
            button.Dispose();

            //Make another button active.
            SetActiveButton(_previousDocument);
        }

        internal void UndockButton(IDocumentHeaderButton button)
        {
            //Note: this method does not unregister the view: it can still be docked!

            //Remove the button to the button panel
            _documentButtons.Remove(button);
            viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
            DocumentButtonPanel.Controls.Remove(button as Control);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();

            if (_previousDocument == button)
                _previousDocument = null;

            //Make another button active.
            SetActiveButton(_previousDocument);

            Form docForm = button.OwnedForm;
            docForm.FormClosed -= WhenFormClosed;

            DocumentViewHelper.UndockView(button.OwnedView);

            //Dispose the button
            button.ParentDocumentPanel = null;
            button.SetViewNull();

            button.ToolStripMenuItem.Dispose();
            button.Dispose();
        }

        internal IDocumentHeaderButton GetActiveButton()
        {
            return _documentButtons.FirstOrDefault(b => b.IsActive);
        }

        internal void SetActiveButton(IDocumentHeaderButton button)
        {
            //deactivate all other buttons
            var otherButton = GetActiveButton();
            if (otherButton == button && otherButton != null)
                return;

            _previousDocument = otherButton;
            if (otherButton != null)
                otherButton.IsActive = false;

            if (button == null || (button as Control).IsDisposed)
            {
                //Get the last document button
                if (_documentButtons.Any())
                    button = _documentButtons.Last();
            }

            if (button == null || (button as Control).IsDisposed || !(button.OwnedView).IsDocked)
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
                DocumentHolderPanel.Controls.Add(button.OwnedForm);
                button.OwnedForm.Show(); //Makes sure that the document is visible
            }
            finally
            {
                DocumentHolderPanel.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Gets or sets whether the close button at the top right corner is enabled or not.
        /// </summary>
        [Category("Behavior"),
        Description("When enabled, a close button is available at the top right of the DocumentPanel."), 
        DefaultValue(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        /// Gets or sets the currently active <see cref="IDocumentView"/>.
        /// </summary>
        public IDocumentView ActiveView
        {
            get
            {
                var button = GetActiveButton();
                return button == null ? null : button.OwnedView;
            }
            set
            {
                // Can't set null as the active view.
                if (value == null)
                    throw new ArgumentNullException("value");

                if (value == ActiveView)
                    return; //Do not act if we're setting the same view.

                // Can't set an undocked view as the active view.
                if (!value.IsDocked)
                    throw new InvalidOperationException("Unable to set the view as the active view, because IsDocked returned false.");

                var button = _documentButtons.FirstOrDefault(b => b.OwnedForm == value);
                if (button == null)
                    throw new NotSupportedException("Unable to set the view as the active view, because it doesn't seem to be registered with this DocumentPanel.");

                SetActiveButton(button);
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
            if (!(local as IDocumentView).AllowClosing)
            {
                local.FormClosed += WhenFormClosed;
                return;    
            }

            DocumentViewHelper.Undock(ActiveView);

            local.Dispose();
        }

        private void WhenFormClosed(object sender, EventArgs e)
        {
            Form docForm = sender as Form;
            if (docForm == null)
                return;

            UndockDocument(docForm);
        }

        /// <summary>
        /// Registers the given <paramref name="view"/> in the <see cref="DocumentPanel"/>.
        /// When the user drags the view over the panel, it can be docked.
        /// </summary>
        /// <param name="view"></param>
        /// <returns>True if registration is successfull, false otherwhise (typically: it has already been registered).</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="view"/> is a null reference.</exception>
        public bool RegisterViewForDocking<TView>(TView view) where TView : Form, IDocumentView
        {
            if (view == null)
                throw new ArgumentNullException("view");

            if (_registeredViews.Contains(view))
                return false;

            _registeredViews.Add(view);
            view.DocumentPanel = this;
            view.Move += (s, e) =>
                {
                    if (view.IsDockingOrUndocking)
                        return;

                    if (pnlFlowHolder.ClientRectangle.Contains(PointToClient(view.Location)))
                    {
                        DocumentHolderPanel.SuspendLayout();
                        try
                        {
                            WindowsApi.ReleaseCapture();
                            DocumentViewHelper.Dock(view);
                        }
                        finally
                        {
                            DocumentHolderPanel.ResumeLayout(true);
                        }
                    }
                };
            view.Disposed += (s, e) => _registeredViews.Remove(view);

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
        }

        private void WhenCloseAllClicked(object sender, EventArgs e)
        {
            CloseAllDocumentsWhere(b => true);
        }

        private void CloseAllDocumentsWhere(Predicate<IDocumentHeaderButton> whereClause)
        {
            SuspendLayout();
            try
            {
                var buttonsToRemove = new List<IDocumentHeaderButton>();
                foreach (var button in _documentButtons.Where(b => whereClause(b)))
                {
                    button.OwnedForm.FormClosed -= WhenFormClosed;
                    button.OwnedForm.Close();
                    if ((button.OwnedForm as IDocumentView).AllowClosing)
                    {
                        button.OwnedForm.Dispose();

                        viewMenuStrip.Items.Remove(button.ToolStripMenuItem);
                        button.ParentDocumentPanel = null;
                        button.SetViewNull();

                        button.ToolStripMenuItem.Dispose();
                        button.Dispose();

                        buttonsToRemove.Add(button);
                    }
                }

                foreach (var button in buttonsToRemove)
                    _documentButtons.Remove(button);
            }
            finally
            {
                ResumeLayout(true);
            }

            RepositionHeaderAndButtons();
        }

        private void OpenViewMenu(object sender, EventArgs e)
        {
            Point position = btnShowViews.Location;
            position.Offset(0, btnShowViews.Height);
            viewMenuStrip.Show(btnShowViews, position);
        }

        private void RepositionHeaderAndButtons()
        {
            SuspendLayout();
            try
            {
                // Reposition the remaining buttons. There could be more than one if there are windows that couldn't be closed.
                var position = Point.Empty;
                foreach (var button in _documentButtons)
                {
                    button.Location = position;
                    position.Offset(button.Width, 0); //Increase the position for the next button.
                }
                // Reset the position of the panel to the starting point
                DocumentButtonPanel.Location = Point.Empty;

                RecalculateHeaderWidth();
            }
            finally
            {
                ResumeLayout(true);
            }

            if (GetActiveButton() == null)
                SetActiveButton(_documentButtons.First());
        }

        /// <summary>
        /// When pressing CTRL+F4, the active window should be closed.
        /// </summary>
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (sender != ActiveView)
                return;

            if (e.Control && e.KeyCode == Keys.F4)
                WhenCloseClicked(this, EventArgs.Empty); //Closes the active view
        }
    }
}
