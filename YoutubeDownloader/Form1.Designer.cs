namespace YoutubeDownloader
{
    partial class Form1
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
            this.txtLink1 = new System.Windows.Forms.TextBox();
            this.lblURL1 = new System.Windows.Forms.Label();
            this.lblDownload = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonfolder = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtLink2 = new System.Windows.Forms.TextBox();
            this.lblURL2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttoncancel1 = new System.Windows.Forms.Button();
            this.buttondl1 = new System.Windows.Forms.Button();
            this.buttondl2 = new System.Windows.Forms.Button();
            this.buttoncancel2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxDownloadLink = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLink1
            // 
            this.txtLink1.Location = new System.Drawing.Point(104, 40);
            this.txtLink1.Name = "txtLink1";
            this.txtLink1.Size = new System.Drawing.Size(333, 20);
            this.txtLink1.TabIndex = 1;
            // 
            // lblURL1
            // 
            this.lblURL1.AutoSize = true;
            this.lblURL1.Location = new System.Drawing.Point(6, 40);
            this.lblURL1.Name = "lblURL1";
            this.lblURL1.Size = new System.Drawing.Size(71, 26);
            this.lblURL1.TabIndex = 3;
            this.lblURL1.Text = "Download\r\none at a time:";
            // 
            // lblDownload
            // 
            this.lblDownload.AutoSize = true;
            this.lblDownload.Location = new System.Drawing.Point(6, 19);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(83, 13);
            this.lblDownload.TabIndex = 4;
            this.lblDownload.Text = "Download Path:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(104, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(333, 20);
            this.txtPath.TabIndex = 0;
            // 
            // buttonfolder
            // 
            this.buttonfolder.Location = new System.Drawing.Point(442, 10);
            this.buttonfolder.Name = "buttonfolder";
            this.buttonfolder.Size = new System.Drawing.Size(75, 23);
            this.buttonfolder.TabIndex = 7;
            this.buttonfolder.Text = "Open";
            this.buttonfolder.UseVisualStyleBackColor = true;
            this.buttonfolder.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(523, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(197, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Video",
            "MP3"});
            this.comboBox1.Location = new System.Drawing.Point(442, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtLink2
            // 
            this.txtLink2.Location = new System.Drawing.Point(107, 246);
            this.txtLink2.Name = "txtLink2";
            this.txtLink2.ReadOnly = true;
            this.txtLink2.Size = new System.Drawing.Size(333, 20);
            this.txtLink2.TabIndex = 2;
            // 
            // lblURL2
            // 
            this.lblURL2.AutoSize = true;
            this.lblURL2.Location = new System.Drawing.Point(9, 251);
            this.lblURL2.Name = "lblURL2";
            this.lblURL2.Size = new System.Drawing.Size(77, 13);
            this.lblURL2.TabIndex = 15;
            this.lblURL2.Text = "List Download:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Video",
            "MP3"});
            this.comboBox2.Location = new System.Drawing.Point(445, 246);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(75, 21);
            this.comboBox2.TabIndex = 16;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(526, 246);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(197, 23);
            this.progressBar2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "label 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(105, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "label2";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttoncancel1
            // 
            this.buttoncancel1.Enabled = false;
            this.buttoncancel1.Location = new System.Drawing.Point(828, 41);
            this.buttoncancel1.Name = "buttoncancel1";
            this.buttoncancel1.Size = new System.Drawing.Size(77, 23);
            this.buttoncancel1.TabIndex = 36;
            this.buttoncancel1.Text = "Cancel";
            this.buttoncancel1.UseVisualStyleBackColor = true;
            this.buttoncancel1.Click += new System.EventHandler(this.buttoncancel1_Click);
            // 
            // buttondl1
            // 
            this.buttondl1.Location = new System.Drawing.Point(726, 41);
            this.buttondl1.Name = "buttondl1";
            this.buttondl1.Size = new System.Drawing.Size(99, 23);
            this.buttondl1.TabIndex = 47;
            this.buttondl1.Text = "Download";
            this.buttondl1.UseVisualStyleBackColor = true;
            this.buttondl1.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttondl2
            // 
            this.buttondl2.Location = new System.Drawing.Point(729, 246);
            this.buttondl2.Name = "buttondl2";
            this.buttondl2.Size = new System.Drawing.Size(99, 23);
            this.buttondl2.TabIndex = 48;
            this.buttondl2.Text = "Download";
            this.buttondl2.UseVisualStyleBackColor = true;
            this.buttondl2.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttoncancel2
            // 
            this.buttoncancel2.Enabled = false;
            this.buttoncancel2.Location = new System.Drawing.Point(831, 246);
            this.buttoncancel2.Name = "buttoncancel2";
            this.buttoncancel2.Size = new System.Drawing.Size(77, 23);
            this.buttoncancel2.TabIndex = 49;
            this.buttoncancel2.Text = "Cancel";
            this.buttoncancel2.UseVisualStyleBackColor = true;
            this.buttoncancel2.Click += new System.EventHandler(this.buttoncancel2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 140);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(425, 95);
            this.listBox1.TabIndex = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(448, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 51;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 52;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // textBoxDownloadLink
            // 
            this.textBoxDownloadLink.Location = new System.Drawing.Point(151, 95);
            this.textBoxDownloadLink.Name = "textBoxDownloadLink";
            this.textBoxDownloadLink.Size = new System.Drawing.Size(291, 20);
            this.textBoxDownloadLink.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 26);
            this.label3.TabIndex = 54;
            this.label3.Text = "Paste video link here\r\nand click \'Add\' to add to list:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(448, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 23);
            this.button3.TabIndex = 55;
            this.button3.Text = "Download List";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 346);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDownloadLink);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttoncancel2);
            this.Controls.Add(this.buttondl2);
            this.Controls.Add(this.buttondl1);
            this.Controls.Add(this.buttoncancel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.lblURL2);
            this.Controls.Add(this.txtLink2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonfolder);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.lblURL1);
            this.Controls.Add(this.txtLink1);
            this.Name = "Form1";
            this.Text = "Youtube Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLink1;
        private System.Windows.Forms.Label lblURL1;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonfolder;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtLink2;
        private System.Windows.Forms.Label lblURL2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttoncancel1;
        private System.Windows.Forms.Button buttondl1;
        private System.Windows.Forms.Button buttondl2;
        private System.Windows.Forms.Button buttoncancel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxDownloadLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}

