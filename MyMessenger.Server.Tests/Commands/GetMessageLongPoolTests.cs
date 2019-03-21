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
	public class GetMessageLongPoolTests
	{
		[Test]
		public void CommandNameTest()
		{
			using (var context = new TestMessengerContext())
			using (var server = new TestServer())
			{
				var cmd = new GetMessageLongPool(context, server.Tokens, server.Notifiers, new GetMessageLongPoolParameters());
				Assert.AreEqual(CommandType.GetMessageLongPool, cmd.CommandName);
			}
		}

		//[Test]
		//public async Task Test()
		//{
		//	using (var context = new TestMessengerContext())
		//	using (var server = new TestServer())
		//	{
		//		Assert.AreEqual(0, context.Accounts.Count());
		//		await new Register(context, new RegisterParameters
		//		{
		//			Nickname = "User123",
		//			Password = "123456"
		//		}).ExecuteAsync();
		//		await new Register(context, new RegisterParameters
		//		{
		//			Nickname = "User3",
		//			Password = "123456"
		//		}).ExecuteAsync();
		//		Assert.AreEqual(2, context.Accounts.Count());

		//		var lc = new Login(context, server.Tokens, new LoginParameters
		//		{
		//			Nickname = "User123",
		//			Password = "123456"
		//		});
		//		await lc.ExecuteAsync();
		//		var token = ((LoginResponse)lc.Response).Token;

		//		Assert.AreEqual(0, context.Dialogs.Count());
		//		var cc = new CreateDialog(context, server.Tokens, new CreateDialogParameters
		//		{
		//			Token = token,
		//			MembersNicknames = new List<string>(new[] { "User123", "User3" })
		//		});
		//		await cc.ExecuteAsync();
		//		Assert.AreEqual(1, context.Dialogs.Count());
		//		var dialogId = ((CreateDialogResponse)cc.Response).DialogId;

		//		var sc = new SendMessage(context, server.Tokens, server.Notifiers, new SendMessageParameters
		//		{
		//			DialogId = dialogId,
		//			Text = "Test Message",
		//			Token = token
		//		});
		//		await sc.ExecuteAsync();
		//		var resp = (SendMessageResponse)sc.Response;
		//		Assert.AreEqual(ResponseCode.Ok, resp.Code);
		//		Assert.AreEqual(1, context.Messages.Count());
		//		Assert.AreEqual("Test Message", context.Messages.First().Text);
		//		Assert.AreEqual("User123", context.Messages.First().Author.Nickname);
		//	}
		//}
	}
}
