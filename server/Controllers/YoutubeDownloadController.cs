using Microsoft.AspNetCore.Mvc;
using server.Services.Youtube;

namespace server.Controllers
{
    [ApiController]
    [Route("/Youtube")]
    public class YoutubeDownloadController : ControllerBase
    {
        [HttpGet("{Id?}")]
        public IActionResult HitForSingleFile(string? Id)
        {
            if (Id == null)
            {
                return BadRequest("Id parameter is required.");
            }


            return Ok($"Got Request for Youtube video with Id: {Id}");
        }
    }
}
