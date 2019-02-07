using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class SendMessage : AbstractCommand
	{
		private SendMessageParameters Config1 { get => (SendMessageParameters)Config; set => Config = value; }

		private IEnumerable<MessageNotifier> Notifiers { get; set; }

		//public SendMessage(MessengerContext context, MessageNotifier notifier, AbstractParameters config) : base(context, config)
		//{
		//	Notifier = notifier;
		//}

		public SendMessage(MessengerContext context, IDictionary<string, IAccount> tokens, Notifiers notifiers, AbstractParameters config) : base(context, tokens, config)
		{
			Notifiers = notifiers[Config1.DialogId];
		}

		static SendMessage()
		{
			CommandName = CommandType.SendMessage;
		}

		protected override void ExecuteImpl()
		{
			var resp = new SendMessageResponse();
			Response = resp;

			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			var requesterid = Tokens[Config1.Token].AccountId;
			if (d.Members.Select(p => p.Account).All(p => p.AccountId != requesterid))
			{
				Code = ResponseCode.AccessDenied;
				return;
			}

			var m = new Message
			{
				Author = new Account { AccountId = Tokens[Config1.Token].AccountId },
				Dialog = new Dialog { DialogId = Config1.DialogId },
				Text = Config1.Text,
				SendDateTime = DateTime.Now
			};
			Context.Messages.Add(m);
			Context.SaveChanges();
			Code = ResponseCode.Ok;

			foreach (var i in Notifiers)
			{
				i.MessageSent(m);
			}
		}

		protected override async Task ExecuteImplAsync()
		{
			var resp = new SendMessageResponse();
			Response = resp;

			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			//var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);

			var requesterId = Tokens[Config1.Token].AccountId;
			if (!await Context
				.Dialogs
				.Where(p => p.DialogId == Config1.DialogId)
					.Where(p => p.MembersIds.Contains(requesterId))
				.AnyAsync())
			{
				Code = ResponseCode.AccessDenied;
				return;
			}

			var m = new Message
			{
				Author = new Account(Tokens[Config1.Token].AccountId),
				Dialog = new Dialog(Config1.DialogId),
				Text = Config1.Text,
				SendDateTime = DateTime.Now
			};
			await Context.Messages.AddAsync(m);
			await Context.SaveChangesAsync();
			Code = ResponseCode.Ok;

			foreach (var i in Notifiers)
			{
				i.MessageSent(m);
			}
		}
	}
}