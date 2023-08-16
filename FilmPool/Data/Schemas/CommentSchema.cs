using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
  {
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
      builder.ToTable("Comments");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.FilmId).HasConversion<int>();
      builder.Property(t => t.UserId).HasConversion<int>();
      builder.Property(t => t.Comment).HasColumnName("Comment").HasMaxLength(4000);
      builder.Property(t => t.CreatedDate).HasConversion<DateTime>();
    }
  }
}
