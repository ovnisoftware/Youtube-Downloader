using System;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO;  //Directory.Exists
using System.Diagnostics;
using System.Net;

namespace YoutubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        public frmYTDownloader()
        {
            InitializeComponent();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtPath.Text = folder;
            folderBrowserDialog1.SelectedPath = folder;
            cboFileType.SelectedIndex = 0;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> isGoodLink = ValidateLink();
            if (isGoodLink.Item1 == true)
            {
                backgroundWorker1.RunWorkerAsync(isGoodLink.Item2);
                RestrictAccessibility();
                //Pass the validated link into the download method so it can be assigned to a property in the YoutubeVideoModel or the YoutubeAudioModel
                Download(isGoodLink.Item2);
            }
        }

        //Tuple returns true if the link is good, the string returned is the valid link to the video
        private Tuple<bool, string> ValidateLink()
        {
            //Contains the normalized URL
            string normalUrl;

            //Checks that a valid folder is entered to store downloaded files
            if (!Directory.Exists(txtPath.Text))
            {
                MessageBox.Show("Please enter a valid folder");
                return Tuple.Create(false, "");
            }
            //Checks that URL entered corresponds to a valid Youtube video
            else if (!(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text, out normalUrl)))
            {
                MessageBox.Show("Please enter a valid Youtube link");
                return Tuple.Create(false, "");
            }
            else
                return Tuple.Create(true, normalUrl);
        }

        private void Download(string validatedLink)
        {
            //Download video
            if ((int)cboFileType.SelectedIndex == 0) //Video is selected from dropdown list
            {
                //Create new videoDownloader object
                YoutubeVideoModel videoDownloader = new YoutubeVideoModel();

                //Set videoDownloader properties
                videoDownloader.Link = validatedLink;
                videoDownloader.FolderPath = txtPath.Text;

                //Download video
                DownloadVideo(videoDownloader);
            }
            //Download audio
            else
            {
                //Create new audioDownloader object
                YoutubeAudioModel audioDownloader = new YoutubeAudioModel();

                //Set AudioDownloader properties
                audioDownloader.Link = validatedLink;
                audioDownloader.FolderPath = txtPath.Text;

                //Download audio
                DownloadAudio(audioDownloader);
            }
        }

        private void DownloadAudio(YoutubeAudioModel audioDownloader)
        {
            try
            {
                //Store VideoInfo object in model
                audioDownloader.VideoInfo = FileDownloader.GetVideoInfos(audioDownloader);

                //Stores VideoInfo object in model
                audioDownloader.Video = FileDownloader.GetVideoInfoAudioOnly(audioDownloader);

                //Updates lblUpdate to show title of video that is downloading
                UpdateLabel(audioDownloader.Video.Title + audioDownloader.Video.AudioExtension);

                //Stores FilePath in model
                audioDownloader.FilePath = FileDownloader.GetPath(audioDownloader);
                audioDownloader.FilePath += audioDownloader.Video.AudioExtension;

                //Stores AudioDownloaderType object in model
                audioDownloader.AudioDownloaderType = FileDownloader.GetAudioDownloader(audioDownloader);

                //Enable buttons once download is complete
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility();
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => OpenFolder(audioDownloader.FilePath);
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => timer1.Stop();

                //Link progress bar up to download progress
                audioDownloader.AudioDownloaderType.DownloadProgressChanged += (sender, args) => pgDownload.Value = (int)args.ProgressPercentage;
                CheckForIllegalCrossThreadCalls = false;

                //Download audio
                FileDownloader.DownloadAudio(audioDownloader);
            }
            catch
            {
                MessageBox.Show("Download cancelled");
                EnableAccessibility();
            }
        }

        private void OpenFolder(string filePath)
        {
            string argument = "/select, \"" + filePath + "\"";
            if (checkBox1.Checked == true)
                Process.Start("explorer.exe", argument);
        }

        private void DownloadVideo(YoutubeVideoModel videoDownloader)
        {
            try
            {
                //Store VideoInfo object in model
                videoDownloader.VideoInfo = FileDownloader.GetVideoInfos(videoDownloader);

                //Stores VideoInfo object in model
                videoDownloader.Video = FileDownloader.GetVideoInfo(videoDownloader);

                //Updates lblUpdate to show title of video that is downloading
                UpdateLabel(videoDownloader.Video.Title + videoDownloader.Video.VideoExtension);

                //Stores FilePath in model
                videoDownloader.FilePath = FileDownloader.GetPath(videoDownloader);
                videoDownloader.FilePath += videoDownloader.Video.VideoExtension;

                //Stores VideoDownloaderType object in model
                videoDownloader.VideoDownloaderType = FileDownloader.GetVideoDownloader(videoDownloader);

                //Enable buttons once download is complete
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => timer1.Stop();
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility();
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => OpenFolder(videoDownloader.FilePath);


                //Link progress bar up to download progress
                videoDownloader.VideoDownloaderType.DownloadProgressChanged += (sender, args) => pgDownload.Value = (int)args.ProgressPercentage;
                CheckForIllegalCrossThreadCalls = false;

                //Download video
                FileDownloader.DownloadVideo(videoDownloader);
            }
            catch (Exception)
            {
                MessageBox.Show("Download cancelled");
                EnableAccessibility();
            }
        }

        private void UpdateLabel(string title)
        {
            lblUpdate.Text = "Downloading " + title + "...";
        }

        //Disables buttons and textboxes so they can't be clicked during a download
        private void RestrictAccessibility()
        {
            btnDownload.Enabled = false;
            cboFileType.Enabled = false;
            btnFolder.Enabled = false;
            txtPath.Enabled = false;
            txtLink.Enabled = false;
        }

        //Enables buttons and textboxes after a download is complete
        private void EnableAccessibility()
        {
            pictureBox1.Image = null;
            lblUpdate.Text = "";
            txtLink.Text = "";
            btnDownload.Enabled = true;
            cboFileType.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink.Enabled = true;
            pgDownload.Value = 0;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            string link = e.Argument as string;
            string youtubeCode = link.Substring(link.Length - 11, 11);
            string imageLink1 = "http://img.youtube.com/vi/" + youtubeCode + "/1.jpg";
            string imageLink2 = "http://img.youtube.com/vi/" + youtubeCode + "/2.jpg";
            string imageLink3 = "http://img.youtube.com/vi/" + youtubeCode + "/3.jpg";
            using (var client = new WebClient())
            {
                client.DownloadFile(imageLink1, "1.jpg");
                client.DownloadFile(imageLink2, "2.jpg");
                client.DownloadFile(imageLink3, "3.jpg");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.ImageLocation == null)
            {
                pictureBox1.ImageLocation = "1.jpg";
            }
            else if (pictureBox1.ImageLocation == "1.jpg")
            {
                pictureBox1.ImageLocation = "2.jpg";
            }
            else if (pictureBox1.ImageLocation == "2.jpg")
            {
                pictureBox1.ImageLocation = "3.jpg";
            }
            else if (pictureBox1.ImageLocation == "3.jpg")
                pictureBox1.ImageLocation = "1.jpg";
        }
    }
}