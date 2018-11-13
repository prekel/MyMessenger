using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;

namespace MyMessenger.Server.Commands
{
	public class Login : AbstractCommand
	{
		private LoginParameters Config1 { get => (LoginParameters)Config; set => Config = value; }
		
		public string Token { get; private set; }

		//private IDictionary<string, IAccount> Tokens { get; set; }
		
		//public Login(MessengerContext context, AbstractParameters config, IDictionary<string, IAccount> tokens) : base(context, config)
		//{
		//	Tokens = tokens;
		//}

		private Login(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public Login(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : base(context, tokens, config)
		{
		}
		
		public override void Execute()
		{
			var account = Context.Accounts.First(p => p.Nickname == Config1.Login);
			if (Crypto.IsPasswordValid(Config1.Password, account.PasswordSalt, account.PasswordHash))
			{
				var r = new Random();
				var token = r.Next(1000, 9999).ToString();
				Tokens[token] = account;
				Token = token;
			}
		}
	}
}