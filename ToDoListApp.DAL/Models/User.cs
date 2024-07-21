using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<Tasklist> Tasklists { get; set; } = null!;
    }
}