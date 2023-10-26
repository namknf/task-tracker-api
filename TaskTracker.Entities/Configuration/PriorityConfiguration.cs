using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Entities.Models;

namespace TaskTracker.Entities.Configuration
{
    public class PriorityConfiguration : IEntityTypeConfiguration<TaskPriority>
    {
        public void Configure(EntityTypeBuilder<TaskPriority> builder)
        {
            builder.HasData(
                new TaskPriority
                {
                    Id = Guid.NewGuid(),
                    PriorityName = "Low"
                },
                new TaskPriority
                {
                    Id = Guid.NewGuid(),
                    PriorityName = "Medium",
                },
                new TaskPriority
                {
                    Id = Guid.NewGuid(),
                    PriorityName = "High",
                });
        }
    }
}
