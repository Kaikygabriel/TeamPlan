using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    } 

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}