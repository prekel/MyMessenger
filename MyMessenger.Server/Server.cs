using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Newtonsoft.Json;

using MyMessenger.Server.Configs;

namespace MyMessenger.Server
{
	public class Server
	{
		private readonly TcpListener listener = new TcpListener(IPAddress.Any, 20522);
		private MessengerContext Context { get; set; }
		private Config Config { get; set; }
		
		public Server(Config config)
		{
			Config = config;
			//Context = new MessengerContext(Config);
			
			listener.Start();
			Console.WriteLine("Started.");

			while (true)
			{
				var client = listener.AcceptTcpClient();
				Console.WriteLine("Connected!");

				// each connection has its own thread
				new Thread(ServeData).Start(client);
			}
		}

		private void ServeData(object clientSocket)
		{
			try
			{
				using (var context = new MessengerContext(Config))
				{
					var client = (TcpClient) clientSocket;

					var s = client.GetStream();

					//var response = "Привет мир";
					var gm = new GetMessages(context, new GetMessages.Query {DialogId = 1});
					gm.Execute();
					var res = gm.Result;
					var list = res.ToList();
					var response = JsonConvert.SerializeObject(list, Formatting.Indented);
					var data = Encoding.UTF8.GetBytes(response);
					s.Write(data, 0, data.Length);
				}
			}
			catch (SocketException e)
			{

			}
		}
	}
}