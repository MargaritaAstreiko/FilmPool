using FilmPool.DbModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Data.Entity.ModelConfiguration;
using System.IO;

namespace FilmPool.Data.Schemas
{
  public class FilmConfiguration : IEntityTypeConfiguration<Film>
  {

    public void Configure(EntityTypeBuilder<Film> builder)
    {
      builder.ToTable("Films");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Title).HasColumnName("Title").HasMaxLength(255);
      builder.HasOne(t => t.FilmGenre)
        .WithMany()
        .HasForeignKey(x => x.Genre)
        .IsRequired(true);
      builder.Property(t => t.Year).HasColumnName("Year");
      builder.Property(t => t.Duration).HasColumnName("Duration").HasMaxLength(30);
      builder.Property(t => t.Description).HasColumnName("Description");
      builder.Property(t => t.Picture).HasColumnName("Picture");
      builder.Property(t => t.TotalRating).HasColumnName("TotalRating");
      builder.Property(t => t.FilmUrl).HasColumnName("FilmUrl");
      builder.HasData(SeedFilmData());
      builder.HasMany(t => t.Ratings)
        .WithOne(x => x.Film)
        .HasForeignKey(x => x.FilmId);
      builder.HasMany(t => t.Comments)
        .WithOne(x => x.Film)
        .HasForeignKey(x => x.FilmId);
      builder.HasMany(t => t.Collections)
        .WithOne(x => x.Film)
        .HasForeignKey(x => x.FilmId);
    }

    public List<Film> SeedFilmData()
    {
      var films = new List<Film>();
      using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\RawData\filmsbase.json"))
      {
        string json = r.ReadToEnd();
        films = JsonConvert.DeserializeObject<List<Film>>(json);
      }
      return films;
    }

  }
}
