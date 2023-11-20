using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
    public interface ICommentsService
    {
        Task<bool> Create(CommentRequestModel comments);
        Task<IEnumerable<CommentsResponseModel>> GetComments(int filmId);
        Task<bool> UpdateComment(CommentUpdateModel comment);
        Task<Comments> Delete(int Id);
    }
}
