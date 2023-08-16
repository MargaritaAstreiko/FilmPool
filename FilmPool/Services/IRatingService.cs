using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
  public interface IRatingService
  {
    Task<bool> Create(RatingRequestModel rating);
    Task<bool> Update(RatingRequestModel rating);
    Task<Rating> GetRating(RatingRequestModel rating);
  }
}
