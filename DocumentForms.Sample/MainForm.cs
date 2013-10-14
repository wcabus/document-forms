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
        private static MainForm _instance;
        private ChildForm _firstForm;

        public MainForm()
        {
            InitializeComponent();
            _instance = this;

            _firstForm = new ChildForm();
            DocumentViewHelper.RegisterView(documentPanel1, _firstForm);
            DocumentViewHelper.Dock(_firstForm);

            int i = 0;
            while (i++ < 50)
            {
                ChildForm2 cf2 = new ChildForm2(i);
                DocumentViewHelper.RegisterView(documentPanel1, cf2);
                DocumentViewHelper.Dock(cf2);
            }
        }

        public static MainForm Instance { get { return _instance; } }

        private void child1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = new ChildForm();
            DocumentViewHelper.RegisterView(documentPanel1, cf);
            DocumentViewHelper.Dock(cf);
        }

        private void child2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm2 cf2 = new ChildForm2();
            DocumentViewHelper.RegisterView(documentPanel1, cf2);
            DocumentViewHelper.Dock(cf2);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (documentPanel1.ActiveView != null)
                DocumentViewHelper.Undock(documentPanel1.ActiveView);
        }

        private void btnActivateFirst_Click(object sender, EventArgs e)
        {
            try
            {
                documentPanel1.ActiveView = _firstForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
