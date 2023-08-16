using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class CollectionsConfiguration : IEntityTypeConfiguration<Collections>
  {
    public void Configure(EntityTypeBuilder<Collections> builder)
    {
      builder.ToTable("Collections");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.UserId).HasConversion<int>();
      builder.Property(t => t.FilmId).HasConversion<int>();
      builder.Property(t => t.CollectionName).HasColumnName("CollectionName").HasMaxLength(2000);
      builder.Property(t => t.CreatedDate).HasConversion<DateTime>();
    }
  }
}
