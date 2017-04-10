using System;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IDialogService
	{
		void ShowMessage(string message);

		void ShowMessage(string message, string title);
		//Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons);

		//Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons);

		//Task DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton, Action afterHideCallback);

		//Task DisplayAlertAsync(string title, string message, string cancelButton);
	}
}
