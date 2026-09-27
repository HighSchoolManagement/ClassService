using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    // Mới thêm — Subject trước đây không có DbSet/Configuration nên EF không nhận diện được.
    // CHƯA cấu hình SchoolId vì entity Subject.cs hiện chưa có cột này (xem review trước:
    // Subject-Topic-decision.md mục 1 đã chốt SchoolId bắt buộc, nhưng chưa được áp dụng
    // vào entity). Thêm SchoolId + unique (SchoolId, Name) là việc còn lại, chưa làm ở đây.
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
