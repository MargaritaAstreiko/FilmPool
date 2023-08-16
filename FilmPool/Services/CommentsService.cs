using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
  public class CommentsService : ICommentsService
  {
    private readonly ICommentsRepository _commentsRepository;


    public CommentsService(ICommentsRepository context)
    {
      _commentsRepository = context;
    }
    public async Task<bool> Create(CommentRequestModel comment)
    {
      return await _commentsRepository.Create(comment);
    }

    public async Task<IEnumerable<Comments>> GetComments(int filmId)
    {
      return await _commentsRepository.GetComments(filmId);
    }

  }
}
