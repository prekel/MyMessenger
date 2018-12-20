using System.Collections.Generic;
using System.Linq;

using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class SendMessage : AbstractCommand
	{
		private SendMessageParameters Config1 { get => (SendMessageParameters)Config; set => Config = value; }
		
		private MessageNotifier Notifier { get; set; }
		
		public SendMessage(MessengerContext context, MessageNotifier notifier, AbstractParameters config) : base(context, config)
		{
			Notifier = notifier;
		}

		public SendMessage(MessengerContext context, IDictionary<string, IAccount> tokens, Notifiers notifiers, AbstractParameters config) : base(context, tokens, config)
		{
			Notifier = notifiers[Config1.DialogId, Config1.Token];
		}

		public override void Execute()
		{
			var resp = new SendMessageResponse();
			Response = resp;
			
			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			 //if (d.FirstMember.AccountId != Tokens[Config1.Token].AccountId && d.SecondMember.AccountId != Tokens[Config1.Token].AccountId)
			 //{
			 //	Code = ResponseCode.AccessDenied;
			 //	return;
			 //}
			
			var m = new Message
			{
				Author = new Account {AccountId = Tokens[Config1.Token].AccountId},
				Dialog = new Dialog {DialogId = Config1.DialogId},
				Text = Config1.Text
			};
			Context.Messages.Add(m);
			Context.SaveChanges();
			Code = ResponseCode.Ok;
			
			Notifier.MessageSent(m);
		}
	}
}