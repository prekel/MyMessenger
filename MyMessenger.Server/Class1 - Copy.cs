using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

				var d1 = new Dialog { FirstMember1 = a1, SecondMember1 = a2 };
				var d2 = new Dialog { FirstMember1 = a1, SecondMember1 = a2 };
				context.Dialogs.Add(d1);
				context.Dialogs.Add(d2);

				context.Messages.Add(new Message { Text = "123text", Author1 = a1, Dialog1 = d1 });
				context.Messages.Add(new Message { Text = "234text", Author1 = a2, Dialog1 = d1 });
				context.Messages.Add(new Message { Text = "111text", Author1 = a1, Dialog1 = d2 });
				context.Messages.Add(new Message { Text = "222text", Author1 = a1, Dialog1 = d2 });

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

				var me = from i in context.Messages where i.Author1.Nickname == "User1" select i;
				Console.WriteLine();
				foreach (var i in me)
				{
					//Console.WriteLine($"DialogId: {i.Dialog.ID} Author: {i.Author.Nickname} First: {i.Dialog.FirstMember.Nickname} Second: {i.Dialog.SecondMember.Nickname}");
					Console.WriteLine($"          {i.Text}");
				}

				//var me2 = context.Messages.in
			}
		}
	}
}
