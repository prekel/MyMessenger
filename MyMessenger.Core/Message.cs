using System;
using Newtonsoft.Json;

namespace MyMessenger.Core
{
	[JsonObject]
	public class Message
	{
		[JsonProperty]
		public string Text { get; set; }
	}
}