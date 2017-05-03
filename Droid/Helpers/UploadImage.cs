using System;
using System.Net;
using ComPact.Helpers;

namespace ComPact.Droid
{
	public class UploadImage : IUploadImage
	{
		WebClient client;

		WebClient GetClient()
		{
			if (client == null)
			{
				client = new WebClient();
			}
			return client;
		}

		void IUploadImage.UploadImage()
		{
			client.UploadFileAsync(null, null);
		}
	}
}
