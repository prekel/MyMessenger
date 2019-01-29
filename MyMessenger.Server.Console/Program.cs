using System;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using Newtonsoft.Json;
using NLog;

using MyMessenger.Core;
using MyMessenger.Server.Configs;

namespace MyMessenger.Server.Console
{
	public class Program
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		public static Config Config;

		public static void Main(string[] args)
		{
			LogManager.Configuration.Variables["starttime"] = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff");
			OutputEncoding = Encoding.UTF8;
			CancelKeyPress += Program_CancelKeyPress;

			if (File.Exists("config.json"))
			{
				Log.Info("Загружается конфирурация");
				Config = JsonConvert.DeserializeObject<Config>(new StreamReader("config.json").ReadToEnd());
				Log.Info("Загружена конфигурация");
			}
			else
			{
				Log.Info("Создаётся конфигурция");

				Config = new Config
				{
					DbConfig = new DbConfig()
					{
						Server = "localhost",
						Port = 3306,
						Name = "MyMessenger3",
						User = "root",
						SslMode = "none"
					}
				};

				Log.Info("Сохраняется конфигурация");
				var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
				using (var w = new StreamWriter("config.json"))
				{
					w.WriteLine(json);
				}
			}

			Log.Info("Вводится пароль");
			var dbpass = new StringBuilder();
			//Write("Enter database password: ");
			while (true)
			{
				var key = ReadKey(true);
				if (key.Key == ConsoleKey.Enter) break;
				if (key.Key == ConsoleKey.Backspace)
				{
					//Write("\nEnter database password: ");
					WriteLine();
					dbpass.Clear();
					continue;
				}
				dbpass.Append(key.KeyChar);
				Write("*");
			}
			WriteLine();
			Log.Info("Введён пароль");

			Config.DbConfig.Password = dbpass.ToString();
			dbpass = null;

			if (args.Length == 0)
			{
				Log.Info("Запускается инициализация базы данных");
				ProgramEnsureCreated.Main(Config);
			}
			else if (args[0] == "1")
			{
				Log.Info("Запускается многопоточный сервер");
				var server = new Server(Config);
			}
			else if (args[0] == "2")
			{
				Log.Info("Запускается миграция базы данных");
				ProgramMigrate.Main(Config);
			}
			else if (args[0] == "3")
			{
				Log.Info("Запускается асинхронный сервер");
				var server = new AsyncServer(Config);
				var start = server.StartAsync();
				start.Wait();
			}
		}

		private static void Program_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			Log.Info("Завершение програмы");
			LogManager.Shutdown();
			Environment.Exit(0);
		}
	}
}