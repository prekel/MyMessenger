using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MyMessenger.Core;
using Newtonsoft.Json;
using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public class SendMessage : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"sendmessage", "sm"});

		//public IList<Message> Result { get; private set; }

		private SendMessageParameters Config1
		{
			get => (SendMessageParameters) Config;
			set => Config = value;
		}

		public SendMessage(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public SendMessage(NetworkStream stream, string token, int dialogid, string text) : base(stream)
		{
			Config1 = new SendMessageParameters
			{
				DialogId = dialogid,
				Token = token,
				Text = text
			};
		}

		public override void Execute()
		{
			CreateSendQuery();

			//var response = ReceiveResponse();

			//Result = JsonConvert.DeserializeObject<List<Message>>(response);
		}
	}
}