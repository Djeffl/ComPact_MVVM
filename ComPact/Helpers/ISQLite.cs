using System;
using SQLite.Net;
using SQLite.Net.Async;

namespace ComPact.Helpers
{
	public interface ISQLite
	{
		SQLiteAsyncConnection GetConnection();
	}
}
