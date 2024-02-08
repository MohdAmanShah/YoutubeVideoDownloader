using System.Diagnostics;
using System.Text.RegularExpressions;
namespace server.Services
{
    public class YoutubeDLWrapper : IYoutubeDLWrapper
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<YoutubeDLWrapper> logger;
        private readonly string YoutubeVideoBaseUrl = "https://www.youtube.com/watch?v=";

        public YoutubeDLWrapper(IWebHostEnvironment webHostEnvironment, ILogger<YoutubeDLWrapper> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        public string DownloadVideo(string Id)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "yt-dlp";
                proc.StartInfo.Arguments = $"{YoutubeVideoBaseUrl + Id} -f bestvideo+bestaudio --output \"%(title)sVideo.%(ext)s\" ";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true; // Redirect standard error stream
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = webHostEnvironment.WebRootPath + "\\Videos"; // Set working directory

                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd(); // Read standard error stream
                proc.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    // Log or handle the error
                    logger.LogError($"Error while executing yt-dlp: {error}");
                }
                //Regex r = new Regex(@"""([^""]+)""");
                //var m = r.Match(output);
                //output = m.Value.Substring(1, m.Value.Length - 2);
                //output = webHostEnvironment.WebRootPath + "\\Videos\\" + output;
                //if (File.Exists(output))
                //{
                //    string newName = Path.GetFileNameWithoutExtension(output);
                //    string ext = Path.GetExtension(output);
                //    System.IO.File.Move(output, webHostEnvironment.WebRootPath + "\\Videos\\" + newName + "Video" + ext);
                //}
                return output;
            }
        }
        public string DownloadAudio(string Id)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "yt-dlp";
                proc.StartInfo.Arguments = $"{YoutubeVideoBaseUrl + Id} -x --output \"%(title)sAudio.%(ext)s\"";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true; // Redirect standard error stream
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = webHostEnvironment.WebRootPath + "\\Videos"; // Set working directory

                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd(); // Read standard error stream
                proc.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    // Log or handle the error
                    logger.LogError($"Error while executing yt-dlp: {error}");
                    return error;
                }
                return output;
            }
        }
    }
}
