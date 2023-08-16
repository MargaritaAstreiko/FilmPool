using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace FilmPool.Repositories
{
  public class RatingRepository: IRatingRepository
  {
    private FilmPoolDbContext Context;

    public RatingRepository(FilmPoolDbContext context)
    {
      Context = context;
    }
    public async Task<Rating> Get(RatingRequestModel rating)
    {
      return await Context.Rating.Where(x=> x.FilmId==rating.FilmId && x.UserId == rating.UserId).FirstOrDefaultAsync();
    }
    public async Task<bool> Create(RatingRequestModel rating)
    {
      Context.Rating.Add(new Rating { 
        FilmId=rating.FilmId,
        UserId=rating.UserId,
        Score=rating.Score,
      });
      await Context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Update(RatingRequestModel rating)
    {
      Rating currentRating = await Get(rating);
      currentRating.Score = rating.Score;

      Context.Rating.Update(currentRating);
      await Context.SaveChangesAsync();
      return true;
    }

    public async Task<double> GetFilmRating(int filmId)
    {
      var rating = await Context.Rating.Where(x => x.FilmId == filmId).Select(x => x.Score).SumAsync();
      int count = await Context.Rating.Where(x => x.FilmId == filmId).Select(x => x.Score).CountAsync();
      var res = rating / count;
      return res;
    }

  }
}
