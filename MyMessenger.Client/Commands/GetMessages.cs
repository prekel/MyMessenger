using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using Newtonsoft.Json;

using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public class GetMessages : AbstractCommand
	{
		public new static ICollection<string> CommandNames { get; } = new List<string>(new[] { "getmessages", "gm" });

		public IList<Message> Result { get; private set; }

		private GetMessagesParameters Config1 { get => (GetMessagesParameters)Config; set => Config = value; }

		public GetMessages(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{

		}

		public GetMessages(NetworkStream stream, int dialogid) : base(stream)
		{
			Config1 = new GetMessagesParameters
			{
				DialogId = dialogid
			};
		}

		public override void Execute()
		{
			var q = new Query
			{
				Config = Config1
			};
			var a = JsonConvert.SerializeObject(q);

			var data = Encoding.UTF8.GetBytes(a);
			Stream.Write(data, 0, data.Length);

			data = new byte[256];
			var response = new StringBuilder();
			do
			{
				var bytes = Stream.Read(data, 0, data.Length);
				response.Append(Encoding.UTF8.GetString(data, 0, bytes));
			} while (Stream.DataAvailable);

			Result = JsonConvert.DeserializeObject<List<Message>>(response.ToString());
		}
	}
}
