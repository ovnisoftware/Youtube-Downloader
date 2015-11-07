using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Tuple<bool, string> isLinkGood = ValidateLink();
            MessageBox.Show("Is it a good link? " + isLinkGood.Item1 + " Link is: " + isLinkGood.Item2);

        }

        private Tuple<bool, string> ValidateLink()
        {
            
        }
    }
}
