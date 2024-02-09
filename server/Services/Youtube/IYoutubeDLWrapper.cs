namespace server.Services.Youtube
{
    public interface IYoutubeDLWrapper
    {
        string YoutubeDownload(string Id);
        string DownloadVideo(string Id);
        string DownloadAudio(string Id);
    }
}
