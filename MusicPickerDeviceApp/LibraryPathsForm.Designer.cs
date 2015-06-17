namespace MusicPickerDeviceApp
{
    partial class LibraryPathsForm
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
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.folder = new System.Windows.Forms.LinkLabel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.foldersLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // 
            // folder
            // 
            this.folder.AutoSize = true;
            this.folder.Location = new System.Drawing.Point(21, 21);
            this.folder.Name = "folder";
            this.folder.Size = new System.Drawing.Size(99, 17);
            this.folder.TabIndex = 0;
            this.folder.TabStop = true;
            this.folder.Text = "Select a folder";
            this.folder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonLoad.Location = new System.Drawing.Point(374, 15);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click_1);
            // 
            // foldersLabel
            // 
            this.foldersLabel.AutoSize = true;
            this.foldersLabel.Location = new System.Drawing.Point(3, 0);
            this.foldersLabel.Name = "foldersLabel";
            this.foldersLabel.Size = new System.Drawing.Size(0, 17);
            this.foldersLabel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.foldersLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(24, 52);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 100);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(461, 163);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.folder);
            this.Name = "LoadForm";
            this.Text = "Music Picker - Load";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.LinkLabel folder;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label foldersLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}