using System.Collections.Generic;
using System.Linq;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class GetMessageLongPool : AbstractCommand
	{
		private GetMessageLongPoolParameters Config1
		{
			get => (GetMessageLongPoolParameters) Config;
			set => Config = value;
		}

		public IQueryable<Message> Result { get; private set; }

		public GetMessageLongPool(MessengerContext context, IDictionary<string, IAccount> tokens,
			AbstractParameters config) : base(context, tokens, config)
		{
		}

		public override void Execute()
		{
			var resp = new GetMessageLongPoolResponse();
			Response = resp;

			var mn = new MessageNotifier();

			mn.NewMessage += MnOnNewMessage;
		}

		private void MnOnNewMessage(object sender, MessageNotifierEventArgs e)
		{
			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.Id == Config1.DialogId);
			if (d.FirstMember.Id != Tokens[Config1.Token].Id && d.SecondMember.Id != Tokens[Config1.Token].Id)
			{
				Code = ResponseCode.AccessDenied;
				return;
			}


			// Запрос сообщений из базы
			var r = from i in Context.Messages where i.Dialog1.Id == Config1.DialogId select i;
			Result = r;

			Code = ResponseCode.Ok;
			resp.Content = r.ToList<IMessage>();
		}
	}
}