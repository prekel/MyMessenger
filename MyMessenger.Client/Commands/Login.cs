using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using Newtonsoft.Json;

using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Core;

namespace MyMessenger.Client.Commands
{
	public class Login : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] { "login", "l" });

		public LoginResponse Response { get; private set; }

		//public string Token { get; private set; }

		private LoginParameters Config1 { get => (LoginParameters)Config; set => Config = value; }

		public Login(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{

		}

		public Login(NetworkStream stream, string nickname, string pass) : base(stream)
		{
			Config1 = new LoginParameters
			{
				Nickname = nickname,
				Password = pass
			};
		}

		public override void Execute()
		{
			CreateSendQuery();

			Response = JsonConvert.DeserializeObject<LoginResponse>(ReceiveResponse(), new InterfaceConverter<IAccount, Account>());
		}
	}
}
