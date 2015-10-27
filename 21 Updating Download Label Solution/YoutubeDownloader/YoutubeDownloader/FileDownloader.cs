using System.Collections.Generic; //IEnumerable Collection
using System.IO;  //Path
using System.Linq;  //First() method
using System.Threading.Tasks;  //Task
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public static class FileDownloader
    {
        //Returns VideoInfo List (Shared by both audio and video models)
        public static IEnumerable<VideoInfo> GetVideoInfos(YoutubeModel model)
        {
            //Get the available video formats.
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(model.Link);
            return videoInfos;
        }

        //Returns VideoInfo object (Only for video model)
        public static VideoInfo GetVideoInfo(YoutubeVideoModel videoModel)
        {
            //Select the first .mp4 video with 360p resolution
            VideoInfo video = videoModel.VideoInfo.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
            return video;
        }

        //Returns VideoInfo object (Only for audio model)
        public static VideoInfo GetVideoInfoAudioOnly(YoutubeAudioModel audioModel)
        {
            //We want the first extractable video with the highest audio quality.
            VideoInfo video = audioModel.VideoInfo
                .Where(info => info.CanExtractAudio)
                .OrderByDescending(info => info.AudioBitrate)
                .First();
            return video;
        }

        //Returns filepath string (Shared by both models)
        public static string GetPath(YoutubeModel model)
        {
            //Decrypts Video if necessary
            if (model.Video.RequiresDecryption)
                DownloadUrlResolver.DecryptDownloadUrl(model.Video);

            //Remove illegal characters from video.Title
            string title = model.Video.Title;
            string cleanTitle = CleanFileName(title);

            //Set FilePath property
            string path = Path.Combine(model.FolderPath, cleanTitle);
            return path;
        }

        //Returns VideoDownloader object (Only for video model)
        public static VideoDownloader GetVideoDownloader(YoutubeVideoModel videoModel)
        {
            return new VideoDownloader(videoModel.Video, videoModel.FilePath);
        }

        //Returns AudioDownloader object (Only for audio model)
        public static AudioDownloader GetAudioDownloader(YoutubeAudioModel audioModel)
        {
            //Create AudioDownloader object
            return new AudioDownloader(audioModel.Video, audioModel.FilePath);
        }

        //Downloads Audio (Only for Audio model)
        public static void DownloadAudio(YoutubeAudioModel audioModel)
        {
            Task.Run(() => audioModel.AudioDownloaderType.Execute());
        }

        //Downloads Video (Only for video model)
        public static void DownloadVideo(YoutubeVideoModel vidDownloader)
        {
            Task.Run(() => vidDownloader.VideoDownloaderType.Execute());
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
