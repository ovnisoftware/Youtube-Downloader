namespace YoutubeDownloader
{
    partial class frmYTDownloader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYTDownloader));
            this.txtLink = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.lblDownload = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFolder = new System.Windows.Forms.Button();
            this.pgDownload = new System.Windows.Forms.ProgressBar();
            this.cboFileType = new System.Windows.Forms.ComboBox();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLink
            // 
            this.txtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLink.Location = new System.Drawing.Point(136, 136);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(395, 22);
            this.txtLink.TabIndex = 1;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblURL.Location = new System.Drawing.Point(12, 139);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(91, 16);
            this.lblURL.TabIndex = 3;
            this.lblURL.Text = "Youtube URL:";
            // 
            // lblDownload
            // 
            this.lblDownload.AutoSize = true;
            this.lblDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownload.Location = new System.Drawing.Point(12, 115);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(102, 16);
            this.lblDownload.TabIndex = 4;
            this.lblDownload.Text = "Download Path:";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(136, 108);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(395, 22);
            this.txtPath.TabIndex = 0;
            // 
            // btnFolder
            // 
            this.btnFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolder.Location = new System.Drawing.Point(537, 107);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 7;
            this.btnFolder.Text = "Open";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // pgDownload
            // 
            this.pgDownload.Location = new System.Drawing.Point(136, 189);
            this.pgDownload.Name = "pgDownload";
            this.pgDownload.Size = new System.Drawing.Size(395, 23);
            this.pgDownload.TabIndex = 10;
            // 
            // cboFileType
            // 
            this.cboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFileType.FormattingEnabled = true;
            this.cboFileType.Items.AddRange(new object[] {
            "Video",
            "MP3"});
            this.cboFileType.Location = new System.Drawing.Point(537, 135);
            this.cboFileType.Name = "cboFileType";
            this.cboFileType.Size = new System.Drawing.Size(75, 24);
            this.cboFileType.TabIndex = 13;
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.Location = new System.Drawing.Point(133, 164);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(11, 16);
            this.lblUpdate.TabIndex = 19;
            this.lblUpdate.Text = ".";
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(537, 189);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(99, 23);
            this.btnDownload.TabIndex = 47;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(204, 1);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(225, 101);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 48;
            this.picLogo.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(371, 218);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(265, 20);
            this.checkBox1.TabIndex = 49;
            this.checkBox1.Text = "Open Folder When Download Complete";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 168);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 70);
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmYTDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 246);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.cboFileType);
            this.Controls.Add(this.pgDownload);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtLink);
            this.Name = "frmYTDownloader";
            this.Text = "Youtube Downloader 1.1";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.ProgressBar pgDownload;
        private System.Windows.Forms.ComboBox cboFileType;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
    }
}

