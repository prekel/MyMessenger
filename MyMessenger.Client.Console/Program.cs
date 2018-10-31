using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace MyMessenger.Client.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				var client = new TcpClient();
				var server = IPAddress.Loopback;
				client.Connect(server, 20522);

				var data = new byte[256];
				var response = new StringBuilder();
				var stream = client.GetStream();

				do
				{
					var bytes = stream.Read(data, 0, data.Length);
					response.Append(Encoding.UTF8.GetString(data, 0, bytes));
				}
				while (stream.DataAvailable);

				WriteLine(response.ToString());

				stream.Close();
				client.Close();
			}
			catch (SocketException e)
			{
				WriteLine("SocketException: {0}", e);
			}
			catch (Exception e)
			{
				WriteLine("Exception: {0}", e.Message);
			}

			WriteLine("Запрос завершен...");
			Read();
		}
	}
}
