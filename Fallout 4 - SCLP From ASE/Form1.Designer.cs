namespace Fallout_4___SCLP_From_ASE
{
    partial class windowProgram
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
            this.groupFilePaths = new System.Windows.Forms.GroupBox();
            this.buttonMakeSCLP = new System.Windows.Forms.Button();
            this.textboxModPath = new System.Windows.Forms.TextBox();
            this.buttonModPath = new System.Windows.Forms.Button();
            this.textboxSourcePath = new System.Windows.Forms.TextBox();
            this.buttonSourcePath = new System.Windows.Forms.Button();
            this.dialogSourcePath = new System.Windows.Forms.OpenFileDialog();
            this.dialogModPath = new System.Windows.Forms.OpenFileDialog();
            this.dialogMakeSCLP = new System.Windows.Forms.SaveFileDialog();
            this.groupFilePaths.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFilePaths
            // 
            this.groupFilePaths.Controls.Add(this.buttonMakeSCLP);
            this.groupFilePaths.Controls.Add(this.textboxModPath);
            this.groupFilePaths.Controls.Add(this.buttonModPath);
            this.groupFilePaths.Controls.Add(this.textboxSourcePath);
            this.groupFilePaths.Controls.Add(this.buttonSourcePath);
            this.groupFilePaths.Location = new System.Drawing.Point(11, 14);
            this.groupFilePaths.Name = "groupFilePaths";
            this.groupFilePaths.Size = new System.Drawing.Size(532, 123);
            this.groupFilePaths.TabIndex = 0;
            this.groupFilePaths.TabStop = false;
            this.groupFilePaths.Text = "ASE File Paths";
            this.groupFilePaths.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // buttonMakeSCLP
            // 
            this.buttonMakeSCLP.Enabled = false;
            this.buttonMakeSCLP.Location = new System.Drawing.Point(226, 84);
            this.buttonMakeSCLP.Name = "buttonMakeSCLP";
            this.buttonMakeSCLP.Size = new System.Drawing.Size(75, 23);
            this.buttonMakeSCLP.TabIndex = 4;
            this.buttonMakeSCLP.Text = "Generate";
            this.buttonMakeSCLP.UseVisualStyleBackColor = true;
            this.buttonMakeSCLP.Click += new System.EventHandler(this.buttonMakeSCLP_Click);
            // 
            // textboxModPath
            // 
            this.textboxModPath.Location = new System.Drawing.Point(87, 50);
            this.textboxModPath.Name = "textboxModPath";
            this.textboxModPath.Size = new System.Drawing.Size(430, 20);
            this.textboxModPath.TabIndex = 3;
            // 
            // buttonModPath
            // 
            this.buttonModPath.Location = new System.Drawing.Point(6, 48);
            this.buttonModPath.Name = "buttonModPath";
            this.buttonModPath.Size = new System.Drawing.Size(75, 23);
            this.buttonModPath.TabIndex = 2;
            this.buttonModPath.Text = "Modified";
            this.buttonModPath.UseVisualStyleBackColor = true;
            this.buttonModPath.Click += new System.EventHandler(this.buttonModPath_Click);
            // 
            // textboxSourcePath
            // 
            this.textboxSourcePath.Location = new System.Drawing.Point(87, 22);
            this.textboxSourcePath.Name = "textboxSourcePath";
            this.textboxSourcePath.Size = new System.Drawing.Size(430, 20);
            this.textboxSourcePath.TabIndex = 1;
            // 
            // buttonSourcePath
            // 
            this.buttonSourcePath.Location = new System.Drawing.Point(6, 19);
            this.buttonSourcePath.Name = "buttonSourcePath";
            this.buttonSourcePath.Size = new System.Drawing.Size(75, 23);
            this.buttonSourcePath.TabIndex = 0;
            this.buttonSourcePath.Text = "Source";
            this.buttonSourcePath.UseVisualStyleBackColor = true;
            this.buttonSourcePath.Click += new System.EventHandler(this.buttonSourcePath_Click);
            // 
            // dialogSourcePath
            // 
            this.dialogSourcePath.DefaultExt = "ASE";
            this.dialogSourcePath.Filter = "3ds Max ASCII|*.ase";
            // 
            // dialogModPath
            // 
            this.dialogModPath.DefaultExt = "ASE";
            this.dialogModPath.Filter = "3ds Max ASCII|*.ase";
            // 
            // dialogMakeSCLP
            // 
            this.dialogMakeSCLP.DefaultExt = "sclp";
            this.dialogMakeSCLP.Filter = "Fallout 4 SCLP|*.sclp";
            // 
            // windowProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 140);
            this.Controls.Add(this.groupFilePaths);
            this.Name = "windowProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fallout 4 - SCLP From ASE";
            this.groupFilePaths.ResumeLayout(false);
            this.groupFilePaths.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFilePaths;
        private System.Windows.Forms.TextBox textboxModPath;
        private System.Windows.Forms.Button buttonModPath;
        private System.Windows.Forms.TextBox textboxSourcePath;
        private System.Windows.Forms.Button buttonSourcePath;
        private System.Windows.Forms.Button buttonMakeSCLP;
        private System.Windows.Forms.OpenFileDialog dialogSourcePath;
        private System.Windows.Forms.OpenFileDialog dialogModPath;
        private System.Windows.Forms.SaveFileDialog dialogMakeSCLP;
    }
}

