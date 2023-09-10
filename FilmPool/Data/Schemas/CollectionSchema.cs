using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmPool.Data.Schemas
{
  public class CollectionsConfiguration : IEntityTypeConfiguration<Collections>
  {
    public void Configure(EntityTypeBuilder<Collections> builder)
    {
      builder.ToTable("Collections");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.UserId).HasConversion<int>();
      builder.Property(t => t.CollectionName).HasColumnName("CollectionName").HasMaxLength(2000);
      builder.Property(t => t.IsPublic);
      builder.Property(t => t.CreatedDate).HasConversion<DateTime>();
      builder.HasMany(t => t.Collctions)
             .WithOne(x => x.Collection)
             .HasForeignKey(x => x.CollectionId);

     }
  }
}
