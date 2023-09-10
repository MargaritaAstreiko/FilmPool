using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Repositories
{
  public interface ICommentsRepository
  {
    Task<Comments> Get(int Id);
    Task<bool> Create(CommentRequestModel comment);
    Task<IEnumerable<CommentsResponseModel>> GetComments(int filmId);
  }
}
