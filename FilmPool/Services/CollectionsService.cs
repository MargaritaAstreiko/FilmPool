using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
    public class CollectionsService: ICollectionsService
    {
        private readonly ICollectionsRepository _collectionsRepository;


        public CollectionsService(ICollectionsRepository context)
        {
            _collectionsRepository = context;
        }
        public async Task<bool> Create(CollectionRequestModel comment)
        {
            return await _collectionsRepository.Create(comment);
        }

        public async Task<IEnumerable<Collections>>GetCollections(int userId)
        {
            return await _collectionsRepository.GetCollections(userId);
        }
    }
}
