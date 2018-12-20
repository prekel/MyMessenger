using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class GetMessages : AbstractCommand
	{
		private GetMessagesParameters Config1 { get => (GetMessagesParameters)Config; set => Config = value; }
		
		public IQueryable<Message> Result { get; private set; }
		
		public GetMessages(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}

		public override void Execute()
		{
			var resp = new GetMessagesResponse();
			Response = resp;
			
			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			if (d.FirstMember.AccountId != Tokens[Config1.Token].AccountId && d.SecondMember.AccountId != Tokens[Config1.Token].AccountId)
			{
				Code = ResponseCode.AccessDenied;
				return;
			}
			
			
			// Запрос сообщений из базы
			var r = from i in Context.Messages where i.Dialog.DialogId == Config1.DialogId select i;
			Result = r;

			Code = ResponseCode.Ok;
			resp.Content = r.ToList<IMessage>();
		}
	}
}