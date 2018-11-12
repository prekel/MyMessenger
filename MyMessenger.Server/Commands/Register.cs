using System;
using System.Collections.Generic;
using System.Text;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class Register : AbstractCommand
	{
		private Parameters Config1 { get => (Parameters)Config; set => Config = value; }

		public Register(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public override void Execute()
		{
			var salt = Crypto.GenerateSaltForPassword();
			var a = new Account
			{
				Nickname = Config1.Nickname,
				PasswordHash = Crypto.ComputePasswordHash(Config1.Password, salt),
				PasswordSalt = salt
			};
			Context.Accounts.Add(a);
			Context.SaveChanges();
		}

		public class Parameters : AbstractParameters
		{
			public string Nickname { get; set; }
			public string Password { get; set; }
		}

	}
}
