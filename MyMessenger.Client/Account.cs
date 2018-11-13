using Newtonsoft.Json;

using MyMessenger.Core;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		public string Nickname { get; set; }
	}
}