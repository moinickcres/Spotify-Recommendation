using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendation.Models;

namespace SpotifyRecommendation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<string>> GetAlbums() 
        {
            var solution = await SpotifyAPI.ShowAlbums();

            return Ok(solution);
        }
    }
}
