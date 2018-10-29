using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MyMessenger.Server
{
	public class Server
	{
		private static readonly TcpListener listener = new TcpListener(IPAddress.Any, 20522);

		public Server()
		{
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

		private static void ServeData(object clientSocket)
		{
			try
			{
				var client = (TcpClient)clientSocket;
				var s = client.GetStream();

				var response = "Привет мир";
				var data = Encoding.UTF8.GetBytes(response);
				s.Write(data, 0, data.Length);
			}
			catch (SocketException e)
			{

			}
		}
	}
}