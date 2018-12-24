using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Server.Commands
{
	public class Login : AbstractCommand
	{
		private LoginParameters Config1 { get => (LoginParameters)Config; set => Config = value; }
		
		public string Token { get; private set; }

		private Login(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public Login(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}
		
		public override void Execute()
		{
			var resp = new LoginResponse();
			Response = resp;
			
			// Проверка на существование
			var account1 = Context.Accounts.Where(p => p.Nickname == Config1.Nickname);
			if (!account1.Any())
			{
				Code = ResponseCode.WrongNickname;
				return;
			}

			var account = account1.First();
			
			if (Crypto.IsPasswordValid(Config1.Password, account.PasswordSalt, account.PasswordHash))
			{
				Code = ResponseCode.Ok;
				var r = new Random();
				var token = r.Next(1000, 9999).ToString();
				Tokens[token] = account;
				Token = token;
				resp.Token = token;
				account.LoginDateTime = DateTime.Now;
				resp.Account = account;
				Context.SaveChanges();
			}
			else
			{
				Code = ResponseCode.WrongPassword;
			}
		}
	}
}