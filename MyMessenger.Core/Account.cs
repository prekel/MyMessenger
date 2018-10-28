using Newtonsoft.Json;

namespace MyMessenger.Core
{
	[JsonObject]
	public class Account
	{
		[JsonProperty]
		public string Nickname { get; set; }
	}
}