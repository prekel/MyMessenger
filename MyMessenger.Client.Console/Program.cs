using System;
using System.Collections.Generic;
using System.Linq;
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
			var arglist = new List<string>(args);
			
			if (!IPAddress.TryParse(arglist.FirstOrDefault() ?? "", out var ip))
			{
				ip = IPAddress.Loopback;
			}
			
			OutputEncoding = Encoding.UTF8;
			//InputEncoding = Encoding.Unicode;
			if (args.Contains("utf16"))
			{
				InputEncoding = Encoding.Unicode;
			}
			if (args.Contains("utf8"))
			{
				InputEncoding = Encoding.UTF8;
			}
			if (args.Contains("windows-1251") || args.Contains("1251"))
			{
				InputEncoding = Encoding.GetEncoding(1251);
			}

			System.Console.CancelKeyPress += ConsoleOnCancelKeyPress;
			
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
					var p = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					var cmd = p[0];

					var client = new TcpClient();

					
					
					client.Connect(ip, 20522);
					var stream = client.GetStream();

					AbstractCommand command = null;
					var needoutraw = true;

					try
					{
						if (GetMessages.CommandNames.Contains(cmd))
						{
							command = new GetMessages(stream, p[1], Int32.Parse(p[2]));
							command.Execute();
							needoutraw = false;

							var res = ((GetMessages)command).Response;
							WriteLine($"{res.Content.Count} сообщения:");
							foreach (var i in res.Content)
							{
								WriteLine("--------");
								WriteLine($"Автор: {i.Author.Nickname}");
								WriteLine($"Текст: {i.Text}");
							}
						}

						if (Login.CommandNames.Contains(cmd))
						{
							command = new Login(stream, p[1], p[2]);
							command.Execute();
							//WriteLine(((Nickname)command).Response.Token);
						}

						if (Register.CommandNames.Contains(cmd))
						{
							command = new Register(stream, p[1], p[2]);
							command.Execute();
						}

						if (SendMessage.CommandNames.Contains(cmd))
						{
							string message;
							if (s.Count(_ => _ == '"') == 2)
							{
								var first = s.IndexOf('"');
								var last = s.LastIndexOf('"');
								message = s.Substring(first + 1, last - first - 1);
							}
							else
							{
								message = p[3];
							}
							command = new SendMessage(stream, p[1], Int32.Parse(p[2]), message);
							command.Execute();
						}

						if (CreateDialog.CommandNames.Contains(cmd))
						{
							command = Int32.TryParse(p[2], out var sid)
								? new CreateDialog(stream, p[1], sid)
								: new CreateDialog(stream, p[1], p[2]);

							command.Execute();
						}

						if (DialogSession.CommandNames.Contains(cmd))
						{
							command = new DialogSession(stream, p[1], Int32.Parse(p[2]));
							command.Execute();
							//needoutraw = false;

							while (true)
							{
								var ds = (DialogSession)command;
								ds.Receive();
								var m = ds.Response.Message;
								WriteLine("--------");
								WriteLine($"Автор: {m.Author.Nickname}");
								WriteLine($"Текст: {m.Text}");
							}
						}

						if (cmd == "test")
						{
							WriteLine(p[1]);
						}
						if (cmd == "utf8")
						{
							InputEncoding = Encoding.UTF8;
						}
						if (cmd == "utf16")
						{
							InputEncoding = Encoding.Unicode;
						}
					}
					catch (Exception e)
					{
						WriteLine(e);
					}
					finally
					{
						stream.Close();
						client.Close();
					}

					if (command is null) continue;
					if (needoutraw) WriteLine(command.RawResponse);
				}
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

		private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			//throw new NotImplementedException();
		}
	}
}