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

		public override void Execute()
		{
			var resp = new CreateDialogResponse();
			Response = resp;

			var secmemid = Config1.SecondMemberId.HasValue
				? Config1.SecondMemberId.Value
				: Context.Accounts.FirstOr(p => p.Nickname == Config1.SecondMemberNickname, new Account { Id = -1 }).Id;
			if (secmemid == -1)
			{
				Code = ResponseCode.NicknameNotFound;
				return;
			}

			var d = new Dialog
			{
				FirstMember1 = new Account { Id = Tokens[Config1.Token].Id },
				SecondMember1 = new Account { Id = secmemid }
			};

			Context.Dialogs.Add(d);
			Context.SaveChanges();

			Code = ResponseCode.Ok;
			resp.DialogId = d.Id;
		}
	}
}