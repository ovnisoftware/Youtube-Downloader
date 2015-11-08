using System.Collections.Generic;
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public abstract class YoutubeModel
    {
        public string Title { get; set; }
        public IEnumerable<VideoInfo> VideoInfo { get; set; }
        public string FolderPath { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public VideoInfo Video { get; set; }
    }

    public class YoutubeVideoModel : YoutubeModel
    {
        public VideoDownloader VideoDownloaderType { get; set; }

        public override string ToString()
        {
            if (Video != null)
                return (Video.Title + Video.VideoExtension);
            return base.ToString();
        }
    }

    public class YoutubeAudioModel : YoutubeModel
    {
        public AudioDownloader AudioDownloaderType { get; set; }

        public override string ToString()
        {
            if (Video != null)
                return (Video.Title + Video.AudioExtension);
            return base.ToString();
        }
    }
}