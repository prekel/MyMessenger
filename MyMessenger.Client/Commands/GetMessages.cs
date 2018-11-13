using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MyMessenger.Core;
using Newtonsoft.Json;
using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public class GetMessages : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"getmessages", "gm"});

		public IList<Message> Result { get; private set; }

		private GetMessagesParameters Config1
		{
			get => (GetMessagesParameters) Config;
			set => Config = value;
		}

		public GetMessages(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public GetMessages(NetworkStream stream, string token, int dialogid) : base(stream)
		{
			Config1 = new GetMessagesParameters
			{
				DialogId = dialogid,
				Token = token
			};
		}

		public override void Execute()
		{
			CreateSendQuery();

			var response = ReceiveResponse();

			Result = JsonConvert.DeserializeObject<List<Message>>(response);
		}
	}
}