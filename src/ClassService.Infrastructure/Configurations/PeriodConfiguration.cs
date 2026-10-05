using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    // Giu ten bang mac dinh "Periods" nhu hien tai (chua co ToTable) de migration khong doi ten bang.
    internal class PeriodConfiguration : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.HasIndex(p => new { p.SchoolId, p.SchoolYearId, p.Number })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
