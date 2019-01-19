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

		public override CommandType CommandName { get; } = CommandType.GetMessageLongPool;

		protected override void ExecuteImpl()
		{
			var resp = new GetMessageLongPoolResponse();
			Response = resp;

			var notifier = Notifiers[Config1.DialogId, Config1.Token];

			// Проверка на наличие запрашивателя в диалоге
			var requesterid = Tokens[Config1.Token].AccountId;
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			if (d.Members.Select(p => p.Account).All(p => p.AccountId != requesterid))
			{
				Code = ResponseCode.AccessDenied;
				return;
			}

			// Запуск ожидания на заданный TimeSpan
			var t = Task.Run(() => { Task.Delay((int)Config1.TimeSpan.TotalMilliseconds).Wait(); });

			notifier.NewMessage += MnOnNewMessage;

			try
			{
				// Если TimeSpan истёк до того, как пришло сообщение
				t.Wait(Notifiers[Config1.DialogId, Config1.Token].CancellationToken);
				Code = ResponseCode.LongPoolTimeSpanExpired;
			}
			catch (OperationCanceledException)
			{
				// Если пришло сообщение
				Code = ResponseCode.Ok;
				resp.Content = Message;
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