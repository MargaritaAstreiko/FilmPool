using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.Migrations;
using FilmPool.RequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FilmPool.Repositories
{
  public class CommentsRepository : ICommentsRepository
  {
    private FilmPoolDbContext Context;

    public CommentsRepository(FilmPoolDbContext context)
    {
      Context = context;
    }
    public async Task<Comments> Get(int Id)
    {
      return await Context.Comments.FindAsync(Id);
    }
    public async Task<bool> Create(CommentRequestModel comment)
    {
      Context.Comments.Add(new Comments
      {
        FilmId = comment.FilmId,
        UserId = comment.UserId,
        Comment = comment.Comment,
        CreatedDate =comment.CreatedDate
      });
      await Context.SaveChangesAsync();
      return true;
    }
    public async Task<IEnumerable<Comments>>GetComments(int filmId)
    {
      return await Context.Comments.Where(x=>x.FilmId==filmId).ToListAsync();
    }
  }
}
