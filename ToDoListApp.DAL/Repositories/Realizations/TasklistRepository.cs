using ToDoListApp.DAL.Models;
using ToDoListApp.DAL.Persistence;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.DAL.Repositories.Realizations
{
    class TasklistRepository : RepositoryBase<Tasklist>, ITasklistRepository
    {
        public TasklistRepository(ToDoListAppDbContext context) : base(context)
        { }
    }
}