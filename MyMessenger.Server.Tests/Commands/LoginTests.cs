using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

using MyMessenger.Server.Commands;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Server.Tests.Commands
{
	[TestFixture]
	public class LoginTests
	{
		[Test]
		public void CommandNameTest()
		{
			using (var context = new TestMessengerContext())
			using (var server = new TestServer())
			{
				var cmd = new Login(context, server.Tokens, new LoginParameters());
				Assert.AreEqual(CommandType.Login, cmd.CommandName);
			}
		}
		
		[Test]
		public async Task Login1()
		{
			using (var context = new TestMessengerContext())
			using (var server = new TestServer())
			{
				Assert.AreEqual(0, context.Accounts.Count());
				Assert.AreEqual(0, server.Tokens.Count);

				var rc = new Register(context, new RegisterParameters
				{
					Nickname = "User123",
					Password = "123456"
				});
				await rc.ExecuteAsync();
				Assert.AreEqual(ResponseCode.Ok, rc.Response.Code);
				Assert.AreEqual(1, context.Accounts.Count());

				var loginTime1 = context.Accounts.First().LoginDateTime;
				Assert.AreEqual(DateTimeOffset.MinValue, loginTime1);

				var lc = new Login(context, server.Tokens, new LoginParameters
				{
					Nickname = "User123",
					Password = "123456"
				});
				await lc.ExecuteAsync();
				Assert.AreEqual(1, server.Tokens.Count);
				var resp = (LoginResponse)lc.Response;
				Assert.AreEqual(ResponseCode.Ok, lc.Response.Code);
				Assert.AreEqual(server.Tokens[resp.Token].Nickname, resp.Account.Nickname);
				Assert.AreEqual(server.Tokens[resp.Token].AccountId, resp.Account.AccountId);

				var loginTime2 = context.Accounts.First().LoginDateTime;
				Assert.AreNotEqual(DateTimeOffset.MinValue, loginTime2);
			}
		}
	}
}
