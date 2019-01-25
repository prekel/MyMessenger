using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using MyMessenger.Client.XamarinForms;

namespace MyMessenger.Client.XamarinForms.WPF
{
	public partial class MainWindow : FormsApplicationPage
	{
		public MainWindow()
		{
			InitializeComponent();

			Forms.Init();
			LoadApplication(new XamarinForms.App());
		}
	}
}