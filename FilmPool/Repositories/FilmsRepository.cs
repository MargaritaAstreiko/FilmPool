using FileStorage.FileStorage;
using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.Migrations;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Linq;
using System.Net.Mail;

namespace FilmPool.Repositories
{
    public class FilmsRepository : IFilmsRepository
    {
        private FilmPoolDbContext Context;

        public FilmsRepository(FilmPoolDbContext context)
        {
            Context = context;
        }

        public async Task<FilmsResponseModel> Get(int pageSize, int currentPage, int year, string search, int genre, bool ratingSort)
        {

            var films =  ratingSort? Context.Films.Where(x => (genre != -1 ? x.Title.Contains(search) && x.Genre.Equals((GenreEnum)genre) : x.Title.Contains(search)) && (year == 0 ? x.Title.Length > 0 : x.Year.Equals(year))).OrderByDescending(x=>x.TotalRating).Skip((currentPage - 1) * pageSize).Take(pageSize)
                : Context.Films.Where(x => (genre != -1 ? x.Title.Contains(search) && x.Genre.Equals((GenreEnum)genre) : x.Title.Contains(search)) && (year ==0?x.Title.Length>0: x.Year.Equals(year))).Skip((currentPage - 1) * pageSize).Take(pageSize);
            List<int> ids = await films.Select(x => x.Id).ToListAsync();
            var rating = Context.Rating.Where(x => ids.Contains(x.FilmId));
            var ratingCommon = from g in rating
                               group g by g.FilmId into a
                               select new { FilmId = a.Key, Rating = a.Average(a => a.Score) };

            var result = from f in films
                         join g in ratingCommon on f.Id equals g.FilmId into x
                         from g in x.DefaultIfEmpty()
                         select
            new FilmModel
            {
                Id = f.Id,
                Title = f.Title,
                Genre = f.Genre,
                FilmGenre = f.FilmGenre,
                Duration = f.Duration,
                Year = f.Year,
                Description = f.Description,
                Picture = f.Picture != null && f.Picture.Length > 0 ? Convert.ToBase64String(f.Picture) : null,
                Rating = g.Rating!=null ? g.Rating : 0,
            };
            var all = await result.ToListAsync();
            int totalFilms = await Context.Films.Where(x => (genre != -1 ? x.Title.Contains(search) && x.Genre.Equals((GenreEnum)genre) : x.Title.Contains(search)) && (year == 0 ? x.Title.Length > 0 : x.Year.Equals(year))).CountAsync();
            var res = new FilmsResponseModel { films = all, totalFilms = totalFilms };
            return res;
        }
        public async Task<Film> Get(int Id)
        {
            return await Context.Films.FindAsync(Id);
        }

        public async Task<bool> Create(Film film)
        {
            Context.Films.Add(film);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(FilmUpdateRequestModel film)
        {
            Film currentFilm = await Get(film.Id);
            currentFilm.Title = film.Title;
            currentFilm.Genre = film.Genre;
            currentFilm.Duration = film.Duration;
            currentFilm.Year = film.Year;
            currentFilm.Description = film.Description;

            Context.Films.Update(currentFilm);
            await Context.SaveChangesAsync();
            return true;

        }

        public async Task<Film> Delete(int Id)
        {
            Film film = await Get(Id);

            if (film != null)
            {
                Context.Films.Remove(film);
                await Context.SaveChangesAsync();
            }

            return film;
        }

        public async Task<IEnumerable<FilmLightVersionResponse>> GetFilmsForCollections()
        {
            var films = await Context.Films.Select(x => new FilmLightVersionResponse
            {
                Id = x.Id,
                Title = x.Title,
            }).ToListAsync();
            return films;
        }
    }
}
