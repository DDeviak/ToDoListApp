using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoListApp.DAL.Persistence;

namespace ToDoListApp.WebAPI.Helpers;
internal class DesignTimeToDoListAppDbContextFactory : IDesignTimeDbContextFactory<ToDoListAppDbContext>
{
    ToDoListAppDbContext IDesignTimeDbContextFactory<ToDoListAppDbContext>.CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ToDoListAppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(connectionString);

        return new ToDoListAppDbContext(builder.Options);
    }
}