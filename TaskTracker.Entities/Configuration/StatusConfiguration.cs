using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Entities.Models;

namespace TaskTracker.Entities.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "To do"
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "In Progress",
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "Closed",
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "Frozen",
                });
        }
    }
}
