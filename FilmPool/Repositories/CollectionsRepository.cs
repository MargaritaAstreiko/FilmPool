using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
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
                UserId = collection.UserId,
                CollectionName = collection.CollectionName,
                CreatedDate = collection.CreatedDate,
                IsPublic = collection.isPublic,
            });
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CollectionsResponseModel>> GetCollections(int userId)
        {
            var collections =  Context.Collections.Where(x => (x.UserId == userId && x.IsPublic == false) || x.IsPublic == true);
            var fimsInCollections =  Context.FilmsInCollections;
            var filmIds = await Context.FilmsInCollections.Select(x=>x.FilmId).ToListAsync();
            var films = Context.Films.Where(x => filmIds.Contains(x.Id)).Select(x => new FilmShortModel { Id = x.Id, Title = x.Title });
            var res = from s in collections
                      join g in fimsInCollections on s.Id equals g.CollectionId into h
                      from i in h.DefaultIfEmpty()
                      select new CollectionsResponseModel
                      {
                          Id = s.Id,
                          CollectionName = s.CollectionName,
                          filmNames = (from f in films
                                       where f.Id == i.FilmId
                                       select f.Title).ToList(),

                      };
            var result = await res.ToListAsync();
            return result;
        }

        public async Task<bool> AddToCollection(FilmsInCollections filmsAdd)
        {
            Context.FilmsInCollections.Add(filmsAdd);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
