using FilmPool.DbModels;

namespace FilmPool.Services
{
  public interface IGenresService
  {
    Task<IEnumerable<Genre>> Get();
  }
}
