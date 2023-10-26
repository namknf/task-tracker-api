using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Entities.Configuration;
using TaskTracker.Entities.Models;
using File = TaskTracker.Entities.Models.File;
using Task = TaskTracker.Entities.Models.Task;

namespace TaskTracker.Entities.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskAction> TaskActions { get; set; }

        public DbSet<TaskPriority> TaskPriorities { get; set; }

        public DbSet<TaskComment> TaskComments { get; set; }

        public DbSet<File> Files { get; set; }
    }
}
