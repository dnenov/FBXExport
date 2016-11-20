using System.Drawing;
namespace FBXExporter
{
    partial class ProgressBarForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonExport = new System.Windows.Forms.Button();
            this.labelProgressPercent = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 31);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(460, 21);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(401, 31);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(71, 21);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // labelProgressPercent
            // 
            this.labelProgressPercent.AutoSize = true;
            this.labelProgressPercent.BackColor = System.Drawing.Color.Transparent;
            this.labelProgressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressPercent.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelProgressPercent.Location = new System.Drawing.Point(237, 9);
            this.labelProgressPercent.Name = "labelProgressPercent";
            this.labelProgressPercent.Size = new System.Drawing.Size(10, 13);
            this.labelProgressPercent.TabIndex = 3;
            this.labelProgressPercent.Text = " ";
            this.labelProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelProgressPercent.Visible = false;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 9);
            this.labelProgress.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(382, 26);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "This process might take a while depending on the number of elements. This will ca" +
    "use Revit to freeze while the process is being executed.";
            // 
            // ProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 61);
            this.Controls.Add(this.labelProgressPercent);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.progressBar);
            this.MaximumSize = new System.Drawing.Size(500, 100);
            this.MinimumSize = new System.Drawing.Size(500, 100);
            this.Name = "ProgressBarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FBX Export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Label labelProgressPercent;
        private System.Windows.Forms.Label labelProgress;
    }
}