using System;
using System.Runtime.CompilerServices;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
{
	[JsonObject]
	public class Dialog : IDialog
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(InterfaceConverter<Account>))]
		public IAccount FirstMember { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(InterfaceConverter<Account>))]
		public IAccount SecondMember { get; set; }
	}
}