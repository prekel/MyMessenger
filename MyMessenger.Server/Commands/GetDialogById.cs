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
	public class GetDialogById : AbstractCommand
	{
		private GetDialogByIdParameters Config1 { get => (GetDialogByIdParameters)Config; set => Config = value; }

		public GetDialogById(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}

		static GetDialogById()
		{
			CommandName = CommandType.GetDialogById;
		}

		protected override void ExecuteImpl()
		{
			var resp = new GetDialogByIdResponse();
			Response = resp;
			
			// Проверка на принадлежность того, кто сделал запрос, к диалогу
			var d = Context.Dialogs.First(p => p.DialogId == Config1.DialogId);
			var requesterid = Tokens[Config1.Token].AccountId;
			if (d.Members.Select(p => p.Account).All(p => p.AccountId != requesterid))
			{
				Code = ResponseCode.AccessDenied;
				return;
			}

			resp.Dialog = d;

			Code = ResponseCode.Ok;
		}
	}
}