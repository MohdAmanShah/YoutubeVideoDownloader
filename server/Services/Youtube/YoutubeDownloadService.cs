using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace server.Services.Youtube
{
    public class FileInformation
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
    }

    public class YoutubeDownloadService : IYoutubeDownloadService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<YoutubeDownloadService> logger;
        private readonly string YoutubeVideoBaseUrl = "https://www.youtube.com/watch?v=";

        public YoutubeDownloadService(IWebHostEnvironment webHostEnvironment, ILogger<YoutubeDownloadService> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        public string DownloadVideo(string Id)
        {
            string fileName = Guid.NewGuid().ToString();
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "yt-dlp";
                proc.StartInfo.Arguments = $"{YoutubeVideoBaseUrl + Id} -f bestvideo+bestaudio --output \"%(title)s{fileName}.%(ext)s\" ";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true; // Redirect standard error stream
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = webHostEnvironment.WebRootPath + "\\Files"; // Set working directory

                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd(); // Read standard error stream
                proc.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    logger.LogError($"Error while executing yt-dlp: {error}");
                    return "1";
                }
                string DirectoryPath = webHostEnvironment.WebRootPath + "\\Files\\";
                var FilePaths = Directory.GetFiles(DirectoryPath, "*" + fileName + ".*");
                string FilePath = FilePaths[0];
                fileName = Path.GetFileName(FilePath);
                return fileName;
            }
        }
        public string DownloadAudio(string Id)
        {
            string fileName = Guid.NewGuid().ToString();
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "yt-dlp";
                proc.StartInfo.Arguments = $"{YoutubeVideoBaseUrl + Id} -x --output \"%(title)s{fileName}.%(ext)s\" ";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true; // Redirect standard error stream
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = webHostEnvironment.WebRootPath + "\\Files"; // Set working directory

                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd(); // Read standard error stream
                proc.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    // Log or handle the error
                    logger.LogError($"Error while executing yt-dlp: {error}");
                    return "1";
                }
                string DirectoryPath = webHostEnvironment.WebRootPath + "\\Files\\";
                var FilePaths = Directory.GetFiles(DirectoryPath, "*" + fileName + ".*");
                string FilePath = FilePaths[0];
                fileName = Path.GetFileName(FilePath);
                return fileName;
            }
        }

        public string SetupFiles(string Id)
        {
            string VideoFile;
            string AudioFile;

            VideoFile = DownloadVideo(Id);
            AudioFile = DownloadAudio(Id);

            var Files = new List<FileInformation>()
            {
                new FileInformation() { FileName = VideoFile,FileType="Video"},
                new FileInformation() { FileName = AudioFile,FileType = "Audio"},
            };

            return JsonSerializer.Serialize(Files);
        }
    }
}
