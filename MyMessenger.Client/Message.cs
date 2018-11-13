using Newtonsoft.Json;

using MyMessenger.Core;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Message : IMessage
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		public string Text { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(InterfaceConverter<Dialog>))]
		public IDialog Dialog { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(InterfaceConverter<Account>))]
		public IAccount Author { get; set; }
	}
}