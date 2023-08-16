using FilmPool.DbModels;
using FilmPool.Repositories;

namespace FilmPool.Services
{
  public class GenresService: IGenresService
  {
    private readonly IGenresRepository _genresRepository;


    public GenresService(IGenresRepository context)
    {
      _genresRepository = context;
    }

    public async Task<IEnumerable<Genre>> Get()
    {
      return await _genresRepository.Get();
    }
  
  }
}
