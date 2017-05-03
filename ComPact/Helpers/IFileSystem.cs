using System;
using System.Threading.Tasks;

namespace ComPact.Helpers
{
	public interface IFileSystem
	{
		void CreateDirectory(string path);
		void DeleteDirectory(string path, bool recursive);
		void Delete(string filePath);
		bool FileExists(string path);
		bool DirectoryExists(string path);
		Task WriteToFile(string fileLocation, byte[] bytes);
		//void DeleteFile(string filename);
		string DocumentsDirectory { get; }
	}
}
