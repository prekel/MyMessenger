using System;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using static System.Console;

using Newtonsoft.Json;

using MyMessenger.Core;
using MyMessenger.Server.Configs;

namespace MyMessenger.Server.Console
{
	public class Program
	{
		public static Config Config;

		public static void Main(string[] args)
		{
			//LogManager.Configuration.Variables["starttime"] = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff");
			OutputEncoding = Encoding.UTF8;
			//CancelKeyPress += Program_CancelKeyPress;

			if (File.Exists("config.json"))
			{
				//Log.Info("Загружается конфирурация");
				Config = JsonConvert.DeserializeObject<Config>(new StreamReader("config.json").ReadToEnd());
				//Log.Info("Загружена конфигурация");
			}
			else
			{
				//Log.Info("Создаётся конфигурция");

				Config = new Config
				{
					DbConfig = new DbConfig()
					{
						Server = "localhost",
						Port = 3306,
						Name = "TestDb",
						User = "vladislav",
						SslMode = "none"
					}
				};

				//Log.Info("Сохраняется конфигурация");
				var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
				using (var w = new StreamWriter("config.json"))
				{
					w.WriteLine(json);
				}
			}


			var dbpass = new StringBuilder();
			Write("Enter database password: ");
			while (true)
			{
				var key = ReadKey(true);
				if (key.Key == ConsoleKey.Enter) break;
				if (key.Key == ConsoleKey.Backspace)
				{
					Write("\nEnter database password: ");
					dbpass.Clear();
					continue;
				}
				dbpass.Append(key.KeyChar);
				Write("*");
			}

			Config.DbConfig.Password = dbpass.ToString();
			dbpass = null;

			if (args.Length > 0)
			{
				var server = new Server(Config);
			}
			else
			{
				Program2.Main2(Config);
			}
		}
	}
}