using System;
using System.IO;
using ComPact.Helpers;
using SQLite;

namespace Onboarding.Droid.Helpers
{
	public class Database : IDatabase
	{
		private string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
		private SQLiteAsyncConnection _asyncConnection;
		private SQLiteConnection _connection;


		public SQLiteAsyncConnection Connection
		{
			get
			{
				if (_asyncConnection == null)
				{
					_asyncConnection = new SQLiteAsyncConnection(_dbPath);
				}

				return _asyncConnection;
			}
		}

		public void CreateTable<TModel>()
		{
			if (_connection == null)
			{
				_connection = new SQLiteConnection(_dbPath);
			}

			_connection.CreateTable<TModel>();
		}
	}
}