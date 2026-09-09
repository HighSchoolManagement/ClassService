using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StudentId)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.EndDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");

            builder.HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SchoolYear)
                .WithMany()
                .HasForeignKey(e => e.SchoolYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.StudentId);
            builder.HasIndex(e => new { e.StudentId, e.ClassId, e.SchoolYearId })
                .IsUnique();
        }
    }
}
