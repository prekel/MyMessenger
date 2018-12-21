using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
			get => (GetMessageLongPoolParameters)Config;
			set => Config = value;
		}

		public IQueryable<Message> Result { get; private set; }

		private Notifiers Notifiers { get; set; }

		public GetMessageLongPool(MessengerContext context, IDictionary<string, IAccount> tokens, Notifiers notifiers,
			AbstractParameters config) : base(context, tokens, config)
		{
			Notifiers = notifiers;
		}

		private IMessage Message { get; set; }

		public override void Execute()
		{
			var resp = new GetMessageLongPoolResponse();
			Response = resp;

			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			var requesterid = Tokens[Config1.Token].AccountId;
			if (!d.Members.Select(p => p.Account).Any(p => p.AccountId == requesterid))
			{
				Code = ResponseCode.AccessDenied;
				return;
			}

			var t = Task.Run(() => { Task.Delay(Config1.TimeSpan.Milliseconds).Wait(); });

			Notifiers[Config1.DialogId, Config1.Token].NewMessage += MnOnNewMessage;

			try
			{
				t.Wait(Notifiers[Config1.DialogId, Config1.Token].CancellationToken);
				Code = ResponseCode.LongPoolTimeSpanExpired;
			}
			catch (OperationCanceledException)
			{
				Code = ResponseCode.Ok;
				resp.Content = new List<IMessage> { Message };
			}
			finally
			{
				Notifiers[Config1.DialogId, Config1.Token].NewMessage -= MnOnNewMessage;
			}
		}

		private void MnOnNewMessage(object sender, MessageNotifierEventArgs e)
		{
			Message = e.Message;
		}
	}
}