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
	public class DialogSessionEventArgs : EventArgs
	{
		public DialogSessionResponse Response;
	}
	
	public class DialogSession : AbstractCommand
	{
		private DialogSessionParameters Config1 { get => (DialogSessionParameters)Config; set => Config = value; }
		
		//public IQueryable<Message> Result { get; private set; }
		
		private MessageNotifier Notifier { get; set; }
		
		public
		
		public DialogSession(MessengerContext context, IDictionary<string, IAccount> tokens, MessageNotifier notifier, AbstractParameters config) : base(context, tokens, config)
		{
			Notifier = notifier;
			Notifier.NewMessage += NotifierOnNewMessage;
		}

		public DialogSession(MessengerContext context, IDictionary<string, IAccount> tokens, MessageNotifier notifier) : base(context, tokens)
		{
			Notifier = notifier;
			Notifier.NewMessage += NotifierOnNewMessage;
		}

		private void NotifierOnNewMessage(object sender, MessageNotifierEventArgs e)
		{
			var resp = new DialogSessionResponse();
			Response = resp;

			resp.Message = e.Message;
			
		}
		
		public override void Execute()
		{
			var resp = new DialogSessionResponse();
			Response = resp;
			
//			// Проверка на принадлежность того, кто сделал запрос, к диалогу
//			var d = Context.Dialogs.First(p => p.Id == Config1.DialogId);
//			if (d.FirstMember.Id != Tokens[Config1.Token].Id && d.SecondMember.Id != Tokens[Config1.Token].Id)
//			{
//				Code = ResponseCode.AccessDenied;
//				return;
//			}
//			
//			
//			// Запрос сообщений из базы
//			var r = from i in Context.Messages where i.Dialog1.Id == Config1.DialogId select i;
//			Result = r;
//
//			Code = ResponseCode.Ok;
//			resp.Content = r.ToList<IMessage>();
		}
	}
}