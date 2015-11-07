using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO;

namespace YouTubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0;//set video as first choice
            //line below gets path to my documents
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //sets the path of the browser dialog box 
            folderBrowserDialog1.SelectedPath = folder;
        }

        private void btnDownloadFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) //if user clicks ok
                //set selected path to display in download folder text box
                txtDownloadFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> isLinkGood = ValidateLink(); //get link validation results
            if(isLinkGood.Item1==true)
            {
                //RestrictAccessibility()
                //Pass the validated link into the download method
                //so it can be assigned to a property in the YouTube video or audio model object
                MessageBox.Show("Is it a good link? " + isLinkGood.Item1 + " Link is: " + isLinkGood.Item2);
            }           
        }
        private Tuple<bool, string> ValidateLink()
        {
            string normalURL;//normalized URL from YouTube
            if (!Directory.Exists(txtDownloadFolder.Text))
            {
                MessageBox.Show("Please enter a valid folder."); //block runs when folder cannot be found
                return Tuple.Create(false, "");
            }
            else if(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text,out normalURL))
            {
                return Tuple.Create(true, normalURL);//return true and actual link if successful
            }
            else
            {
                MessageBox.Show("Please enter a valid YouTube link.");
                return Tuple.Create(false, "");//return nothing if the link is not good
            }
        }
    }
}
