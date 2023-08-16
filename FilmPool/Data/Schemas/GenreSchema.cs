using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class GenreConfiguration : IEntityTypeConfiguration<Genre>
  {
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
      builder.ToTable("Genres");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).HasConversion<int>().IsRequired();
      builder.Property(t => t.GenreName).HasConversion<string>().IsRequired();
      builder.HasData(
                Enum.GetValues(typeof(GenreEnum))
                    .Cast<GenreEnum>()
                    .Select(e => new Genre()
                    {
                      Id = e,
                      GenreName = e.ToString()
                    })
                    );
    }
  }
}
