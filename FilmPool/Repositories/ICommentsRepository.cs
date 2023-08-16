using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Repositories
{
  public interface ICommentsRepository
  {
    Task<Comments> Get(int Id);
    Task<bool> Create(CommentRequestModel comment);
    Task<IEnumerable<Comments>> GetComments(int filmId);
  }
}
