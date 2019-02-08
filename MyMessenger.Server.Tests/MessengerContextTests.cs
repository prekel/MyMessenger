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

namespace MyMessenger.Server.Tests
{
	[TestFixture]
	public class MessengerContextTests
	{
		//private MessengerContext Context { get; set; }
		private IDictionary<string, IAccount> Tokens { get; } = new Dictionary<string, IAccount>();

		[SetUp]
		public void Setup()
		{
			//var options = new DbContextOptionsBuilder<MessengerContext>()
			//	.UseLazyLoadingProxies()
			//	.UseInMemoryDatabase("MyMessenger_Test")
			//	.Options;
			//Context = new MessengerContext(TestDbOptions.Options);
		}

		[Test]
		public async Task Test1()
		{
			using (var context = new TestMessengerContext())
			{
				var s = Crypto.GenerateSaltForPassword();
				var a1 = new Account()
				{
					Nickname = "User1",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s,
					RegistrationDateTime = DateTime.Now,
				};
				s = Crypto.GenerateSaltForPassword();
				var a2 = new Account()
				{
					Nickname = "User2",
					PasswordHash = Crypto.ComputePasswordHash("123456", s),
					PasswordSalt = s,
					RegistrationDateTime = DateTime.Now,
				};

				await context.Accounts.AddAsync(a1);
				await context.Accounts.AddAsync(a2);

				await context.SaveChangesAsync();

				Assert.AreEqual("User1", context.Accounts.First().Nickname);

				Assert.Pass();
			}
		}

		[Test]
		public async Task Test2()
		{
			using (var context = new TestMessengerContext())
			{
				var b = await context.Accounts.CountAsync();

				var sm = new Register(context,
					new RegisterParameters { Nickname = "User3", Password = "123456" });
				await sm.ExecuteAsync();

				Assert.AreEqual(b + 1, await context.Accounts.CountAsync());
			}
		}
	}
}