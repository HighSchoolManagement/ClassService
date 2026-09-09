using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClassService.Infrastructure.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment", e => e.HasCheckConstraint("CK_SchoolYear_DateRange", "[EndDate] > [StartDate]"));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StudentId)
                .HasColumnType("int");

            builder.HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.StartDate)
         .HasColumnType("datetime2");

            builder.Property(e => e.EndDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
