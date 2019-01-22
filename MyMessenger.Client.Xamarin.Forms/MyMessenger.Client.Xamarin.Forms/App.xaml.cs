using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMessenger.Client.Xamarin.Forms.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyMessenger.Client.Xamarin.Forms
{
	public partial class App : Application
	{
		public static Services.Client Client { get; } = new Services.Client();

		public App()
		{
			InitializeComponent();


			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
