using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMessenger.Client.XamarinForms.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyMessenger.Client.XamarinForms
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
