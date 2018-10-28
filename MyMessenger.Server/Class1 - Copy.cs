using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;
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

				var s = Crypto.GenerateSaltForPassword();
				var a1 = new Account()
				{
					Nickname = "User1",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s
				};
				context.Accounts.Add(a1);
				s = Crypto.GenerateSaltForPassword();
				var a2 = new Account()
				{
					Nickname = "User2",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s
				};
				context.Accounts.Add(a2);

				var d1 = new Dialog { FirstMember = a1, SecondMember = a2 };
				var d2 = new Dialog { FirstMember = a1, SecondMember = a2 };
				context.Dialogs.Add(d1);
				context.Dialogs.Add(d2);

				context.Messages.Add(new Message { Text = "123text", Author = a1, Dialog = d1 });
				context.Messages.Add(new Message { Text = "234text", Author = a2, Dialog = d1 });
				context.Messages.Add(new Message { Text = "111text", Author = a1, Dialog = d2 });
				context.Messages.Add(new Message { Text = "222text", Author = a1, Dialog = d2 });

				context.SaveChanges();
			}
		}

		private static void PrintData()
		{
			using (var context = new MessengerContext(Config))
			{
				var d = context.Dialogs;
				var m = context.Messages;
				var a = context.Accounts;

				var me = from i in context.Messages where i.Author.Nickname == "User1" select i;
				Console.WriteLine();
				foreach (var i in me)
				{
					//Console.WriteLine($"DialogId: {i.Dialog.ID:4} Author: {i.Author.ID:4} First: {i.Dialog.FirstMember:4} Second: {i.Dialog.SecondMember:4}");
					Console.WriteLine($"          {i.Text}");
				}

				//var me2 = context.Messages.in
			}
		}
	}
}
