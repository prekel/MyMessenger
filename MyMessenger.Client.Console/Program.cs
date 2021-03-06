﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using static System.Console;
using Newtonsoft.Json;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Client;
using MyMessenger.Client.Commands;
using MyMessenger.Client.Console.Commands;

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

			var ipEndPoint = new IPEndPoint(ip, 20522);

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

			CancelKeyPress += ConsoleOnCancelKeyPress;

			var cmds = typeof(AbstractCommand)
				.Assembly
				.ExportedTypes
				.Where(_ => _.BaseType == typeof(AbstractCommand))
				.Select(_ => ((IEnumerable<string>)_
						.GetProperty("CommandNames")
						.GetValue(null))
					.First())
				.Append(StartDialogSession.CommandNames.First())
				.Append(StopDialogSession.CommandNames.First())
				.Append("connect");

			var reader = new SmartConsoleReader(cmds, "> ");
			var writer = new SmartConsoleWriter(reader);

			//var timer = new Timer(7000);
			//timer.Elapsed += (sender, eventArgs) =>
			//{
			//	writer.WriteLine(eventArgs.SignalTime.ToLongTimeString());
			//};
			//timer.AutoReset = true;
			//timer.Enabled = true;

			reader.WritePrefix();
			//while (true)
			//{
			//	var s = reader.NextString();
			//var p = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			//if (p[0] == "123")
			//{
			//	writer.WriteLine("qwerty");
			//}
			//}

			try
			{
				var isDialogSessionStarted = false;
				StartDialogSession start = null;
				var token = "";
				var dialogsessionid = 0;
				while (true)
				{
					//Write("> ");
					var s = reader.NextString();
					if (s.Length == 0) continue;
					var p = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					var cmd = p[0];

					AbstractCommand command = null;
					var needoutraw = true;

					try
					{
						if (p[0] == "connect")
						{
							ipEndPoint = new IPEndPoint(IPAddress.Parse(p[1]), 20522);
							continue;
						}

						if (StartDialogSession.CommandNames.Contains(cmd))
						{
							token = p[1];
							dialogsessionid = Int32.Parse(p[2]);
							start = new StartDialogSession(ipEndPoint, writer, token, dialogsessionid,
								TimeSpan.FromSeconds(Int32.Parse(p[3])));

							reader.IsNeedNewLine = false;
							reader.WipeCurrent();
							reader.Prefix = "<- ";
							reader.WritePrefix();
							isDialogSessionStarted = true;

							start.Execute();
							continue;
						}

						if (StopDialogSession.CommandNames.Contains(cmd) && start != null)
						{
							var stop = new StopDialogSession(start.CancellationTokenSource);
							stop.Execute();
							reader.WipeCurrent();
							reader.Prefix = "> ";
							reader.WritePrefix();
							isDialogSessionStarted = false;
							reader.IsNeedNewLine = true;
							continue;
						}

						var client = new TcpClient();
						client.Connect(ipEndPoint);
						var stream = client.GetStream();
						if (p[0] == "123")
						{
							writer.WriteLine("qwerty");
						}

						if (isDialogSessionStarted)
						{
							reader.WipeCurrent(true);
							var c = new SendMessage(stream, token, dialogsessionid, s);
							c.Execute();
							//writer.WriteLine($"<-- {DateTime.Now.ToLongTimeString()} {1,3}  {s}");
							continue;
						}

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
								WriteLine($"Автор: {i.AuthorId}");
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
							//command = new Register(stream, p[1], p[2], TimeZoneInfo.Local);
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
							//var token = p[1];
							if (Int32.TryParse(p[2], out var sid))
							{
								var mids = new List<int>();
								for (var i = 2; i < p.Length; i++)
								{
									mids.Add(Int32.Parse(p[i]));
								}

								command = new CreateDialog(stream, p[1], mids);
							}
							else
							{
								var mn = new List<string>();
								for (var i = 2; i < p.Length; i++)
								{
									mn.Add(p[i]);
								}

								command = new CreateDialog(stream, p[1], mn);
							}

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
								WriteLine($"Автор: {m.AuthorId}");
								WriteLine($"Текст: {m.Text}");
							}
						}

						if (GetMessageLongPool.CommandNames.Contains(cmd))
						{
							command = new GetMessageLongPool(stream, p[1], Int32.Parse(p[2]),
								TimeSpan.FromSeconds(Int32.Parse(p[3])));

							command.Execute();
						}

						if (GetAccountById.CommandNames.Contains(cmd))
						{
							command = new GetAccountById(stream, p[1], Int32.Parse(p[2]));

							command.Execute();
						}

						if (GetDialogById.CommandNames.Contains(cmd))
						{
							command = new GetDialogById(stream, p[1], Int32.Parse(p[2]));

							command.Execute();
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

						stream.Close();
						client.Close();
					}
					catch (Exception e)
					{
						WriteLine(e);
					}
					finally
					{
					}

					if (command is null) continue;
					if (needoutraw)
					{
						writer.WriteLine(command.RawResponse);
					}
				}
			}
			catch (SocketException e)
			{
				writer.WriteLine($"SocketException: {e}");
			}
			catch (Exception e)
			{
				writer.WriteLine(String.Format("Exception: {0}", e.Message));
			}
		}

		private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			//throw new NotImplementedException();
		}
	}
}