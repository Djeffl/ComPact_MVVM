using System;
using System.Threading.Tasks;

namespace ComPact.Services
{
	public interface IFileDownloadService
	{
		Task<string> DownloadImage(string url);
	}
}
