using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FilmPool.Data.Schemas
{

    public class FilmsInCollectionsConfiguration : IEntityTypeConfiguration<FilmsInCollections>
    {
        public void Configure(EntityTypeBuilder<FilmsInCollections> builder)
        {
            builder.ToTable("FilmsInCollections");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CollectionId).HasConversion<int>();
            builder.Property(t => t.FilmId).HasConversion<int>();
            builder.Property(t => t.AddedDate).HasConversion<DateTime>();
        }

    }
}

