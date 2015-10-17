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
    public class VideoGetter
    {
        public static void GetVideo(ref DownloadObject downloadObject, string txtLink, ref BackgroundWorker back, string folderPath)
        {
            try
            {
                //Get the available video formats.
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(txtLink);
                if (downloadObject.videoInsteadOfMP3)
                {
                    //Select the first .mp4 video with 360p resolution
                    VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
                    //If the video has a decrypted signature, decipher it
                    if (video.RequiresDecryption)
                    {
                        DownloadUrlResolver.DecryptDownloadUrl(video);
                    }
                    //Remove illegal characters from video.Title
                    string title = video.Title;
                    string cleanTitle = CleanFileName(title);
                    string path = Path.Combine(folderPath, cleanTitle + video.VideoExtension);
                    //Check if path exists
                    int switch_statement;
                    if (File.Exists(path))
                    {
                        DialogResult dialogResult = MessageBox.Show("The file " + path + " already exists.  Do you want to overwrite it?", downloadObject.lineNumber, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            switch_statement = 0;
                        }
                        else
                            switch_statement = 1;
                    }
                    else
                        switch_statement = 0;
                    switch (switch_statement)
                    {
                        case 0:
                            downloadObject.videoDownloader = new VideoDownloader(video, path);
                            //Set tracker, isVideoDownloaderNull to set progressbar in timer
                            downloadObject.isVideoDownloaderNull = false;
                            downloadObject.vidName = cleanTitle + video.VideoExtension;
                            downloadObject.downloadThread = new Thread(downloadObject.videoDownloader.Execute);
                            downloadObject.downloadThread.Start();
                            while ((back.CancellationPending == false))
                            {
                                //wait for cancel button to get clicked or download to complete
                            }
                            break;
                        case 1:
                            downloadObject.resetBoxes = true;
                            break;
                    }
                }
                else
                {
                    //We want the first extractable video with the highest audio quality.
                    VideoInfo video = videoInfos
                        .Where(info => info.CanExtractAudio)
                        .OrderByDescending(info => info.AudioBitrate)
                        .First();
                    //If the video has a decrypted signature, decipher it
                    if (video.RequiresDecryption)
                    {
                        DownloadUrlResolver.DecryptDownloadUrl(video);
                    }
                    //Remove illegal characters from video.Title
                    string title = video.Title;
                    string cleanTitle = CleanFileName(title);
                    string path = Path.Combine(folderPath, cleanTitle + video.AudioExtension);
                    //Check if path exists
                    int switch_statement;
                    if (File.Exists(path))
                    {
                        DialogResult dialogResult = MessageBox.Show("The file " + path + " already exists.  Do you want to overwrite it?", downloadObject.lineNumber, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            switch_statement = 0;
                        }
                        else
                            switch_statement = 1;
                    }
                    else
                        switch_statement = 0;
                    switch (switch_statement)
                    {
                        case 0:
                            downloadObject.audioDownloader = new AudioDownloader(video, path);
                            //Set tracker, isAudioDownloaderNull to set progressbar in timer
                            downloadObject.isAudioDownloaderNull = false;
                            downloadObject.vidName = cleanTitle + video.AudioExtension;
                            downloadObject.downloadThread = new Thread(downloadObject.audioDownloader.Execute);
                            downloadObject.downloadThread.Start();
                            while ((back.CancellationPending == false))
                            {
                                //wait for cancel button to get clicked or download to complete
                            }
                            break;
                        case 1:
                            downloadObject.resetBoxes = true;
                            break;
                    }
                }
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("Please enter a valid Youtube link.", downloadObject.lineNumber);
                downloadObject.resetBoxes = true;
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Please enter a valid Youtube link.  You may not have pasted in a Youtube link.", downloadObject.lineNumber);
                downloadObject.resetBoxes = true;
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("Please enter a valid Youtube link.  That doesn't link to a valid video.", downloadObject.lineNumber);
                downloadObject.resetBoxes = true;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Please enter a valid file path", downloadObject.lineNumber);
                downloadObject.resetBoxes = true;
            }
            catch (YoutubeExtractor.VideoNotAvailableException)
            {
                MessageBox.Show("That video is not available.", downloadObject.lineNumber);
                downloadObject.resetBoxes = true;
            }
        }
        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
