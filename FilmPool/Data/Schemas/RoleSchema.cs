using FilmPool.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace FilmPool.Data.Schemas
{
  public class RoleConfiguration : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.ToTable("Roles");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).HasConversion<int>().IsRequired();
      builder.Property(t => t.RoleName).HasConversion<string>().IsRequired();
      builder.HasData(
                Enum.GetValues(typeof(RoleEnum))
                    .Cast<RoleEnum>()
                    .Select(e => new Role()
                    {
                      Id = e,
                      RoleName = e.ToString()
                    })
                    );
    }
  }
}
