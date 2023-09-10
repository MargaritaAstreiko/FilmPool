using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.Migrations;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
    public async Task<IEnumerable<CommentsResponseModel>>GetComments(int filmId)
    {
      return await Context.Comments.Where(x=>x.FilmId==filmId)
                .Select(x=> new CommentsResponseModel
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    CreatedDate = x.CreatedDate.ToString("g", new CultureInfo("de-CH")),
                    FilmId =x.FilmId,
                    UserId=x.UserId,
                    Film=x.Film,
                    User=x.User

                })
                .ToListAsync();
    }
  }
}
