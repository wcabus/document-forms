namespace DocumentForms
{
    partial class DocumentPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DocumentHolderPanel = new System.Windows.Forms.Panel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.pnlFlowHolder = new System.Windows.Forms.Panel();
            this.DocumentButtonPanel = new System.Windows.Forms.Panel();
            this.pnlScrollRight = new System.Windows.Forms.Panel();
            this.pnlCloseActiveView = new System.Windows.Forms.Panel();
            this.pnlShowAllViews = new System.Windows.Forms.Panel();
            this.ScrollLeftPanel = new System.Windows.Forms.Panel();
            this.buttonContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCloseActiveViewFromMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseAllDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseAllButActive = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnScrollRight = new DocumentForms.FlatButton();
            this.btnCloseActiveView = new DocumentForms.FlatButton();
            this.btnShowViews = new DocumentForms.FlatButton();
            this.btnScrollLeft = new DocumentForms.FlatButton();
            this.HeaderPanel.SuspendLayout();
            this.pnlFlowHolder.SuspendLayout();
            this.pnlScrollRight.SuspendLayout();
            this.pnlCloseActiveView.SuspendLayout();
            this.pnlShowAllViews.SuspendLayout();
            this.ScrollLeftPanel.SuspendLayout();
            this.buttonContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DocumentHolderPanel
            // 
            this.DocumentHolderPanel.AutoScroll = true;
            this.DocumentHolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentHolderPanel.Location = new System.Drawing.Point(0, 21);
            this.DocumentHolderPanel.Name = "DocumentHolderPanel";
            this.DocumentHolderPanel.Size = new System.Drawing.Size(649, 278);
            this.DocumentHolderPanel.TabIndex = 1;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.pnlFlowHolder);
            this.HeaderPanel.Controls.Add(this.pnlScrollRight);
            this.HeaderPanel.Controls.Add(this.pnlCloseActiveView);
            this.HeaderPanel.Controls.Add(this.pnlShowAllViews);
            this.HeaderPanel.Controls.Add(this.ScrollLeftPanel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(649, 21);
            this.HeaderPanel.TabIndex = 0;
            this.HeaderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WhenHeaderPaintFired);
            // 
            // pnlFlowHolder
            // 
            this.pnlFlowHolder.AllowDrop = true;
            this.pnlFlowHolder.BackColor = System.Drawing.Color.Transparent;
            this.pnlFlowHolder.Controls.Add(this.DocumentButtonPanel);
            this.pnlFlowHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFlowHolder.Location = new System.Drawing.Point(18, 0);
            this.pnlFlowHolder.Name = "pnlFlowHolder";
            this.pnlFlowHolder.Size = new System.Drawing.Size(577, 21);
            this.pnlFlowHolder.TabIndex = 5;
            // 
            // DocumentButtonPanel
            // 
            this.DocumentButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.DocumentButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DocumentButtonPanel.Name = "DocumentButtonPanel";
            this.DocumentButtonPanel.Size = new System.Drawing.Size(200, 21);
            this.DocumentButtonPanel.TabIndex = 0;
            // 
            // pnlScrollRight
            // 
            this.pnlScrollRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlScrollRight.Controls.Add(this.btnScrollRight);
            this.pnlScrollRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlScrollRight.Location = new System.Drawing.Point(595, 0);
            this.pnlScrollRight.Name = "pnlScrollRight";
            this.pnlScrollRight.Size = new System.Drawing.Size(18, 21);
            this.pnlScrollRight.TabIndex = 4;
            // 
            // pnlCloseActiveView
            // 
            this.pnlCloseActiveView.BackColor = System.Drawing.Color.Transparent;
            this.pnlCloseActiveView.Controls.Add(this.btnCloseActiveView);
            this.pnlCloseActiveView.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCloseActiveView.Location = new System.Drawing.Point(613, 0);
            this.pnlCloseActiveView.Name = "pnlCloseActiveView";
            this.pnlCloseActiveView.Size = new System.Drawing.Size(18, 21);
            this.pnlCloseActiveView.TabIndex = 3;
            this.pnlCloseActiveView.Visible = false;
            // 
            // pnlShowAllViews
            // 
            this.pnlShowAllViews.BackColor = System.Drawing.Color.Transparent;
            this.pnlShowAllViews.Controls.Add(this.btnShowViews);
            this.pnlShowAllViews.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlShowAllViews.Location = new System.Drawing.Point(631, 0);
            this.pnlShowAllViews.Name = "pnlShowAllViews";
            this.pnlShowAllViews.Size = new System.Drawing.Size(18, 21);
            this.pnlShowAllViews.TabIndex = 2;
            // 
            // ScrollLeftPanel
            // 
            this.ScrollLeftPanel.BackColor = System.Drawing.Color.Transparent;
            this.ScrollLeftPanel.Controls.Add(this.btnScrollLeft);
            this.ScrollLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ScrollLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.ScrollLeftPanel.Name = "ScrollLeftPanel";
            this.ScrollLeftPanel.Size = new System.Drawing.Size(18, 21);
            this.ScrollLeftPanel.TabIndex = 1;
            // 
            // buttonContextMenu
            // 
            this.buttonContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseActiveViewFromMenu,
            this.btnCloseAllDocs,
            this.btnCloseAllButActive});
            this.buttonContextMenu.Name = "buttonContextMenu";
            this.buttonContextMenu.Size = new System.Drawing.Size(185, 70);
            // 
            // btnCloseActiveViewFromMenu
            // 
            this.btnCloseActiveViewFromMenu.Name = "btnCloseActiveViewFromMenu";
            this.btnCloseActiveViewFromMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.btnCloseActiveViewFromMenu.Size = new System.Drawing.Size(184, 22);
            this.btnCloseActiveViewFromMenu.Text = "&Close";
            this.btnCloseActiveViewFromMenu.Click += new System.EventHandler(this.WhenCloseContextClicked);
            // 
            // btnCloseAllDocs
            // 
            this.btnCloseAllDocs.Name = "btnCloseAllDocs";
            this.btnCloseAllDocs.Size = new System.Drawing.Size(184, 22);
            this.btnCloseAllDocs.Text = "Close &All Documents";
            this.btnCloseAllDocs.Click += new System.EventHandler(this.WhenCloseAllClicked);
            // 
            // btnCloseAllButActive
            // 
            this.btnCloseAllButActive.Name = "btnCloseAllButActive";
            this.btnCloseAllButActive.Size = new System.Drawing.Size(184, 22);
            this.btnCloseAllButActive.Text = "Close All But &This";
            this.btnCloseAllButActive.Click += new System.EventHandler(this.WhenCloseAllButThisClicked);
            // 
            // viewMenuStrip
            // 
            this.viewMenuStrip.Name = "viewMenuStrip";
            this.viewMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // btnScrollRight
            // 
            this.btnScrollRight.ArrowDirection = System.Windows.Forms.ArrowDirection.Right;
            this.btnScrollRight.IsArrowButton = true;
            this.btnScrollRight.IsCloseButton = false;
            this.btnScrollRight.Location = new System.Drawing.Point(0, 3);
            this.btnScrollRight.Name = "btnScrollRight";
            this.btnScrollRight.ParentPanel = null;
            this.btnScrollRight.Size = new System.Drawing.Size(15, 15);
            this.btnScrollRight.TabIndex = 0;
            // 
            // btnCloseActiveView
            // 
            this.btnCloseActiveView.ArrowDirection = System.Windows.Forms.ArrowDirection.Right;
            this.btnCloseActiveView.IsArrowButton = false;
            this.btnCloseActiveView.IsCloseButton = true;
            this.btnCloseActiveView.Location = new System.Drawing.Point(0, 3);
            this.btnCloseActiveView.Name = "btnCloseActiveView";
            this.btnCloseActiveView.ParentPanel = null;
            this.btnCloseActiveView.Size = new System.Drawing.Size(15, 15);
            this.btnCloseActiveView.TabIndex = 1;
            this.btnCloseActiveView.Click += new System.EventHandler(this.WhenCloseClicked);
            // 
            // btnShowViews
            // 
            this.btnShowViews.ArrowDirection = System.Windows.Forms.ArrowDirection.Down;
            this.btnShowViews.IsArrowButton = true;
            this.btnShowViews.IsCloseButton = false;
            this.btnShowViews.Location = new System.Drawing.Point(0, 3);
            this.btnShowViews.Name = "btnShowViews";
            this.btnShowViews.ParentPanel = null;
            this.btnShowViews.Size = new System.Drawing.Size(15, 15);
            this.btnShowViews.TabIndex = 1;
            this.btnShowViews.Click += new System.EventHandler(this.OpenViewMenu);
            // 
            // btnScrollLeft
            // 
            this.btnScrollLeft.ArrowDirection = System.Windows.Forms.ArrowDirection.Left;
            this.btnScrollLeft.IsArrowButton = true;
            this.btnScrollLeft.IsCloseButton = false;
            this.btnScrollLeft.Location = new System.Drawing.Point(3, 3);
            this.btnScrollLeft.Name = "btnScrollLeft";
            this.btnScrollLeft.ParentPanel = null;
            this.btnScrollLeft.Size = new System.Drawing.Size(15, 15);
            this.btnScrollLeft.TabIndex = 0;
            // 
            // DocumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DocumentHolderPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Name = "DocumentPanel";
            this.Size = new System.Drawing.Size(649, 299);
            this.HeaderPanel.ResumeLayout(false);
            this.pnlFlowHolder.ResumeLayout(false);
            this.pnlScrollRight.ResumeLayout(false);
            this.pnlCloseActiveView.ResumeLayout(false);
            this.pnlShowAllViews.ResumeLayout(false);
            this.ScrollLeftPanel.ResumeLayout(false);
            this.buttonContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DocumentHolderPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Panel ScrollLeftPanel;
        private System.Windows.Forms.Panel pnlScrollRight;
        private System.Windows.Forms.Panel pnlCloseActiveView;
        private System.Windows.Forms.Panel pnlShowAllViews;
        private System.Windows.Forms.Panel pnlFlowHolder;
        private FlatButton btnScrollRight;
        private FlatButton btnCloseActiveView;
        private FlatButton btnShowViews;
        private FlatButton btnScrollLeft;
        private System.Windows.Forms.Panel DocumentButtonPanel;
        private System.Windows.Forms.ContextMenuStrip buttonContextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnCloseActiveViewFromMenu;
        private System.Windows.Forms.ToolStripMenuItem btnCloseAllDocs;
        private System.Windows.Forms.ToolStripMenuItem btnCloseAllButActive;
        private System.Windows.Forms.ContextMenuStrip viewMenuStrip;
    }
}
