using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Code)
                .HasMaxLength(20);

            // Cot moi them vao bang da co du lieu: mac dinh true de mon cu van active.
            // ValueGeneratedNever de EF luon gui gia tri that (neu khong IsActive = false se bi thay bang true khi insert).
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedNever();

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");

            builder.HasIndex(e => new { e.SchoolId, e.Name })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
