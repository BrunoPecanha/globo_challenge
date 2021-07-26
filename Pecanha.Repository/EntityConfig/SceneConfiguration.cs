using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pecanha.Domain.Entity;

namespace Pecanha.Repository.EntityConfig {
    public class SceneConfiguration : IEntityTypeConfiguration<Scene> {

        public void Configure(EntityTypeBuilder<Scene> builder) {

            builder
             .ToTable("Scene")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
            .Property(c => c.LastUpdate)
            .HasColumnName("LastUpdate");

            builder
            .Property(c => c.Name)
            .HasColumnName("Name");        

            builder
           .Property(c => c.State)
           .HasColumnName("State")
           .HasConversion<int>();
        }
    }
}
