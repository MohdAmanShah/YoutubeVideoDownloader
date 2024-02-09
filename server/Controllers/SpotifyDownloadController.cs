using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [ApiController]
    [Route("/Spotify")]
    public class SpotifyDownloadController : ControllerBase
    {
        [HttpGet("{Id?}")]
        public IActionResult HitForSingleFile(string? Id)
        {
            if (Id == null)
            {
                return BadRequest("Id parameter is required.");
            }
            return Ok($"Got Request for Spotify track with Id: {Id}");
        }
    }
}
