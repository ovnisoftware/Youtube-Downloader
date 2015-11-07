using System.Collections.Generic; //IEnumerable Collection
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public class YoutubeModel
    {
        public IEnumerable<VideoInfo> VideoInfo { get; set; } //IEnumerable is a collection, meaning a grouping of objects, in this case it is a collection of VideoInfo objects
        public string FolderPath { get; set; } //Stores the folder location we designated in the folderbrowserdialog
        public string Link { get; set; } //This is the Youtube link we typed in
        public string FilePath { get; set; } //Stores the folder location we designated in the folderbrowserdialog plus the actual name of the file and it's file extension (.MP4 or .MP3)
        public VideoInfo Video { get; set; } //This object is from the Youtube Extractor library and contains info about the file we will be downloading including it's title
    }
    
    public class YoutubeVideoModel :YoutubeModel
    {
        public VideoDownloader VideoDownloaderType { get; set; }  //This is the object that contains the Execute method which actually downloads the file, the DownloadFinished event handler which is called when the download is finished, we'll use it to enable access to the buttons after disabling access during the download and the DownloadProgressChange event handler which updates the progress bar
    }

    public class YoutubeAudioModel : YoutubeModel
    {
        public AudioDownloader AudioDownloaderType { get; set; }  //This is the object that contains the Execute method which actually downloads the file, the DownloadFinished event handler which is called when the download is finished, we'll use it to enable access to the buttons after disabling access during the download and the DownloadProgressChange event handler which updates the progress bar
    }
}