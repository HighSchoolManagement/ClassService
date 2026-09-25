using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    internal class RoomScheduleConfiguration : IEntityTypeConfiguration<RoomSchedule>
    {
        public void Configure(EntityTypeBuilder<RoomSchedule> builder)
        {
            builder.ToTable("RoomSchedule", e => e.HasCheckConstraint("CK_RoomSchedule_DateRange", "[EndTime] > [StartTime]"));
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ClassSubject)
                .WithMany(c => c.RoomSchedules)
                .HasForeignKey(e => e.ClassSubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Room)
                .WithMany(r => r.RoomSchedules)
                .HasForeignKey(e => e.RoomId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Diagram có field PeriodId nhưng bản Configuration gốc không cấu hình quan hệ này —
            // thêm FK sang Period, đây là quan hệ bắt buộc bị thiếu hoàn toàn trước đó.
            builder.HasOne(e => e.Period)
                .WithMany(p => p.RoomSchedules)
                .HasForeignKey(e => e.PeriodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.StartTime)
                .HasColumnType("datetime2");

            builder.Property(e => e.EndTime)
                .HasColumnType("datetime2");

            builder.Property(e => e.Title)
                .HasMaxLength(200);

            // BookingType: chưa rõ CLR type (enum hay string) nên chưa cấu hình — xem tin nhắn kèm theo.
            // Nếu là enum, thường sẽ là:
            // builder.Property(e => e.BookingType).HasConversion<string>().HasMaxLength(50);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");

            // Hỗ trợ truy vấn kiểm tra trùng giờ / xem lịch theo phòng
            builder.HasIndex(e => new { e.RoomId, e.StartTime });
        }
    }
}
