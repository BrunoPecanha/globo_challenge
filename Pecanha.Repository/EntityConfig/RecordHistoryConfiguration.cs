using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pecanha.Domain.Entity;

namespace Pecanha.Repository.EntityConfig {
    public class RecordHistoryConfiguration : IEntityTypeConfiguration<RecordHistory> {

        public void Configure(EntityTypeBuilder<RecordHistory> builder) {

            builder
             .ToTable("RecordHistory")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
            .Property(c => c.LastUpdate)
            .HasColumnName("LastUpdate");

            builder
           .Property(c => c.PreviousState)
           .HasColumnName("PreviousState")
           .HasConversion<int>();

            builder
           .Property(c => c.ActualState)
           .HasColumnName("ActualState")
           .HasConversion<int>();

            //Relacionamento com a cena
            builder
                .HasOne(x => x.Scene)
                .WithMany(x => x.RecordHistory)
                .HasForeignKey(x => x.SceneId)
                .IsRequired();
             
        }
    }
}
