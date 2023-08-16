using FilmPool.DbModels;

namespace FilmPool.Repositories
{
  public interface IGenresRepository
  {
      Task<IEnumerable<Genre>> Get();
    }
}
