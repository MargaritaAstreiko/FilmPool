using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
    public interface  ICollectionsService
    {
        Task<bool> Create(CollectionRequestModel comment);
        Task<IEnumerable<CollectionsResponseModel>> GetCollections(int userId);
        Task<bool> AddToCollection(FilmsInCollectionsRequest filmsAdd);
        Task<Collections> Delete(int Id);

    }
}
