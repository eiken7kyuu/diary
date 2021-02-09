using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProtectedDiary.Core.Specifications;

namespace ProtectedDiary.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}