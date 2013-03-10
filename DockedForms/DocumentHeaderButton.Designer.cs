namespace DocumentForms
{
    partial class DocumentHeaderButton
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
            this.pnlFill = new System.Windows.Forms.Panel();
            this.btnClose = new DocumentForms.FlatButton();
            this.lblDocumentText = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlFill.Controls.Add(this.btnClose);
            this.pnlFill.Controls.Add(this.lblDocumentText);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(77, 18);
            this.pnlFill.TabIndex = 0;
            this.pnlFill.Click += new System.EventHandler(this.WhenButtonClick);
            this.pnlFill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WhenDragStarts);
            this.pnlFill.MouseEnter += new System.EventHandler(this.WhenButtonMouseEnter);
            this.pnlFill.MouseLeave += new System.EventHandler(this.WhenButtonMouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ArrowDirection = System.Windows.Forms.ArrowDirection.Left;
            this.btnClose.IsArrowButton = false;
            this.btnClose.IsCloseButton = true;
            this.btnClose.Location = new System.Drawing.Point(59, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.ParentPanel = null;
            this.btnClose.Size = new System.Drawing.Size(15, 15);
            this.btnClose.TabIndex = 3;
            this.btnClose.MouseEnter += new System.EventHandler(this.WhenButtonMouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.WhenButtonMouseLeave);
            // 
            // lblDocumentText
            // 
            this.lblDocumentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocumentText.AutoSize = true;
            this.lblDocumentText.BackColor = System.Drawing.Color.Transparent;
            this.lblDocumentText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentText.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblDocumentText.Location = new System.Drawing.Point(3, 3);
            this.lblDocumentText.Name = "lblDocumentText";
            this.lblDocumentText.Size = new System.Drawing.Size(53, 13);
            this.lblDocumentText.TabIndex = 2;
            this.lblDocumentText.Text = "ChildForm";
            this.lblDocumentText.Click += new System.EventHandler(this.WhenButtonClick);
            this.lblDocumentText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WhenDragStarts);
            this.lblDocumentText.MouseEnter += new System.EventHandler(this.WhenButtonMouseEnter);
            this.lblDocumentText.MouseLeave += new System.EventHandler(this.WhenButtonMouseLeave);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 18);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(77, 3);
            this.pnlBottom.TabIndex = 1;
            // 
            // DocumentHeaderButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlBottom);
            this.Name = "DocumentHeaderButton";
            this.Size = new System.Drawing.Size(77, 21);
            this.pnlFill.ResumeLayout(false);
            this.pnlFill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblDocumentText;
        private FlatButton btnClose;
    }
}
