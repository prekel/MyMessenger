using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using Newtonsoft.Json;

using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Commands
{
	public class Register : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] { "register", "reg" });

		//public IList<Message> Result { get; private set; }

		//public string Token { get; private set; }
		
		public RegisterResponse Response { get; private set; }

		private RegisterParameters Config1 { get => (RegisterParameters)Config; set => Config = value; }

		public Register(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{

		}

		public Register(NetworkStream stream, string nickname, string pass) : base(stream)
		{
			Config1 = new RegisterParameters
			{
				Nickname = nickname,
				Password = pass
			};
		}

		public override void Execute()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<RegisterResponse>(ReceiveResponse());
		}
	}
}
