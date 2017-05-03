using System;
using SQLite;

namespace ComPact.Helpers
{
	public interface IDatabase
	{
		SQLiteAsyncConnection Connection { get; }

		void CreateTable<TModel>();
	}
}
