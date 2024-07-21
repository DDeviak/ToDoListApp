using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.DAL.Models;

namespace ToDoListApp.DAL.Persistence
{
    public class ToDoListAppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ToDoListAppDbContext(DbContextOptions<ToDoListAppDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Tasklist> Tasklists { get; set; }

        public DbSet<TaskToDo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Tasklists)
                .WithOne(tl => tl.User)
                .HasForeignKey(tl => tl.UserId);

            builder.Entity<Tasklist>()
                .HasMany(tl => tl.Tasks)
                .WithOne(t => t.Tasklist)
                .HasForeignKey(t => t.TasklistId);

            builder.Entity<TaskToDo>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}