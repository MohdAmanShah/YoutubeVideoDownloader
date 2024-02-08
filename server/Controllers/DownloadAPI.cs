using Microsoft.AspNetCore.Mvc;
using server.Services;
namespace server.Controllers
{
    [ApiController]
    [Route("/")]
    public class DownloadAPI : ControllerBase
    {
        private readonly IYoutubeDLWrapper Yt;

        public DownloadAPI(IYoutubeDLWrapper Yt)
        {
            this.Yt = Yt;
        }

        [HttpGet("/Video/{Id?}")]
        public IActionResult DownloadVideo(string? Id)
        {
            return Ok(Yt.DownloadVideo(Id));
        }

        [HttpGet("/Audio/{Id?}")]
        public IActionResult DownloadAudio(string? Id)
        {
            return Ok(Yt.DownloadAudio(Id));
        }

    }
}
