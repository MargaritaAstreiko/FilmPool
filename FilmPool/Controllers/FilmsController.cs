using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FileStorage.FileStorage;
using FilmPool.DbModels;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Utilities;
using System.Drawing;
using Microsoft.AspNet.Identity;

namespace FilmPool.Controllers
{

    [Route("api/films")]
    [Authorize]
    [ApiController]
    public class FilmsController : Controller
    {
        IFilmsService _filmsService;

        public FilmsController(IFilmsService filmsService)
        {
            _filmsService = filmsService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmsRequestModel filmsRequestModel)
        {
            string search = filmsRequestModel.search ?? string.Empty;
            int genre = filmsRequestModel.genre?.GenreName.Length > 0 ? (int)filmsRequestModel.genre.Id : -1;
            var films = await _filmsService.Get(filmsRequestModel.pageSize, filmsRequestModel.currentPage, filmsRequestModel.year, search, genre, (bool)filmsRequestModel.rating);

            return Ok(films);
        }

        [HttpPost("Picture/{id}")]
        public async Task<IActionResult> PostPicture( int id, IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var picture = new Picture();
            var res = picture.UploadImage(bytes, id,"dbo.Films");
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await _filmsService.Get(id);
            double rating = await _filmsService.GetRating(id);
            var res = new { film, rating };
            return Ok(res);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFilm([FromBody] FilmUpdateRequestModel film)
        {
            var res = await _filmsService.Update(film);
            return Ok(res);
        }


        [HttpPost("new")]
        public async Task<IActionResult> CreateFilm([FromBody] FilmUpdateRequestModel film)
        {
            var res = await _filmsService.CreateFilm(film);
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFilm(int id)
        {
            var res = await _filmsService.Delete(id);
            return Ok(res);
        }

        [HttpGet("light")]
        public async Task<IActionResult> GetFilmsForCollections()
        {
            var res = await _filmsService.GetFilmsForCollections();
            return Ok(res);
        }

    }
}
