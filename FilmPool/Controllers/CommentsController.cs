using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
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
        public async Task<IActionResult> GetComments([FromBody] CommentsRequestModel commentRequestModel)
        {
            var comments = await _commentsService.GetComments(commentRequestModel.filmId);
            return Ok(comments);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentUpdateModel comment)
        {
            var res = await _commentsService.UpdateComment(comment);
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var res = await _commentsService.Delete(id);
            return Ok(res);
        }
    }
}
