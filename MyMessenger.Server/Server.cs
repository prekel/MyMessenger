using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using static System.Console;
using Newtonsoft.Json;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Commands;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class Server
	{
		private readonly TcpListener _listener = new TcpListener(IPAddress.Any, 20522);
		private MessengerContext Context { get; set; }
		private Config Config { get; set; }

		private IDictionary<string, IAccount> Tokens { get; set; } = new Dictionary<string, IAccount>();

		public Server(Config config)
		{
			Config = config;

			_listener.Start();
			WriteLine("Started.");

			while (true)
			{
				var client = _listener.AcceptTcpClient();
				WriteLine("Connected!");

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

					var data1 = new byte[256];
					var response1 = new StringBuilder();
					var stream = client.GetStream();

					do
					{
						var bytes = stream.Read(data1, 0, data1.Length);
						response1.Append(Encoding.UTF8.GetString(data1, 0, bytes));
					} while (stream.DataAvailable);


					Query q;
					try
					{
						q = JsonConvert.DeserializeObject<Query>(response1.ToString());
					}
					catch
					{
						return;
					}


					var s = client.GetStream();

					try
					{
						// TODO: должны отправлять Response
						if (q.Config.CommandName == CommandType.GetMessages)
						{
							var gm = new GetMessages(context, Tokens, q.Config);
							gm.Execute();


							//var res = gm.Result;
							//var list = res.ToList();
							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.Register)
						{
							var gm = new Register(context, q.Config);
							gm.Execute();
							//var res = gm.Result;
							//var list = res.ToList();
							//var response = JsonConvert.SerializeObject(list, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.Login)
						{
							var gm = new Login(context, Tokens, q.Config);
							gm.Execute();
							//var res = gm.Token;
							//var response = JsonConvert.SerializeObject(res, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.SendMessage)
						{
							var gm = new SendMessage(context, Tokens, q.Config);
							gm.Execute();

							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.CreateDialog)
						{
							var gm = new CreateDialog(context, Tokens, q.Config);
							gm.Execute();

							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}
						
						if (q.Config.CommandName == CommandType.DialogSession)
						{
							var gm = new DialogSession(context, Tokens, q.Config);
							gm.Execute();

							var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							var data = Encoding.UTF8.GetBytes(response);
							s.Write(data, 0, data.Length);
						}
					}
					catch (Exception e)
					{
						WriteLine(e);

						var res = new CommonResponse {Code = ResponseCode.UnknownError};
						var response = JsonConvert.SerializeObject(res, Formatting.Indented);
						var data = Encoding.UTF8.GetBytes(response);
						s.Write(data, 0, data.Length);

						s.Close();
						client.Close();
						return;
					}


					s.Close();
					client.Close();
				}
			}
			catch (SocketException e)
			{
			}
		}
	}
}