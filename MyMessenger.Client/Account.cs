using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using MyMessenger.Core;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonProperty]
		public int AccountId { get; set; }

		[JsonProperty]
		public DateTimeOffset LoginDateTime { get; }

		[JsonProperty]
		public string Nickname { get; set; }

		[JsonProperty]
		public IEnumerable<int> DialogsIds { get; }

		[JsonProperty]
		public DateTimeOffset RegistrationDateTime { get; }

		[JsonProperty]
		public TimeZoneInfo TimeZone { get; }
	}
}