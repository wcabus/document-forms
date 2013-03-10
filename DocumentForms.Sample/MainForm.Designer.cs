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
            this.documentPanel1 = new DocumentForms.DocumentPanel();
            this.SuspendLayout();
            // 
            // documentPanel1
            // 
            this.documentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentPanel1.Location = new System.Drawing.Point(0, 0);
            this.documentPanel1.Name = "documentPanel1";
            this.documentPanel1.Renderer = documentPanelDefaultRenderer1;
            this.documentPanel1.Size = new System.Drawing.Size(557, 349);
            this.documentPanel1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 349);
            this.Controls.Add(this.documentPanel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private DocumentPanel documentPanel1;
    }
}

