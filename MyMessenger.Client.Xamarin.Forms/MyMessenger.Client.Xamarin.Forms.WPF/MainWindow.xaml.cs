using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using MyMessenger.Client.Xamarin.Forms;

namespace MyMessenger.Client.Xamarin.Forms.WPF
{
	public partial class MainWindow : FormsApplicationPageWorkaround
	{
		public MainWindow()
		{
			InitializeComponent();

			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new Forms.App());
		}
	}
}