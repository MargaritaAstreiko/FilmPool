using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class UserConfiguration: IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("Users");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.FirstName).HasColumnName("FirsName").HasMaxLength(50);
      builder.Property(t => t.LastName).HasColumnName("LastName").HasMaxLength(50);
      builder.Property(t => t.UserName).HasColumnName("UserName").HasMaxLength(50);
      builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(60);
      builder.Property(t => t.Password).HasColumnName("Password");
      builder.HasOne(x => x.Role)
        .WithMany()
        .HasForeignKey(x => x.UserRole)
        .IsRequired(true);
      builder.HasMany(t => t.Ratings)
        .WithOne(x => x.User)
        .HasForeignKey(x => x.UserId);
      builder.HasMany(t => t.Comments)
        .WithOne(x => x.User)
        .HasForeignKey(x => x.UserId);
      builder.HasMany(t => t.Collections)
        .WithOne(x => x.User)
        .HasForeignKey(x => x.UserId);


    }

  }
}
