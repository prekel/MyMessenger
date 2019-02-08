using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using NUnit.Framework;

using MyMessenger.Server;
using MyMessenger.Server.Entities;
using MyMessenger.Server.Commands;

namespace MyMessenger.Server.Tests.Commands
{
	[TestFixture]
	public class RegisterTests
	{
		//private MessengerContext Context { get; set; }
		private IDictionary<string, IAccount> Tokens { get; } = new Dictionary<string, IAccount>();

		[Test]
		public void Test1Sync()
		{
			using (var context = new TestMessengerContext())
			{
				var b = context.Accounts.Count();

				var sm = new Register(context,
					new RegisterParameters { Nickname = "User3", Password = "123456" });
				sm.Execute();

				Assert.AreEqual(b + 1, context.Accounts.Count());
			}
		}

		[Test]
		public async Task Test2Async()
		{
			//using (var context = new MessengerContext(TestDbOptions<RegisterTests>.Options))
			using (var context = new TestMessengerContext())
			{
				var b = await context.Accounts.CountAsync();

				var sm = new Register(context,
					new RegisterParameters { Nickname = "User4", Password = "123456" });
				await sm.ExecuteAsync();

				Assert.AreEqual(b + 1, await context.Accounts.CountAsync());
			}
		}
	}
}