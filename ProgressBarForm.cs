using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBXExporter
{
    public partial class ProgressBarForm : Form
    {
        private Autodesk.Revit.DB.Document doc;
        private string message;

        public ProgressBarForm(Autodesk.Revit.DB.Document doc)
        {
            this.doc = doc;

            InitializeComponent();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            this.message = ExportFBX.Export(this.doc, progressBar, labelProgress);

            if(message.Contains("Successfully"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
