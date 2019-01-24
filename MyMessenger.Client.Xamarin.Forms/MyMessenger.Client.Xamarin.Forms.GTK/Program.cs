using System;
using MyMessenger.Client.Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace MyMessenger.Client.Xamarin.Forms.GTK
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Gtk.Application.Init();
			global::Xamarin.Forms.Forms.Init();

			var app = new App();
			var window = new FormsWindow();
			window.LoadApplication(app);
			window.SetApplicationTitle("MyMessenger.Client.Xamarin.Forms.GTK");
			window.Show();

			Gtk.Application.Run();
		}
	}
}