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
    public partial class ChildForm2 : DocumentView
    {
        public ChildForm2()
        {
            InitializeComponent();
        }

        public ChildForm2(int index) : this()
        {
            Text = string.Format("Instance {0}", index);
            label1.Text = string.Format("This is form 2, instance {0}", index);
        }
    }
}
