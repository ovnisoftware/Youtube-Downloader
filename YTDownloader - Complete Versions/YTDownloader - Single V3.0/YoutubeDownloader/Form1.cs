using System;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO;  //Directory.Exists
using System.Diagnostics;

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
            //Create new videoDownloader object
            YoutubeVideoModel videoDownloader = new YoutubeVideoModel();

            //Set videoDownloader properties
            videoDownloader.Link = validatedLink;
            videoDownloader.FolderPath = txtPath.Text;

            //Download video
            DownloadVideo(videoDownloader);
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
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility(null);
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => OpenFolder(audioDownloader.FilePath);

                //Link progress bar up to download progress
                audioDownloader.AudioDownloaderType.DownloadProgressChanged += (sender, args) => pgDownload.Value = (int)args.ProgressPercentage;
                CheckForIllegalCrossThreadCalls = false;

                //Download audio
                FileDownloader.DownloadAudio(audioDownloader);
            }
            catch
            {
                MessageBox.Show("Download cancelled");
                EnableAccessibility(null);
            }
        }

        private void OpenFolder(string filePath)
        {
            string argument = "/select, \"" + filePath + "\"";
            //string argument = @"/select, " + fileName;
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
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => EnableAccessibility(videoDownloader.FilePath);
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
                EnableAccessibility(null);
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
        private void EnableAccessibility(string name)
        {
            lblUpdate.Text = "";
            txtLink.Text = "";
            btnDownload.Enabled = true;
            cboFileType.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink.Enabled = true;
            pgDownload.Value = 0;

            //Checks if MP3 is selected from combobox
            if ((name != null) & ((int)cboFileType.SelectedIndex == 1))
            {
                DownloadMP3(name);
            }
        }
        private void DownloadMP3(string name)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string destination = name.Remove(name.Length - 4);
            destination += ".mp3";
            startInfo.Arguments = @"/C ffmpeg -i " + "\"" + @name + "\"" + " " + "\"" + @destination + "\"";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}