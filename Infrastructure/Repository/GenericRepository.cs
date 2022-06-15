using Infrastructure.Base;
using Infrastructure.Pagination;
using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
	{
		private readonly SqlContext _context;
		protected readonly DbSet<T> _dbSet;
		protected Func<IQueryable<T>, IIncludableQueryable<T, object>> _include;

		public GenericRepository(SqlContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public virtual void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
		{
			_include = include;
		}


		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}
		public IQueryable<T> GetAll()
		{
			return _dbSet.AsNoTracking();
		}
		public async Task<List<T>> GetAllWithPaging(PageParameters page)
		{
			var stack = _dbSet.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);

			if (_include != null)
			{
				stack = _include(stack);
			}

			return await stack.AsNoTracking().ToListAsync();
		}
		public async Task<T> GetByIdAsync(Guid id)
		{
			var stack = _dbSet.Where(a => a.Id == id);

			if (_include != null)
				stack = _include(stack);

			var result = await stack.AsNoTracking().FirstOrDefaultAsync();

			return result;
		}
		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
		{
			var stack = _dbSet.Where(predicate);

			if (_include != null)
				stack = _include(stack);

			var result = await stack.AsNoTracking().FirstOrDefaultAsync();

			return result;
		}
	}
}
