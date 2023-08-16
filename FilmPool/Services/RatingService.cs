using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
  public class RatingService : IRatingService
  {
    private readonly IRatingRepository _ratingRepository;


    public RatingService(IRatingRepository context)
    {
      _ratingRepository = context;
    }
    public async Task<bool> Create(RatingRequestModel rating)
    {
      return await _ratingRepository.Create(rating);
    }

    public async Task<bool> Update(RatingRequestModel rating)
    {
      return await _ratingRepository.Update(rating);
    }

    public async Task<Rating> GetRating(RatingRequestModel rating)
    {
      return await _ratingRepository.Get(rating);
    }
  }
}
