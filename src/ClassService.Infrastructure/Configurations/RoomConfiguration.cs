using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
           .HasMaxLength(100)
           .IsRequired();

            builder.Property(r => r.Capacity)
         .HasColumnType("int");


            builder.Property(c => c.CreatedDate)
          .HasColumnType("datetime2");

            builder.Property(c => c.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");

            // Ten phong duy nhat trong mot truong, chi tinh phong active.
            builder.HasIndex(r => new { r.SchoolId, r.Name })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
