using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using NLog;

using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class ProgramEnsureCreated
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		public static Config Config;

		public static void Main(Config config)
		{
			Config = config;
			InsertData();
			PrintData();
		}

		private static void InsertData()
		{
			using (var context = new MessengerContext(Config))
			{
				Log.Debug("Начата генерация таблиц и т.д.");
				context.Database.EnsureCreated();
				Log.Debug("Закончена генерация таблиц и т.д.");
				
				Log.Debug("Начато заполнение");
				var s = Crypto.GenerateSaltForPassword();
				var a1 = new Account()
				{
					Nickname = "User1",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s,
					RegistrationDateTime = DateTime.Now,
					//Dialogs = new List<AccountDialog>()
				};
				s = Crypto.GenerateSaltForPassword();
				var a2 = new Account()
				{
					Nickname = "User2",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s,
					RegistrationDateTime = DateTime.Now,
					//Dialogs = new List<AccountDialog>()
				};


				var d1 = new Dialog();
				var d2 = new Dialog();

				var ac11 = new AccountDialog(a1, d1);
				//a1.Dialogs.Add(ac11); d1.Members.Add(ac11);
				var ac12 = new AccountDialog(a1, d2);
				//a1.Dialogs.Add(ac12); d2.Members.Add(ac12);
				var ac21 = new AccountDialog(a2, d1);
				//a2.Dialogs.Add(ac21); d1.Members.Add(ac21);
				var ac22 = new AccountDialog(a2, d2);
				//a2.Dialogs.Add(ac22); d2.Members.Add(ac22);
				context.AccountsDialogs.Add(ac11);
				context.AccountsDialogs.Add(ac12);
				context.AccountsDialogs.Add(ac21);
				context.AccountsDialogs.Add(ac22);

				context.Accounts.Add(a1);
				context.Accounts.Add(a2);
				context.Dialogs.Add(d1);
				context.Dialogs.Add(d2);

				context.Messages.Add(new Message
				{
					Text = "123text",
					Author = a1,
					Dialog = d1,
					SendDateTime = DateTime.Now
				});
				context.Messages.Add(new Message
				{
					Text = "234text",
					Author = a2,
					Dialog = d1,
					SendDateTime = DateTime.Now
				});
				context.Messages.Add(new Message
				{
					Text = "111text",
					Author = a1,
					Dialog = d2,
					SendDateTime = DateTime.Now
				});
				context.Messages.Add(new Message
				{
					Text = "222text",
					Author = a1,
					Dialog = d2,
					SendDateTime = DateTime.Now
				});
				Log.Debug("Закончено заполнение");
				Log.Debug("Сохранение изменений");
				context.SaveChanges();
				Log.Debug("Изменения сохранены");
			}
		}

		private static void PrintData()
		{
			using (var context = new MessengerContext(Config))
			{
				Log.Debug("Начата печать запрашиваемых данных");
				var d = context.Dialogs;
				var m = context.Messages;
				var a = context.Accounts;

				var me = from i in context.Messages where i.Author.Nickname == "User1" select i;
				Console.WriteLine();
				foreach (var i in me)
				{
					Console.WriteLine($"          {i.Text} {i.SendDateTime}");
				}
				Console.WriteLine();
				foreach (var i in context.Dialogs)
				{
					Console.WriteLine($"          {i.DialogId} {String.Join("; ", from j in i.Members select j.Account.Nickname)}");
				}
				Log.Debug("Закончена печать запрашиваемых данных");
			}
		}
	}
}

