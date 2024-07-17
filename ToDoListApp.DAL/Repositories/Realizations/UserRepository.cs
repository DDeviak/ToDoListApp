using ToDoListApp.DAL.Models;
using ToDoListApp.DAL.Repositories.Interfaces;
using ToDoListApp.DAL.Persistence;

namespace ToDoListApp.DAL.Repositories.Realizations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ToDoListAppDbContext context) : base(context)
        { }
    }
}