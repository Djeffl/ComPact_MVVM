//using System;
//using System.IO;
//using ComPact.Helpers;
//using SQLite.Net;
//using SQLite.Net.Async;

//namespace ComPact.Droid.Helpers
//{
//	public class SQLiteA : ISQLite
//	{
//		SQLiteConnectionWithLock _conn;
//		string _folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
//		const string sqliteFilename = "comPactDatabase.db3";
//		public SQLiteA()
//		{
//		}
//		public bool CreateDatabase()
//		{
//			try
//			{
//				using (var connection = new SQLiteConnection(System.IO.Path.Combine((_folderPath, sqliteFileName)))
//				{
//					return true;
//			catch (SQLiteException ex)
//			{
//				Console.WriteLine(ex.Message);
//			}
//		}

//		}

//		private static string GetDatabasePath()
//		{
//			 "mydatabase.db3";

//			// Documents folder
//			var path = Path.Combine(_folderPath, sqliteFilename);

//			return path;
//		}

//		public void CloseConnection()
//		{
//			throw new NotImplementedException();
//		}

//		public void DeleteDatabase()
//		{
//			throw new NotImplementedException();
//		}

//		public SQLiteAsyncConnection GetAsyncConnection()
//		{
//			throw new NotImplementedException();
//		}

//		public SQLiteConnection GetConnection()
//		{
//			var dbPath = GetDatabasePath();

//			// Return the synchronous database connection 
//			return new SQLiteConnection(new SQLitePlatformAndroid(), dbPath);
//		}
//	}
//}
