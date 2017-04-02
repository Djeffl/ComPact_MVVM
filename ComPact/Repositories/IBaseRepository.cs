using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComPact.Repositories
{
	public interface IBaseRepository<TEntity, TKey>
	{
		Task<IEnumerable<TEntity>> All();

		IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter);

		Task<int> Count();

		Task<TEntity> Get(TKey key);

		Task InsertOrReplace(TEntity entity);

		Task InsertOrReplace(IEnumerable<TEntity> entities);

		Task<TEntity> Insert(TEntity entity);

		Task Insert(IEnumerable<TEntity> entities);

		Task Update(TEntity entity);

		Task Update(IEnumerable<TEntity> entities);

		Task Delete(TEntity entity);

		Task Delete(IEnumerable<TEntity> entities);

		Task Delete(TKey key);

		Task Truncate();

		Task DropTable();
	}
}
