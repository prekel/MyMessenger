using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MyMessenger.Core;
using Newtonsoft.Json;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Commands
{
	public class DialogSession : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"sialogsession", "ds"});

		public DialogSessionResponse Response { get; private set; }

		private DialogSessionParameters Config1
		{
			get => (DialogSessionParameters) Config;
			set => Config = value;
		}

		public DialogSession(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public DialogSession(NetworkStream stream, string token, int dialogid) : base(stream)
		{
			Config1 = new DialogSessionParameters
			{
				DialogId = dialogid,
				Token = token
			};
		}

		public override void Execute()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<DialogSessionResponse>(ReceiveResponse(), new InterfaceConverter<IMessage, Message>());
		}
	}
}