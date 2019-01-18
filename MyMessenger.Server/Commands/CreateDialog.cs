using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class CreateDialog : AbstractCommand
	{
		private CreateDialogParameters Config1 { get => (CreateDialogParameters)Config; set => Config = value; }

		public CreateDialog(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}

		public override CommandType CommandName { get; } = CommandType.CreateDialog;

		protected override void ExecuteImpl()
		{
			var resp = new CreateDialogResponse();
			Response = resp;

			var d = new Dialog();

			// Ошибка если не задан ни список идентификаторов, ни список никнеймов
			if (Config1.MembersIds == null && Config1.MembersNicknames == null)
			{
				Code = ResponseCode.InvalidRequest;
				return;
			}

			// Если задан список никнеймов
			if (Config1.MembersNicknames != null)
			{
				foreach (var i in Config1.MembersNicknames)
				{
					var a = Context.Accounts.FirstOr(p => p.Nickname == i, new Account { AccountId = -1 });
					if (a.AccountId == -1)
					{
						Code = ResponseCode.NicknameNotFound;
						return;
					}
					Context.AccountsDialogs.Add(new AccountDialog(a, d));
				}
			}
			// Иначе если задан список идентификаторов
			else if (Config1.MembersIds != null)
			{
				foreach (var i in Config1.MembersIds)
				{
					var a = Context.Accounts.FirstOr(p => p.AccountId == i, new Account { AccountId = -1 });
					if (a.AccountId == -1)
					{
						Code = ResponseCode.IdNotFound;
						return;
					}
					Context.AccountsDialogs.Add(new AccountDialog(a, d));
				}
			}

			Context.Dialogs.Add(d);
			Context.SaveChanges();

			Code = ResponseCode.Ok;
			resp.DialogId = d.DialogId;
		}
	}
}