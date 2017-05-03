using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact.Helpers;

namespace ComPact.Services
{
	public class FileDownloadService : IFileDownloadService
	{
		readonly IPaymentDataService _paymentDataService;
		readonly IFileSystem _fileSystem;

		public FileDownloadService(IPaymentDataService paymentDataService, IFileSystem fileSystem)
		{
			_paymentDataService = paymentDataService;
			_fileSystem = fileSystem;
		}

		public async Task<string> DownloadImage(string url)
		{
			//try
			//{
			//	var extension = Path.GetExtension(new Uri(url).AbsolutePath);
			//	var fileName = GetUrlName(url) + extension;
			//	var path = Path.Combine(_fileSystem.DocumentsDirectory, "Images", fileName);
			//	if (_fileSystem.FileExists(path))
			//	{
			//		return path;
			//	}

				//var bytes = await _paymentDataService.DownloadFileFromUrl(url);

			//	if (bytes == null)
			//	{
			//		return null;
			//	}

			//	await _fileSystem.WriteToFile(path, bytes);

			//	return path;
			//}
			//catch (Exception)
			//{
				return null;
			//}
		}

		string GetUrlName(string url)
		{
			//this is to make sure that every image has a unique name(even with difference in maxHeight cover/archive)
			return Regex.Replace(url, "[^a-zA-Z0-9]", string.Empty);
		}
	}
}
