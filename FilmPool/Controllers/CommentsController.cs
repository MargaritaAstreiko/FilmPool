using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmPool.Controllers
{

  [Route("api/comments")]
  [ApiController]
  public class CommentsController : Controller
  {
    ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
      _commentsService = commentsService;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentRequestModel comment)
    {
      var comments = await _commentsService.Create(comment);
      return Ok();
    }

    [HttpPost("film")]
    public async Task<IActionResult> GetComments([FromBody] CommentsRequestModel commentRequestModel )
    {
      var comments = await _commentsService.GetComments(commentRequestModel.filmId);
      return Ok(comments);
    }
  }
}
