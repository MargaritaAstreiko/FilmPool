using AutoMapper;
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
        private readonly IMapper _mapper;

        public CollectionsController(ICollectionsService collectionsService, IMapper mapper)
        {
            _collectionsService = collectionsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection([FromBody] CollectionRequestModel collection)
        {
            var collectionModel = _mapper.Map<Collections>(collection);
            var colletions = await _collectionsService.Create(collectionModel);
            return Ok();
        }

        [HttpGet("user{id}")]
        public async Task<IActionResult> GetCollections(int id)
        {
            var collections = await _collectionsService.GetCollections(id);
            return Ok(collections);
        }

        [HttpPost("addFilm")]
        public async Task<IActionResult> AddToCollection([FromBody] FilmsInCollectionsRequest filmsAdd)
        {
            var res = await _collectionsService.AddToCollection(filmsAdd);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCollection(int id)
        {
            var res = await _collectionsService.Delete(id);
            return Ok(res);
        }
    }
}
