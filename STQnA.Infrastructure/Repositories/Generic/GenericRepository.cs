using Microsoft.EntityFrameworkCore;
using STQnA.Core.Interfaces.Generic;

namespace STQnA.Infrastructure.Repositories.Generic;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly STQnAContext _dbContext;
    protected GenericRepository(STQnAContext context)
    {
        _dbContext = context;
    }

    public async Task<T> GetById(int id)
    {
        var res = await _dbContext.Set<T>().FindAsync(id);
        if (res != null)
        {
            return res;
        }
        return Activator.CreateInstance<T>();
    }
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
    public async Task Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }
    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }
    public int Save()
    {
        return _dbContext.SaveChanges();
    }

}
