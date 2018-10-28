using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MyMessenger.Server
{
	public class Program2
	{
		public static Config Config;
		
		public static void Main2(Config config)
		{
			Config = config;
			InsertData();
			PrintData();
		}

		private static void InsertData()
		{
			using (var context = new MessengerContext(Config))
			{
				context.Database.EnsureCreated();

				var d = new Dialog();
				context.Dialogs.Add(d);
				context.Messages.Add(new Message { Text = "123text", Dialog = d });
				context.Messages.Add(new Message { Text = "234text", Dialog = d });

				var d1 = new Dialog();
				context.Dialogs.Add(d1);
				context.Messages.Add(new Message { Text = "111text", Dialog = d1});
				context.Messages.Add(new Message { Text = "222text", Dialog = d1});

				context.SaveChanges();
			}
		}

		private static void PrintData()
		{
			// Gets and prints all books in database
			using (var context = new MessengerContext(Config))
			{
				var d = context.Dialogs;
				var m = d.First().Messages.First();
			}
		}
	}
}
