using FilmPool.Data;
using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FilmPool.Repositories
{
  public class GenresRepository: IGenresRepository
  {
      private FilmPoolDbContext Context;

      public GenresRepository(FilmPoolDbContext context)
      {
        Context = context;
      }

      public async Task<IEnumerable<Genre>> Get()
      {
        return await Context.Genres.ToListAsync();
      }
    }
}
