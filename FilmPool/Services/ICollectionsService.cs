using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
    public interface  ICollectionsService
    {
        Task<bool> Create(CollectionRequestModel comment);
        Task<IEnumerable<Collections>> GetCollections(int userId);
    }
}
