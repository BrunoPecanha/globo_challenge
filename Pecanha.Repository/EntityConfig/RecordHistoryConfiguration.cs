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
            .Property(c => c.OperationHour)
            .HasColumnName("OperationHour")
            .IsRequired();


            builder
             .Property(c => c.OperationHour)
             .HasColumnName("OperationHour")
             .IsRequired();


            builder
            .Property(c => c.OperationHour)
            .HasColumnName("OperationHour");

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
                .WithMany(x => x.RecordHistories)
                .IsRequired();

        }
    }
}
