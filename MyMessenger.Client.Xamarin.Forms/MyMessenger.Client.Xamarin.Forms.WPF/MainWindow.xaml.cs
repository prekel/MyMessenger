using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using MyMessenger.Client.XamarinForms;

namespace MyMessenger.Client.XamarinForms.WPF
{
	public partial class MainWindow : FormsApplicationPageWorkaround
	{
		public MainWindow()
		{
			InitializeComponent();

			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new XamarinForms.App());
		}
	}
}