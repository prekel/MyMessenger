using System;
using System.Net;
using System.Threading.Tasks;
using Terminal.Gui;

using MyMessenger.Client;

namespace MyMessenger.Client.TerminalGui
{
	public class Program
	{
		private static Label loginLabel = new Label(3, 2, "Login: ");
		private static TextField loginTextField = new TextField(14, 2, 40, "User1");

		private static Label passwordLabel = new Label(3, 4, "Password: ");
		private static TextField passwordField = new TextField(14, 4, 40, "123456") { Secret = true, };

		private static Label ipLabel = new Label(3, 6, "Ip: ");
		private static TextField ipField = new TextField(14, 6, 40, "51.158.73.185");

		private static Label portLabel = new Label(3, 8, "Port: ");
		private static TextField portTextField = new TextField(14, 8, 40, "20522");

		private static Label dialogidLabel = new Label(3, 10, "DialogId: ");
		private static TextField dialogidTextField = new TextField(14, 10, 40, "1");

		private static Button connectButton = new Button(3, 14, "Connect") { Clicked = OnConnectClicked };

		private static Label sendLabel = new Label(3, 17, "Send: ");
		private static TextField sendTextField = new TextField(14, 17, 40, "");

		private static Button sendButton = new Button(3, 19, "Send") { Clicked = OnSendClicked };

		private static Button getButton = new Button(3, 21, "Get") { Clicked = OnGetClicked };

		private static Label messageLabel = new Label(3, 23, "Message: ");

		private static Toplevel Top;

		private static BaseClient Client = new BaseClient();

		public static void Main()
		{
			Application.Init();
			Top = Application.Top;

			// Creates the top-level window to show
			var win = new Window(new Rect(0, 1, Top.Frame.Width, Top.Frame.Height - 1), "MyMessenger.Client.Terminal.Gui");
			Top.Add(win);

			// Creates a menubar, the item "New" has a help menu.
			var menu = new MenuBar(new MenuBarItem[] {
				new MenuBarItem ("_File", new MenuItem [] {
					new MenuItem ("_New", "Creates new file", NewFile),
					new MenuItem ("_Close", "", Close),
					new MenuItem ("_Quit", "", Quit)
				}),
				new MenuBarItem ("_Edit", new MenuItem [] {
					new MenuItem ("_Copy", "", null),
					new MenuItem ("C_ut", "", null),
					new MenuItem ("_Paste", "", null)
				})
			});
			Top.Add(menu);

			// Add some controls
			win.Add(
				loginLabel,
				loginTextField,
				passwordLabel,
				passwordField,
				ipLabel,
				ipField,
				portLabel,
				portTextField,
				dialogidLabel,
				dialogidTextField,
				connectButton,
				sendLabel,
				sendTextField,
				sendButton,
				getButton,
				messageLabel
			);

			Application.Run();
		}

		private static async void OnConnectClicked()
		{
			await Client.Connect(new IPEndPoint(IPAddress.Parse(ipField.Text.ToString()), Int32.Parse(portTextField.Text.ToString())),
				loginTextField.Text.ToString(), passwordField.Text.ToString());
		}

		private static async void OnSendClicked()
		{
			await Client.SendMessage(Int32.Parse(dialogidTextField.Text.ToString()), sendTextField.Text.ToString());
		}

		private static async void OnGetClicked()
		{
			var m = await Client.GetMessageLongPool(Int32.Parse(dialogidTextField.Text.ToString()), TimeSpan.FromSeconds(25));
			messageLabel.Text = NStack.ustring.Make(m.Text);

			MessageBox.Query(30, 10, m.AuthorId.ToString(), m.Text, "Ok");

			//var alert = new Window(new Rect(70,5, 30,10), NStack.ustring.Make(m.AuthorId.ToString()));
			//Top.Add(alert);
			//var label = new Label(3, 3, NStack.ustring.Make(m.Text));
			//alert.Add(label);
		}

		private static void Quit()
		{
			throw new System.NotImplementedException();
		}

		private static void Close()
		{
			throw new System.NotImplementedException();
		}

		private static void NewFile()
		{
			throw new System.NotImplementedException();
		}
	}
}