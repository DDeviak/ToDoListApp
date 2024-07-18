using ToDoListApp.DAL.Models;
using ToDoListApp.DAL.Persistence;
using ToDoListApp.DAL.Repositories.Interfaces;

namespace ToDoListApp.DAL.Repositories.Realizations
{
    public class TaskToDoRepository : RepositoryBase<TaskToDo>, ITaskToDoRepository
    {
        public TaskToDoRepository(ToDoListAppDbContext context) : base(context)
        { }
    }
}