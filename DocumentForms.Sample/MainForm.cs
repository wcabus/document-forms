using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentForms.Sample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ChildForm cf = new ChildForm();
            DocumentViewHelper.Dock(documentPanel1, cf);

            ChildForm2 cf2 = new ChildForm2();
            DocumentViewHelper.Dock(documentPanel1, cf2);
        }

        private void child1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = new ChildForm();
            DocumentViewHelper.Dock(documentPanel1, cf);
        }

        private void child2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm2 cf2 = new ChildForm2();
            DocumentViewHelper.Dock(documentPanel1, cf2);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (documentPanel1.ActiveView != null)
                DocumentViewHelper.Undock(documentPanel1.ActiveView);
        }
    }
}
