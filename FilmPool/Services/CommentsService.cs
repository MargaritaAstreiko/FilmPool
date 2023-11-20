using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

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

        public async Task<IEnumerable<CommentsResponseModel>> GetComments(int filmId)
        {
            return await _commentsRepository.GetComments(filmId);
        }

        public async Task<bool> UpdateComment(CommentUpdateModel comment)
        {
            return await _commentsRepository.Update(comment);
        }

        public async Task<Comments> Delete(int Id)
        {
            return await _commentsRepository.Delete(Id);
        }

    }
}
