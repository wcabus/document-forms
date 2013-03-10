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
    public partial class ChildForm2 : Form, IDocumentView
    {
        public ChildForm2()
        {
            InitializeComponent();
            WindowBorderStyle = FormBorderStyle;
        }

        public bool IsDocked { get; set; }
        public DocumentPanel ParentDocumentPanel { get; set; }
        public FormBorderStyle WindowBorderStyle { get; private set; }
    }
}
