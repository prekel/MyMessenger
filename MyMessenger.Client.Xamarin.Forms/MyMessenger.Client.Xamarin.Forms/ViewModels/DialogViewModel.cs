using System;
using System.Collections.Generic;
using System.Text;
using MyMessenger.Core;

namespace MyMessenger.Client.Xamarin.Forms.ViewModels
{
	public class DialogViewModel : BaseViewModel
	{
		private IDialog Dialog { get; set; }

		public DialogViewModel()
		{
			Title = "Dialog";
		}
	}
}
