using FilmPool.Data.Schemas;
using FilmPool.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FilmPool.Data
{
    public class FilmPoolDbContext : DbContext
    {
        public FilmPoolDbContext(DbContextOptions<FilmPoolDbContext> options) : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Collections> Collections { get; set; }
        public DbSet<FilmsInCollections> FilmsInCollections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new CommentsConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionsConfiguration());
            modelBuilder.ApplyConfiguration(new FilmsInCollectionsConfiguration());


        }
    }
}
