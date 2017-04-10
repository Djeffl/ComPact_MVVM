using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ComPact.Helpers;
using SQLite;
using ComPact.Repositories;

namespace ComPact
{
	public abstract class BaseRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> where TEntity : class, new()
	{
		readonly SQLiteAsyncConnection _connection;

		protected BaseRepository(IDatabase database)
		{
			database.CreateTable<TEntity>();
			_connection = database.Connection;
		}

		public virtual async Task<IEnumerable<TEntity>> All()
		{
			return await _connection.Table<TEntity>().ToListAsync();
		}
		public virtual Task DropTable()
		{
			return _connection.DropTableAsync<TEntity>();
		}

		public virtual async Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> filter)
		{
			return (await _connection.Table<TEntity>().ToListAsync()).AsQueryable().Where((filter ?? (e => true)));
		}

		public virtual async Task<int> Count()
		{
			return await _connection.Table<TEntity>().CountAsync();
		}

		public virtual async Task<TEntity> Get(TKey key)
		{
			return await _connection.GetAsync<TEntity>(key);
		}

		public virtual async Task InsertOrReplace(TEntity entity)
		{
			await _connection.InsertOrReplaceAsync(entity);
		}

		public virtual async Task InsertOrReplace(IEnumerable<TEntity> entities)
		{
			await _connection.InsertOrReplaceAsync(entities);
		}

		public virtual async Task<TEntity> Insert(TEntity entity)
		{
			await _connection.InsertAsync(entity);
			return (await All())?.LastOrDefault();
		}

		public virtual async Task Insert(IEnumerable<TEntity> entities)
		{
			await _connection.InsertAllAsync(entities);
		}

		public virtual async Task Update(TEntity entity)
		{
			await _connection.UpdateAsync(entity);
		}

		public virtual async Task Update(IEnumerable<TEntity> entities)
		{
			await _connection.UpdateAllAsync(entities);
		}

		public virtual async Task Delete(TEntity entity)
		{
			await _connection.DeleteAsync(entity);
		}

		public virtual async Task Delete(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				await Delete(entity);
			}
		}

		public virtual async Task Delete(TKey key)
		{
			await _connection.DeleteAsync(await Get(key));
		}

		public virtual async Task Truncate()
		{
			await _connection.DropTableAsync<TEntity>();
			await _connection.CreateTableAsync<TEntity>();
		}
	}
}
