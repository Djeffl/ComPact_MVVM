using System;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IDialogService
	{
		void ShowMessage(string message);

		Task<bool> ShowMessage(string message, string title);

		Task<bool> ShowMessage(string message, string title, string negativeButtonText, string positiveButtonText);
	}
}
