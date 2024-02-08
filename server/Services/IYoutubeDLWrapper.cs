namespace server.Services
{
    public interface IYoutubeDLWrapper
    {
        string DownloadVideo(string Id);
        string DownloadAudio(string Id);
    }
}
