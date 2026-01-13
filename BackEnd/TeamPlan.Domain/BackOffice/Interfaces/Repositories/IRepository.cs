using System.Linq.Expressions;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}