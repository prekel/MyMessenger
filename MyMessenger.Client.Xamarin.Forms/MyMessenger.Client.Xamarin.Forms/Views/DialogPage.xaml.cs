﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMessenger.Client.Xamarin.Forms.ViewModels;

namespace MyMessenger.Client.Xamarin.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPage : ContentPage
	{
		private DialogViewModel viewModel;

		public DialogPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new DialogViewModel();
		}

		private async void SendButton_OnClicked(object sender, EventArgs e)
		{
			await App.Client.SendMessage(App.Client.DialogId, SendMessageEntry.Text);
		}

		private async void GetButton_OnClicked(object sender, EventArgs e)
		{
			var m = await App.Client.GetMessageLongPool(App.Client.DialogId, TimeSpan.FromSeconds(25));
			await DisplayAlert(m.AuthorId.ToString(), m.Text, "Ok");
		}
	}
}