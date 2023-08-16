using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace FilmPool.Repositories
{
  public class CollectionsRepository : ICollectionsRepository
  {
    private FilmPoolDbContext Context;

    public CollectionsRepository(FilmPoolDbContext context)
    {
      Context = context;
    }
    public async Task<Collections> Get(int Id)
    {
      return await Context.Collections.FindAsync(Id);
    }
    public async Task<bool> Create(CollectionRequestModel collection)
    {
      Context.Collections.Add(new Collections
      {
        FilmId = collection.FilmId,
        UserId = collection.UserId,
        CollectionName = collection.CollectionName,
        CreatedDate = collection.CreatedDate
      });
      await Context.SaveChangesAsync();
      return true;
    }
    public async Task<IEnumerable<Collections>> GetCollections(int userId)
    {
      return await Context.Collections.Where(x => x.UserId == userId).ToListAsync();
    }
  }
}
