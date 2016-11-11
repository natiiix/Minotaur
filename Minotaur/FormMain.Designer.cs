﻿namespace Minotaur
{
    partial class FormMain
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
            this.openFileDialogLabyrinth = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.pictureBoxLabyrinth = new Minotaur.PictureBoxWithInterpolationMode();
            this.checkBoxDisplayProgress = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabyrinth)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialogLabyrinth
            // 
            this.openFileDialogLabyrinth.DefaultExt = "bmp";
            this.openFileDialogLabyrinth.FileName = "labyrinth.bmp";
            this.openFileDialogLabyrinth.Filter = "Bitmap Files|*.bmp";
            this.openFileDialogLabyrinth.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogLabyrinth_FileOk);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonLoad.Location = new System.Drawing.Point(12, 12);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(240, 23);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Enabled = false;
            this.buttonSolve.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSolve.Location = new System.Drawing.Point(258, 12);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(240, 23);
            this.buttonSolve.TabIndex = 1;
            this.buttonSolve.Text = "Solve";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // pictureBoxLabyrinth
            // 
            this.pictureBoxLabyrinth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxLabyrinth.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBoxLabyrinth.Location = new System.Drawing.Point(12, 41);
            this.pictureBoxLabyrinth.Name = "pictureBoxLabyrinth";
            this.pictureBoxLabyrinth.Size = new System.Drawing.Size(600, 388);
            this.pictureBoxLabyrinth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLabyrinth.TabIndex = 2;
            this.pictureBoxLabyrinth.TabStop = false;
            // 
            // checkBoxDisplayProgress
            // 
            this.checkBoxDisplayProgress.AutoSize = true;
            this.checkBoxDisplayProgress.Checked = true;
            this.checkBoxDisplayProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisplayProgress.Location = new System.Drawing.Point(504, 15);
            this.checkBoxDisplayProgress.Name = "checkBoxDisplayProgress";
            this.checkBoxDisplayProgress.Size = new System.Drawing.Size(104, 17);
            this.checkBoxDisplayProgress.TabIndex = 3;
            this.checkBoxDisplayProgress.Text = "Display Progress";
            this.checkBoxDisplayProgress.UseVisualStyleBackColor = true;
            this.checkBoxDisplayProgress.CheckedChanged += new System.EventHandler(this.checkBoxDisplayProgress_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.checkBoxDisplayProgress);
            this.Controls.Add(this.pictureBoxLabyrinth);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.buttonLoad);
            this.Name = "FormMain";
            this.Text = "Minotaur";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabyrinth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogLabyrinth;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSolve;
        private PictureBoxWithInterpolationMode pictureBoxLabyrinth;
        private System.Windows.Forms.CheckBox checkBoxDisplayProgress;
    }
}

