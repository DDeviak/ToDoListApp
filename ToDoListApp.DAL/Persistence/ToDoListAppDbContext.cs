using Microsoft.EntityFrameworkCore;
using ToDoListApp.DAL.Models;

namespace ToDoListApp.DAL.Persistence
{
    public class ToDoListAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tasklist> Tasklists { get; set; }
        public DbSet<TaskToDo> Tasks { get; set; }

        public ToDoListAppDbContext(DbContextOptions<ToDoListAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasklists)
                .WithOne(tl => tl.User)
                .HasForeignKey(tl => tl.UserId);

            modelBuilder.Entity<Tasklist>()
                .HasMany(tl => tl.Tasks)
                .WithOne(t => t.Tasklist)
                .HasForeignKey(t => t.TasklistId);

            modelBuilder.Entity<TaskToDo>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}