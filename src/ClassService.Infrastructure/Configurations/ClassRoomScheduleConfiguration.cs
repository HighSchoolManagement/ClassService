using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    internal class ClassRoomScheduleConfiguration : IEntityTypeConfiguration<ClassRoomSchedule>
    {
        public void Configure(EntityTypeBuilder<ClassRoomSchedule> builder)
        {
            builder.ToTable("ClassRoomSchedule", e => e.HasCheckConstraint("CK_ClassRoomSchedule_DateRange", "[EndTime] > [StartTime]"));
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Class)
               .WithMany(e => e.ClassRoomSchedules)
               .HasForeignKey(e => e.ClassId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Room)
              .WithMany(e => e.ClassRoomSchedules)
              .HasForeignKey(e => e.ClassId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);


            builder.Property(e => e.StartTime)
         .HasColumnType("datetime2");

            builder.Property(e => e.EndTime)
                .HasColumnType("datetime2");

            builder.Property(e => e.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
