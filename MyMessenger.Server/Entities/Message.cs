using System;

using Newtonsoft.Json;

using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
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

		public virtual Dialog Dialog { get; set; }
		
		public virtual Account Author { get; set; }
	}
}