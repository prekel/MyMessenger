using System;
using System.Collections.Generic;
using System.Text;
using MyMessenger.Core;

namespace MyMessenger.Client.XamarinForms.ViewModels
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
