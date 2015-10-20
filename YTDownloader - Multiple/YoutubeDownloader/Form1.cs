using System;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO; //For Directory.Exists
using System.ComponentModel; //For background worker
using System.Collections.Generic;  //For List
using System.Linq;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        List<YoutubeModel> downloadList = new List<YoutubeModel>();

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
            var isGoodLink = ValidateLink();
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
            else if (!(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text, out normalUrl)))
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
                videoDownloader.Link = txtLink.Text;
                videoDownloader.FolderPath = txtPath.Text;

                //Download video
                DownloadVideo(videoDownloader);
            }
            else //Audio is selected from dropdown list
            {
                //Create new audioDownloader object
                YoutubeAudioModel audioDownloader = new YoutubeAudioModel();

                //Set AudioDownloader properties
                audioDownloader.Link = txtLink.Text;
                audioDownloader.FolderPath = txtPath.Text;

                //Download audio
                DownloadAudio(audioDownloader);
            }
        }

        private void DownloadAudio(YoutubeAudioModel audioDownloader)
        {
            try
            {
                //Updates lblUpdate to show title of video that is downloading
                UpdateLabel(audioDownloader.Video.Title + audioDownloader.Video.AudioExtension);

                //Stores FilePath in model
                audioDownloader.FilePath = FileDownloader.GetPath(audioDownloader);
                audioDownloader.FilePath += audioDownloader.Video.AudioExtension;

                //Stores AudioDownloaderType object in model
                audioDownloader.AudioDownloaderType = FileDownloader.GetAudioDownloader(audioDownloader);

                //Call DownloadList method until all items in list are downloaded
                audioDownloader.AudioDownloaderType.DownloadFinished += (sender, args) => DownloadList();

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
                //Updates lblUpdate to show title of video that is downloading
                UpdateLabel(videoDownloader.Video.Title + videoDownloader.Video.VideoExtension);

                //Stores FilePath in model
                videoDownloader.FilePath = FileDownloader.GetPath(videoDownloader);
                videoDownloader.FilePath += videoDownloader.Video.VideoExtension;

                //Stores VideoDownloaderType object in model
                videoDownloader.VideoDownloaderType = FileDownloader.GetVideoDownloader(videoDownloader);

                //Call DownloadList method until all items in list are downloaded
                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => DownloadList();

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

        //Removes selected row in listbox and corresponding item in downloadList
        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                downloadList.RemoveAt(i);
            }
            listBox1.DataSource = null;
            listBox1.DataSource = downloadList;
        }

        //Adds URL to list of downloads
        //Backgroundworker is necessary so the screen doesn't freeze
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, args) =>
            {
                var isGoodLink = ValidateLink();
                if (isGoodLink.Item1 == true)
                    CreateVideoOrAudioObject();
            };
            bg.RunWorkerAsync();
        }

        private void CreateVideoOrAudioObject()
        {
            //Create video object
            if ((int)comboBox1.SelectedIndex == 0) //Video is selected from dropdown list
            {
                //Create new videoDownloader object
                YoutubeVideoModel videoDownloader = new YoutubeVideoModel();

                //Set videoDownloader properties
                videoDownloader.Link = txtLink.Text;
                videoDownloader.FolderPath = txtPath.Text;

                //Store VideoInfo object in model
                videoDownloader.VideoInfo = FileDownloader.GetVideoInfos(videoDownloader);

                //Stores VideoInfo object in model
                videoDownloader.Video = FileDownloader.GetVideoInfo(videoDownloader);
                UpdateListBox(videoDownloader);
            }
            //Create audio object
            else
            {
                //Create new audioDownloader object
                YoutubeAudioModel audioDownloader = new YoutubeAudioModel();

                //Set AudioDownloader properties
                audioDownloader.Link = txtLink.Text;
                audioDownloader.FolderPath = txtPath.Text;

                //Store VideoInfo object in model
                audioDownloader.VideoInfo = FileDownloader.GetVideoInfos(audioDownloader);

                //Stores VideoInfo object in model
                audioDownloader.Video = FileDownloader.GetVideoInfoAudioOnly(audioDownloader);

                UpdateListBox(audioDownloader);
            }
        }

        private void UpdateListBox(YoutubeModel model)
        {
            //Add item to download to the beginning of the list
            //If you use Add it adds to the end of the list
            downloadList.Insert(0, model);

            //Update listbox
            listBox1.DataSource = null;
            listBox1.DataSource = downloadList;

            //Reset the textbox where the link was typed in
            txtLink.Text = "";
        }

        private void btnDownloadList_Click(object sender, EventArgs e)
        {
            DownloadList();
        }

        private void DownloadList()
        {
            if (!Directory.Exists(txtPath.Text))
                MessageBox.Show("Please enter a valid folder");

            else if (downloadList.Count == 0)
                EnableAccessibility();

            else if (downloadList.Count != 0)
            {
                RestrictAccessibility();

                //Update listbox
                listBox1.DataSource = null;
                listBox1.DataSource = downloadList;

                //Get first DownloadItem from list
                YoutubeModel model = (YoutubeModel)downloadList.First();

                //Delete first DownloadItem from list
                downloadList.RemoveAt(0);

                //Download video

                if (model is YoutubeVideoModel)
                    DownloadVideo((YoutubeVideoModel)model);

                //Download audio
                else if (model is YoutubeAudioModel)
                    DownloadAudio((YoutubeAudioModel)model);
            }
        }

        //Disables buttons and textboxes so they can't be clicked during a download
        private void RestrictAccessibility()
        {
            listBox1.Enabled = false;
            btnDownloadList.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            comboBox1.Enabled = false;
            btnFolder.Enabled = false;
            txtPath.Enabled = false;
            txtLink.Enabled = false;
        }

        //Enables buttons and textboxes after a download is complete
        private void EnableAccessibility()
        {
            //Update listbox
            listBox1.DataSource = null;
            listBox1.DataSource = downloadList;
            listBox1.Enabled = true;
            btnDownloadList.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            comboBox1.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink.Enabled = true;

            lblUpdate.Text = "";
            txtLink.Text = "";
            progressBar1.Value = 0;
        }
    }
}