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
            builder.ToTable("ClassRoomSchedule", e => e.HasCheckConstraint("CK_ClassRoomSchedule_DateRange", "[EndTime] > [StartTime]"));
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Booking).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.Title).HasMaxLength(200);
            builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.RoomId);
            builder.HasOne(x => x.Period).WithMany().HasForeignKey(x => x.PeriodId);
            builder.HasOne(x => x.Class).WithMany().HasForeignKey(x => x.ClassId);
            
            // Phòng không bị chiếm đôi
            builder.HasIndex(x => new { x.RoomId, x.Date, x.PeriodId })
                .IsUnique()
                .HasFilter("\"IsActive\" = true");          // cú pháp filter phụ thuộc DB

            // Lớp không ở hai phòng cùng tiết
            builder.HasIndex(x => new { x.ClassId, x.Date, x.PeriodId })
                .IsUnique()
                .HasFilter("\"IsActive\" = true AND \"ClassId\" IS NOT NULL");

            // Type = Class <=> có ClassId
            builder.ToTable(t => t.HasCheckConstraint("CK_RoomSchedule_Class",
                "(\"Type\" = 'Class') = (\"ClassId\" IS NOT NULL)"));

            builder.Property(e => e.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
