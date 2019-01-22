using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyMessenger.Core;
using Newtonsoft.Json;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Commands
{
	public class GetMessages : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"getmessages", "gm"});

		public GetMessagesResponse Response { get; private set; }

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

		protected override void ExecuteImpl()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<GetMessagesResponse>(ReceiveResponse(), new InterfaceConverter<IMessage, Message>());
		}

		protected override Task ExecuteImplAsync()
		{
			throw new NotImplementedException();
		}
	}
}