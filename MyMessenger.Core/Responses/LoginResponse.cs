using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public class LoginResponse : AbstractResponse
	{
		[JsonProperty]
		public string Token { get; set; }

		[JsonProperty]
		public IAccount Account { get; set; }
	}
}