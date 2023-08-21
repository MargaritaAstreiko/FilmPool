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

        [HttpPost("user")]
        public async Task<IActionResult> GetCollections([FromBody] CollectionRequestModel collection)
        {
            var collections = await _collectionsService.GetCollections(collection.UserId);
            return Ok(collections);
        }
    }
}
