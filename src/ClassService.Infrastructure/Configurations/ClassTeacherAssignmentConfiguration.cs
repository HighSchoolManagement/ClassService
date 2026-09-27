using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    public class ClassTeacherAssignmentConfiguration :IEntityTypeConfiguration<ClassTeacherAssignment>
    {
        public void Configure(EntityTypeBuilder<ClassTeacherAssignment> builder)
        {
            builder.ToTable("ClassTeacherAssignment");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.TeacherId)
            .IsRequired();
            // TeacherId là tham chiếu cross-service (giống Class.SchoolId) — cố tình KHÔNG
            // có FK thật trong DB. Nếu Teacher là entity local trong ClassService thì cần
            // sửa lại thành HasOne<Teacher>().WithMany().HasForeignKey(c => c.TeacherId) —
            // vẫn là câu hỏi mở ở mục 4 doc Subject-Topic-decision.md, chưa có câu trả lời.

            builder.Property(c => c.ClassId)
                .IsRequired();

            builder.HasOne<Class>().WithMany().HasForeignKey(c => c.ClassId);

            builder.Property(s => s.IsHomeRoomTeacher)
               .HasColumnType("BIT")
               .HasDefaultValue(false)
               .IsRequired();

            builder.Property(s => s.StartDate)
       .HasColumnType("datetime2");

            builder.Property(s => s.EndDate)
                .HasColumnType("datetime2");

            builder.Property(s => s.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(s => s.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
