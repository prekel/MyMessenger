using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class Register : AbstractCommand
	{
		private RegisterParameters Config1
		{
			get => (RegisterParameters)Config;
			set => Config = value;
		}

		public Register(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		static Register()
		{
			CommandName = CommandType.Register;
		}

		protected override void ExecuteImpl()
		{
			var resp = new RegisterResponse();
			Response = resp;

			// Проверка на существование
			if (Context.Accounts.Any(p => p.Nickname == Config1.Nickname))
			{
				Code = ResponseCode.NicknameAlreadyExists;
				return;
			}

			var salt = Crypto.GenerateSaltForPassword();
			var a = new Account
			{
				Nickname = Config1.Nickname,
				PasswordHash = Crypto.ComputePasswordHash(Config1.Password, salt),
				PasswordSalt = salt,
				RegistrationDateTime = DateTime.Now,
				LoginDateTime = DateTime.MinValue,
				//TimeZone = Config1.TimeZone
			};
			Context.Accounts.Add(a);
			Context.SaveChanges();
			Code = ResponseCode.Ok;
		}
	}
}
