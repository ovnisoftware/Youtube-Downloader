using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDownloader;
using YoutubeExtractor;
using System.Threading;

namespace YoutubeDownloader
{
    public class DownloadObject
    {
        public DownloadObject(string lineNumber)
        {
            this.lineNumber = lineNumber;
            videoInsteadOfMP3 = true;
            vidName = "";
            duplicationError = false;
            isVideoDownloaderNull = true;
            isAudioDownloaderNull = true;
            resetBoxes = false;
        }
        public Thread downloadThread { get; set; }
        public bool videoInsteadOfMP3 { get; set; }
        public string vidName { get; set; }
        public VideoDownloader videoDownloader { get; set; }
        public AudioDownloader audioDownloader { get; set; }
        public string lineNumber { get; set; }
        public bool duplicationError { get; set; }
        public bool isVideoDownloaderNull { get; set; }
        public bool isAudioDownloaderNull { get; set; }
        public bool resetBoxes { get; set; }
    }
}