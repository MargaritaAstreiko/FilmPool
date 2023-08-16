using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Repositories
{
  public interface IRatingRepository
  {
    Task<Rating> Get(RatingRequestModel rating);
    Task<bool> Create(RatingRequestModel rating);
    Task<bool> Update(RatingRequestModel rating);
    Task<double> GetFilmRating(int filmId);
  }
}
