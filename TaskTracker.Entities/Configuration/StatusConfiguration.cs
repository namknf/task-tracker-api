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
                    StatusName = "К выполнению"
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "В процессе выполнения",
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "Закрыто",
                },
                new Status
                {
                    Id = Guid.NewGuid(),
                    StatusName = "Выполнение приостановлено",
                });
        }
    }
}
