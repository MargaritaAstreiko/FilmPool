using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class RatingConfiguration : IEntityTypeConfiguration<Rating>
  {
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
      builder.ToTable("Rating");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.FilmId).HasConversion<int>();
      builder.Property(t => t.UserId).HasConversion<int>();
      builder.Property(t => t.Score).HasConversion<int>();
    }
  }
}
