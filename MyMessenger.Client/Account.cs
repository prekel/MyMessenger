using Newtonsoft.Json;

using MyMessenger.Core;
using System;
using System.Collections.Generic;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonProperty]
		public int AccountId { get; set; }

		[JsonProperty]
		public DateTime LoginDateTime { get; }

		[JsonProperty]
		public string Nickname { get; set; }

		[JsonProperty]
		public IEnumerable<IDialog> DialogsA { get; }

		[JsonProperty]
		public DateTime RegistrationDateTime { get; }
	}
}