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
	public class GetAccountByIdTests
	{
		[Test]
		public void CommandNameTest()
		{
			using (var context = new TestMessengerContext())
			using (var server = new TestServer())
			{
				var cmd = new GetAccountById(context, server.Tokens, new GetDialogByIdParameters());
				Assert.AreEqual(CommandType.GetAccountById, cmd.CommandName);
			}
		}

		[Test]
		public async Task Test()
		{
			using (var context = new TestMessengerContext())
			using (var server = new TestServer())
			{
				Assert.AreEqual(0, context.Accounts.Count());
				await new Register(context, new RegisterParameters
				{
					Nickname = "User123",
					Password = "123456"
				}).ExecuteAsync();
				await new Register(context, new RegisterParameters
				{
					Nickname = "User3",
					Password = "123456"
				}).ExecuteAsync();
				Assert.AreEqual(2, context.Accounts.Count());

				var lc = new Login(context, server.Tokens, new LoginParameters
				{
					Nickname = "User123",
					Password = "123456"
				});
				await lc.ExecuteAsync();
				Assert.AreEqual(ResponseCode.Ok, lc.Response.Code);
				var token = ((LoginResponse)lc.Response).Token;
				var account = ((LoginResponse)lc.Response).Account;

				var gc = new GetAccountById(context, server.Tokens, new GetAccountByIdParameters
				{
					Token = token,
					AccountId = account.AccountId
				});
				await gc.ExecuteAsync();
				Assert.AreEqual(ResponseCode.Ok, gc.Response.Code);
				var gaccount = ((GetAccountByIdResponse)gc.Response).Account;

				Assert.AreEqual(account.AccountId, gaccount.AccountId);
				Assert.AreEqual(account.Nickname, gaccount.Nickname);
				Assert.AreEqual(account.LoginDateTime, gaccount.LoginDateTime);
				Assert.AreEqual(account.RegistrationDateTime, gaccount.RegistrationDateTime);
			}
		}
	}
}
