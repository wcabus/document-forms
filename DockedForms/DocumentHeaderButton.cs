﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This control is a button that sits in the header part of a <see cref="DocumentPanel"/>.
    /// It allows selecting a docked <see cref="IDocumentView"/>, closing it and tearing it off.
    /// </summary>
    internal partial class DocumentHeaderButton : UserControl
    {
        private bool _isActive;
        private bool _isMouseOver;
        
        private Form _documentView;
        private DocumentPanel _parentDocumentPanel;

        public DocumentHeaderButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Form"/> associated with this button.
        /// </summary>
        /// <remarks>
        /// Because the <see cref="DocumentHeaderButton"/> can only be created by <see cref="DocumentPanel"/> objects,
        /// we may assume that the DocumentView also implements the <see cref="IDocumentView"/> interface.
        /// </remarks>
        public Form DocumentView
        {
            get { return _documentView; }
            set { 
                if (_documentView == value)
                    return;

                _documentView = value;
                if (value == null)
                    return;

                btnClose.ParentPanel = ParentDocumentPanel;

                //Make sure to update the button if the text of the document would change
                _documentView.TextChanged += (s, e) => UpdateButtonText();
                UpdateButtonText(); //And of course, update now.
            }
        }

        private void UpdateButtonText()
        {
            //Update the text on the button
            lblDocumentText.Text = _documentView.Text;

            //Resize the button
            int textWidth = TextRenderer.MeasureText(lblDocumentText.Text, lblDocumentText.Font).Width;
            Size = new Size(textWidth + 9 + btnClose.Width, Size.Height);
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value)
                    return;

                _isActive = value;

                lblDocumentText.Font = new Font(lblDocumentText.Font, _isActive ? FontStyle.Bold : FontStyle.Regular);
                pnlFill.BackColor = _isActive || IsMouseHover ? SystemColors.ActiveCaption : SystemColors.InactiveCaption;
                pnlBottom.BackColor = _isActive || IsMouseHover ? SystemColors.ActiveCaption : Color.Transparent;
                
                UpdateButtonText(); //font has changed, resize the button accordingly
            }
        }

        public DocumentPanel ParentDocumentPanel
        {
            get { return _parentDocumentPanel; }
            set
            {
                if (_parentDocumentPanel == value)
                    return;

                _parentDocumentPanel = value;
                btnClose.ParentPanel = _parentDocumentPanel;
            }
        }

        public bool IsMouseHover { 
            get { return _isMouseOver; }
            private set
            {
                if (_isMouseOver == value)
                    return;

                _isMouseOver = value;
                pnlFill.BackColor = _isActive || IsMouseHover ? SystemColors.ActiveCaption : SystemColors.InactiveCaption;
                pnlBottom.BackColor = _isActive || IsMouseHover ? SystemColors.ActiveCaption : Color.Transparent;
            } 
        }

        private void WhenButtonMouseEnter(object sender, EventArgs e)
        {
            IsMouseHover = true;
        }

        private void WhenButtonMouseLeave(object sender, EventArgs e)
        {
            IsMouseHover = false;
        }

        private void WhenButtonClick(object sender, EventArgs e)
        {
            ParentDocumentPanel.SetActiveButton(this);
        }

        private Point _dragStartPos = Point.Empty;

        // When moving the button while the left mouse button is pressed, undock the window
        // if the user drags away for a certain amount of pixels.
        private void WhenMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragStartPos = e.Location;
        }

        private void WhenMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragStartPos = Point.Empty;
        }

        private void WhenDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _dragStartPos != Point.Empty)
            {
                Point p = new Point(e.X - _dragStartPos.X, e.Y - _dragStartPos.Y);
                
                if (Math.Abs(p.X) > 25 || Math.Abs(p.Y) > 25)
                    ParentDocumentPanel.UndockButton(this);
            }
        }

        private void WhenCloseClicked(object sender, EventArgs e)
        {
            var local = DocumentView;
            local.Close(); //hides the form already, before Undock would pop it up.
            DocumentViewHelper.Undock(DocumentView as IDocumentView); //sets DocumentView to null

            local.Dispose();
        }
    }
}
