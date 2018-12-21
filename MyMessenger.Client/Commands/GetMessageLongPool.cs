using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using Newtonsoft.Json;

namespace MyMessenger.Client.Commands
{
	public class GetMessageLongPool : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] { "getmessagelongpool", "gmlp" });

		public GetMessageLongPoolResponse Response { get; private set; }

		private GetMessageLongPoolParameters Config1
		{
			get => (GetMessageLongPoolParameters)Config;
			set => Config = value;
		}

		public GetMessageLongPool(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public GetMessageLongPool(NetworkStream stream, string token, int dialogid, TimeSpan ts) : base(stream)
		{
			Config1 = new GetMessageLongPoolParameters
			{
				DialogId = dialogid,
				Token = token,
				TimeSpan = ts
			};
		}

		public override void Execute()
		{
			CreateSendQuery();

			Response = JsonConvert.DeserializeObject<GetMessageLongPoolResponse>(ReceiveResponse(), new InterfaceConverter<IMessage, Message>());
		}
	}
}