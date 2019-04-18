using System;
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
            this.progressBar.Location = new System.Drawing.Point(27, 147);
            this.progressBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(639, 39);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(711, 147);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(130, 39);
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
            this.labelProgressPercent.Location = new System.Drawing.Point(435, 17);
            this.labelProgressPercent.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelProgressPercent.Name = "labelProgressPercent";
            this.labelProgressPercent.Size = new System.Drawing.Size(15, 24);
            this.labelProgressPercent.TabIndex = 3;
            this.labelProgressPercent.Text = " ";
            this.labelProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelProgressPercent.Visible = false;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress.Location = new System.Drawing.Point(22, 30);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelProgress.MaximumSize = new System.Drawing.Size(733, 300);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(620, 75);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "This process might take a while depending on the number of elements.\r\nThis will c" +
    "ause Revit to freeze while the process is being executed.\r\nTo increase performan" +
    "ce, hide unnecessary elements.";
            // 
            // ProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(873, 236);
            this.Controls.Add(this.labelProgressPercent);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.progressBar);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximumSize = new System.Drawing.Size(897, 300);
            this.MinimumSize = new System.Drawing.Size(897, 300);
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