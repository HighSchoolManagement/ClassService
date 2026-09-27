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
            builder.ToTable("RoomSchedule");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Booking).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.Title).HasMaxLength(200);
            builder.HasOne(x => x.Room).WithMany(r => r.RoomSchedules).HasForeignKey(x => x.RoomId);
            builder.HasOne(x => x.Period).WithMany(p => p.RoomSchedules).HasForeignKey(x => x.PeriodId);
            builder.HasOne(x => x.Class).WithMany(c => c.RoomSchedules).HasForeignKey(x => x.ClassId);

            // Phòng không bị chiếm đôi (SQL Server: cột IsActive kiểu bit, so sánh bằng 1/0)
            builder.HasIndex(x => new { x.RoomId, x.Date, x.PeriodId })
                .IsUnique()
                .HasFilter("[IsActive] = 1");

            // Lớp không ở hai phòng cùng tiết
            builder.HasIndex(x => new { x.ClassId, x.Date, x.PeriodId })
                .IsUnique()
                .HasFilter("[IsActive] = 1 AND [ClassId] IS NOT NULL");
            
            builder.ToTable(t => t.HasCheckConstraint("CK_RoomSchedule_Class",
                "([Booking] = 'Class' AND [ClassId] IS NOT NULL) OR ([Booking] <> 'Class' AND [ClassId] IS NULL)"));

            builder.Property(e => e.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
