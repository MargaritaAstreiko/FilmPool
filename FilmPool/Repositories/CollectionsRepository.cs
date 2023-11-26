using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.Migrations;
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
            var filmsInCollections =  Context.FilmsInCollections;
            var filmIds = await Context.FilmsInCollections.Select(x=>x.FilmId).ToListAsync();
            var films = Context.Films.Where(x => filmIds.Contains(x.Id)).Select(x => new FilmShortModel { Id = x.Id, Title = x.Title });
            var res = from s in collections
                      select new CollectionsResponseModel
                      {
                          Id = s.Id,
                          CollectionName = s.CollectionName,
                          FilmNames = (from i in filmsInCollections
                                     join t in films on i.FilmId equals t.Id 
                                     where i.CollectionId==s.Id
                                     select t.Title).ToList(),
                         
                      };
            var result = await res.ToListAsync();
            return result;
        }

        public async Task<bool> AddToCollection(FilmsInCollectionsRequest filmsAdd)
        {

            FilmsInCollections coll = await Context.FilmsInCollections.Where(x => filmsAdd.FilmId==x.FilmId && filmsAdd.CollectionId==x.CollectionId).FirstOrDefaultAsync();
            if (coll == null)
            {
                Context.FilmsInCollections.Add(new FilmsInCollections
                {
                    FilmId = filmsAdd.FilmId,
                    CollectionId = filmsAdd.CollectionId,
                    AddedDate = filmsAdd.AddedDate,
                });
                await Context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<Collections> Delete(int Id)
        {
            Collections collection = await Get(Id);

            if (collection != null)
            {
                Context.Collections.Remove(collection);
                await Context.SaveChangesAsync();
            }

            return collection;
        }

    }
}
