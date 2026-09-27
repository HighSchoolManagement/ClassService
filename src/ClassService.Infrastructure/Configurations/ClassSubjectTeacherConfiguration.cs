using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    // Mới thêm — ClassSubjectTeacher trước đây không có DbSet/Configuration nên EF không
    // nhận diện được. TeacherId để trần (không FK) vì theo pattern School/Class hiện tại,
    // Teacher nhiều khả năng là cross-service — cần xác nhận lại (mục 4, Subject-Topic-decision.md).
    internal class ClassSubjectTeacherConfiguration : IEntityTypeConfiguration<ClassSubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectTeacher> builder)
        {
            builder.ToTable("ClassSubjectTeacher");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TeacherId)
                .IsRequired();

            builder.HasOne<Class>().WithMany().HasForeignKey(e => e.ClassId);
            builder.HasOne<Subject>().WithMany().HasForeignKey(e => e.SubjectId);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");

            // CHƯA làm: cột IsActive (entity chưa có) + unique filtered index
            // (ClassId, SubjectId) WHERE IsActive = 1 — đây là lý do bạn tách bảng này
            // riêng ra ngay từ đầu (giữ lịch sử đổi giáo viên), nhưng thiếu IsActive thì
            // mục đích đó chưa đạt được. Xem lại review trước.
        }
    }
}
