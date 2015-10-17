using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeDownloader;
using YoutubeExtractor;
using System.IO;
using System.Collections;
using System.Threading;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        DownloadObject downloadObject1 = new DownloadObject("Youtube URL #1");
        DownloadObject downloadObject2 = new DownloadObject("Youtube URL #2");
        List<string> downloadList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtPath.Text = folder;
            folderBrowserDialog1.SelectedPath = folder;
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;
            label1.Text = "";
            label2.Text = "";
            backgroundWorker1.DoWork += (s, args) =>
            {
              VideoGetter.GetVideo(ref downloadObject1, txtLink1.Text, ref backgroundWorker1, txtPath.Text);
            };
            backgroundWorker2.DoWork += (s, args) =>
            {
                VideoGetter.GetVideo(ref downloadObject2, txtLink2.Text, ref backgroundWorker2, txtPath.Text);
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        void videoDownloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            backgroundWorker1.ReportProgress((int)e.ProgressPercentage);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                downloadObject1.videoInsteadOfMP3 = true;
            else
                downloadObject1.videoInsteadOfMP3 = false;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
                downloadObject2.videoInsteadOfMP3 = true;
            else
                downloadObject2.videoInsteadOfMP3 = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (downloadObject1.isAudioDownloaderNull == false)
            {
                downloadObject1.audioDownloader.DownloadProgressChanged += (sender2, args) => backgroundWorker1.ReportProgress((int)args.ProgressPercentage);
                downloadObject1.audioDownloader.DownloadFinished += (sender2, args) => backgroundWorker1.CancelAsync();
                downloadObject1.isAudioDownloaderNull = true;
            }
            if (downloadObject1.isVideoDownloaderNull == false)
            {
                downloadObject1.videoDownloader.DownloadProgressChanged += (sender2, args) => backgroundWorker1.ReportProgress((int)args.ProgressPercentage);
                downloadObject1.videoDownloader.DownloadFinished += (sender2, args) => backgroundWorker1.CancelAsync();
                downloadObject1.isVideoDownloaderNull = true;
            }
            if (downloadObject2.isAudioDownloaderNull == false)
            {
                downloadObject2.audioDownloader.DownloadProgressChanged += (sender2, args) => backgroundWorker2.ReportProgress((int)args.ProgressPercentage);
                downloadObject2.audioDownloader.DownloadFinished += (sender2, args) => backgroundWorker2.CancelAsync();
                downloadObject2.isAudioDownloaderNull = true;
            }
            if (downloadObject2.isVideoDownloaderNull == false)
            {
                downloadObject2.videoDownloader.DownloadProgressChanged += (sender2, args) => backgroundWorker2.ReportProgress((int)args.ProgressPercentage);
                downloadObject2.videoDownloader.DownloadFinished += (sender2, args) => backgroundWorker2.CancelAsync();
                downloadObject2.isVideoDownloaderNull = true;
            }
            if ((backgroundWorker1.IsBusy) && (progressBar1.Value > 0))
            {
                label1.Text = downloadObject1.vidName + " downloading...";
                buttondl1.Enabled = false;
                comboBox1.Enabled = false;
                buttoncancel1.Enabled = true;
                buttondl1.Text = "Downloading...";
            }
            if ((backgroundWorker2.IsBusy) && (progressBar2.Value > 0))
            {
                label2.Text = downloadObject2.vidName + " downloading...";
                buttondl2.Enabled = false;
                comboBox2.Enabled = false;
                buttoncancel2.Enabled = true;
                buttondl2.Text = "Downloading...";
            }
          
            if (downloadObject1.duplicationError == true || downloadObject1.resetBoxes == true)
            {
                downloadObject1.vidName = "";
                label1.Text = "";
                txtLink1.Text = "";
                comboBox1.Enabled = true;
                buttondl1.Enabled = true;
                buttondl1.Text = "Download";
                downloadObject1.duplicationError = false;
                downloadObject1.resetBoxes = false;
            }
            if (downloadObject2.duplicationError == true || downloadObject2.resetBoxes == true)
            {
                downloadObject2.vidName = "";
                label2.Text = "";
                txtLink2.Text = "";
                comboBox2.Enabled = true;
                buttondl2.Enabled = true;
                buttondl2.Text = "Download";
                downloadObject2.duplicationError = false;
                downloadObject2.resetBoxes = false;
            }
            
            if (progressBar1.Value == 100)
            {
                txtLink1.Text = "";
                label1.Text = "";
                progressBar1.Value = 0;
                buttondl1.Enabled = true;
                buttondl1.Text = "Download";
                comboBox1.Enabled = true;
            }
            if (progressBar2.Value == 100)
            {
                txtLink2.Text = "";
                label2.Text = "";
                progressBar2.Value = 0;
                buttondl2.Enabled = true;
                buttondl2.Text = "Download";
                comboBox2.Enabled = true;
                button3.PerformClick();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string normalUrl;
            if (Directory.Exists(txtPath.Text))
            {
                if ((txtLink1.Text != "") && !backgroundWorker1.IsBusy)
                    if (YoutubeExtractor.DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink1.Text, out normalUrl))
                    {
                        {
                            label1.Text = "Converting " + normalUrl + ", please wait, some videos take longer than others...";
                            buttondl1.Enabled = false;
                            comboBox1.Enabled = false;
                            buttoncancel1.Enabled = false;
                            buttondl1.Text = "Downloading...";
                            backgroundWorker1.RunWorkerAsync();
                        }
                    }
                    else
                        MessageBox.Show("Please enter a valid Youtube link", downloadObject1.lineNumber);
            }
            else
                MessageBox.Show("Please enter a valid folder", downloadObject1.lineNumber);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string normalUrl;
            if (Directory.Exists(txtPath.Text))
            {
                if ((txtLink2.Text != "") && !backgroundWorker2.IsBusy)
                    if (YoutubeExtractor.DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink2.Text, out normalUrl))
                    {
                        {
                            label2.Text = "Converting " + normalUrl + ", please wait, some videos take longer than others...";
                            buttondl2.Enabled = false;
                            comboBox2.Enabled = false;
                            buttoncancel2.Enabled = false;
                            buttondl2.Text = "Downloading...";
                            backgroundWorker2.RunWorkerAsync();
                        }
                    }
                    else
                        MessageBox.Show("Please enter a valid Youtube link", downloadObject2.lineNumber);
            }
            else
                MessageBox.Show("Please enter a valid folder", downloadObject2.lineNumber);
        }

        private void buttoncancel1_Click(object sender, EventArgs e)
        {
            downloadObject1.downloadThread.Abort();
            progressBar1.Value = 100;
            backgroundWorker1.CancelAsync();
        }

        private void buttoncancel2_Click(object sender, EventArgs e)
        {
            downloadObject2.downloadThread.Abort();
            progressBar2.Value = 100;
            backgroundWorker2.CancelAsync();
        }

        //Adds URL to list of downloads
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxDownloadLink.Text != "")
            {
                downloadList.Add(textBoxDownloadLink.Text);
                listBox1.DataSource = null;
                listBox1.DataSource = downloadList;
                textBoxDownloadLink.Text = "";
            }
        }

        //Button to download all URLS in list sequentially
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (downloadList.Count != 0)
            {
                txtLink2.Text = downloadList[0];
                downloadList.RemoveAt(0);
                listBox1.DataSource = null;
                listBox1.DataSource = downloadList;
                buttondl2.PerformClick();
            }
        }

        //Removes items from list
        private void button2_Click(object sender, EventArgs e)
        {
            downloadList.RemoveAt(listBox1.SelectedIndex);
            listBox1.DataSource = null;
            listBox1.DataSource = downloadList;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                downloadList.RemoveAt(listBox1.SelectedIndex);
                listBox1.DataSource = null;
                listBox1.DataSource = downloadList;
            }
        }
    }
}