﻿using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using static System.Console;

using Newtonsoft.Json;
using NLog;

using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Commands;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class AsyncServer
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();
		private readonly TcpListener _listener = new TcpListener(IPAddress.Any, 20522);
		private MessengerContext Context { get; set; }
		private Config Config { get; set; }

		private IDictionary<string, IAccount> Tokens { get; } = new Dictionary<string, IAccount>();
		//private IDictionary<int, MessageNotifier> Notifiers { get; set; } = new Dictionary<int, MessageNotifier>();

		private Notifiers Notifiers { get; } = new Notifiers();

		public AsyncServer(Config config)
		{
			Config = config;
		}

		public async Task StartAsync()
		{
			_listener.Start();
			Log.Debug("Запущен сервер");

			using (var context = new MessengerContext(Config))
			{
				Log.Debug("Запись в базу данных информации о запуске");
				await context.Launches.AddAsync(new Launch
				{
					MachineName = Environment.MachineName,
					LaunchDateTime = DateTimeOffset.Now,
					Pid = System.Diagnostics.Process.GetCurrentProcess().Id,
					User = Config.DbConfig.User,
					AssemblyVersion = typeof(Server).Assembly.GetName().Version.ToString()
				});
				await context.SaveChangesAsync();
				Log.Debug("Изменения сохранены");
			}

			while (true)
			{
				var client = await _listener.AcceptTcpClientAsync();
				//WriteLine("Connected!");
				//var t = new Thread(ServeData);

				Log.Debug($"Подключен клиент {client.Client.RemoteEndPoint}");

				var sd = ServeDataAsync(client);
				//sd.Start();
				//t.Start(client);
			}

		}

		private async Task<string> ReadStringAsync(NetworkStream stream)
		{
			var data1 = new byte[256];
			var response1 = new StringBuilder();
			//var stream = client.GetStream();

			do
			{
				var bytes = await stream.ReadAsync(data1, 0, data1.Length);
				response1.Append(Encoding.UTF8.GetString(data1, 0, bytes));
			} while (stream.DataAvailable);

			return response1.ToString();
		}

		private async Task<Query> ReadQueryAsync(NetworkStream stream)
		{
			return JsonConvert.DeserializeObject<Query>(await ReadStringAsync(stream));
		}

		private async Task SendResponseAsync(Stream s, AbstractResponse resp)
		{
			var str = JsonConvert.SerializeObject(resp, Formatting.Indented);
			await SendStringAsync(s, str);
		}

		private async Task SendStringAsync(Stream s, string response)
		{
			var data = Encoding.UTF8.GetBytes(response);
			await s.WriteAsync(data, 0, data.Length);
		}

		private async Task ServeDataAsync(TcpClient clientSocket)
		{
			try
			{
				using (var context = new MessengerContext(Config))
				{
					var client = (TcpClient)clientSocket;

					//var data1 = new byte[256];
					//var response1 = new StringBuilder();
					//var stream = client.GetStream();

					//do
					//{
					//	var bytes = stream.Read(data1, 0, data1.Length);
					//	response1.Append(Encoding.UTF8.GetString(data1, 0, bytes));
					//} while (stream.DataAvailable);

					var s = client.GetStream();

					Query q;
					try
					{
						//q = JsonConvert.DeserializeObject<Query>(response1.ToString());
						q = await ReadQueryAsync(s);
					}
					catch (Exception e)
					{
						Log.Warn(e.Message);

						Log.Trace($"Возвращено {ResponseCode.UnknownError}");
						var res = new CommonResponse(ResponseCode.UnknownError);

						await SendResponseAsync(s, res);

						//var response = JsonConvert.SerializeObject(res, Formatting.Indented);
						//var data = Encoding.UTF8.GetBytes(response);
						//s.Write(data, 0, data.Length);

						s.Close();
						client.Close();

						return;
					}

					try
					{
						//Log.Trace($"Выполняется запрос {q.Config.CommandName}");

						if (q.Config.CommandName == CommandType.GetMessages)
						{
							var gm = new GetMessages(context, Tokens, q.Config);
							await gm.ExecuteAsync();


							//var res = gm.Result;
							//var list = res.ToList();

							await SendResponseAsync(s, gm.Response);

							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.Register)
						{
							var gm = new Register(context, q.Config);
							await gm.ExecuteAsync();
							//var res = gm.Result;
							//var list = res.ToList();
							//var response = JsonConvert.SerializeObject(list, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.Login)
						{
							var gm = new Login(context, Tokens, q.Config);
							await gm.ExecuteAsync();
							//var res = gm.Token;
							//var response = JsonConvert.SerializeObject(res, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.SendMessage)
						{
							var gm = new SendMessage(context, Tokens, Notifiers, q.Config);
							await gm.ExecuteAsync();

							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.CreateDialog)
						{
							var gm = new CreateDialog(context, Tokens, q.Config);
							await gm.ExecuteAsync();

							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.DialogSession)
						{
							var gm = new DialogSession(context, Tokens, Notifiers, q.Config);

							gm.NewMessage += (sender, args) =>
							{
								var response = JsonConvert.SerializeObject(args.Response, Formatting.Indented);
								var data = Encoding.UTF8.GetBytes(response);
								try
								{
									s.Write(data, 0, data.Length);
								}
								catch (SocketException)
								{
									var conf = (DialogSessionParameters)q.Config;
									Notifiers[conf.DialogId, conf.Token] = null;
									gm.NewMessage = null;
								}
							};
							gm.NewMessage += OnNewMessage;

							while (true)
							{
								Thread.Sleep(1);
							}
						}

						if (q.Config.CommandName == CommandType.GetMessageLongPool)
						{
							var gm = new GetMessageLongPool(context, Tokens, Notifiers, q.Config);
							await gm.ExecuteAsync();

							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.GetAccountById)
						{
							var gm = new GetAccountById(context, Tokens, q.Config);
							await gm.ExecuteAsync();

							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}

						if (q.Config.CommandName == CommandType.GetDialogById)
						{
							var gm = new GetDialogById(context, Tokens, q.Config);
							await gm.ExecuteAsync();

							await SendResponseAsync(s, gm.Response);
							//var response = JsonConvert.SerializeObject(gm.Response, Formatting.Indented);
							//var data = Encoding.UTF8.GetBytes(response);
							//s.Write(data, 0, data.Length);
						}
					}
					catch (Exception e)
					{
						//WriteLine(e);
						Log.Warn(e.Message);

						Log.Trace($"Возвращено {ResponseCode.UnknownError}");
						var res = new CommonResponse(ResponseCode.UnknownError);

						await SendResponseAsync(s, res);
						//var response = JsonConvert.SerializeObject(res, Formatting.Indented);
						//var data = Encoding.UTF8.GetBytes(response);
						//s.Write(data, 0, data.Length);

						s.Close();
						client.Close();
						return;
					}

					s.Close();
					client.Close();
				}
			}
			catch (Exception e)
			{
			}
		}

		public void OnNewMessage(object sender, EventArgs args)
		{

		}
	}
}