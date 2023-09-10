using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Mvc;


namespace FilmPool.Controllers
{

  [Route("api/collections")]
    [ApiController]
    public class CollectionsController : Controller
    {
        ICollectionsService _collectionsService;

        public CollectionsController(ICollectionsService collectionsService)
        {
            _collectionsService = collectionsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection([FromBody] CollectionRequestModel collection)
        {
            var colletions = await _collectionsService.Create(collection);
            return Ok();
        }

        [HttpGet("user{id}")]
        public async Task<IActionResult> GetCollections(int id)
        {
            var collections = await _collectionsService.GetCollections(id);
            return Ok(collections);
        }

        [HttpPost("addFilm")]
        public async Task<IActionResult> AddToCollection([FromBody] FilmsInCollections filmsAdd)
        {
            var res = await _collectionsService.AddToCollection(filmsAdd);
            return Ok();
        }
    }
}
