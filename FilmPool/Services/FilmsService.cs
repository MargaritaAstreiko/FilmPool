using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using System.Drawing.Printing;

namespace FilmPool.Services
{
    public class FilmsService : IFilmsService
    {
        private readonly IFilmsRepository _filmsRepository;
        private readonly IRatingRepository _ratingRepository;


        public FilmsService(IFilmsRepository context, IRatingRepository ratingRepository)
        {
            _filmsRepository = context;
            _ratingRepository = ratingRepository;
        }

        public async Task<FilmsResponseModel> Get(int pageSize, int currentPage, int year, string search, int genre , bool rating)
        {
            return await _filmsRepository.Get(pageSize, currentPage, year, search, genre, rating);
        }

        public async Task<Film> Get(int id)
        {
            return await _filmsRepository.Get(id);
        }
        public async Task<bool> Update(FilmUpdateRequestModel film)
        {
            return await _filmsRepository.Update(film);
        }

        public async Task<Film> Delete(int id)
        {
            return await _filmsRepository.Delete(id);
        }

        public async Task<int> CreateFilm(FilmUpdateRequestModel film)
        {
            return await _filmsRepository.CreateFilm(film);
        } 

        public async Task<double> GetRating(int filmId)
        {
            return await _ratingRepository.GetFilmRating(filmId);
        }

        public async Task<IEnumerable<FilmLightVersionResponse>> GetFilmsForCollections()
        {
            return await _filmsRepository.GetFilmsForCollections();
        }
    }
}
