using System.Linq.Expressions;

namespace Core.Repositorys.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(int id);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
