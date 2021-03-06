﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    /// <summary>
    /// This control is a button that sits in the header part of a <see cref="DocumentPanel"/>.
    /// It allows selecting a docked <see cref="IDocumentView"/>, closing it and tearing it off.
    /// </summary>
    internal partial class DocumentHeaderButton<TView> : UserControl, IDocumentHeaderButton where TView : Form, IDocumentView
    {
        private bool _isActive;
        private bool _isMouseOver;

        private TView _documentView;
        private DocumentPanel _parentDocumentPanel;

        public DocumentHeaderButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Form"/> associated with this button.
        /// </summary>
        /// <remarks>
        /// Because the <see cref="DocumentHeaderButton{T}"/> can only be created by <see cref="DocumentPanel"/> objects,
        /// we may assume that the DocumentView also implements the <see cref="IDocumentView"/> interface.
        /// </remarks>
        public TView DocumentView
        {
            get { return _documentView; }
            set { 
                if (_documentView == value)
                    return;

                if (_documentView != null)
                    _documentView.TextChanged -= WhenUpdateButtonText;

                _documentView = value;
                if (value == null)
                    return;

                btnClose.ParentPanel = ParentDocumentPanel;

                //Make sure to update the button if the text of the document would change
                _documentView.TextChanged += WhenUpdateButtonText;
                UpdateButtonText(); //And of course, update now.
            }
        }

        public Form OwnedForm { get { return _documentView; } }
        public IDocumentView OwnedView { get { return _documentView; } }

        private void WhenUpdateButtonText(object sender, EventArgs e)
        {
            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            //Update the text on the button
            lblDocumentText.Text = _documentView.Text;
            if (_toolStripMenuItem != null)
                _toolStripMenuItem.Text = _documentView.Text;

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

                lblDocumentText.Font = _isActive ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabFont : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabFont;
                pnlFill.BackColor = _isActive || IsMouseHover ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabBackground : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabBackground;
                pnlBottom.BackColor = _isActive || IsMouseHover ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabBottomBackground : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabBottomBackground;
                lblDocumentText.ForeColor = _isActive || IsMouseHover
                                                ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabForeground
                                                : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabForeground;

                if (_toolStripMenuItem != null)
                {
                    _toolStripMenuItem.Font = _isActive
                                                  ? new Font(_toolStripMenuItem.Font, FontStyle.Bold)
                                                  : new Font(_toolStripMenuItem.Font, FontStyle.Regular);
                }

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
                pnlFill.BackColor = _isActive || IsMouseHover ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabBackground : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabBackground;
                pnlBottom.BackColor = _isActive || IsMouseHover ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabBottomBackground : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabBottomBackground;
                lblDocumentText.ForeColor = _isActive || IsMouseHover
                                                ? ParentDocumentPanel.Renderer.ColorTable.ActiveDocumentTabForeground
                                                : ParentDocumentPanel.Renderer.ColorTable.InactiveDocumentTabForeground;

            } 
        }

        public ToolStripMenuItem ToolStripMenuItem
        {
            get { return _toolStripMenuItem; }
            set
            {
                if (_toolStripMenuItem == value)
                    return;

                _toolStripMenuItem = value;
                if (_toolStripMenuItem != null)
                    _toolStripMenuItem.Click += (s, e) => ParentDocumentPanel.SetActiveButton(this);
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
        
        private Point _dragStartPos = Point.Empty;
        private ToolStripMenuItem _toolStripMenuItem;

        // When moving the button while the left mouse button is pressed, undock the window
        // if the user drags away for a certain amount of pixels.
        private void WhenMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragStartPos = e.Location;
        }

        private void WhenMouseUp(object sender, MouseEventArgs e)
        {
            ParentDocumentPanel.SetActiveButton(this);

            if (e.Button == MouseButtons.Left)
                _dragStartPos = Point.Empty;

            if (e.Button == MouseButtons.Middle && ParentDocumentPanel.AllowCloseUsingMiddleMouse)
                WhenCloseClicked(sender, e);
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

            // Try to close the window. If this window contains code that would prevent it from closing, 
            // then AllowClosing should be set to false.
            local.Close(); 
            if (!local.AllowClosing)
                return;

            local.Visible = false;  //Make sure that the window is hidden, before Undock would pop it up.
            DocumentViewHelper.Undock(local); //sets DocumentView to null 
            // (Note that the DocumentView property already is null if the window was closed, 
            // because we listen to the FormClosed event handler).
            
            local.Dispose();
        }

        public void SetViewNull()
        {
            DocumentView = null;
        }
    }
}
