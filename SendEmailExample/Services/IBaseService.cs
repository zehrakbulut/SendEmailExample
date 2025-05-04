namespace SendEmailExample.Services
{
	public interface IBaseService<T> where T : class
	{
		Task<T> CreateAsync(T entity);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(Guid id);
		Task<T?> UpdateAsync(Guid id, T entity);
		Task<bool> DeleteAsync(Guid id);
	}
}
