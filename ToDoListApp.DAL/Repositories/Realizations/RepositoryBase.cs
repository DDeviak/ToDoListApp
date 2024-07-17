using ToDoListApp.DAL.Models;
using ToDoListApp.DAL.Repositories.Interfaces;
using ToDoListApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.DAL.Repositories.Realizations
{
    public abstract class RepositoryBase<T> : IRepository<T>
    where T : class
    {
        private readonly ToDoListAppDbContext _context;

        protected RepositoryBase(ToDoListAppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}