using System.Collections.Generic;
using System.Linq;

using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class SendMessage : AbstractCommand
	{
		private SendMessageParameters Config1 { get => (SendMessageParameters)Config; set => Config = value; }
		
		public SendMessage(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public SendMessage(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}

		public override void Execute()
		{
			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.Id == Config1.DialogId);
			if (d.FirstMember.Id != Tokens[Config1.Token].Id && d.SecondMember.Id != Tokens[Config1.Token].Id) return;
			
			var m = new Message
			{
				Author1 = new Account {Id = Tokens[Config1.Token].Id},
				Dialog1 = new Dialog {Id = Config1.DialogId},
				Text = Config1.Text
			};
			Context.Messages.Add(m);
			Context.SaveChanges();
		}
	}
}