using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using MyMessenger.Server.Configs;
using Newtonsoft.Json;

namespace MyMessenger.Server
{
	public class MessengerContextFactory : IDesignTimeDbContextFactory<MessengerContext>
	{
		public MessengerContext CreateDbContext(string[] args)
		{
			//OutputEncoding = Encoding.UTF8;
			Config Config;

			Config = JsonConvert.DeserializeObject<Config>(new StreamReader("config.json").ReadToEnd());

			var dbpass = new StringBuilder();
			Console.Write("Enter database password: ");
			while (true)
			{
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter) break;
				if (key.Key == ConsoleKey.Backspace)
				{
					Console.Write("\nEnter database password: ");
					dbpass.Clear();
					continue;
				}
				dbpass.Append(key.KeyChar);
				Console.Write("*");
			}

			Config.DbConfig.Password = dbpass.ToString();

			Console.WriteLine();

			return new MessengerContext(Config);
		}
	}
}