using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllWithPaging(PageParameters page);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    }
}

