using FilmPool.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmPool.Controllers
{
  [Route("api/genres")]
  [ApiController]
  public class GenresController : Controller
  {
    IGenresService _genresService;

    public GenresController(IGenresService genresService)
    {
      _genresService = genresService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var genres = await _genresService.Get();
      return Ok(genres);
    }

  }
}
