using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmPool.Controllers
{
  [Route("api/rating")]
  [Authorize]
  [ApiController]
  public class RatingController : Controller
  {
    IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
      _ratingService = ratingService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRating([FromBody] RatingRequestModel rating)
    {

      var ratingExisting = await _ratingService.GetRating(rating);
      var res = false;
      if (ratingExisting != null)
      {
        res = await _ratingService.Update(rating);
      }
      else
      {
        res = await _ratingService.Create(rating);
      }
      return Ok();
    }


  }

}
