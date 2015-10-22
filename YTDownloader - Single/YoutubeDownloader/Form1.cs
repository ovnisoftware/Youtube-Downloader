using System;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO;  //Directory.Exists

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtPath.Text = folder;
            folderBrowserDialog1.SelectedPath = folder;
            comboBox1.SelectedIndex = 0;
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
                RestrictAccessibility();
                Download();
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
            else if (!(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink1.Text, out normalUrl)))
            {
                MessageBox.Show("Please enter a valid Youtube link");
                return Tuple.Create(false, "");
            }
            else
                return Tuple.Create(true, normalUrl);
        }

        private void Download()
        {
            //Download video
            if ((int)comboBox1.SelectedIndex == 0) //Video is selected from dropdown list
            {
                //Create new videoDownloader object
                YoutubeVideoModel videoDownloader = new YoutubeVideoModel();

                //Set videoDownloader properties
                videoDownloader.Link = txtLink1.Text;
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
                audioDownloader.Link = txtLink1.Text;
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

                //Link progress bar up to download progress
                audioDownloader.AudioDownloaderType.DownloadProgressChanged += (sender, args) => progressBar1.Value = (int)args.ProgressPercentage;

                //Download audio
                FileDownloader.DownloadAudio(audioDownloader);
            }
            catch
            {
                MessageBox.Show("Download cancelled");
                EnableAccessibility();
            }
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
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility();

                //Link progress bar up to download progress
                videoDownloader.VideoDownloaderType.DownloadProgressChanged += (sender, args) => progressBar1.Value = (int)args.ProgressPercentage;

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
            comboBox1.Enabled = false;
            btnFolder.Enabled = false;
            txtPath.Enabled = false;
            txtLink1.Enabled = false;
        }

        //Enables buttons and textboxes after a download is complete
        private void EnableAccessibility()
        {
            lblUpdate.Text = "";
            txtLink1.Text = "";
            btnDownload.Enabled = true;
            comboBox1.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink1.Enabled = true;
            progressBar1.Value = 0;
        }
    }
}