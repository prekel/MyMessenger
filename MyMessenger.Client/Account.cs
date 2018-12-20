using Newtonsoft.Json;

using MyMessenger.Core;
using System;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonProperty]
		public int AccountId { get; set; }

		[JsonProperty]
		public string Nickname { get; set; }

		[JsonProperty]
		public DateTime RegistrationDateTime { get; set; }
	}
}