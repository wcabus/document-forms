using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentForms
{
    internal interface IDocumentHeaderButton : IDisposable
    {
        bool IsActive { get; set; }
        DocumentPanel ParentDocumentPanel { get; set; }
        bool IsMouseHover { get; }
        ToolStripMenuItem ToolStripMenuItem { get; set; }

        int Width { get; }
        Point Location { get; set; }

        void SetViewNull();
        Form OwnedForm { get; }
        IDocumentView OwnedView { get; }
    }
}