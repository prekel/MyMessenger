using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyMessenger.Client.Xamarin.Forms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMessenger.Client.Xamarin.Forms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		private LoginViewModel viewModel;

		public LoginPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new LoginViewModel();
		}

		private async void Connect_OnClicked(object sender, EventArgs e)
		{
			//viewModel = new LoginViewModel();
			App.Client.DialogId = Int32.Parse(DialogId.Text);
			var ip = new IPEndPoint(IPAddress.Parse(IpAddress.Text), Int32.Parse(Port.Text));
			var t = await App.Client.Connect(ip, Login.Text, Password.Text);

			//await DisplayAlert("RawResponse", t, "Ok");
		}
	}
}