using System;
using System.IO;
using System.Threading.Tasks;
using ComPact.Helpers;

namespace ComPact.Droid.Helpers
{

	public class FileSystem : IFileSystem
	{
		public void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}
		public void DeleteDirectory(string path, bool recursive)
		{
			if (DirectoryExists(path))
			{
				Directory.Delete(path, recursive);
			}
		}
		public void Delete(string filePath)
		{
			if (FileExists(filePath))
			{
				File.Delete(filePath);
			}
		}
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}
		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}
		public string DocumentsDirectory
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); }
		}
		public string TempDirectory
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); }
		}
		public async Task WriteToFile(string fileLocation, byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				var directory = Path.GetDirectoryName(fileLocation);
				if (!DirectoryExists(directory))
				{
					CreateDirectory(directory);
				}
				if (FileExists(fileLocation))
				{
					Delete(fileLocation);
				}
				using (var sourceStream = new FileStream(fileLocation, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 12288))
				{
					await sourceStream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
					sourceStream.Close();
				}
			}
		}
	}
}
