namespace DocumentForms.Sample
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DocumentForms.DocumentPanelDefaultRenderer documentPanelDefaultRenderer1 = new DocumentForms.DocumentPanelDefaultRenderer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.documentPanel1 = new DocumentForms.DocumentPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.child1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.child2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // documentPanel1
            // 
            this.documentPanel1.CloseEnabled = true;
            this.documentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentPanel1.Location = new System.Drawing.Point(0, 25);
            this.documentPanel1.Name = "documentPanel1";
            this.documentPanel1.Renderer = documentPanelDefaultRenderer1;
            this.documentPanel1.Size = new System.Drawing.Size(557, 324);
            this.documentPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(557, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.child1ToolStripMenuItem,
            this.child2ToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton1.Text = "Add new child";
            // 
            // child1ToolStripMenuItem
            // 
            this.child1ToolStripMenuItem.Name = "child1ToolStripMenuItem";
            this.child1ToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.child1ToolStripMenuItem.Text = "Child 1";
            this.child1ToolStripMenuItem.Click += new System.EventHandler(this.child1ToolStripMenuItem_Click);
            // 
            // child2ToolStripMenuItem
            // 
            this.child2ToolStripMenuItem.Name = "child2ToolStripMenuItem";
            this.child2ToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.child2ToolStripMenuItem.Text = "Child 2";
            this.child2ToolStripMenuItem.Click += new System.EventHandler(this.child2ToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(122, 22);
            this.toolStripButton2.Text = "Undock current child";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 349);
            this.Controls.Add(this.documentPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DocumentPanel documentPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem child1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem child2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

