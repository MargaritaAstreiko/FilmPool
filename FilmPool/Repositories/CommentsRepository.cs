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
                CreatedDate = comment.CreatedDate
            });
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CommentUpdateModel comment)
        {
            Comments currentComment = await Get(comment.Id);
            currentComment.Comment = comment.Comment;
            currentComment.CreatedDate = comment.CreatedDate;

            Context.Comments.Update(currentComment);
            await Context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<CommentsResponseModel>> GetComments(int filmId)
        {
            var comments = Context.Comments.Where(x => x.FilmId == filmId);
            var userIds = await Context.Comments.Where(x => x.FilmId == filmId).Select(x => x.UserId).ToListAsync();
            var users = Context.Users.Where(x => userIds.Contains(x.Id));

            var result = from x in comments
                         join g in users on x.UserId equals g.Id into f
                         from g in f.DefaultIfEmpty()
                         select
              new CommentsResponseModel
              {
                  Id = x.Id,
                  Comment = x.Comment,
                  CreatedDate = x.CreatedDate.ToString("g", new CultureInfo("de-CH")),
                  FilmId = x.FilmId,
                  UserId = x.UserId,
                  UserName = g.UserName,
                  Picture = g.Picture != null && g.Picture.Length > 0 ? Convert.ToBase64String(g.Picture) : null,
                  Film = x.Film,
                  User = x.User

              };

            return await result.ToListAsync();
        }

        public async Task<Comments> Delete(int Id)
        {
            Comments comment = await Get(Id);

            if (comment!= null)
            {
                Context.Comments.Remove(comment);
                await Context.SaveChangesAsync();
            }

            return comment;
        }

    }
}
