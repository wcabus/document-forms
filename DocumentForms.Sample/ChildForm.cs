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
    public partial class ChildForm : DocumentView
    {
        public ChildForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSpawn_Click(object sender, EventArgs e)
        {
            ChildForm2 cf2 = new ChildForm2();
            DocumentViewHelper.RegisterView(MainForm.Instance.documentPanel1, cf2);
            
            cf2.Show(false, this);
        }

        private void WhenClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No ==
                MessageBox.Show("Do you want to close this window?", "Close Window", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation))
            {
                e.Cancel = true;    
            }
        }
    }
}
