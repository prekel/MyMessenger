using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

using Newtonsoft.Json;

using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Client;
using MyMessenger.Client.Commands;

namespace MyMessenger.Client.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			OutputEncoding = Encoding.UTF8;

			try
			{
				//var q = new Query
				//{
				//	Config = new RegisterParameters
				//	{
				//		Nickname = "User3",
				//		Password = "123"
				//	}
				//};
				//var a = JsonConvert.SerializeObject(q);
				//var w = JsonConvert.DeserializeObject<Query>(a);


				//var data = new byte[256];
				//var response = new StringBuilder();

				while (true)
				{
					Write("> ");
					var s = ReadLine();
					var p = s.Split();
					var cmd = p[0];

					var client = new TcpClient();
					var server = args.Length == 1 ? IPAddress.Parse(args[0]) : IPAddress.Loopback;
					client.Connect(server, 20522);
					var stream = client.GetStream();

					AbstractCommand command = null;
					
					if (GetMessages.CommandNames.Contains(cmd))
					{
						command = new GetMessages(stream, p[1], Int32.Parse(p[2]));
						command.Execute();
						
						//WriteLine(re);
						
						//var res = ((GetMessages) command).Response;

						//WriteLine($"{res.Content.Count} сообщения:");
						//foreach (var i in res.Content)
						//{
						//	WriteLine("--------");
						//	WriteLine($"Автор: {i.Author.Nickname}");
						//	WriteLine($"Текст: {i.Text}");
						//}
					}

					if (Login.CommandNames.Contains(cmd))
					{
						command = new Login(stream, p[1], p[2]);
						command.Execute();
						//WriteLine(((Login)command).Response.Token);
					}

					if (Register.CommandNames.Contains(cmd))
					{
						command = new Register(stream, p[1], p[2]);
						command.Execute();
					}

					if (SendMessage.CommandNames.Contains(cmd))
					{
						command = new SendMessage(stream, p[1], Int32.Parse(p[2]), p[3]);
						command.Execute();
					}

					stream.Close();
					client.Close();
					
					if (command is null) continue;
					WriteLine(command.RawResponse);

					//if (p[0] == "register")
					//{
					//	var nickname = p[1];
					//	var pass = p[2];
					//	var q = new Query
					//	{
					//		Config = new RegisterParameters
					//		{
					//			Nickname = nickname,
					//			Password = pass
					//		}
					//	};
					//	var a = JsonConvert.SerializeObject(q);
					//	var data = Encoding.UTF8.GetBytes(a);
					//	stream.Write(data, 0, data.Length);
					//}

					//if (p[0] == "getmessages")
					//{
					//	var dialogid = p[1];
					//	var q = new Query
					//	{
					//		Config = new GetMessagesParameters
					//		{
					//			DialogId = Int32.Parse(dialogid)
					//		}
					//	};
					//	var a = JsonConvert.SerializeObject(q);
					//	var data = Encoding.UTF8.GetBytes(a);
					//	stream.Write(data, 0, data.Length);

					//	data = new byte[256];
					//	var response = new StringBuilder();
					//	do
					//	{
					//		var bytes = stream.Read(data, 0, data.Length);
					//		response.Append(Encoding.UTF8.GetString(data, 0, bytes));
					//	} while (stream.DataAvailable);

					//	var res = JsonConvert.DeserializeObject<List<Message>>(response.ToString());
					//	WriteLine($"{res.Count} сообщения:");
					//	foreach (var i in res)
					//	{
					//		WriteLine("--------");
					//		WriteLine($"Автор: {i.Author.Nickname}");
					//		WriteLine($"Текст: {i.Text}");
					//	}
					//}

					//if (p[0] == "login")
					//{
					//	var nickname = p[1];
					//	var pass = p[2];
					//	var q = new Query
					//	{
					//		Config = new LoginParameters
					//		{
					//			Login = nickname,
					//			Password = pass
					//		}
					//	};
					//	var a = JsonConvert.SerializeObject(q);
					//	var data = Encoding.UTF8.GetBytes(a);
					//	stream.Write(data, 0, data.Length);
						
					//	data = new byte[256];
					//	var response = new StringBuilder();
					//	do
					//	{
					//		var bytes = stream.Read(data, 0, data.Length);
					//		response.Append(Encoding.UTF8.GetString(data, 0, bytes));
					//	} while (stream.DataAvailable);

					//	var token = JsonConvert.DeserializeObject<string>(response.ToString());
					//}

				}


				//WriteLine(response.ToString());

				//var r = "Привет мир1";
				//var data1 = Encoding.UTF8.GetBytes(r);
				//stream.Write(data1, 0, data1.Length);

				//var res = JsonConvert.DeserializeObject<List<Message>>(response.ToString());
			}
			catch (SocketException e)
			{
				WriteLine("SocketException: {0}", e);
			}
			catch (Exception e)
			{
				WriteLine("Exception: {0}", e.Message);
			}
		}
	}
}