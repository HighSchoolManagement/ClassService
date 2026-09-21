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
