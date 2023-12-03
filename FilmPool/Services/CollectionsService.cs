using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.Services
{
    public class CollectionsService: ICollectionsService
    {
        private readonly ICollectionsRepository _collectionsRepository;

        public CollectionsService(ICollectionsRepository context)
        {
            _collectionsRepository = context;
        }
        public async Task<bool> Create(Collections collection)
        {
            return await _collectionsRepository.Create(collection);
        }

        public async Task<IEnumerable<CollectionsResponseModel>>GetCollections(int userId)
        {
            return await _collectionsRepository.GetCollections(userId);
        }

        public async Task<bool> AddToCollection(FilmsInCollectionsRequest filmsAdd)
        {
            return await _collectionsRepository.AddToCollection(filmsAdd);
        }

        public async Task<Collections> Delete(int Id)
        {
            return await _collectionsRepository.Delete(Id);
        }



    }
}
