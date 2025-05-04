
using Microsoft.EntityFrameworkCore;
using SendEmailExample.Models.Tables;

namespace SendEmailExample.Services
{
	public class BaseService<T> : IBaseService<T> where T : class
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public BaseService(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public virtual async Task<T> CreateAsync(T entity)
		{
			_dbSet.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity == null)
				return false;

			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T?> UpdateAsync(Guid id, T entity)
		{
			var existing = await _dbSet.FindAsync(id);
			if (existing == null)
				return null;

			if(existing is Medal existingMedal && entity is Medal updatedMedal)
			{
				existingMedal.Name = updatedMedal.Name;
				existingMedal.IconUrl = updatedMedal.IconUrl;
			}

			await _context.SaveChangesAsync();
			return entity;
		}
	}
}
