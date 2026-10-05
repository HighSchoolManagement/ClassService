using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClassService.Infrastructure.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment", e => e.HasCheckConstraint("CK_Enrollment_DateRange", "[EndDate] IS NULL OR [EndDate] > [StartDate]"));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StudentId)
                .HasColumnType("int");

            // FK kep (ClassId, SchoolYearId) -> Class(Id, SchoolYearId):
            // hoc sinh khong the ghi danh vao lop cua nam hoc khac.
            builder.HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => new { e.ClassId, e.SchoolYearId })
                .HasPrincipalKey(c => new { c.Id, c.SchoolYearId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.StartDate)
                .HasColumnType("date");

            builder.Property(e => e.EndDate)
                .HasColumnType("date");

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");

            // Mot hoc sinh chi co mot ghi danh dang mo trong mot nam hoc.
            builder.HasIndex(e => new { e.StudentId, e.SchoolYearId })
                .IsUnique()
                .HasFilter("[EndDate] IS NULL");
        }
    }
}
