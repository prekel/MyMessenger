using System;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
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