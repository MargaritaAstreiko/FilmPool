using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
    public interface IFilmsService
    {
        Task<FilmsResponseModel> Get(int pageSize, int currentPage, int year, string search, int genre , bool rating, int collectionId);
        Task<Film> Get(int id);
        Task<bool> Update(Film film);
        Task<Film> Delete(int id);
        Task<double> GetRating(int filmId);
        Task<IEnumerable<FilmLightVersionResponse>> GetFilmsForCollections();
        Task<int> CreateFilm(Film film);
    }
}
