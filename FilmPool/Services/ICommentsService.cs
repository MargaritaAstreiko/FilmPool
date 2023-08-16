using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
  public interface ICommentsService
  {
    Task<bool> Create(CommentRequestModel comments);
    Task<IEnumerable<Comments>> GetComments(int filmId);
  }
}
