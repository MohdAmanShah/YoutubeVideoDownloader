namespace server.Services.Youtube
{
    public interface IYoutubeDownloadService
    {
        string SetupFiles(string Id);
        string DownloadVideo(string Id);
        string DownloadAudio(string Id);
    }
}
