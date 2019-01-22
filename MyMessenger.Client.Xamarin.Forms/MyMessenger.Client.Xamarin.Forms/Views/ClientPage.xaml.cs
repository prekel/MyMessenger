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
	public partial class ClientPage : ContentPage
	{
		public ClientPage()
		{
			InitializeComponent();
		}

		private async void Connect_OnClicked(object sender, EventArgs e)
		{
			var vm = new ClientViewModel();
			var ip = new IPEndPoint(IPAddress.Parse("51.158.73.185"), 20522);
			var t = await vm.Client.Receive(ip, Login.Text, Password.Text);

			await DisplayAlert("RawResponse", t, "Ok");
			//var item = args.SelectedItem as Item;
			//if (item == null)
			//	return;

			//await Navigation.PushAsync(new ItemDetailPage(new ClientViewModel()));

			// Manually deselect item.
			//ItemsListView.SelectedItem = null;
		}
	}
}