using Newtonsoft.Json;

using MyMessenger.Core;
using System.Collections.Generic;

namespace MyMessenger.Client
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

		public IList<IAccount> Members { get; set; }
	}
}