using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    // TeacherId la tham chieu service ngoai nen khong co FK that.
    internal class ClassSubjectTeacherConfiguration : IEntityTypeConfiguration<ClassSubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectTeacher> builder)
        {
            builder.ToTable("ClassSubjectTeacher");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TeacherId)
                .IsRequired();

            builder.HasOne(e => e.ClassSubject)
                .WithMany(cs => cs.ClassSubjectTeachers)
                .HasForeignKey(e => e.ClassSubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");

            // Tai mot thoi diem chi co mot giao vien dang day mot ClassSubject.
            builder.HasIndex(e => e.ClassSubjectId)
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
