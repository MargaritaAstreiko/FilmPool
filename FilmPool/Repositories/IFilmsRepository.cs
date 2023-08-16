using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Repositories
{
  public interface IFilmsRepository
  {
    Task<FilmsResponseModel> Get(int pageSize, int currentPage, string search, int genre);
    Task<Film> Get(int id);
    Task<bool> Create(Film film);
    Task<bool> Update(FilmUpdateRequestModel film);
    Task<Film> Delete(int id);
  }
}
