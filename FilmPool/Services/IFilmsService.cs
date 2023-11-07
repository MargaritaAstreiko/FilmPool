using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
    public interface IFilmsService
    {
        Task<FilmsResponseModel> Get(int pageSize, int currentPage, int year, string search, int genre , bool rating);
        Task<Film> Get(int id);
        Task<bool> Create(Film film);
        Task<bool> Update(FilmUpdateRequestModel film);
        Task<Film> Delete(int id);
        Task<double> GetRating(int filmId);
        Task<IEnumerable<FilmLightVersionResponse>> GetFilmsForCollections();
    }
}
