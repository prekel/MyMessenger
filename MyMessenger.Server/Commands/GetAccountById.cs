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
	public class GetAccountById : AbstractCommand
	{
		private GetAccountByIdParameters Config1 { get => (GetAccountByIdParameters)Config; set => Config = value; }

		public GetAccountById(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}

		public override CommandType CommandName { get; } = CommandType.GetAccountById;

		protected override void ExecuteImpl()
		{
			var resp = new GetAccountByIdResponse();
			Response = resp;

			// Проверка на существование
			var account1 = Context.Accounts.Where(p => p.AccountId == Config1.AccountId);
			if (!account1.Any())
			{
				Code = ResponseCode.WrongNickname;
				return;
			}

			resp.Account = account1.First();
			Code = ResponseCode.Ok;
		}
	}
}