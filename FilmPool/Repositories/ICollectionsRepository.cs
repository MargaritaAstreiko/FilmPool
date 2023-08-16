using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Repositories
{
  public interface ICollectionsRepository
  {
    Task<Collections> Get(int Id);
    Task<bool> Create(CollectionRequestModel collection);
    Task<IEnumerable<Collections>> GetCollections(int userId);
  }
}
