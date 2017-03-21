using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact;
using ComPact.Helpers;
using SQLite;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace ComPact
{
	public class UserRepository: BaseRepository<User, string>, IUserRepository
	{
		private SQLiteAsyncConnection database;
		//ISQLitePlatform platform; 
		public UserRepository(ISQLite connection)
			:base(connection)
		{
		}

		public async Task SaveUser(User user)
		{
			await Insert(user);
		}
		public IEnumerable<User> GetUser()
		{
			return All();
		}

		//private string createDatabase()
		//{
		//	try
		//	{
		//		var connection = new SQLiteAsyncConnection(_path);
		//		{
		//			connection.CreateTableAsync<User>();
		//			return "Database created";
		//		}
		//	}

		//	catch (SQLiteException ex)
		//	{
		//		return ex.Message;
		//	}
		//}

		//private async Task<string> insertUpdateData(User data)
		//{
		//	try
		//	{
		//		int response = await GetConnection().InsertAsync(data);
		//		if (response != 0)
		//		{
		//			await db.UpdateAsync(data);
		//			return "Single data file inserted or updated";
		//		}
		//		return "File not saved";
		//	}
		//	catch (SQLiteException ex)
		//	{
		//		return ex.Message;
		//	}
		//}
		//private async Task<int> findNumberRecords()
		//{
		//	try
		//	{
		//		var db = new SQLiteAsyncConnection(_path);
		//		// this counts all records in the database, it can be slow depending on the size of the database
		//		var count = await db.ExecuteScalarAsync<int>("SELECT * FROM User");

		//		// for a non-parameterless query
		//		// var count = db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

		//		return count;
		//	}
		//	catch (SQLiteException ex)
		//	{
		//		return -1;
		//	}
		//}
		//private int findNumberRecords(string path)
		//{
		//	try
		//	{
		//		var db = new SQLiteAsyncConnection(path);
		//		// this counts all records in the database, it can be slow depending on the size of the database
		//		var count = db.ExecuteScalarAsync("SELECT * FROM User", null);  

		//		// for a non-parameterless query
		//		// var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

		//		return count;
		//	}
		//	catch (SQLiteException ex)
		//	{
		//		return -1;
		//	}
		//}

		//public SQLiteConnection GetConnection()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
