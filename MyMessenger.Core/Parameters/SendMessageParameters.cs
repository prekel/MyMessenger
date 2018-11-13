using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class SendMessageParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.SendMessage;

		[JsonProperty]
		public int DialogId { get; set; }
		
		[JsonProperty]
		public string Token { get; set; }
		
		[JsonProperty]
		public string Text { get; set; }
	}
}
