using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyMessenger.Client.Commands;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Console.Commands
{
	public class StartDialogSession //: AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] { "startdialogsession", "sds" });

		//public StartDialogSessionResponse Response { get; private set; }

		//private StartDialogSessionParameters Config1
		//{
		//	get => (StartDialogSessionParameters) Config;
		//	set => Config = value;
		//}

		//public StartDialogSession(NetworkStream stream, AbstractParameters config) : base(stream, config)
		//{
		//}

		private string Token { get; }
		private int DialogId { get; }
		private TimeSpan TimeSpan { get; }
		private SmartConsoleWriter Writer { get; }

		public CancellationTokenSource CancellationTokenSource { get; } = new CancellationTokenSource();

		//private CancellationToken CancellationToken => CancellationTokenSource.Token;

		private IPEndPoint IpEndPoint { get; }

		//private int MyAccountId { get; set; }

		public StartDialogSession(IPEndPoint ipEndPoint, SmartConsoleWriter writer,
			string token, int dialogId, TimeSpan ts) //: base(stream)
		{
			IpEndPoint = ipEndPoint;
			Token = token;
			DialogId = dialogId;
			TimeSpan = ts;
			Writer = writer;
		}

		public void Execute()
		{
			//{
			//	var client = new TcpClient();
			//	client.Connect(IpEndPoint);
			//	var stream = client.GetStream();
			//	var c = new GetAccountById(stream, Token, );
			//}

			var task = new Task(() =>
			{
				while (true)
				{
					var client = new TcpClient();
					client.Connect(IpEndPoint);
					var stream = client.GetStream();
					try
					{
						var gmlp = new GetMessageLongPool(stream, Token, DialogId, TimeSpan);
						gmlp.Execute();
						if (CancellationTokenSource.Token.IsCancellationRequested) break;
						var resp = gmlp.Response;
						if (resp.Code == ResponseCode.LongPoolTimeSpanExpired) continue;
						Writer.WriteLine(
							$"--> {resp.Content.SendDateTime.ToLongTimeString()} {resp.Content.AuthorId,3}  {resp.Content.Text}");
					}
					catch
					{
						// ignored
					}
					finally
					{
						stream.Close();
						client.Close();
					}
				}
			});

			task.Start();
		}
	}
}