using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0;
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderBrowserDialog1.SelectedPath = folder;
            txtPath.Text = folder;
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
                //Pass the validated link into the download method so it can be assigned to a property in the YoutubeVideoModel or the YoutubeAudioModel
                Download(isGoodLink.Item2);
            }
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
                //DownloadAudio(audioDownloader);
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

                //Stores FilePath in model
                videoDownloader.FilePath = FileDownloader.GetPath(videoDownloader);
                videoDownloader.FilePath += videoDownloader.Video.VideoExtension;

                //Stores VideoDownloaderType object in model
                videoDownloader.VideoDownloaderType = FileDownloader.GetVideoDownloader(videoDownloader);

                //Enable buttons once download is complete
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility();

                //Link progress bar up to download progress
                videoDownloader.VideoDownloaderType.DownloadProgressChanged += (sender, args) => pgDownload.Value = (int)args.ProgressPercentage;

                //Download video
                FileDownloader.DownloadVideo(videoDownloader);
            }
            catch (Exception)
            {
                MessageBox.Show("Download cancelled");
                EnableAccessibility();
            }
        }

        ////Tuple returns true if the link is good, the string returned is the valid link to the video
        private Tuple<bool, string> ValidateLink()
        {
            string normalUrl;
            if (!Directory.Exists(txtPath.Text))
            {
                MessageBox.Show("Please enter a valid folder");
                return Tuple.Create(false, "");
            }

            else if (!(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text, out normalUrl)))
            {
                MessageBox.Show("Please enter a valid Youtube link");
                return Tuple.Create(false, "");
            }
            else
                return Tuple.Create(true, normalUrl);
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
            lblUpdate.Text = "";
            txtLink.Text = "";
            btnDownload.Enabled = true;
            cboFileType.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink.Enabled = true;
            pgDownload.Value = 0;
        }
    }
}