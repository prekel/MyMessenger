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
		public DialogSessionResponse Response { get; private set; }

		public DialogSessionEventArgs(DialogSessionResponse response)
		{
			Response = response;
		}
	}

	[Obsolete]
	public class DialogSession : AbstractCommand
	{
		private DialogSessionParameters Config1
		{
			get => (DialogSessionParameters)Config;
			set => Config = value;
		}

		//public IQueryable<Message> Result { get; private set; }

		private MessageNotifier Notifier { get; set; }

		public EventHandler<DialogSessionEventArgs> NewMessage;

		public DialogSession(MessengerContext context, IDictionary<string, IAccount> tokens, Notifiers notifiers,
			AbstractParameters config) : base(context, tokens, config)
		{
			Notifier = notifiers[Config1.DialogId, Config1.Token];
			Notifier.NewMessage += NotifierOnNewMessage;
		}

		private void NotifierOnNewMessage(object sender, MessageNotifierEventArgs e)
		{
			var resp = new DialogSessionResponse();
			Response = resp;

			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			 //if (d.FirstMember.AccountId != Tokens[Config1.Token].AccountId && d.SecondMember.AccountId != Tokens[Config1.Token].AccountId)
			 //{
			 //	Code = ResponseCode.AccessDenied;
			 //	return;
			 //

			resp.Message = e.Message;
			resp.Code = ResponseCode.Ok;

			NewMessage?.Invoke(this, new DialogSessionEventArgs(resp));
		}

		static DialogSession()
		{
			CommandName = CommandType.DialogSession;
		}

		[Obsolete]
		protected override void ExecuteImpl()
		{
			var resp = new DialogSessionResponse();
			Response = resp;

			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			 //if (d.FirstMember.AccountId != Tokens[Config1.Token].AccountId && d.SecondMember.AccountId != Tokens[Config1.Token].AccountId)
			 //{
			 //	Code = ResponseCode.AccessDenied;
			 //	return;
			 //}

			var gm = new GetMessages(Context, Tokens,
				new GetMessagesParameters { DialogId = Config1.DialogId, Token = Config1.Token });
			gm.Execute();
			var m = ((GetMessagesResponse)gm.Response).Content.Last();
			resp.Message = m;
			Code = ResponseCode.Ok;
			//			
			//			// Запрос сообщений из базы
			//			var r = from i in Context.Messages where i.Dialog.DialogId == Config1.DialogId select i;
			//			Result = r;
			//
			//			Code = ResponseCode.Ok;
			//			resp.Content = r.ToList<IMessage>();
		}
	}
}