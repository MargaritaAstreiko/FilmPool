using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Repositories
{
    public interface ICollectionsRepository
    {
        Task<Collections> Get(int Id);
        Task<bool> Create(Collections collection);
        Task<IEnumerable<CollectionsResponseModel>> GetCollections(int userId);
        Task<bool> AddToCollection(FilmsInCollectionsRequest filmsAdd);
        Task<Collections> Delete(int Id);

    }
}
