using Newtonsoft.Json;

using MyMessenger.Core;
using System;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Message : IMessage
	{
		[JsonProperty]
		public int MessageId { get; set; }

		[JsonProperty]
		public string Text { get; set; }

		[JsonProperty]
		public DateTimeOffset SendDateTime { get; set; }

		[JsonProperty]
		public int DialogId { get; set; }

		[JsonProperty]
		public int AuthorId { get; set; }
	}
}