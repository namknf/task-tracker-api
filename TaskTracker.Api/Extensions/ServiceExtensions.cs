using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Data;

namespace TaskTracker.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
                services.AddDbContext<DataContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                ma => ma.MigrationsAssembly("TaskTracker.Api")));
    }
}
