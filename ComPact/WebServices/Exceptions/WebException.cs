using System;
namespace ComPact.Exceptions
{
	public class WebException : Exception
	{
		public WebException() { }

		public WebException(string message, Exception ex)
			: base(message, ex)
		{ }
	}
}
