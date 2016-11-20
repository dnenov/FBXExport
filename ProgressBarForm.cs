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
            buttonExport.Visible = false;
            progressBar.Visible = true;
            labelProgressPercent.Visible = true;
            this.labelProgress.Text = " ..";
            this.message = ExportFBX.Export(this.doc, progressBar, labelProgress, labelProgressPercent);

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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
