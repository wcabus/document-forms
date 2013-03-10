using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Default constructor
        /// </summary>
        public DocumentPanel()
        {
            Renderer = new DocumentPanelDefaultRenderer();
            
            InitializeComponent();
            btnScrollLeft.ParentPanel = this;
            btnScrollRight.ParentPanel = this;
            btnCloseActiveView.ParentPanel = this;
            btnShowViews.ParentPanel = this;
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
        public void DockDocument<TView>(TView documentView) where TView : Form, IDocumentView
        {
            //Create a header button
            DocumentHeaderButton button = new DocumentHeaderButton
                {
                    Location = new Point(GetNextButtonPosition(), 0),
                    DocumentView = documentView
                };

            //Add the button to the button panel
            _documentButtons.Add(button);
            DocumentButtonPanel.Controls.Add(button);
            RecalculateHeaderWidth();
            button.ParentDocumentPanel = this;

            button.SizeChanged += (s, e) => RecalculateHeaderWidth();

            //Make the button active, this will select the document.
            SetActiveButton(button);
        }
        
        /// <summary>
        /// Undocks the given <paramref name="documentView"/> from this DocumentPanel.
        /// </summary>
        /// <param name="documentView"></param>
        public void UndockDocument(IDocumentView documentView)
        {
            //Find the button for this view
            DocumentHeaderButton button = _documentButtons.FirstOrDefault(b => b.DocumentView == documentView);
            if (button == null)
                return;

            //Remove the button from the header
            _documentButtons.Remove(button);
            DocumentButtonPanel.Controls.Remove(button);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();

            //Dispose the button
            button.ParentDocumentPanel = null;
            button.DocumentView = null;
            button.Dispose();

            //Make another button active.
            if (_documentButtons.Any())
                SetActiveButton(_documentButtons.First());
        }

        internal void UndockButton(DocumentHeaderButton button)
        {
            //Remove the button to the button panel
            _documentButtons.Remove(button);
            DocumentButtonPanel.Controls.Remove(button);
            RecalculateHeaderWidth();

            DocumentHolderPanel.Controls.Clear();
            //Make another button active.
            if (_documentButtons.Any())
                SetActiveButton(_documentButtons.First());

            DocumentViewHelper.UndockView(button.DocumentView as IDocumentView);
        }

        internal void SetActiveButton(DocumentHeaderButton button)
        {
            //deactivate all other buttons
            foreach (var otherButton in _documentButtons.Where(b => b != button))
                otherButton.IsActive = false;

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
        public DocumentPanelRenderer Renderer { get; set; }

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
            Renderer.DrawHeader(new HeaderRenderEventArgs(e.Graphics, HeaderPanel, e.ClipRectangle, SystemColors.Control, SystemColors.ControlDarkDark));
        }
    }
}
