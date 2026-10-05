using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    internal class ClassSubjectConfiguration : IEntityTypeConfiguration<ClassSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSubject> builder)
        {
            builder.ToTable("ClassSubject");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(e => e.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreatedDate).HasColumnType("datetime2");
            builder.Property(e => e.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");

            // Mot lop chi hoc mot mon mot lan (tinh ban ghi active).
            builder.HasIndex(e => new { e.ClassId, e.SubjectId })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
