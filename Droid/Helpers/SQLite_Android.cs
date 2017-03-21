using System;
using System.IO;
using ComPact.Helpers;
using SQLite.Net;
using SQLite.Net.Async;

namespace ComPact.Droid.Helpers
{
	public class SQLite_Android: ISQLite
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var sqliteFilename = "ComPact.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var param = new SQLiteConnectionString(path, false);
			var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
			return connection;
		}
	}
}
