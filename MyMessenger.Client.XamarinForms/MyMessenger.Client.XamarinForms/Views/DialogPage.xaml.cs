using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMessenger.Client.XamarinForms.ViewModels;

namespace MyMessenger.Client.XamarinForms.Views
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

		private async Task Send()
		{
			await App.Client.SendMessage(App.DialogId, SendMessageEntry.Text);
		}

		private async void SendButton_OnClicked(object sender, EventArgs e)
		{
			await Send();
		}

		private async void GetButton_OnClicked(object sender, EventArgs e)
		{
			GetButton.IsEnabled = false;
			while (true)
			{
				var m = await App.Client.GetMessageLongPool(App.DialogId, TimeSpan.FromSeconds(25));
				if (m == null) continue;
				var author = await App.Client.GetAccountById(m.AuthorId);
				DialogMessages.Text = DialogMessages.Text + $"[{m.SendDateTime.LocalDateTime}][{author.Nickname}]{m.Text}" + '\n';
				//await DisplayAlert(m.AuthorId.ToString(), m.Text, "Ok");
			}
		}

		private async void SendMessageEntry_Completed(object sender, EventArgs e)
		{
			await Send();
		}
	}
}