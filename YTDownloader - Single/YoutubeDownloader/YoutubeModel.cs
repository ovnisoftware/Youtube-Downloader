using System.Collections.Generic; //IEnumerable Collection
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public class YoutubeModel
    {
        public IEnumerable<VideoInfo> VideoInfo { get; set; }
        public string FolderPath { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public VideoInfo Video { get; set; }
    }

    public class YoutubeVideoModel :YoutubeModel
    {
        public VideoDownloader VideoDownloaderType { get; set; }
    }

    public class YoutubeAudioModel : YoutubeModel
    {
        public AudioDownloader AudioDownloaderType { get; set; }
    }
}