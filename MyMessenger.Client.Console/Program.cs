using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MyMessenger.Core;
using Newtonsoft.Json;
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
				
				var r = "Привет мир1";
				var data1 = Encoding.UTF8.GetBytes(r);
				stream.Write(data1, 0, data1.Length);

				//var res = JsonConvert.DeserializeObject<List<Message>>(response.ToString(), new IDialogConvert(), new IAccountConvert());
				
				var settings = new JsonSerializerSettings();
				settings.Converters.Add(new Account.Converter());
				settings.Converters.Add(new Dialog.Converter());
				settings.Converters.Add(new Message.Converter());
				
				var res = JsonConvert.DeserializeObject<List<Message>>(response.ToString(), settings);
				
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
			//Read();
		}
	}
}
